using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class MachineModel
    {
        public int MachineId { get; set; }
        
        public int CompanyId { get; set; }
        
        [StringLength(50)]
        public string Code { get; set; }
        
        [Required(ErrorMessage = "Machine name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(100)]
        public string MacAddress { get; set; }
        
        [StringLength(100)]
        public string Model { get; set; }
        
        [StringLength(100)]
        public string SerialNo { get; set; }
        
        [StringLength(50)]
        public string IpAddress { get; set; }
        
        public int? LocationId { get; set; }
        
        public bool IsAssign { get; set; }
        
        public bool IsMiniPos { get; set; }
        
        public bool IsActive { get; set; }
        
        [StringLength(500)]
        public string ReceiptHeader { get; set; }
        
        public bool IsMasterSelf { get; set; }
        
        [StringLength(500)]
        public string ReceiptFooter { get; set; }
        
        public int? KeymapId { get; set; }
        
        public int? FunctionId { get; set; }
        
        public int? TillId { get; set; }
        
        [StringLength(50)]
        public string TillIpAddress { get; set; }
        
        public int? MasterTill { get; set; }
        
        public bool TillServer { get; set; }
        
        public bool TableSharing { get; set; }
        
        public bool PrinterSharing { get; set; }
        
        [StringLength(50)]
        public string SyncTime { get; set; }
        
        public bool BackToMainFunctionOnTill { get; set; }
        
        public bool ExtraSurcharge { get; set; }
        
        public bool OnlyTableTrans { get; set; }
        
        public bool AutoSurcharge { get; set; }
        
        public bool AutoSurchargeTables { get; set; }
        
        public bool AutoSurchargeNonTables { get; set; }
        
        public bool AutoSurchargeCloakroom { get; set; }
        
        public bool AutoSurchargeChips { get; set; }
        
        public bool NoCashDrawer { get; set; }
        
        public bool ReSyncRequest { get; set; }
        
        public bool ServiceControllerStart { get; set; }
        
        public bool ServiceWebsalePrint { get; set; }
        
        public bool ServicePrintShare { get; set; }
        
        public bool ServicePrintShareOtherTill { get; set; }
        
        public bool IsNoLogout { get; set; }
        
        public bool IsPrintServer { get; set; }
        
        public bool IsServiceBooking { get; set; }
        
        public bool IsOnlineZreport { get; set; }
        
        public bool IsKiosk { get; set; }
        
        public int? TranLimit { get; set; }
        
        [StringLength(100)]
        public string GatewayTID { get; set; }
        
        public bool QuickTran { get; set; }
        
        public bool TillRequest { get; set; }
        
        public bool KioskRequest { get; set; }
        
        public bool KitchenPrint { get; set; }
        
        public bool ReceiptPrint { get; set; }
        
        public bool ElinaTran { get; set; }
        
        public bool POSLite { get; set; }
        
        public bool SunmiSecondScreen { get; set; }
        
        public byte[] SecondScreenImage1 { get; set; }
        
        public byte[] SecondScreenImage2 { get; set; }
        
        [StringLength(500)]
        public string SunmiVideoPath { get; set; }
        
        public int? HardwareType { get; set; }
        
        [StringLength(100)]
        public string TillUUID { get; set; }
    }
} 