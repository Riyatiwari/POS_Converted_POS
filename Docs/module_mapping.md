# Module Mapping

This document maps the old ASP.NET WebForms modules to their new ASP.NET Core counterparts.

## Core Modules

### Product Management
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Product_Master.aspx` | `ProductController.cs` | ✅ |
| `Product_Group.aspx` | `ProductGroupController.cs` | ✅ |
| `Product_Price.aspx` | `ProductPriceController.cs` | ✅ |
| `Product_Image.aspx` | `ProductController.cs` (Images Action) | 🟨 |

### Sales & Reporting
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `z_report_OperatorWise.aspx` | `ZReportController.cs` | ✅ |
| `ZR_VAT_Detail.aspx` | `ZReportController.cs` | ✅ |
| `ZReport.aspx` | `ZReportController.cs` | ✅ |
| `Sales_Report.aspx` | `SalesReportController.cs` | ✅ |

### Device Management
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Device_Master.aspx` | `DeviceController.cs` | ✅ |
| `Device_Type.aspx` | `Device_TypeController.cs` | ✅ |
| `Printer_Setup.aspx` | `PrinterController.cs` | ✅ |
| `Machine_Config.aspx` | `MachineController.cs` | ✅ |

### User Management
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Staff_Master.aspx` | `StaffController.cs` | ✅ |
| `User_Access.aspx` | `UserController.cs` | 🟨 |
| `User_Role.aspx` | Not Implemented | ❌ |

### Location & Table
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Location_Master.aspx` | `LocationController.cs` | ✅ |
| `Tab_Table_Management.aspx` | `TableController.cs` | 🟨 |
| `Table_Transaction.aspx` | Not Implemented | ❌ |

### Financial
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Tax_Master.aspx` | `TaxController.cs` | ✅ |
| `Voucher_Master.aspx` | Not Implemented | ❌ |
| `Till_Summary.aspx` | Not Implemented | ❌ |

### Customer
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Customer_Master.aspx` | `CustomerController.cs` | ✅ |
| `Customer_History.aspx` | Not Implemented | ❌ |
| `Customer_Loyalty.aspx` | Not Implemented | ❌ |

## Supporting Modules

### Integration
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Xero_integration.aspx` | Not Implemented | ❌ |
| `Payment_Gateway.aspx` | Not Implemented | ❌ |
| `QR_Code.aspx` | `QRController.cs` | ✅ |

### Configuration
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| `Web.config` | `appsettings.json` | ✅ |
| `Global.asax` | `Startup.cs` | ✅ |
| `App_Code/*.vb` | Various Services | 🟨 |

### Authentication
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| Forms Authentication | ASP.NET Core Identity | 🟨 |
| Role Provider | Identity Roles | ❌ |
| Custom Membership | Custom Identity Store | ❌ |

## UI Components

### Controls
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| User Controls (.ascx) | Partial Views | 🟨 |
| Server Controls | Tag Helpers | 🟨 |
| Custom Controls | View Components | ❌ |

### Static Content
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| CSS Files | wwwroot/css | 🟨 |
| JavaScript Files | wwwroot/js | 🟨 |
| Images | wwwroot/images | 🟨 |

## Database Access

### Data Layer
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| ADO.NET Code | Entity Framework Core | ✅ |
| Stored Procedures | LINQ Queries | 🟨 |
| DataSet/DataTable | Entity Classes | ✅ |

### Business Layer
| Old Module (WebForms) | New Module (ASP.NET Core) | Status |
|----------------------|---------------------------|---------|
| Business Classes | Service Layer | ✅ |
| Helper Classes | Extension Methods | ✅ |
| Utility Classes | Common Library | 🟨 |

## Notes

### Migration Patterns
1. WebForms Page → Controller + View
2. Code Behind → Controller Actions
3. User Controls → Partial Views
4. Master Pages → Layout Pages
5. Page Methods → API Endpoints

### Pending Decisions
1. Custom Control Migration Strategy
2. State Management Approach
3. Session Handling
4. Cache Implementation
5. Report Generation

### Technical Considerations
1. Database Schema Updates
2. API Versioning
3. Authentication Flow
4. Authorization Rules
5. Error Handling

### UI/UX Improvements
1. Responsive Design
2. Modern UI Framework
3. Client-side Validation
4. Progressive Enhancement
5. Accessibility Standards 