using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Services
{
    public interface IZReportService
    {
        /// <summary>
        /// Gets the Z Report data based on filter parameters
        /// </summary>
        Task<DataTable> GetZReportDataAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? machineId,
            int? venueId,
            string salesType,
            string shiftName,
            int? operatorId);

        /// <summary>
        /// Gets venues for the dropdown
        /// </summary>
        Task<List<SelectListItem>> GetVenuesAsync(int companyId);

        /// <summary>
        /// Gets locations for the dropdown based on venue
        /// </summary>
        Task<List<SelectListItem>> GetLocationsByVenueAsync(int companyId, int venueId);

        /// <summary>
        /// Gets machines/tills for the dropdown based on location
        /// </summary>
        Task<List<SelectListItem>> GetMachinesByLocationAsync(int companyId, int locationId);

        /// <summary>
        /// Gets operators/staff for the dropdown
        /// </summary>
        Task<List<SelectListItem>> GetOperatorsAsync(int companyId);

        /// <summary>
        /// Gets shifts based on dates and duration
        /// </summary>
        Task<List<SelectListItem>> GetShiftsByDurationAsync(int companyId, string duration, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Exports Z Report data to Excel and returns the byte array
        /// </summary>
        Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? machineId,
            int? venueId,
            string salesType,
            string shiftName,
            int? operatorId);

        /// <summary>
        /// Sends the Z Report via email
        /// </summary>
        Task<bool> SendZReportByEmailAsync(
            string email,
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? machineId,
            int? venueId,
            string salesType,
            string shiftName,
            int? operatorId);
    }
} 