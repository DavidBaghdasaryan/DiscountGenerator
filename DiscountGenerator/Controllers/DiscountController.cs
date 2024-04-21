using DiscountGenerator.Abstractions;
using DiscountGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscountGenerator.Controllers
{
    public class DiscountController : Controller
    {
        IDiscountManager iDiscount;

        public DiscountController(IDiscountManager _iDiscount)
        {
            iDiscount = _iDiscount;
        }

        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
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
        [HttpPost("SetDiscount")]
        public async Task<IActionResult> import()
        {
            await iDiscount.SetDiscount();
            return Ok();
        }
    }
}
