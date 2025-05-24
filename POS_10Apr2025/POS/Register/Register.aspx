<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="Register_Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Welcome:Register you details here</title>
    <style>
        * {
            box-sizing: border-box;
        }

        .row::after {
            content: "";
            clear: both;
            display: table;
        }

        [class*="col-"] {
            float: left;
        }

        html {
            font-family: "Lucida Sans", sans-serif;
        }

        .loader {
            background-color: #efefef; /* Green background */
            height: 800px;
            width: 100%;
            position:absolute; 
            top:0;
            left:0;
            opacity:50%;
            
            
        }

        .header {
            background-color: rgba(0,0,0,0.24);            
            padding: 10px;
            width: 100%;
            height:100%;
        }




        .Subheader {
            background-color: #000000;
            color: #ffffff;
            width: 79%;
            height: 20%;
            float: left;
            text-align: center;
            font-family: sans-serif;
        }

        .menu ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }

        .menu li {
            padding: 8px;
            margin-bottom: 7px;
            background-color: #33b5e5;
            color: #ffffff;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
        }

            .menu li:hover {
                background-color: #0099cc;
            }

        .aside {
            background-color: #000000;
            color: #ffffff;
            text-align: center;
            font-size: 14px;
            box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
        }

        .footer {
            background-color: #ffffff;
            color: #000000;
            text-align: center;
            font-size: 12px;
            padding: 15px;
            float: left;
            width: 100%;
        }

        /* For mobile phones: */
        [class*="col-"] {
            width: 100%;
        }

        @media only screen and (min-width: 600px) {
            /* For tablets: */
            .col-s-1 {
                width: 8.33%;
            }

            .col-s-2 {
                width: 16.66%;
            }

            .col-s-3 {
                width: 25%;
            }

            .col-s-4 {
                width: 33.33%;
            }

            .col-s-5 {
                width: 41.66%;
            }

            .col-s-6 {
                width: 50%;
            }

            .col-s-7 {
                width: 58.33%;
            }

            .col-s-8 {
                width: 66.66%;
            }

            .col-s-9 {
                width: 75%;
            }

            .col-s-10 {
                width: 83.33%;
            }

            .col-s-11 {
                width: 91.66%;
            }

            .col-s-12 {
                width: 100%;
            }
        }

        label {
            padding: 12px 12px 12px 0px;
            display: inline-block;
        }

        input[type=text], select, textarea {
            width: 50%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            resize: vertical;
        }

        @media only screen and (min-width: 768px) {
            /* For desktop: */
            .col-1 {
                width: 8.33%;
            }

            .col-2 {
                width: 16.66%;
            }

            .col-3 {
                width: 25%;
            }

            .col-4 {
                width: 33.33%;
            }

            .col-5 {
                width: 41.66%;
            }

            .col-6 {
                width: 50%;
            }

            .col-7 {
                width: 58.33%;
            }

            .col-8 {
                width: 66.66%;
            }

            .col-9 {
                width: 75%;
            }

            .col-10 {
                width: 83.33%;
            }

            .col-11 {
                width: 91.66%;
            }

            .col-12 {
                width: 100%;
            }
        }

        .button {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="header" style="text-align: left;">
                    <img src="https://www.therylston.com/wp-content/uploads/2012/10/logo_Reversed1.png" />


                </div>

                <div class="row">


                    <div class="col-12 col-s-9">
                        <h3 style="text-align: center;">Congratulations you are 1 step away from receiving your voucher for a complimentary house drink at your favourite local between May 1st and 5th</h3>
                    </div>

                    <div class="col-12 col-s-12">
                        <div class="aside">
                            
                            
                            <br />
                            <h2>Please fill in the below fields:</h2>
                            <p>
                                <label>Name :</label>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </p>
                            <p>
                                <label>Email :</label>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" ErrorMessage = "Invalid email address"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
    ForeColor="Red" Display = "Dynamic" ErrorMessage = "Required" />

                            </p>
                            <p>
                                <label>Post Code:</label>
                                <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox>
                            </p>
                            <asp:Button CssClass="button" ID="btnRegister" OnClick="btnRegister_Click" Text="Submit" runat="server" />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
                <br />
                <br />

                <div class="footer">
                    <p>All Right Reserved @ 2023</p>
                </div>

                <asp:UpdateProgress runat="server" ID="uprog1" AssociatedUpdatePanelID="up1" DisplayAfter="2">
                    <ProgressTemplate>
                        <div class="loader">
                            <img src="../images/icons/loading.gif" width="50px" height="50px" style="top:50%;left:50%;position:absolute;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />
            </Triggers>

        </asp:UpdatePanel>



    </form>
</body>
</html>
