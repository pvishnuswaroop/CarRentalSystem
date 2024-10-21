using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRentalLibrary.entity
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int LeaseID { get; set; }  
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Default constructor
        public Payment() { }

        // Parameterized constructor to initialize a Payment object with specific values
        public Payment(int paymentID, int leaseID, DateTime paymentDate, decimal amount)
        {
            PaymentID = paymentID;
            LeaseID = leaseID;
            PaymentDate = paymentDate;
            Amount = amount;
        }
    }
}

