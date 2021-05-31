using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modern_Milkman_Tech_Test.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }    
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public int CustomerID { get; set; }
        public bool Primary_Address { get; set; }


    }
}
