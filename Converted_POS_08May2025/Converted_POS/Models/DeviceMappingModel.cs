using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class DeviceMappingModel
    {
        public int PrinterId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int selectedDeviceId { get; set; }
    }
}
