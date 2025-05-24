using System;
using System.Data;
using System.Threading.Tasks;
using Converted_POS.ViewModels;

namespace Converted_POS.Services
{
    // Make this interface inherit from the Interfaces namespace one to prevent ambiguity
    public interface ITillSummaryService : Interfaces.ITillSummaryService
    {
        // No need to redefine methods here, they're inherited from the base interface
    }
} 