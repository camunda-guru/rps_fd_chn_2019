using CQRSDemo.Events;
using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreateCustomerCommand : Command
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<CreatePhoneCommand> Phones { get; set; }
        public CustomerCreatedEvent ToCustomerEvent(long id)
        {
            return new CustomerCreatedEvent
            {
                Id = id,
                Name = this.Name,
                Email = this.Email,
                Age = this.Age,
                Phones = this.Phones.Select(phone => 
                new PhoneCreatedEvent { AreaCode = phone.AreaCode,
                    Number = phone.Number }).ToList()
            };
        }
        public CustomerRecord ToCustomerRecord()
        {
            return new CustomerRecord
            {
                Name = this.Name,
                Email = this.Email,
                Age = this.Age,
                Phones = this.Phones.Select
                (phone => new PhoneRecord { AreaCode = phone.AreaCode,
                    Number = phone.Number }).ToList()
            };
        }
    }
}
