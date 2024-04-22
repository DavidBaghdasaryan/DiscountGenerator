using AutoMapper;
using DiscountGenerator.Abstractions;
using DiscountGenerator.DAL.Abstractions.UnitOfWork;
using DiscountGenerator.DAL.Entity;
using DiscountGenerator.Models;
using DiscountGenerator.XMLModels.ExportModels;
using DiscountGenerator.XMLModels.ImportModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DiscountGenerator.Managers
{
    public class DiscountManager : IDiscountManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _path;
        public DiscountManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _path=_unitOfWork.GetValuesFromApps("ProductInfo");
        }
        public async Task<List<ProductModel>> GetInfo()
        {
            List<ProductModel> response = new();
            var prods =  _unitOfWork.ProductInfoRepository.GetAllProducts();
            response= _mapper.Map<List<ProductModel>>(prods);
            return response;
        }


        public async Task PostInfo(ProductModel productModel)
        {             
            ProductInfo productInfo= _mapper.Map<ProductInfo>(productModel);
            var checkProd=_unitOfWork.ProductInfoRepository.GetNoTracking(x=>x==productInfo).FirstOrDefault();
            if (checkProd==null) 
                _unitOfWork.ProductInfoRepository.Add(productInfo);
            else
                _unitOfWork.ProductInfoRepository.Update(productInfo);

             await _unitOfWork.SaveChangesAsync(); 
            
        }

        public async Task PostDiscount(DiscountModel discount)
        {
            XMLProductInfoExportModel exportModel = new();
            exportModel.Discount = _mapper.Map<DiscountExport>(discount);
            XmlSerializer serializer = new XmlSerializer(typeof(XMLProductInfoExportModel));
            var path = string.Format(@"{0}\{1}.xml", _path, DateTime.Now.ToString("HHmmss"));
            try
            {
                Directory.EnumerateFiles(_path, "*.*").Where(s => s.EndsWith(".xml"));
            }
            catch 
            {
                Directory.CreateDirectory(_path);
            }
           
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (XmlWriter file = XmlWriter.Create(path, settings))
            {
                serializer.Serialize(file, exportModel,ns);
            }
        }
        public async Task SetDiscount()
        {
            IEnumerable<string> files = default;
            XMLProductInfoImportModel importModel = new();
            XmlSerializer serializer = new XmlSerializer(typeof(XMLProductInfoImportModel));
            decimal discountValue=default;
            bool isPercet=false;
           
            try
            {
                files = Directory.EnumerateFiles(_path, "*.*").Where(s => s.EndsWith(".xml"));
            }
            catch 
            {
                Directory.CreateDirectory(_path);
                files = Directory.EnumerateFiles(_path, "*.*").Where(s => s.EndsWith(".xml"));
              
            }
            foreach (var file in files)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        importModel = (XMLProductInfoImportModel)serializer.Deserialize(reader);
                        if (importModel!=null)
                        {
                            if(importModel.DiscountImport.Fix>0)
                                discountValue= importModel.DiscountImport.Fix;
                            if (importModel.DiscountImport.Percent > 0) {
                                discountValue = importModel.DiscountImport.Percent;
                                isPercet = true;
                            }
                            if (discountValue > 0) 
                            {
                                var product = _unitOfWork.ProductInfoRepository.GetNoTracking(x=>x.ProductID==importModel.DiscountImport.ProductId).FirstOrDefault();
                                if (product != null)
                                {
                                    product.Price = isPercet? product.Price - (product.Price *discountValue/100) :
                                        product.Price - discountValue;
                                    _unitOfWork.ProductInfoRepository.Update(product);
                                    await _unitOfWork.SaveChangesAsync();
                                }
                            }

                        }
                    }
                }
                catch (Exception ex) { } 
            }
        }
    }
}
