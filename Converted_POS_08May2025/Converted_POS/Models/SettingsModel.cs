using System;
using System.ComponentModel.DataAnnotations;

namespace Converted_POS.Models
{
    public class SettingsModel
    {
        public int LocationId { get; set; }
        
        public bool TillAutoLogoff { get; set; }
        
        public bool Cashback { get; set; }
        
        public bool IsDelivery { get; set; }
        
        public decimal MinAmount { get; set; }
        
        public bool ServiceChargeClickCollect { get; set; }
        
        public bool ServiceChargeKiosk { get; set; }
        
        public bool ServiceChargeOrder { get; set; }
        
        public bool ServiceChargeDelivery { get; set; }
        
        public bool IsSkipPay { get; set; }
        
        public bool IsEmail { get; set; }
        
        public bool WebsalesCheckSchedule { get; set; }
        
        public bool ScheduleRequired { get; set; }
        
        public string StartDate { get; set; }
        
        public string EndDate { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Contact { get; set; }
        
        public string HeaderReceipt { get; set; }
        
        public string BackgroundColor { get; set; }
        
        public string FontColor { get; set; }
        
        public string BodyColor { get; set; }
        
        public bool ClickAndCollect { get; set; }
        
        public bool OrderTable { get; set; }
        
        public string Theme { get; set; }
        
        public string TipAs { get; set; }
        
        public double DeliveryCharges { get; set; }
        
        public double ServiceCharges { get; set; }
        
        public string MessageDelivery { get; set; }
        
        public string MessageOrderTable { get; set; }
        
        public bool ScheduleClickAndCollect { get; set; }
        
        public int FutureBookingDays { get; set; }
        
        public bool HourlySlot { get; set; }
        
        public bool IsEmailAPK { get; set; }
        
        public string TableAsBox { get; set; }
        
        public string ClientId { get; set; }
        
        public string ClientSecret { get; set; }
        
        public string RedirectUri { get; set; }
        
        public bool PosLite { get; set; }
        
        public bool SunmiSecondScreen { get; set; }
        
        public string SunmiVideoPath { get; set; }
        
        public string GCBtnStyle { get; set; }
        
        public string GCBtnImgType { get; set; }
        
        public string GCBtnFontSize { get; set; }
        
        public string GCBtnBackgroundColor { get; set; }
        
        public string GCBtnTextColor { get; set; }
        
        public string GCBtnImgTypePosition { get; set; }
    }
} 