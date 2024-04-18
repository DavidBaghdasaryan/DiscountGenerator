using DiscountGenerator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DiscountGenerator.Controllers
{
    public class DiscountController : Controller
    {
        IDiscount iDiscount;

        public DiscountController(IDiscount _iDiscount)
        {
            iDiscount = _iDiscount;
        }

        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo( )
        {
            await iDiscount.GetInfo();
            return Ok();
        }

        [HttpPost("PostInfo")]
        public async Task<IActionResult> PostInfo( )
        {
            await iDiscount.PostInfo();
            return Ok();
        }

        [HttpPost("PostDiscount")]
        public async Task<IActionResult> PostDiscount()
        {
            await iDiscount.PostDiscount();
            return Ok();
        }
    }
}
