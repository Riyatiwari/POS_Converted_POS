using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    [Route("api/[controller]")]
    public class TaxController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");
        public IEnumerable<clsTax> SelectAll(int? c_id, string conn)
        {
            List<clsTax> list = new List<clsTax>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Tax", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsTax tax = new clsTax();

                    tax.Tax_id = Convert.ToInt32(rdr["Tax_id"]);
                    tax.Name = rdr["name"].ToString();
                    tax.Mode = rdr["mode"].ToString();
                    tax.Value = Convert.ToInt32(rdr["value"]);
                    if (rdr["Effective_Date"] != DBNull.Value)
                    {
                        string dateString = rdr["Effective_Date"].ToString();

                        // Specify the exact date format (DD/MM/YYYY)
                        string[] dateFormats = { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy" }; // Handle different formats

                        if (DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime effectiveDate))
                        {
                            // Strip off the time part using `.Date` to keep only the date
                            tax.Effective_Date = effectiveDate.Date; // This keeps only the date part (no time)
                        }
                        else
                        {
                            // If parsing fails, set it to a default value (e.g., DateTime.MinValue)
                            tax.Effective_Date = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        tax.Effective_Date = DateTime.MinValue; // Or any default value you prefer
                    }
                    tax.Is_active = rdr["is_active"].ToString() == "Yes";
                    string Active = tax.Is_active ? "Yes" : "No";
                    list.Add(tax);
                }
                con.Close();
            }
            return list;
        }

        public void Delete(clsTax tax, string connStr, string ip_address)
        {
            tax.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Tax", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", tax.cmp_id);
                cmd.Parameters.AddWithValue("@Tax_id ", tax.Tax_id);
                cmd.Parameters.AddWithValue("@name", tax.Name);
                cmd.Parameters.AddWithValue("@mode", tax.Mode);
                cmd.Parameters.AddWithValue("@value", tax.Value);
                cmd.Parameters.AddWithValue("@is_active", tax.Is_active);
                cmd.Parameters.AddWithValue("@login_id", tax.Login_id);
                cmd.Parameters.AddWithValue("@machine_id", tax.machine_id);
                cmd.Parameters.AddWithValue("@ip_address", tax.ip_address);
                cmd.Parameters.AddWithValue("@Effective_Date", tax.Effective_Date);

                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsTax tax, string connStr, string ip_address)
        {
            tax.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Tax", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", tax.cmp_id);
                cmd.Parameters.AddWithValue("@Tax_id ", tax.Tax_id);
                cmd.Parameters.AddWithValue("@name", tax.Name);
                cmd.Parameters.AddWithValue("@mode", tax.Mode);
                cmd.Parameters.AddWithValue("@value", tax.Value);
                cmd.Parameters.AddWithValue("@is_active", tax.Is_active);
                cmd.Parameters.AddWithValue("@login_id", tax.Login_id);
                cmd.Parameters.AddWithValue("@machine_id", tax.machine_id);
                cmd.Parameters.AddWithValue("@ip_address", tax.ip_address);
                cmd.Parameters.AddWithValue("@Effective_Date", tax.Effective_Date);

                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Insert(clsTax tax, string connStr, string ip_address)
        {
            tax.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Tax", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", tax.cmp_id);
                cmd.Parameters.AddWithValue("@Tax_id ", tax.Tax_id);
                cmd.Parameters.AddWithValue("@name", tax.Name);
                cmd.Parameters.AddWithValue("@mode", tax.Mode);
                cmd.Parameters.AddWithValue("@value", tax.Value);
                cmd.Parameters.AddWithValue("@is_active", tax.Is_active);
                cmd.Parameters.AddWithValue("@login_id", tax.Login_id);
                cmd.Parameters.AddWithValue("@machine_id", tax.machine_id);
                cmd.Parameters.AddWithValue("@ip_address", tax.ip_address);
                cmd.Parameters.AddWithValue("@Effective_Date", tax.Effective_Date);
               
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public clsTax Select(int? c_id, int? id, string conn)
        {
            clsTax tax = new clsTax();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Tax", con);
                cmd.CommandType = CommandType.StoredProcedure;

              
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tax.Tax_id = Convert.ToInt32(rdr["tax_id"]);
                    tax.Name = rdr["name"].ToString();
                    tax.Mode = rdr["mode"].ToString();
                    tax.Value = Convert.ToInt32(rdr["value"]);
                    //tax.Effective_Date = Convert.ToDateTime(rdr["Effective_Date"]);
                    string dateString = rdr["Effective_Date"].ToString();

                    // Specify the exact date format (DD/MM/YYYY)
                    string[] dateFormats = { "dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy" }; // Handle different formats

                    if (DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime effectiveDate))
                    {
                        // Strip off the time part using `.Date` to keep only the date
                        tax.Effective_Date = effectiveDate.Date; // This keeps only the date part (no time)
                    }
                    else
                    {
                        // If parsing fails, set it to a default value (e.g., DateTime.MinValue)
                        tax.Effective_Date = DateTime.MinValue;
                    }
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        tax.Is_active = true;
                    }
                    else
                    {
                        tax.Is_active = false;
                    }
                }
                con.Close();
            }
            return tax;
        }
    }
}
