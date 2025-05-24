
using Converted_POS.Models;
using Converted_POS.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using ZXing;
using System.Diagnostics;

namespace Converted_POS
{
    public class QRController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");

        [Route("QR/QR_List")]
        public IActionResult QR_List(string qrText)
        {
            if (string.IsNullOrEmpty(qrText))
            {
                ViewBag.ErrorMessage = "Please enter text to generate QR Code.";
                return View();
            }

            // Generate QR code as byte array
            var qrCodeBytes = GenerateQRCodeFromText(qrText); // Use the new method name

            return View("QR_List", qrCodeBytes);
        }
        public IActionResult QR_List()
        {
            var model = new QR1_ListModel();

            // Initialize QR with an empty list to avoid null reference errors
            model.QR = new List<clsQR>(); // Or populate this list with actual data from a database

            // Example: adding a mock QR data for testing purposes
            model.QR.Add(new clsQR
            {
                machine_name = "Machine 1",
                TillUUID = "UUID123",
                location_name = "Location 1",
                venue_name = "Venue 1",
                QRCodeBytes = new byte[] { } // You can populate this with actual QR code byte data
            });

            return View(model);
        }

        public byte[] GenerateQRCodeFromText(string qrText)
        {
            var qrGenerator = new QRCoder.QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20); // Adjust the size here (20)

            //return BitmapToBytes(qrCodeImage);
        }

        // Helper method to convert Bitmap to byte array
        private static byte[] BitmapToBytes(System.Drawing.Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }


        public IActionResult OnGet()
        {
            var model = new QR1_ListModel();
            model.QR = model.QR ?? new List<clsQR>();  // Ensure QR is never null
            return View(model);
        }

        public List<clsQR> SelectAll(int? c_id, string conn, string tillUUID = null, string machineName = null, string locationName = null, string venueName = null)
        {
            List<clsQR> list = new List<clsQR>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("GetMachineList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsQR QR = new clsQR();
                    QR.machine_id = Convert.ToInt32(rdr["machine_id"]);
                    QR.machine_name = rdr["machine_name"].ToString();
                    QR.TillUUID = rdr["TillUUID"].ToString();
                    QR.location_id = Convert.ToInt32(rdr["location_id"]);
                    QR.location_name = rdr["location_name"].ToString();
                    QR.venue_name = rdr["Venue_Name"].ToString();
                    QR.venue_id = Convert.ToInt32(rdr["venue_id"]);
                    //QR.QRCodeUrl = GenerateQRCodeBase64(QR.TillUUID);

                    list.Add(QR);
                }
                con.Close();
            }
            return list;
        }

        private Bitmap GenerateQRCodeImage(string tillUUID)
        {
            var writer = new ZXing.BarcodeWriter<Bitmap>
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 250,
                    Height = 250,
                    Margin = 1
                }
            };

            try
            {
                return writer.Write(tillUUID);  // Returns a Bitmap object
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating QR code: {ex.Message}");
                return null;
            }
        }

        private string ConvertQRCodeToBase64(Bitmap qrCodeImage)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                qrCodeImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Save as PNG format
                byte[] imageBytes = memoryStream.ToArray(); // Get byte array from MemoryStream
                return Convert.ToBase64String(imageBytes); // Convert byte array to Base64 string
            }
        }

        private Bitmap GenerateQRCode(string tillUUID)
        {
            Debug.WriteLine("tilluuid fron qr generator fun controller : " + tillUUID);
            var writer = new BarcodeWriter<Bitmap>
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 150,
                    Height = 150
                }
            };

            try
            {
                return writer.Write(tillUUID);
            }
            catch (Exception ex)
            {
                // Log the error here (use your logging framework)
                Console.WriteLine($"Error generating QR code: {ex.Message}");
                return null;
            }
        }

    }
}
