using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsFunction
    {

        //public List<clsFunction> SelectedData { get; set; }
        public int Id { get; set; }
        public string Back_Color { get; set; }
        public string For_Color { get; set; }
        public string Is_Active { get; set; }
        public string Big_Button { get; set; }
        public string Function_Id { get; set; }
        public int Mfunction_id { get; set; }
        public string Caption_Type { get; set; }
        public string ip_address { get; set; }
    
        //public int Function_id { get; set; }

        public int selectedPanelId { get; set; }
        public int? mainfunction_id { get; set; }
        public int? FunctionDetails_Id { get; set; }
        public string name { get; set; }
        public string Fname { get; set; }
        public string Till_name { get; set; }
        public int FunctionId { get; set; } // Numeric(18,0)
        public int CmpId { get; set; } // Numeric(18,0)
        public string Name { get; set; } // nvarchar(100)
        public string Code { get; set; } // nvarchar(10)
        public string CaptionType { get; set; } // nvarchar(100)
        public bool is_active1 { get; set; } // tinyint
        public bool is_active { get; set; } // tinyint
        public string Active
        {
            get { return is_active ? "Yes" : "No"; }
        }
      
        public string ShortingNo { get; set; } // nvarchar(50)
        public string IpAddress { get; set; } // nvarchar(100) 
        public int LoginId { get; set; } // Numeric(18,0)
        public decimal Item { get; set; } // Numeric(18,2)
        public decimal Item2 { get; set; } // Numeric(18,2)
        public decimal Item1 { get; set; } // Numeric(18,2)
        public string MacId { get; set; } // nvarchar(100)
        public string BackColor { get; set; } // nvarchar(100)
        public string ForColor { get; set; } // nvarchar(100)
        public int? TMachineId { get; set; } // Numeric(18,0)
        public int? MachineId { get; set; } // Numeric(18,0)
        public bool BigButton { get; set; } // tinyint
        public int PaymentId { get; set; } // Numeric(18,0)
        public byte PayType { get; set; } // tinyint
        public int Product_Price_ID { get; set; } // tinyint
        public string PaySubType { get; set; } // nvarchar(100)
        public bool IsGroupByDept { get; set; } // tinyint
        public bool IsGroupByCourse { get; set; } // tinyint
        public string DeptId { get; set; } // nvarchar(500)
        public string CourseId { get; set; } // nvarchar(500)
        public int Panel_Id { get; set; } // Numeric(18,0)
        public int ParentId { get; set; } // Numeric(18,0)
      
        public string PlatformAdd { get; set; } // nvarchar(MAX)
        public string ClientToken { get; set; } // nvarchar(MAX)
        public string AccessToken { get; set; } // nvarchar(MAX)
        public string ServiceId { get; set; } // nvarchar(MAX)
        public int TaxId { get; set; } // Numeric(18,0)
        public decimal EOHelpOutMaxAmountEach { get; set; } // Numeric(18,2)
        public decimal TotalValue { get; set; } // Numeric(18,2)
        public decimal SalesHandlingFee { get; set; } // Numeric(18,2)
        public decimal PaymentHandlingFee { get; set; } // Numeric(18,2)
        public decimal TaxAmount { get; set; } // Numeric(18,2)
        public decimal  Amount { get; set; } // Numeric(18,2)
        public int ProfileId { get; set; } // Numeric(18,0)
        public string DefaultDateTime { get; set; } // nvarchar(20)
        public int ZrVenueId { get; set; } // Numeric(18,0)
        public int ZrLocationId { get; set; } // Numeric(18,0)
        public string ZrTillId { get; set; } // nvarchar(MAX)
        public int CardPayType { get; set; } // Numeric(18,0)
        public bool Visible { get; set; }
        public string Style { get; set; }
        public string ExpireDateTime { get; set; }
        //DataTable dt = new DataTable();
        public List<SelectListItem> DTTill { get; set; }
        public DataTable dt { get; set; }
        public DataTable GetDataTable()
        {
            return dt;
        }


    }
}
