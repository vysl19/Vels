using ContactService.Data;
using ContactService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }        

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] Contact contact)
        {
            _contactRepository.Add(contact);
            _contactRepository.SaveChanges();            
        }
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            return Ok(_contactRepository.GetAllContacts());
        }
        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _contactRepository.Delete(id);
            _contactRepository.SaveChanges();
        }
    }
}
