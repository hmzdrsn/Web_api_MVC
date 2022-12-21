using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using webapi_1.Models;

namespace webapi_1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<item> GetAll()
        {
            return Veritabani.Liste;
        }
        
        [HttpGet("{id}")]
        public IEnumerable<item> GetById(int id)
        {
            return Veritabani.Liste.Where(a => a.id == id);
        }

        [HttpPost]
        public void Insert(int id,string carName,string description)
        {         
         Veritabani.Add(id,carName,description);          
        }
        
        [HttpPost]
        public void Update(int id, string carName, string description)
        {
            Veritabani.Update(id,carName,description);
        }

        [HttpPost("{id}")]
        public void Delete(int id)
        {
            Veritabani.Delete(id);
        }


    }
}
