using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class TableTransactionDetailController : Controller
    {
        [HttpPost]
        [Route("reports/TableTransactionDetail")]
        public IActionResult TableTransactionDetail(int id)
        {
            return Json(new
            {
                success = true
            });
        }
    }
}
