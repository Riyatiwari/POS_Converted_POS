Imports System.Web.Script.Serialization

Partial Class sale
    Inherits System.Web.UI.Page

    Private Sub sale_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim strResp As String = "{'success':true,'transaction':{'account_number':'*****','amount':600,'amount_other':null,'app_confirmation_required':false,'app_label':null,'attempted_sources':[],'attempts_remaining':3,'auth_code':null,'card_details':{},'card_expiry':null,'currency_scale':2,'currency_symbol':'Â£','cvm_mode':'none','duration':1.997,'ended_at':1686555183.648,'cvm_result':null,'host':'Ukgbs','host_module_name':'SystemGenerated','id':'5c5af576-08f3-11ee-8c88-03c612ae24da','input_source':null,'is_api':true,'is_active':true,'is_contactless':false,'is_contactless_transaction_limit_exceeded':false,'is_deferred_auth':false,'is_emv':false,'is_fallback':false,'is_force_online':false,'is_keyed':false,'is_online':true,'is_online_pin':false,'is_offline_pin':false,'is_reversed':false,'is_sca_single_tap':false,'is_signature_required':false,'issuer_id':null,'job_id':null,'ksn':null,'last_deferred_auth_attempted_at':null,'message_number':null,'num_attempts':0,'num_deferred_auth_attempts':0,'online_request':null,'online_response':null,'online_result':null,'pin_block':null,'reference_number':null,'reference_txn_id':null,'result':null,'services':{'code':null,'interchange_rules':'unknown','authorization_processing_rules':'unknown','range_of_services':'unknown'},'session_number':null,'should_fallback':false,'should_retry':false,'stan':423,'started_at':1686555181.651,'status':null,'status_details':null,'tags':{},'terminal_online_decision':null,'timeout_ms':150000,'tip_amount':0,'total_amount':600,'type':'sale'}}"
        Dim j As JavaScriptSerializer = New JavaScriptSerializer()
        Dim a As Object = j.Deserialize(strResp, GetType(Object))

        Response.Write(serializer.Serialize(a))

    End Sub
End Class
