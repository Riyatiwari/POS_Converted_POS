<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Customer.ascx.vb" Inherits="Bookingeasy_UserControl_Customer" %>
<style type="text/css">
    .chkmrng td
    {
        margin-right: 5px;
    }
    .checkbox input 
    {
        margin-right: 5px;
    }
    .checkbox label 
    {
        margin-right: 5px;
    }
</style>
<%--<asp:Panel ID="Panel1" runat="server" Visible="false">
<div class="form-group">
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                First Name:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtFname" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFname" runat="server" ControlToValidate="txtFname"
                    ForeColor="Red" ErrorMessage="Enter First Name." Display="Dynamic" CssClass="rfv">
                </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                Last Name:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtLname" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLname" runat="server" ControlToValidate="txtLname"
                    ForeColor="Red" ErrorMessage="Enter Last Name." Display="Dynamic" CssClass="rfv">
                </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                Mobile:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile"
                    ForeColor="Red" ErrorMessage="Enter Mobile No." Display="Dynamic" CssClass="rfv">
                </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                Work Number:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtWorkNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-10">
        <div class="form-group">
            <label class="col-xs-3 col-lg-2 control-label">
                Email:</label>
            <div class="col-sm-9 col-lg-10 controls">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ForeColor="Red" ErrorMessage="Enter Email." Display="Dynamic" CssClass="rfv">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ForeColor="Red" ErrorMessage="Enter Email In Proper Format." Display="Dynamic"
                    CssClass="rfv" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-10">
        <div class="form-group">
            <label class="col-xs-3 col-lg-2 control-label">
                Address:</label>
            <div class="col-sm-9 col-lg-10 controls">
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-10">
        <div class="form-group">
            <label class="col-xs-3 col-lg-2 control-label">
                Address:</label>
            <div class="col-sm-9 col-lg-10 controls">
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-10">
        <div class="form-group">
            <label class="col-xs-3 col-lg-2 control-label">
                Address:</label>
            <div class="col-sm-9 col-lg-10 controls">
                <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
<div class="form-group">
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                City:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="col-md-5">
        <div class="form-group">
            <label class="col-xs-3 col-lg-4 control-label">
                Post Code:</label>
            <div class="col-sm-9 col-lg-8 controls">
                <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
</asp:Panel>--%>
<asp:HiddenField ID="hfCategory" Value="B" runat="server" />
<asp:Panel ID="Paenl_DynamicField" runat="server">
    <div class="form-group" id="frm01" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd01" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd02" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm11" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd11" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd12" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm21" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd21" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd22" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm31" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd31" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd32" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm41" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd41" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd42" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm51" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd51" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd52" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm61" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd61" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd62" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm71" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd71" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd72" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm81" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd81" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd82" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm91" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd91" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd92" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm101" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd101" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd102" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm111" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd111" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd112" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm121" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd121" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd122" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm131" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd131" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd132" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm141" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd141" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd142" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm151" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd151" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd152" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm161" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd161" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd162" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm171" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd171" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd172" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm181" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd181" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd182" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm191" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd191" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd192" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm201" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd201" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd202" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm211" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd211" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd212" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm221" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd221" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd222" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm231" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd231" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd232" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm241" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd241" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd242" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm251" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd251" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd252" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm261" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd261" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd262" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm271" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd271" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd272" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm281" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd281" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd282" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm291" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd291" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd292" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm301" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd301" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd302" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm311" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd311" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd312" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm321" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd321" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd322" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm331" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd331" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd332" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm341" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd341" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd342" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm351" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd351" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd352" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm361" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd361" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd362" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm371" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd371" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd372" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm381" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd381" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd382" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm391" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd391" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd392" runat="server">
                </div>
            </div>
        </div>
    </div>

        <div class="form-group" id="frm401" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd401" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd402" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm411" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd411" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd412" runat="server">
                </div>
            </div>
        </div>
    </div>
        <div class="form-group" id="frm421" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd421" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd422" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm431" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd431" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd432" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm441" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd441" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd442" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm451" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd451" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd452" runat="server">
                </div>
            </div>
        </div>
    </div>
    <div class="form-group" id="frm461" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd461" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd462" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm471" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd471" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd472" runat="server">
                </div>
            </div>
        </div>
    </div>
        <div class="form-group" id="frm481" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd481" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd482" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm491" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd491" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd492" runat="server">
                </div>
            </div>
        </div>
    </div>
        <div class="form-group" id="frm501" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd501" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd502" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm511" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd511" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd512" runat="server">
                </div>
            </div>
        </div>
    </div>
            <div class="form-group" id="frm521" runat="server" visible="false">
        <div class="col-md-5">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd521" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd522" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-5" id="frm531" runat="server" visible="false">
            <div class="form-group">
                <div class="col-xs-3 col-lg-4 control-label" id="Tdd531" runat="server">
                </div>
                <div class="col-sm-9 col-lg-8 controls" id="Tdd532" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
