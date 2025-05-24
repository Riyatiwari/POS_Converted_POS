using System.Threading.Tasks;
using Converted_POS.ViewModels;

namespace Converted_POS.Services
{
    public interface IRoleService
    {
        Task<bool> HasViewPermissionAsync(int userId, string moduleName);
        
        Task<bool> HasEditPermissionAsync(int userId, string moduleName);
        
        Task<bool> HasDeletePermissionAsync(int userId, string moduleName);
        
        Task<bool> HasCreatePermissionAsync(int userId, string moduleName);

        Task<RoleAccessViewModel> CheckAccessAsync(int companyId, int roleId, string formName);
    }
} 