using CQRSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo.Commands
{
    public class CreatePhoneCommand : Command
    {
        public PhoneType Type { get; set; }
        public int AreaCode { get; set; }
        public int Number { get; set; }
    }
}
