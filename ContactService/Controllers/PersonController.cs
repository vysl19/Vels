using ContactService.Data;
using ContactService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        // GET: api/<PersonController>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            var persons=  _personRepository.List();
            return Ok(persons);
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(string id)
        {
            var person = _personRepository.Get(id);
            if(person== null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            _personRepository.Add(person);
            _personRepository.SaveChanges();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _personRepository.Delete(id);
            _personRepository.SaveChanges();
        }
    }
}
