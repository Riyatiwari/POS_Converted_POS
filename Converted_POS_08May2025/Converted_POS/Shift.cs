using System;

namespace Converted_POS
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public DateTime ShiftStartTime { get; set; }
        public DateTime ShiftEndTime { get; set; }
    }
}