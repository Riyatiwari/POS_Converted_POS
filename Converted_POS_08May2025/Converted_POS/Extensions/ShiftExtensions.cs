using System;

namespace Converted_POS.Extensions
{
    // Extension methods for the root namespace Shift class
    public static class ShiftExtensions
    {
        public static bool IsActive(this Converted_POS.Shift shift)
        {
            // Default to true if the property doesn't exist
            return true;
        }

        public static int CompanyId(this Converted_POS.Shift shift)
        {
            // Return a default value since the property doesn't exist
            return 0;
        }
        
        public static string Name(this Converted_POS.Shift shift)
        {
            // Return the ShiftName property
            return shift.ShiftName;
        }
    }
} 