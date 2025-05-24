using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Converted_POS.Data;
using Converted_POS.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Converted_POS.Services
{
    public class ZReportService : IZReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public ZReportService(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<DataTable> GetZReportDataAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? machineId,
            int? venueId,
            string salesType,
            string shiftName,
            int? operatorId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId),
                new SqlParameter("@date1", fromDate),
                new SqlParameter("@date2", toDate),
                new SqlParameter("@location_id", locationId.HasValue ? locationId.Value.ToString() : "0"),
                new SqlParameter("@machine_id", machineId.HasValue ? machineId.Value.ToString() : "0"),
                new SqlParameter("@venue_id", venueId.HasValue ? venueId.Value.ToString() : "0"),
                new SqlParameter("@duration", duration ?? "Today"),
                new SqlParameter("@salestype", salesType ?? "All"),
                new SqlParameter("@shift_name", shiftName ?? "0"),
                new SqlParameter("@login_id", operatorId.HasValue ? operatorId.Value.ToString() : "0")
            };

            return await ExecuteStoredProcedureAsync("WS_Get_Z_Report", parameters);
        }

        public async Task<List<SelectListItem>> GetVenuesAsync(int companyId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId)
            };

            var dataTable = await ExecuteStoredProcedureAsync("Get_Venue_By_Cmp", parameters);
            var venues = new List<SelectListItem>();

            foreach (DataRow row in dataTable.Rows)
            {
                venues.Add(new SelectListItem
                {
                    Value = row["venue_id"].ToString(),
                    Text = row["venue_name"].ToString()
                });
            }

            return venues;
        }

        public async Task<List<SelectListItem>> GetLocationsByVenueAsync(int companyId, int venueId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId),
                new SqlParameter("@venue_id", venueId)
            };

            var dataTable = await ExecuteStoredProcedureAsync("Get_Location_By_Venue", parameters);
            var locations = new List<SelectListItem>();

            foreach (DataRow row in dataTable.Rows)
            {
                locations.Add(new SelectListItem
                {
                    Value = row["location_id"].ToString(),
                    Text = row["name"].ToString()
                });
            }

            return locations;
        }

        public async Task<List<SelectListItem>> GetMachinesByLocationAsync(int companyId, int locationId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId),
                new SqlParameter("@location_id", locationId)
            };

            var dataTable = await ExecuteStoredProcedureAsync("Get_Machine_By_Location", parameters);
            var machines = new List<SelectListItem>();

            foreach (DataRow row in dataTable.Rows)
            {
                machines.Add(new SelectListItem
                {
                    Value = row["machine_id"].ToString(),
                    Text = row["machine_name"].ToString()
                });
            }

            return machines;
        }

        public async Task<List<SelectListItem>> GetOperatorsAsync(int companyId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId)
            };

            var dataTable = await ExecuteStoredProcedureAsync("Get_Staff_By_Cmp", parameters);
            var operators = new List<SelectListItem>();

            foreach (DataRow row in dataTable.Rows)
            {
                operators.Add(new SelectListItem
                {
                    Value = row["staff_id"].ToString(),
                    Text = row["name"].ToString()
                });
            }

            return operators;
        }

        public async Task<List<SelectListItem>> GetShiftsByDurationAsync(int companyId, string duration, DateTime fromDate, DateTime toDate)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cmp_id", companyId),
                new SqlParameter("@s_date", fromDate),
                new SqlParameter("@e_date", toDate),
                new SqlParameter("@duration", duration ?? "Today")
            };

            var dataTable = await ExecuteStoredProcedureAsync("Get_Shift_By_Duration", parameters);
            var shifts = new List<SelectListItem>();

            foreach (DataRow row in dataTable.Rows)
            {
                shifts.Add(new SelectListItem
                {
                    Value = row["shift_name"].ToString(),
                    Text = row["shift_name"].ToString()
                });
            }

            return shifts;
        }

        public async Task<byte[]> ExportToExcelAsync(
            int companyId,
            DateTime fromDate,
            DateTime toDate,
            string duration,
            int? locationId,
            int? machineId,
            int? venueId,
            string salesType,
            string shiftName,
            int? operatorId)
        {
            var dataTable = await GetZReportDataAsync(
                companyId,
                fromDate,
                toDate,
                duration,
                locationId,
                machineId,
                venueId,
                salesType,
                shiftName,
                operatorId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Z Report");

                // Add header
                worksheet.Cell(1, 1).Value = "Description";
                worksheet.Cell(1, 2).Value = "Number";

                // Add data
                int row = 2;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    worksheet.Cell(row, 1).Value = dataRow["Description"].ToString();
                    worksheet.Cell(row, 2).Value = dataRow["Number"].ToString();
                    row++;
                }

                // Format header
                var headerRange = worksheet.Range(1, 1, 1, 2);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

                // Auto-adjust column widths
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public async Task<bool> SendZReportByEmailAsync(
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
            int? operatorId)
        {
            try
            {
                // Generate Excel file
                var excelBytes = await ExportToExcelAsync(
                    companyId,
                    fromDate,
                    toDate,
                    duration,
                    locationId,
                    machineId,
                    venueId,
                    salesType,
                    shiftName,
                    operatorId);

                // Get report data for email body
                var reportData = await GetZReportDataAsync(
                    companyId,
                    fromDate,
                    toDate,
                    duration,
                    locationId,
                    machineId,
                    venueId,
                    salesType,
                    shiftName,
                    operatorId);

                // Create email body
                string emailBody = GenerateHtmlEmailBody(reportData, fromDate, toDate, duration);

                // Create email attachment
                var attachment = new Attachment(new MemoryStream(excelBytes), "ZReport.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                // Send email
                await _emailService.SendEmailAsync(
                    email,
                    "Z Report",
                    emailBody,
                    true,
                    new List<Attachment> { attachment });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateHtmlEmailBody(DataTable reportData, DateTime fromDate, DateTime toDate, string duration)
        {
            var htmlBuilder = new System.Text.StringBuilder();
            
            htmlBuilder.AppendLine("<html><body>");
            htmlBuilder.AppendLine("<h2>Z Report</h2>");
            htmlBuilder.AppendLine($"<p><strong>Date Range:</strong> {fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}</p>");
            htmlBuilder.AppendLine($"<p><strong>Duration:</strong> {duration}</p>");
            
            if (reportData.Rows.Count > 0)
            {
                htmlBuilder.AppendLine("<h3>Report Summary</h3>");
                htmlBuilder.AppendLine("<table border='1' cellpadding='5' cellspacing='0'>");
                htmlBuilder.AppendLine("<tr><th>Description</th><th>Number</th></tr>");
                
                foreach (DataRow row in reportData.Rows)
                {
                    htmlBuilder.AppendLine($"<tr><td>{row["Description"]}</td><td>{row["Number"]}</td></tr>");
                }
                
                htmlBuilder.AppendLine("</table>");
            }
            
            htmlBuilder.AppendLine("<p>Please find the attached Excel report for more details.</p>");
            htmlBuilder.AppendLine("</body></html>");
            
            return htmlBuilder.ToString();
        }

        private async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, List<SqlParameter> parameters)
        {
            var dataTable = new DataTable();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(dataTable));
                    }
                }
            }

            return dataTable;
        }
    }
} 