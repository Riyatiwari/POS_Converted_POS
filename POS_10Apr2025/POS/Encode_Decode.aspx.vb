
Partial Class Encode_Decode

    Inherits BaseClass

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim s As String
        s = TextBox1.Text
        Label1.Text = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(s))
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim s As String
        s = TextBox2.Text
        'Return Encoding.UTF8.GetString(System.Convert.FromBase64String(Str))
        Label2.Text = Encoding.UTF8.GetString(System.Convert.FromBase64String(s))
    End Sub
    'dB6MtyRJJL3BuuCwOKrtKgff/pPg5LtnCsdreS9jtAI='

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim s As String
        s = TextBox3.Text
        Label3.Text = Decrypt(s)
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim s As String
        s = TextBox4.Text
        Label4.Text = Encrypt(s)
    End Sub
    
End Class
