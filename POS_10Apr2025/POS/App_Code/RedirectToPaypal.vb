Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System
''' <summary>
''' Summary description for RedirectToPaypal
''' </summary>
Public Class RedirectToPaypal

    Public Shared Function getItemNameAndCost(itemName As String, itemCost As String, email_id As String, URL As String, currency As String, ireturn As String, icancel As String, x_first_name As String, x_last_name As String, x_email As String, x_address As String, invoice As String, x_phone As String) As String

        Dim price As String = Convert.ToDecimal(itemCost)
        Dim returnURL As String = ""

        'https://www.paypal.com/xclick/business=
        'https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=
        returnURL += URL + email_id

        returnURL += Convert.ToString("&item_name=") & itemName

        returnURL += "&first_name=" + x_first_name

        returnURL += "&last_name=" + x_last_name

        returnURL += "&address1=" + x_address

        returnURL += "&email=" + x_email

        returnURL += "&amount=" + price

        returnURL += "&invoice=" + invoice

        returnURL += "&currency=" + currency

        returnURL += "&return=" + ireturn

        returnURL += "&rm=" + "2"

        returnURL += "&cancel_return=" + icancel

        Return returnURL

    End Function

End Class