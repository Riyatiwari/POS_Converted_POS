<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Status.ascx.vb" Inherits="Controls_Status" %>
<div class="container_12">
    <ul id="status-infos">
        <li class="spaced"></li>
        <%--<li class="spaced">Logged as: <strong>Admin</strong></li>--%>
        <%--  <li><a href="javascript:void(0)" class="button" title="5 messages">
            <img src="~/images/icons/fugue/mail.png" width="16" height="16">
            <strong>5</strong></a>
            <div id="messages-list" class="result-block">
                <span class="arrow"><span></span></span>
                <ul class="small-files-list icon-mail">
                    <li><a href="javascript:void(0)"><strong>10:15</strong> Please update...<br>
                        <small>From: System</small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Yest.</strong> Hi<br>
                        <small>From: Jane</small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Yest.</strong> System update<br>
                        <small>From: System</small></a> </li>
                    <li><a href="javascript:void(0)"><strong>2 days</strong> Database backup<br>
                        <small>From: System</small></a> </li>
                    <li><a href="javascript:void(0)"><strong>2 days</strong> Re: bug report<br>
                        <small>From: Max</small></a> </li>
                </ul>
                <p id="messages-info" class="result-info">
                    <a href="javascript:void(0)">Go to inbox &raquo;</a></p>
            </div>
        </li>
        <li><a href="javascript:void(0)" class="button" title="25 comments">
            <img src="~/images/icons/fugue/balloon.png" width="16" height="16">
            <strong>25</strong></a>
            <div id="comments-list" class="result-block">
                <span class="arrow"><span></span></span>
                <ul class="small-files-list icon-comment">
                    <li><a href="javascript:void(0)"><strong>Jane</strong>: I don't think so<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Ken_54</strong>: What about using a different...<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Jane</strong> Sure, but no more.<br>
                        <small>On <strong>Another post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Max</strong>: Have you seen that...<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Anonymous</strong>: Good luck!<br>
                        <small>On <strong>My first post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Sébastien</strong>: This sure rocks!<br>
                        <small>On <strong>Another post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>John</strong>: Me too!<br>
                        <small>On <strong>Third post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>John</strong> This can be solved by...<br>
                        <small>On <strong>Another post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Jane</strong>: No prob.<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Anonymous</strong>: I had the following...<br>
                        <small>On <strong>My first post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Anonymous</strong>: Yes<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Lian</strong>: Please make sure that...<br>
                        <small>On <strong>Last post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Ann</strong> Thanks!<br>
                        <small>On <strong>Last post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Max</strong>: Don't tell me what...<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Gordon</strong>: Here is an article about...<br>
                        <small>On <strong>My another post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Lee</strong>: Try to reset the value first<br>
                        <small>On <strong>Last title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Lee</strong>: Sure!<br>
                        <small>On <strong>Second post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Many</strong> Great job, keep on!<br>
                        <small>On <strong>Third post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>John</strong>: I really like this<br>
                        <small>On <strong>First title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Paul</strong>: Hello, I have an issue with...<br>
                        <small>On <strong>My first post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>June</strong>: Yuck.<br>
                        <small>On <strong>Another title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Jane</strong>: Wow, sounds amazing, do...<br>
                        <small>On <strong>Another title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Esther</strong>: Man, this is the best...<br>
                        <small>On <strong>Another post</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>Max</strong>: Thanks!<br>
                        <small>On <strong>Post title</strong></small></a> </li>
                    <li><a href="javascript:void(0)"><strong>John</strong>: I'd say it is not safe...<br>
                        <small>On <strong>My first post</strong></small></a> </li>
                </ul>
                <p id="comments-info" class="result-info">
                    <a href="javascript:void(0)">Manage comments &raquo;</a></p>
            </div>
        </li>--%>
        <li>
            <telerik:RadButton ID="btnType" runat="server" ButtonType="ToggleButton" ToggleType="CustomToggle"
                Width="150px" Height="26px" AutoPostBack="true" style="padding-top:14px;" >
                <ToggleStates>
                    <telerik:RadButtonToggleState ImageUrl="../images/Switch_AE.png" HoveredImageUrl="../images/Switch_AE.png"
                        CssClass="adminType" Text="Admin" Selected="true" ></telerik:RadButtonToggleState>
                    <telerik:RadButtonToggleState ImageUrl="../images/Switch_AE.png" HoveredImageUrl="../images/Switch_AE.png"
                        CssClass="EssType" Text="ESS"></telerik:RadButtonToggleState>
                </ToggleStates>
            </telerik:RadButton>
        </li>
        <li>
            <asp:LinkButton ID="lbLogout" runat="server" CssClass="button red" ToolTip="Logout"><span class="smaller">
                    LOGOUT</span> </asp:LinkButton>
        </li>
    </ul>
    <!-- v1.5: you can now add class red to the breadcrumb -->
  <%--  <ul id="breadcrumb">
        <li><a href="Default.aspx" title="Home">Home</a></li>
        <li><a href="Home.aspx" title="Dashboard">Employee</a></li>
    </ul>--%>
</div>
