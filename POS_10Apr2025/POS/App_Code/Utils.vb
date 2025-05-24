Imports System.Configuration
Imports System.Web.Security

Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data
Imports System.Net
Imports System

Public Class Utils
    Public Shared Function GetNullInt(ByVal text As String) As System.Nullable(Of Integer)
        If String.IsNullOrWhiteSpace(text) Then
            Return Nothing
        Else
            Return ParseInt(text)
        End If
    End Function
    Public Shared Function GetNullDecimal(ByVal text As String) As System.Nullable(Of Decimal)
        If String.IsNullOrWhiteSpace(text) Then
            Return Nothing
        Else
            Return ParseDecimal(text)
        End If
    End Function
    Public Shared Function GetNullBool(ByVal text As String) As System.Nullable(Of Boolean)
        If String.IsNullOrWhiteSpace(text) Then
            Return Nothing
        Else
            Return ParseBool(text)
        End If
    End Function
    Public Shared Function GetNullDate(ByVal text As String) As System.Nullable(Of DateTime)
        If String.IsNullOrEmpty(text) Then
            Return Nothing
        Else
            Return ParseDateTime(text)
        End If
    End Function
    Public Shared Function GetNullDouble(ByVal text As String) As System.Nullable(Of Double)
        If String.IsNullOrWhiteSpace(text) Then
            Return Nothing
        Else
            Return ParseDouble(text)
        End If
    End Function
    Public Shared Function ParseInt(ByVal text As String) As Integer
        Return ParseInt(text, -1)
    End Function
    Public Shared Function ParseInt(ByVal text As String, ByVal defaultValue As Integer) As Integer
        Dim result As Integer
        Return If(Integer.TryParse(text, result), result, defaultValue)
    End Function
    Public Shared Function ParseDateTime(ByVal text As String, ByVal defaultValue As DateTime) As DateTime
        Dim result As DateTime
        Return If(DateTime.TryParse(text, result), result, defaultValue)
    End Function
    Public Shared Function ParseDateTime(ByVal text As String) As DateTime
        Return ParseDateTime(text, DateTime.MinValue)
    End Function
    Public Shared Function ParseDateTimeWithNull(ByVal text As String) As System.Nullable(Of DateTime)
        Dim result As System.Nullable(Of DateTime)
        Try
            result = Convert.ToDateTime(text)
        Catch
            result = Nothing
        End Try
        Return result
    End Function
    Public Shared Function ParseDecimal(ByVal text As String) As Decimal
        Dim result As Decimal
        Return If(Decimal.TryParse(text, result), result, Decimal.Zero)
    End Function
    Public Shared Function ParseDecimalWithNull(ByVal text As String) As System.Nullable(Of Decimal)
        Dim result As System.Nullable(Of Decimal) = Utils.ParseDecimal(text)
        If String.IsNullOrWhiteSpace(text) OrElse result = 0 Then
            result = Nothing
        End If
        Return result
    End Function
    Public Shared Function ParseDecimal(ByVal text As String, ByVal allowCurrency As Boolean) As Decimal
        If Not allowCurrency Then
            Return ParseDecimal(text)
        Else
            Dim result As Decimal
            Return If(Decimal.TryParse(text, System.Globalization.NumberStyles.AllowCurrencySymbol, System.Globalization.NumberFormatInfo.CurrentInfo, result), result, Decimal.Zero)
        End If
    End Function
    Public Shared Function ParseBool(ByVal text As String, ByVal defaultValue As Boolean) As Boolean
        Dim result As Boolean
        Return If(Boolean.TryParse(text, result), result, defaultValue)
    End Function
    Public Shared Function ParseBool(ByVal text As String) As Boolean
        Return ParseBool(text, False)
    End Function
    Public Shared Function ParseFloat(ByVal text As String) As Single
        Dim result As Single
        Return If(Single.TryParse(text, result), result, Single.NaN)
    End Function
    Public Shared Function ParseDouble(ByVal text As String) As Double
        Dim result As Double
        Return If(Double.TryParse(text, result), result, Double.NaN)
    End Function
    Public Shared Function GetWebConfigKeyValue(ByVal keyNmae As String) As String
        Return ConfigurationManager.AppSettings(keyNmae)
    End Function
    Public Shared Function generateRandomPassword() As String
        Dim randomString As String = Guid.NewGuid().ToString("N")
        randomString = randomString.Substring(0, 10)
        Return randomString
    End Function
    Public Shared Function generateRandomPassword(ByVal len As Integer) As String
        Dim randomString As String = Guid.NewGuid().ToString("N")
        randomString = randomString.Substring(0, len)
        Return randomString
    End Function
    Public Shared Function Encrypt(ByVal toEncrypt As String) As String
        Dim keyArray As Byte()
        Dim toEncryptArray As Byte() = UTF8Encoding.UTF8.GetBytes(toEncrypt)
        Dim key As String = "Booking@System"
        'If hashstring use get hashcode regards to your key

        Dim hashmd5 As New MD5CryptoServiceProvider()
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
        'Always release the resources and flush data
        ' of the Cryptographic service provide. Best Practice

        hashmd5.Clear()

        Dim tdes As New TripleDESCryptoServiceProvider()
        'set the secret key for the tripleDES algorithm
        tdes.Key = keyArray
        'mode of operation. there are other 4 modes.
        'We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB
        'Padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7

        Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
        'transform the specified region of bytes array to resultArray
        Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
        'Release resources held by TripleDes Encryptor
        tdes.Clear()
        'Return the encrypted data into unreadable string format
        Return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace("+", "___")
    End Function
    Public Shared Function Decrypt(ByVal cipherstring As String) As String
        Try
            cipherstring = cipherstring.Replace("___", "+")

            Dim keyArray As Byte()
            'get the byte code of the string
            Dim key As String = "Booking@System"
            Dim toEncryptArray As Byte() = Convert.FromBase64String(cipherstring)

            'if hashstring was used get the hash code with regards to your key
            Dim hashmd5 As New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            'release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear()

            Dim tdes As New TripleDESCryptoServiceProvider()
            'set the secret key for the tripleDES algorithm
            tdes.Key = keyArray
            'mode of operation. there are other 4 modes. 
            'We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB
            'Padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7

            Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
            'Release resources held by TripleDes Encryptor                
            tdes.Clear()
            'return the Clear decrypted TEXT
            Return UTF8Encoding.UTF8.GetString(resultArray)
        Catch
            Return ""
        End Try
    End Function
    Public Shared Function NullToString(ByVal obj As [Object]) As String
        Return If(obj Is Nothing, "", obj.ToString())
    End Function
    Public Shared Function NullToStringColumns(ByVal dr As DataRow, ByVal ColumnName As String) As [String]
        Return If(dr.Table.Columns.Contains(ColumnName), NullToString(dr(ColumnName)), "")
    End Function
    Public Shared Function NullToString(ByVal obj As [Object], ByVal replaceString As String) As String
        Return If(obj Is Nothing, replaceString, obj.ToString())
    End Function
    Public Shared Function getInteger(ByVal value As Object) As Integer
        Dim retVal As Integer
        If Integer.TryParse(NullToString(value), retVal) Then
            Return retVal
        Else
            Return 0
        End If
    End Function
    Public Shared Function ResizeImage(ByVal thumbHeight As Integer, ByVal srcPath As String) As String
        Dim strExtension As String = Path.GetExtension(srcPath).ToLower()
        Dim savePath As String = srcPath.Replace(strExtension, "_thmb" & strExtension)

        Using image As System.Drawing.Image = System.Drawing.Image.FromFile(srcPath)
            Dim srcWidth As Integer = image.Width
            Dim srcHeight As Integer = image.Height
            Dim thumbWidth As Integer = CInt(Math.Truncate((CDbl(thumbHeight) * CDbl(srcWidth)) / CDbl(srcHeight)))

            Using bmp As New Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppArgb)
                bmp.SetResolution(72, 72)

                Dim gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)

                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                gr.CompositingMode = CompositingMode.SourceCopy

                Dim rectDestination As New System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight)

                gr.DrawImage(image, rectDestination)


                bmp.Save(savePath, image.RawFormat)
            End Using
        End Using
        Return savePath
    End Function
    Public Shared Function ResizeImageUsingWidth(ByVal thumbWidth As Integer, ByVal srcPath As String) As String
        Dim strExtension As String = Path.GetExtension(srcPath).ToLower()
        Dim savePath As String = srcPath.Replace(strExtension, "_thmb" & strExtension)

        Using image As System.Drawing.Image = System.Drawing.Image.FromFile(srcPath)
            Dim srcWidth As Integer = image.Width
            Dim srcHeight As Integer = image.Height
            Dim thumbHeight As Integer = CInt(Math.Truncate((CDbl(srcHeight) * CDbl(thumbWidth)) / CDbl(srcWidth)))

            Using bmp As New Bitmap(thumbWidth, thumbHeight, PixelFormat.Format32bppArgb)
                bmp.SetResolution(72, 72)

                Dim gr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)

                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                gr.CompositingMode = CompositingMode.SourceCopy

                Dim rectDestination As New System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight)

                gr.DrawImage(image, rectDestination)


                bmp.Save(savePath, image.RawFormat)
            End Using
        End Using
        Return savePath
    End Function
    Public Shared Sub GetDifference(ByVal date1 As DateTime, ByVal date2 As DateTime, ByRef Years As Integer, ByRef Months As Integer)
        'assumes date2 is the bigger date for simplicity

        'years
        Dim diff As TimeSpan = date2 - date1
        Years = diff.Days \ 366
        Dim workingDate As DateTime = date1.AddYears(Years)

        While workingDate.AddYears(1) <= date2
            workingDate = workingDate.AddYears(1)
            Years += 1
        End While

        'months
        diff = date2 - workingDate
        Months = diff.Days \ 31
        workingDate = workingDate.AddMonths(Months)

        While workingDate.AddMonths(1) <= date2
            workingDate = workingDate.AddMonths(1)
            Months += 1
        End While

        'weeks and days
        diff = date2 - workingDate
        'Weeks = diff.Days / 7; //weeks always have 7 days
        'Days = diff.Days % 7;
    End Sub

    Shared ReadOnly PasswordHash As String = "P@@Sw0rd"
    Shared ReadOnly SaltKey As String = "S@LT&KEY"
    Shared ReadOnly VIKey As String = "@1B2c3D4e5F6g7H8"
    Public Shared ReadOnly ProductSort As String = "10"
    Public Const SettingUsername As String = "BookingEasy"
    Public Const SettingPassword As String = "Bo0kInG$2001"

    Public Shared Function EncryptString(ByVal plainText As String) As String
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        Dim keyBytes As Byte() = New Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 \ 8)
        Dim symmetricKey = New RijndaelManaged() With { _
            .Mode = CipherMode.CBC, _
            .Padding = PaddingMode.Zeros _
        }
        Dim encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey))

        Dim cipherTextBytes As Byte()

        Using memoryStream = New MemoryStream()
            Using cryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
                cryptoStream.FlushFinalBlock()
                cipherTextBytes = memoryStream.ToArray()
                cryptoStream.Close()
            End Using
            memoryStream.Close()
        End Using
        Return Convert.ToBase64String(cipherTextBytes)
    End Function

End Class
