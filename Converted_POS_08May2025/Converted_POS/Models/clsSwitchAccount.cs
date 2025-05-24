using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Converted_POS.Pages.BackOffice.Switch_Account;

namespace Converted_POS.Models
{
    public class clsSwitchAccount
    {
        public List<UserEntry> UserEntries { get; set; } = new List<UserEntry>();
    }
}
