using AutoMapper;
using DiscountGenerator.Abstractions;
using DiscountGenerator.Controllers;
using DiscountGenerator.DAL.Abstractions.UnitOfWork;
using DiscountGenerator.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

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
            _unitOfWork.ProductInfoRepository.Add(productInfo);
            _unitOfWork.ProductInfoRepository.SaveChanges();
            
        }

        public Task PostDiscount()
        {
            throw new NotImplementedException();
        }
        public async Task SetDiscount()
        {
            IEnumerable<string> files = Directory.EnumerateFiles("", "*.*").Where(s => s.EndsWith(".xml"));

        }
    }
}
