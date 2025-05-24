using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Converted_POS.Pages.Reports
{
    public class TableTransactionDetailModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult TableTransaction()
        {
            // Get data from session (assuming you have set it in session earlier)
            //var dt = HttpContext.Session.GetObjectFromJson<DataTable>("dt");

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    var model = new TableTransactionModel
            //    {
            //        TableName = dt.Rows[0]["table_name"].ToString(),
            //        PaymentType = dt.Rows[0]["Payment_Type"].ToString(),
            //        RefNo = dt.Rows[0]["ref_id"].ToString(),
            //        PaymentAmount = decimal.Parse(dt.Rows[0]["payment_amount"].ToString()),
            //        TableStatus = dt.Rows[0]["status"].ToString(),
            //        SurchargeAmount = decimal.Parse(dt.Rows[0]["Surcharge_Amount"].ToString()),
            //        Change = decimal.Parse(dt.Rows[0]["change"].ToString()),
            //        TipAmount = decimal.Parse(dt.Rows[0]["tip_amount"].ToString()),
            //        // Add the DataTable to the model (for binding to Razor)
            //        DataTable = dt
            //    };

            //    return View(model);
            //}
            return RedirectToAction("SignIn", "Account");  // Redirect to SignIn page if session is invalid
        }
    }
}
