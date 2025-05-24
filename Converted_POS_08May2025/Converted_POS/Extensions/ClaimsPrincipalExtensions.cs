using System;
using System.Linq;
using System.Security.Claims;

namespace Converted_POS.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Extension method to get the company ID from the ClaimsPrincipal
        /// </summary>
        /// <param name="principal">The ClaimsPrincipal</param>
        /// <returns>The company ID as an integer</returns>
        public static int GetCompanyId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var companyIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "CompanyId");
            if (companyIdClaim == null || !int.TryParse(companyIdClaim.Value, out int companyId))
            {
                // Default to 1 if no company ID is found or if it can't be parsed
                return 1;
            }

            return companyId;
        }
    }
} 