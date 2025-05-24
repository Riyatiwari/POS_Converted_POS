Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class RD_Detail
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRD As New Controller_clsResDiary()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("BookingId") = Nothing Then
                    BindRD_Detail()

                End If

            End If
        Catch ex As Exception
            LogHelper.Error("ResDiary_Detail:Page_Load:" + ex.Message)
        End Try
    End Sub


    Private Sub BindRD_Detail()
        Try

            oclsRD.BookingId = Val(Session("BookingId"))
            Dim dt As DataTable = oclsRD.SelectAll()
            If dt.Rows.Count > 0 Then
                lblBookingId.InnerText = dt.Rows(0)("BookingId").ToString
                lblref.InnerText = dt.Rows(0)("BookingReference").ToString
                lblvisit_DT.InnerText = dt.Rows(0)("VisitDateTime_cnvrted").ToString
                lblduration.InnerText = dt.Rows(0)("Duration").ToString
                lblturn_time.InnerText = dt.Rows(0)("TurnTime").ToString
                lblcust_name.InnerText = dt.Rows(0)("CustomerFullName").ToString
                lblcovers.InnerText = dt.Rows(0)("Covers").ToString
                lblcust_vip.InnerText = dt.Rows(0)("CustomerIsVip").ToString()
                lbltbl_locked.InnerText = dt.Rows(0)("IsTableLocked").ToString()
                lblpromotion.InnerHtml = dt.Rows(0)("HasPromotions").ToString()
                lblpayments.InnerHtml = dt.Rows(0)("HasPayments").ToString()
                lbl_leave_time.InnerHtml = dt.Rows(0)("IsLeaveTimeConfirmed").ToString()
                lblwalkin.InnerHtml = dt.Rows(0)("IsWalkIn").ToString()
                lblnumof_booking.InnerHtml = dt.Rows(0)("NumberOfBookings").ToString()
                lblcomments.InnerHtml = dt.Rows(0)("Comments").ToString()
                lblarrival_sts.InnerHtml = dt.Rows(0)("ArrivalStatus").ToString()
                lblmeal_sts.InnerHtml = dt.Rows(0)("MealStatus").ToString()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ResDiary_Detail:BindRD_Detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("ResDiary_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ResDiary_Detail:btnBack_Click:" + ex.Message)
        End Try
    End Sub
End Class
