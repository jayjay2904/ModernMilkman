using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modern_Milkman_Tech_Test.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email_Address { get; set; }
        public string Moblie { get; set; }
        public bool Active { get; set; }
    }
}
