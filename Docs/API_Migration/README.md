# API Migration Documentation

This folder contains documentation for the API migration from VB.NET ASP Classic to C# ASP.NET Core.

## Migration Progress

| API Controller | Status | Migration Date | Notes |
|----------------|--------|---------------|-------|
| StoreController | ✅ Completed | 2024-05-13 | Store/Location management API |
| CompanyListController | ✅ Completed | 2024-05-13 | Company management API |
| SettingsController | ✅ Completed | 2024-05-13 | Location settings API |
| ValueListController | ✅ Completed | 2024-05-13 | Various lookup value APIs |
| LocationListController | ✅ Completed | 2024-05-13 | Location listing APIs |
| MachineListController | ✅ Completed | 2024-05-13 | Machine management API |
| ShiftListController | ✅ Completed | 2024-05-13 | Shift management API |
| EmailSettingController | ✅ Completed | 2024-05-13 | Email configuration API |
| FunctionListController | ✅ Completed | 2024-05-13 | Function management API |
| ProductListController | ✅ Completed | 2024-05-13 | Product management API |
| UserListController | ✅ Completed | 2024-05-13 | User management API |
| StaffRulesMasterListController | ✅ Completed | 2024-05-14 | Staff rules master API |
| StaffRulesDetailsListController | ✅ Completed | 2024-05-14 | Staff rules details API |
| CompanyDetailsController | ✅ Completed | 2024-05-14 | Company details API |
| ProductGroupListController | ✅ Completed | 2024-05-14 | Product group management API |
| CustomerListController | ✅ Completed | 2024-05-14 | Customer management API |
| RoleListController | ✅ Completed | 2024-05-14 | Role management API |
| PrefixListController | ✅ Completed | 2024-05-14 | Prefix management API |
| BarcodeListController | ✅ Completed | 2024-05-14 | Barcode management API |
| VenueListController | ✅ Completed | 2024-05-14 | Venue management API |
| SizeListController | ✅ Completed | 2024-05-14 | Size management API |
| ProductCondimentListController | ✅ Completed | 2024-05-15 | Product condiment management API |
| PriceBySizeController | ✅ Completed | 2024-05-15 | Price by size management API |
| PricesListController | ✅ Completed | 2024-05-15 | Prices list management API |
| BuyingSizeController | ✅ Completed | 2024-05-16 | Buying size management API |
| TaxListController | ✅ Completed | 2024-05-16 | Tax management API |
| IngredientsListController | ✅ Completed | 2024-05-16 | Ingredients management API |
| DeviceListController | ✅ Completed | 2024-05-16 | Device list with payments API |
| KeyMapListController | ✅ Completed | 2024-05-16 | Key map management API |
| PrinterListController | ✅ Completed | 2024-05-16 | Printer management API |
| PrinterDetailsByMachineController | ✅ Completed | 2024-05-16 | Printer details by machine API |
| CourseListController | ✅ Completed | 2024-05-17 | Course management API |
| KeyMapDetailsNewController | ✅ Completed | 2024-05-17 | Key map details API |
| TableListController | ✅ Completed | 2024-05-17 | Table management API |
| DepartmentListController | ✅ Completed | 2024-05-18 | Department management API |
| VoucherListController | ✅ Completed | 2024-05-18 | Voucher management API |
| MachineAssignDetailsController | ✅ Completed | 2024-05-18 | Machine assign details API |
| GroupCategoryListController | ✅ Completed | 2024-05-19 | Group category management API |
| UnitListController | ✅ Completed | 2024-05-19 | Unit management API |

## Migration Guidelines

1. **Keep Original Logic**: Maintain the exact same business logic from the original VB.NET code.
2. **Naming Conventions**: Use C# naming conventions (PascalCase for methods and properties).
3. **Error Handling**: Implement consistent error handling in all APIs.
4. **Documentation**: Document each API with XML comments.
5. **Validation**: Implement proper model validation.

## Testing

Each API should be tested against the original VB.NET implementation to ensure exact functionality is preserved.

## Connection Strings

The APIs use the following connection strings:
- `POSConnectionString`: For POS database operations
- `POSControllerConnectionString`: For POS Controller database operations 