<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Menu.ascx.vb" Inherits="Controls_Menu" %>
 <telerik:RadMenu runat="server" ID="RadMenu1" DataSourceID="SqlDataSource1" DataFieldID="Form_id"
                    DataTextField="Form_Name" Style="z-index: 5" EnableRoundedCorners="true"
                    EnableShadows="true" EnableTextHTMLEncoding="true">
               </telerik:RadMenu>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRMSConnectionString %>"
    SelectCommand="SELECT Form_id, Form_Name, Alias, Parent_id, Sorting_No FROM  M_Form order by sorting_no">
</asp:SqlDataSource>
<%--
<ul class="container_12">
    <li class="home current">
        <ul>
            <%-- <li class="with-menu"><a href="javascript:void(0)" title="My settings">Client Setup</a>
                <div class="menu">
                    <img src="../images/icons/icon-setting1.png" width="16" height="16">
                </div>
            </li>--%>
<%-- <li class="with-menu"><a href="javascript:void(0)" title="My settings">Temp</a>
                <div class="menu">
                    <img src="../images/icons/icon-setting1.png" width="16" height="16">
                    <ul>
                        <li class="icon_address"><a href="company_general_setting.aspx">Company General Setting</a></li>
                        <li class="icon_address"><a href="company_general_setting1.aspx">Company General Setting1</a></li>
                        <li class="icon_address"><a href="company_general_setting2.aspx">Company General Setting2</a></li>
                        <li class="icon_address"><a href="company_general_setting3.aspx">Company General Setting3</a></li>
                        <li class="icon_address"><a href="company_general_setting4.aspx">Company General Setting4</a></li>
                        <li class="icon_address"><a href="employee_asset_detail_list.aspx">Employee Asset Detail</a></li>
                        <li class="icon_address"><a href="employee_childran_detail_list.aspx">Employee Childran
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_contract_detail_list.aspx">Employee Contract
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_dependant_detail_list.aspx">Employee Dependant
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_document_detail_list.aspx">Employee Document
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_emergency_contact_detail_list.aspx">Employee
                            Emergency Contact Detail</a></li>
                        <li class="icon_address"><a href="employee_experience_detail_list.aspx">Employee Experience
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_immigration_detail_list.aspx">Employee Immigration
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_insurance_detail_list.aspx">Employee insurance
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_language_detail_list.aspx">Employee Language
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_license_detail_list.aspx">Employee License
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_reporting_detail_list.aspx">Employee Reporting
                            Detail</a></li>
                        <li class="icon_address"><a href="employee_skill_detail_list.aspx">Employee Skill Detail</a></li>
                        <li class="icon_address"><a href="leave_monthly_detail_list.aspx">Leave Monthly Detail</a></li>
                        <li class="icon_address"><a href="leave_monthly_salary_list.aspx">Leave Monthly Salary</a></li>
                        <li class="icon_address"><a href="leave_transaction_list.aspx">Leave Transaction</a></li>
                    </ul>
                </div>
            </li>--%>
<li class="with-menu"><a href="javascript:void(0)" title="My settings">Control Panel</a>
    <div class="menu">
        <img src="../images/icons/icon-setting1.png" width="16" height="16">
        <ul>
            <li class="icon_address"><a href="company_information.aspx">Company Information</a></li>
            <li class="icon_blog"><a href="#">Change Password</a> </li>
            <li class="icon_blog"><a href="#">Company Settings</a> </li>
            <li class="icon_blog"><a href="javascript:void(0)">Role Master</a>
                <ul>
                    <li class="icon_blog"><a href="Role_List.aspx">Role</a></li>
                    <li class="icon_blog"><a href="Role_Member_List.aspx">Role Member</a></li>
                </ul>
            </li>
            <li class="icon_blog"><a href="#">Imports Data</a></li>
            <li class="icon_blog"><a href="#">Email Notification</a> </li>
            <li class="icon_blog"><a href="#">IP Address Master</a> </li>
            <li class="icon_blog"><a href="#">SMS Setting</a> </li>
        </ul>
    </div>
</li>
<li class="with-menu"><a href="javascript:void(0)" title="Dashboard">Masters</a>
    <div class="menu">
        <img src="../images/icons/icon-master.png" width="16" height="16">
        <ul>
            <li class="icon_address"><a href="javascript:void(0)">Job Master</a>
                <ul>
                    <li class="icon_blog"><a href="branch_list.aspx">Branch Master</a></li>
                    <li class="icon_blog"><a href="brand_list.aspx">Brand Master</a></li>
                    <li class="icon_blog"><a href="country_list.aspx">Country</a></li>
                    <li class="icon_blog"><a href="#">City</a></li>
                    <li class="icon_blog"><a href="#">Cost Center</a></li>
                    <li class="icon_blog"><a href="designation_list.aspx">Designation Master</a></li>
                    <li class="icon_blog"><a href="department_list.aspx">Department Master</a></li>
                    <li class="icon_blog"><a href="#">Document Check List</a></li>
                    <li class="icon_blog"><a href="emp_list.aspx">Employee Type</a></li>
                    <li class="icon_blog"><a href="grade_list.aspx">Grade Master</a></li>
                    <li class="icon_blog"><a href="j#">Job Description</a></li>
                    <li class="icon_blog"><a href="shift_list.aspx">Shift Master</a></li>
                    <li class="icon_blog"><a href="#">State</a></li>
                </ul>
            </li>
            <li class="icon_export"><a href="javascript:void(0)">Company Structure</a>
                <ul>
                    <li class="icon_network"><a href="#">Divisions</a></li>
                    <li class="icon_network"><a href="#">Warning Master</a></li>
                    <li class="icon_network"><a href="#">Allowance/Deduction</a></li>
                    <li class="icon_network"><a href="#">Present/Late Scenario</a></li>
                    <li class="icon_network"><a href="#">Holiday Master</a></li>
                    <li class="icon_network"><a href="#">Perfomance Master</a></li>
                    <li class="icon_network"><a href="#">AD Slab Setting</a></li>
                </ul>
            </li>
            <li class="icon_export"><a href="javascript:void(0)">Qualification</a>
                <ul>
                    <li class="icon_network"><a href="#">Education</a></li>
                    <li class="icon_network"><a href="#x">License</a></li>
                </ul>
            </li>
            <%-- <li class="icon_export"><a href="javascript:void(0)">Skills</a>
                            <ul>
                                <li class="icon_network"><a href="skill_list.aspx">Skills</a></li>
                                <li class="icon_network"><a href="language_list.aspx">Language</a></li>
                            </ul>
                        </li>
                        <li class="icon_export"><a href="javascript:void(0)">Membership</a>
                            <ul>
                                <li class="icon_network"><a href="membershiptype_list.aspx">Membership Type</a></li>
                                <li class="icon_network"><a href="membership_list.aspx">Memberships</a></li>
                            </ul>
                        </li>
                        <li class="icon_export"><a href="javascript:void(0)">Nationality & Race</a>
                            <ul>
                                <li class="icon_network"><a href="nationality_list.aspx">Nationality</a></li>
                                <li class="icon_network"><a href="ethnicrace_list.aspx">Ethnic Race</a></li>
                            </ul>
                        </li>
                        <li class="icon_export"><a href="javascript:void(0)">Project Info</a>
                            <ul>
                                <li class="icon_network"><a href="customer_list.aspx">Customer</a></li>
                                <li class="icon_network"><a href="projects_list.aspx">Projects</a></li>
                                <li class="icon_network"><a href="projectsactivity_list.aspx">Projects Activity</a></li>
                            </ul>
                        </li>--%>
            <li class="icon_export"><a href="javascript:void(0)">Aduit Trial</a>
                <ul>
                    <li class="icon_network"><a href="javascript:void(0)">Various Activities</a></li>
                </ul>
            </li>
        </ul>
    </div>
</li>
<li class="with-menu"><a href="table.aspx" title="Table">Employee</a>
    <div class="menu">
        <img src="../images/icons/icon_employee.png" width="16" height="16" />
        <ul>
            <li class="icon_address"><a href="employee_list.aspx">Employee Master</a></li>
            <li class="icon_address"><a href="#">Left Employee Master </a></li>
            <li class="icon_address"><a href="#">Employee Increment </a></li>
            <li class="icon_address"><a href="#">Employee Transfer/Deputation </a></li>
            <li class="sep"></li>
            <%--  <li class="icon_address"><a href="javascript:void(0)">Gradewise Allowance </a></li>--%>
            <li class="icon_address"><a href="#">Warning Card </a></li>
            <li class="icon_address"><a href="javascript:void(0)">Employee Privileges </a></li>
            <li class="icon_address"><a href="#">Reporting Manager </a></li>
        </ul>
    </div>
</li>
<li class="with-menu"><a href="calendars.aspx" title="calendars">Leave Management</a>
    <div class="menu">
        <img src="../images/icons/icon-leave.png" width="16" height="16">
        <ul>
            <li class="icon_address"><a href="#">Leave Master </a></li>
            <li class="icon_address"><a href="#">Leave Details </a></li>
            <li class="icon_address"><a href="#">Leave Opening </a></li>
            <li class="sep"></li>
            <li class="icon_address"><a href="#">Leave Application </a></li>
            <li class="icon_address"><a href="#">Leave Approval </a></li>
            <li class="icon_address"><a href="#">Admin Leave Approval </a></li>
            <%-- <li class="icon_address"><a href="javascript:void(0)">Leave Updates </a></li>--%>
            <li class="sep"></li>
            <li class="icon_address"><a href="#">Leave Cancellation </a></li>
            <li class="icon_address"><a href="#">Leave Carry Forward </a></li>
            <%--  <li class="icon_address"><a href="leave_encash_approval_list.aspx">Leave Encash Approve</a></li>
                        <li class="icon_address"><a href="leave_encash_application_list.aspx">Leave Encash Application
                        </a></li>--%>
            <%-- <li class="icon_address"><a href="javascript:void(0)">Leave Direct Encash </a></li>
                        <li class="icon_address"><a href="javascript:void(0)">LeaveWise Encashment </a></li>
                        <li class="icon_address"><a href="javascript:void(0)">TimeOff Management</a></li>--%>
        </ul>
    </div>
</li>
<%-- <li class="with-menu"><a href="explorer.aspx" title="explorer">Expense Management</a>
                <div class="menu">
                    <img src="../images/icons/icon-loan.png" width="16" height="16">
                    <ul>
                        <li class="icon_address"><a href="expense_master_list.aspx">Expense Master</a></li>
                        <li class="icon_address"><a href="expense_application_list.aspx">Expense Application</a></li>
                        <li class="icon_address"><a href="expense_approval_list.aspx">Expense Approval</a></li>
                        <li class="icon_address"><a href="expense_monthly_payment_list.aspx">Expense Payment</a></li>
                        <li class="icon_address"><a href="expense_transaction_list.aspx">Expense Transaction</a></li>
                    </ul>
                </div>
            </li>--%>
<%--  <li class="with-menu"><a href="forms.aspx" title="forms">Medical Management</a>
                <div class="menu">
                    <img src="../images/icons/icon-r1.png" width="16" height="16">
                    <ul>
                        <li class="icon_address"><a href="medical_setting_list.aspx">Medical Setting </a>
                        </li>
                        <li class="icon_address"><a href="medical_detail_list.aspx">Medical Details</a></li>
                        <li class="sep"></li>
                        <li class="icon_address"><a href="medical_application_list.aspx">Medical Application
                            & Approval</a></li>
                        <li class="icon_address"><a href="medical_approval_list.aspx">Medical Approval</a></li>
                        <li class="icon_address"><a href="medical_payment_list.aspx">Medical Payment </a>
                        </li>
                        <li class="icon_address"><a href="javascript:void(0)">Medical History </a></li>
                        <li class="icon_address"><a href="medical_transaction_list.aspx">Medical Transaction</a></li>
                    </ul>
                </div>
            </li>--%>
<li class="with-menu"><a href="#" title="forms">Reports</a>
    <div class="menu">
        <img src="../images/icons/icon-r1.png" width="16" height="16">
        <ul>
            <li class="icon_address"><a href="javascript:void(0)">Employee </a></li>
            <li class="icon_address"><a href="javascript:void(0)">Time Management</a></li>
            <li class="sep"></li>
            <li class="icon_address"><a href="javascript:void(0)">Leave Management</a></li>
            <li class="icon_address"><a href="javascript:void(0)">Medical Management </a></li>
            <li class="icon_address"><a href="javascript:void(0)">Expense Management </a></li>
            <li class="sep"></li>
        </ul>
    </div>
</li>
<li class="with-menu"><a href="#" title="forms">HRMS</a>
    <div class="menu">
        <img src="../images/icons/icon-r1.png" width="16" height="16">
    </div>
</li>
</li> </ul> --%>