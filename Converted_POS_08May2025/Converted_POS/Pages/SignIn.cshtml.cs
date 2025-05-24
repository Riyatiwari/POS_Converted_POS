using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Converted_POS.Pages
{
    public class SignInModel : PageModel
    {
        public static IConfiguration _configuration;
        public SignInModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public clsSignIn signIn { get; set; }

        public void OnGet()
        {

        }

        [HttpPost("Login")]
        public ActionResult OnPost(string UserName, string Password)
        {

            HttpContext.Session.Clear();
            string connectionString = _configuration.GetConnectionString("POS_ControllerConnection");

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Get_DB_Connection_UserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserName", signIn.UserName);
           
                cmd.Parameters.AddWithValue("@Password", Encrypt(signIn.Password));

                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("db_server", dt.Rows[0]["db_server"].ToString());
                    HttpContext.Session.SetString("StoreId", dt.Rows[0]["Store_id"].ToString());
                    HttpContext.Session.SetString("user_name", dt.Rows[0]["db_username"].ToString());
                    HttpContext.Session.SetString("password", Decode_data(dt.Rows[0]["db_password"].ToString()));
                    HttpContext.Session.SetString("db_name", dt.Rows[0]["db_name"].ToString());
                    HttpContext.Session.SetString("conStoreuuid", dt.Rows[0]["store_uuid"].ToString());
                    
                    
                }
                else
                {
                    return Page();
                }
            }

            string db_ser = HttpContext.Session.GetString("db_server").ToString();
            string db_name = HttpContext.Session.GetString("db_name").ToString();
            string db_psw = HttpContext.Session.GetString("password").ToString();
            string db_uName = HttpContext.Session.GetString("user_name").ToString();

            string conString = "Data Source=" + db_ser + ";Initial Catalog=" + db_name + ";User ID=" + db_uName + ";Password=" + db_psw + ";";


            HttpContext.Session.SetString("conString", conString.ToString());

            using (SqlConnection con1 = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("Get_Login", con1);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", signIn.UserName);
              //  cmd.Parameters.AddWithValue("@Password", Encrypt(signIn.Password));

                con1.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                cmd.ExecuteNonQuery();
                con1.Close();
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("cmp_id", dt.Rows[0]["Company_id"].ToString());
                    HttpContext.Session.SetString("cmp_name", dt.Rows[0]["Name"].ToString());
                    HttpContext.Session.SetString("Login_id", dt.Rows[0]["Login_id"].ToString());
                    HttpContext.Session.SetString("staff_role_id", dt.Rows[0]["Role_id"].ToString());
                    //HttpContext.Session.SetString("store", signIn.StoreName);
                    HttpContext.Session.SetString("show_chips", dt.Rows[0]["Show_chips"].ToString());
                    HttpContext.Session.SetString("IsAddTax2", dt.Rows[0]["IsAddTax2"].ToString());
                    HttpContext.Session.SetString("IsExclusiveTax", dt.Rows[0]["IsExclusiveTax"].ToString());
                    HttpContext.Session.SetString("StoreUUID", dt.Rows[0]["Store_uuid"].ToString());
                    HttpContext.Session.SetString("Storename", dt.Rows[0]["C_Name"].ToString());
                    HttpContext.Session.SetString("Username", dt.Rows[0]["email"].ToString());
                    //getList();

                    return RedirectToPage("/Dashboard");

                }
                else
                {
                    return Page();
                }

            }
        }

        private void getList()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(HttpContext.Session.GetString("conString").ToString()))
                {
                    List<clsMenuList> list = new List<clsMenuList>();

                    SqlCommand cmd = new SqlCommand("P_Load_Form", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Role_Id", Convert.ToInt32(HttpContext.Session.GetString("staff_role_id")));
                    cmd.Parameters.AddWithValue("@Type", 0);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        clsMenuList menu = new clsMenuList();

                        menu.formlist = rdr["Form_Name"].ToString();

                        list.Add(menu);
                    }
                    
                    ViewData["MenuList"] = list;

                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable DT = new DataTable();
                    //da.Fill(DT);
                    //cmd.ExecuteNonQuery();

                    //if (DT.Rows.Count > 0)
                    //{
                    //    ViewData["MenuList"] = DT;
                    //}
                    //else
                    //{
                    //    ViewData["MenuList"] = "";
                    //}


                    con.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decode_data(string v)
        {
            try
            {
                return Encoding.UTF8.GetString(System.Convert.FromBase64String(v));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}