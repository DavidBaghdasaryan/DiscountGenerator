using AutoMapper;
using DiscountGenerator.Abstractions;
using DiscountGenerator.DAL.Abstractions.UnitOfWork;
using DiscountGenerator.DAL.Entity;
using DiscountGenerator.ExportModels;
using DiscountGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

        public DiscountManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
         
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
     
            var path = string.Format(@"{0}\{1}.xml", _unitOfWork.GetValuesFromApps("ProductInfo"), DateTime.Now.ToString("HHmmss"));
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

    }
}
