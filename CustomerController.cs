using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSDemo.Commands;
using CQRSDemo.Models;
using CQRSDemo.Models.Mongo;
using CQRSDemo.Models.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRSDemo.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICommandHandler<Command> _commandHandler;
        private readonly CustomerMongoRepository _mongoRepository;
        private readonly CustomerSQLiteRepository _sqliteRepository;
        public CustomersController(ICommandHandler<Command> commandHandler,
            CustomerSQLiteRepository sqliteRepository,
            CustomerMongoRepository repository)
        {
            _commandHandler = commandHandler;
            _sqliteRepository = sqliteRepository;
            _mongoRepository = repository;
            if (_mongoRepository.GetCustomers().Count == 0)
            {
                var customerCmd = new CreateCustomerCommand
                {
                    Name = "Ajay",
                    Email = "ajay@email.com",
                    Age = 23,
                    Phones = new List<CreatePhoneCommand>
                    {
                        new CreatePhoneCommand { Type = PhoneType.CELLPHONE, AreaCode = 123, Number = 7543010 }
                    }
                };
                _commandHandler.Execute(customerCmd);
            }
        }
        [HttpGet]
        public List<CustomerEntity> Get()
        {
            //new Events.CustomerMessageListener(_mongoRepository).Start();
            return _mongoRepository.GetCustomers();
        }
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var product = _mongoRepository.GetCustomer(id);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            var product = _mongoRepository.GetCustomerByEmail(email);
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerCommand customer)
        {
            _commandHandler.Execute(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] UpdateCustomerCommand customer)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            customer.Id = id;
            _commandHandler.Execute(customer);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _sqliteRepository.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            _commandHandler.Execute(new DeleteCustomerCommand()
            {
                Id = id
            });
            return NoContent();
        }
    }
}
