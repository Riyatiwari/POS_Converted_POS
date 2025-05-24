using Converted_POS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Extensions;

namespace Converted_POS.Services
{
    public class BindingService : IBindingService
    {
        private readonly ApplicationDbContext _context;

        public BindingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelectListItem>> GetVenuesAsync(int companyId)
        {
            var venues = await _context.Venues
                .Where(v => v.CompanyId == companyId && v.IsActive)
                .Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Name
                })
                .ToListAsync();

            venues.Insert(0, new SelectListItem { Value = "", Text = "-- Select Venue --" });
            return venues;
        }

        public async Task<List<SelectListItem>> GetLocationsAsync(int companyId, int venueId = 0)
        {
            var query = _context.Locations.Where(l => l.CompanyId == companyId && l.IsActive);
            
            if (venueId > 0)
            {
                query = query.Where(l => l.VenueId == venueId);
            }
            
            var locations = await query
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                })
                .ToListAsync();

            locations.Insert(0, new SelectListItem { Value = "", Text = "-- Select Location --" });
            return locations;
        }

        public async Task<List<SelectListItem>> GetLocationsByVenueAsync(int companyId, int venueId)
        {
            var locations = await _context.Locations
                .Where(l => l.VenueId == venueId && l.IsActive)
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                })
                .ToListAsync();

            locations.Insert(0, new SelectListItem { Value = "", Text = "-- Select Location --" });
            return locations;
        }

        public async Task<List<SelectListItem>> GetTillsAsync(int companyId, int locationId = 0)
        {
            var query = _context.Tills.Where(t => t.CompanyId == companyId && t.IsActive);
            
            if (locationId > 0)
            {
                query = query.Where(t => t.LocationId == locationId);
            }
            
            var tills = await query
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                })
                .ToListAsync();

            tills.Insert(0, new SelectListItem { Value = "", Text = "-- Select Till --" });
            return tills;
        }

        public async Task<List<SelectListItem>> GetMachinesByLocationAsync(int companyId, int locationId)
        {
            var tills = await _context.Tills
                .Where(t => t.LocationId == locationId && t.IsActive)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                })
                .ToListAsync();

            tills.Insert(0, new SelectListItem { Value = "", Text = "-- Select Till --" });
            return tills;
        }

        public async Task<List<SelectListItem>> GetStaffAsync(int companyId)
        {
            var staff = await _context.Staff
                .Where(s => s.CompanyId == companyId && s.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            staff.Insert(0, new SelectListItem { Value = "", Text = "-- Select Staff --" });
            return staff;
        }

        public async Task<List<SelectListItem>> GetShiftsByDurationAsync(int companyId, string duration, DateTime fromDate, DateTime toDate)
        {
            var shifts = await _context.Shifts
                .Where(s => s.CompanyId() == companyId && s.IsActive())
                .Select(s => new SelectListItem
                {
                    Value = s.Name(),
                    Text = s.Name()
                })
                .ToListAsync();

            shifts.Insert(0, new SelectListItem { Value = "", Text = "-- Select Shift --" });
            return shifts;
        }

        public async Task<List<SelectListItem>> GetProductGroupsAsync(int companyId)
        {
            // Implement product group binding here
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Product Group --" }
            };
        }

        public async Task<List<SelectListItem>> GetProductsByGroupAsync(int companyId, int groupId)
        {
            // Implement products by group binding here
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Product --" }
            };
        }

        public async Task<List<SelectListItem>> GetCustomersAsync(int companyId)
        {
            // Implement customers binding here
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Customer --" }
            };
        }
    }
} 