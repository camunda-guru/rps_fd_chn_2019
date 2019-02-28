using CQRSDemo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class DeleteCustomerCommand : Command
    {
        internal CustomerDeletedEvent ToCustomerEvent()
        {
            return new CustomerDeletedEvent
            {
                Id = this.Id
            };
        }
    }
}
