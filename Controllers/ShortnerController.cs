using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using URLShortner.Helpers;
using URLShortner.Models;

namespace URLShortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortnerController : ControllerBase
    {
        IConfiguration _configuration;
        CoreContext _coreContext;
        public ShortnerController(IConfiguration configuration, CoreContext coreContext)
        {
            _configuration = configuration;
            _coreContext = coreContext;
        }
        [Route("Query")]
        [HttpPost]
        public IActionResult GetOriginalURL([FromBody] string ShortCode)
        {
            ShortURL shortURL = DAL.URLShortner.GetByCode(ShortCode, _configuration);
            if (shortURL != null && !string.IsNullOrEmpty(shortURL.ShortCode))
            {
                return Ok(shortURL);
            }
            else
            {
                return NotFound(shortURL);
            }
        }

        [Route("Generate")]
        [HttpPost]
        public IActionResult Generate([FromBody] string OriginalURL)
        {
            ShortURL url = DAL.URLShortner.GetByOriginal(OriginalURL, _configuration);
            if (url != null && !string.IsNullOrEmpty(url.ShortCode))
            {
                return Ok(url);
            }
            else
            {
                ShortURL shorten = new ShortURL
                {
                    Original_Url = OriginalURL,
                    ShortCode = ShortGenerator.RandomString(7),
                    DateAdded = DateTime.Now,
                    DateUpdated= DateTime.Now
                };
                _coreContext.ShortenedURLs.Add(shorten);
                _coreContext.SaveChanges();
                return Ok(shorten);
            }
        }

    }
}