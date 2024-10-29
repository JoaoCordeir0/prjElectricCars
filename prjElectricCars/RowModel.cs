using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjElectricCars
{
    internal class RowModel
    {       
        public string Vin { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string EvType { get; set; }
        public string CafvType { get; set; }
        public int ElectricRange { get; set; }
        public decimal BaseMsrp { get; set; }
        public string LegislativeDistrict { get; set; }
        public string DolVehicleId { get; set; }
        public string GeocodedColumn { get; set; }
        public string ElectricUtility { get; set; }        
    }
}
