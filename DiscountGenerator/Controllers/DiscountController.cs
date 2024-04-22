using DiscountGenerator.Abstractions;
using DiscountGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscountGenerator.Controllers
{
    public class DiscountController : Controller
    {
        IDiscountManager iDiscount;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountManager _iDiscount, ILogger<DiscountController> logger)
        {
            iDiscount = _iDiscount;
            _logger = logger;
        }

        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
            _logger.LogInformation("GetInfo called.");
            var result= await iDiscount.GetInfo();
            return Ok(result);
        }

        [HttpPost("PostInfo")]
        public async Task<IActionResult> PostInfo([FromBody] ProductModel  productModel)
        {
         
            await iDiscount.PostInfo(productModel);
            return Ok();
        }

        [HttpPost("PostDiscount")]
        public async Task<IActionResult> PostDiscount([FromBody]  DiscountModel discount)
        {
           
            await iDiscount.PostDiscount(discount);
            return Ok();
        }
        //[HttpPost("SetDiscount")]  //FORE CHECKING JOB WORK
        //public async Task<IActionResult> import()
        //{
        //    await iDiscount.SetDiscount();
        //    return Ok();
        //}
    }
}
