namespace Converted_POS.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string Code { get; set; }
        public string MachineId { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string bluetooth_name { get; set; }
        public string application_profile_id { get; set; }
        public string service_key { get; set; }
        public string SerialNo { get; set; }
        public string DeviceCategory { get; set; }
        public string DeviceType { get; set; }
        public bool IsActive { get; set; }
    }
}