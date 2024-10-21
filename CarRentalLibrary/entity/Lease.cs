using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lease
{
    public int LeaseID { get; set; }
    public int VehicleID { get; set; }
    public int CustomerID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Type { get; set; }

    // Parameterized constructor to initialize a Lease object with specific values
    public Lease(int leaseID, int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
    {
        LeaseID = leaseID;
        VehicleID = vehicleID;
        CustomerID = customerID;
        StartDate = startDate;
        EndDate = endDate;
        Type = type;
    }

    // Default constructor
    public Lease() { }
}
