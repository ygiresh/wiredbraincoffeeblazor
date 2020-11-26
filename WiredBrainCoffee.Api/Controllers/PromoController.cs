using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiredBrainCoffee.Api.Controllers
{
    public class PromoController : Controller
    {
        public IActionResult CheckPromoCodeValue(string promocode)
        {
            return Ok();
        }
    }
}
