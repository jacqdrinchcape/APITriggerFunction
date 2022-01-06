using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Model
{
    public class VehicleIdentificationNumber
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string vin { get; set; }
    }
}
