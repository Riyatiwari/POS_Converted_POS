using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Shared
{
    public class QR1_ListModel : PageModel
    {
        public byte[] QRCodeBytes { get; internal set; }
        public List<clsQR> QR { get; internal set; }

        public void OnGet()
        {
        }
    }
}
