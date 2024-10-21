using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CarRentalLibrary.exception
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message) { }
    }
}

