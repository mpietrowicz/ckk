using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CKK.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageSendController : ControllerBase
    {
        // GET: api/ImageSend
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ImageSend/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ImageSend
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ImageSend/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ImageSend/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
