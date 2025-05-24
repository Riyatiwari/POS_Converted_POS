using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Converted_POS.Services
{
    public interface IBindingService
    {
        Task<List<SelectListItem>> GetVenuesAsync(int companyId);
        
        Task<List<SelectListItem>> GetLocationsAsync(int companyId, int venueId);
        
        Task<List<SelectListItem>> GetLocationsByVenueAsync(int companyId, int venueId);
        
        Task<List<SelectListItem>> GetTillsAsync(int companyId, int locationId);
        
        Task<List<SelectListItem>> GetMachinesByLocationAsync(int companyId, int locationId);
        
        Task<List<SelectListItem>> GetStaffAsync(int companyId);
        
        Task<List<SelectListItem>> GetShiftsByDurationAsync(int companyId, string duration, System.DateTime fromDate, System.DateTime toDate);
        
        Task<List<SelectListItem>> GetProductGroupsAsync(int companyId);
        
        Task<List<SelectListItem>> GetProductsByGroupAsync(int companyId, int groupId);
        
        Task<List<SelectListItem>> GetCustomersAsync(int companyId);
    }
} 