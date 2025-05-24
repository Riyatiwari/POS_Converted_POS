using Converted_POS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class PrinterController
    {
        public IEnumerable<clsPrinter> SelectAll(int? c_id, string conn)
        {
            List<clsPrinter> list = new List<clsPrinter>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Printer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsPrinter printer = new clsPrinter();
                    var portValue = rdr["port"];
                    printer.printer_id = Convert.ToInt32(rdr["printer_id"]);
                    printer.name = rdr["name"].ToString();
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        printer.is_active = true;
                    }
                    else
                    {
                        printer.is_active = false;
                    }
                    printer.is_active = rdr["is_active"].ToString() == "Yes";
                    string Active = printer.is_active ? "Yes" : "No";

                    if (portValue != DBNull.Value)
                    {
                        if (int.TryParse(portValue.ToString(), out int port))
                        {
                            printer.port = port;
                        }
                        else
                        {
                            // Handle invalid format (e.g., set a default value or log the error)
                            printer.port = 0;  // Default value if the port value is not a valid integer
                        }
                    }
                    else
                    {
                        // Handle the case where the port is null or DBNull
                        printer.port = 0;  // Default value when the port is null or missing
                    }
                    printer.portValue = rdr["port"].ToString() == "Yes";
                    string portV = printer.portValue ? "Yes" : "No";
                    if (rdr["Group by"].ToString() == "Yes")
                    {
                        printer.group_by = true;
                    }
                    else
                    {
                        printer.group_by = false;
                    }

                    //if (rdr["is_condiment_small_large"].ToString() == "Yes")
                    //{
                    //    printer.is_condiment_small_large = true;
                    //}
                    //else
                    //{
                    //    printer.is_condiment_small_large = false;
                    //}
                    printer.is_product_small_large = Convert.ToByte(rdr["is_product_small_large"]);
                    printer.is_condiment_small_large = Convert.ToByte(rdr["is_condiment_small_large"]);
                    //if (rdr["is_product_small_large"].ToString() == "Yes")
                    //{
                    //    printer.is_product_small_large = true;
                    //}
                    //else
                    //{
                    //    printer.is_product_small_large = false;
                    //}
                    list.Add(printer);
                }
                con.Close();
            }
            return list;
        }

        public void Update(clsPrinter printer, string connStr, string ip_address)
        {
            printer.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Printer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", printer.cmp_id);
                cmd.Parameters.AddWithValue("@printer_id ", printer.printer_id);
                cmd.Parameters.AddWithValue("@name", printer.name);
                cmd.Parameters.AddWithValue("@Alias", printer.Alias);
                cmd.Parameters.AddWithValue("@is_active", printer.is_active);
                cmd.Parameters.AddWithValue("@login_id", printer.login_id);
                cmd.Parameters.AddWithValue("@ip_address", printer.ip_address);
                cmd.Parameters.AddWithValue("@printer_ip_address", 0);
                cmd.Parameters.AddWithValue("@port", printer.portValue);
                cmd.Parameters.AddWithValue("@network_type", 0);
                cmd.Parameters.AddWithValue("@vender_id", 0);
                cmd.Parameters.AddWithValue("@budrate", 0);
                cmd.Parameters.AddWithValue("@device_name", 0);
                cmd.Parameters.AddWithValue("@group_by_with", printer.group_by_with);
                cmd.Parameters.AddWithValue("@group_by", printer.group_by);
                cmd.Parameters.AddWithValue("@consile_date", printer.consile_date);
                cmd.Parameters.AddWithValue("@OrderFlag", printer.OrderFlag);
                cmd.Parameters.AddWithValue("@is_product_small_large", printer.is_product_small_large);
                cmd.Parameters.AddWithValue("@is_condiment_small_large", printer.is_condiment_small_large);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Insert(clsPrinter printer, string connStr, string ip_address)
        {
            Dictionary<int, string> productSizeMapping = new Dictionary<int, string>
{
    { 0, "Print Product Small" },
    { 1, "Print Product Large" },
    { 2, "Print Product Extra Large" }
};
            printer.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Printer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", printer.cmp_id);
                cmd.Parameters.AddWithValue("@printer_id ", printer.printer_id);
                cmd.Parameters.AddWithValue("@name", printer.name);
                cmd.Parameters.AddWithValue("@Alias", printer.Alias);
                cmd.Parameters.AddWithValue("@is_active", printer.is_active);
                cmd.Parameters.AddWithValue("@login_id", printer.login_id);
                cmd.Parameters.AddWithValue("@ip_address", printer.ip_address);
                cmd.Parameters.AddWithValue("@printer_ip_address", 0);
                cmd.Parameters.AddWithValue("@port", printer.portValue);
                cmd.Parameters.AddWithValue("@network_type", 0);
                cmd.Parameters.AddWithValue("@vender_id", 0);
                cmd.Parameters.AddWithValue("@budrate", 0);
                cmd.Parameters.AddWithValue("@device_name", 0);
                cmd.Parameters.AddWithValue("@group_by_with", printer.group_by_with);
                cmd.Parameters.AddWithValue("@group_by", printer.group_by);
                cmd.Parameters.AddWithValue("@consile_date", printer.consile_date);
                cmd.Parameters.AddWithValue("@OrderFlag", printer.OrderFlag);

                //byte selectedValue = printer.is_product_small_large;  // assuming it's a string, convert to int

                //// Get the display name of the selected option
                //string selectedProductName = productSizeMapping[selectedValue];

                //// You can now use the selectedProductName along with the selected value
                //cmd.Parameters.AddWithValue("@is_product_small_large", selectedValue);
                
                cmd.Parameters.AddWithValue("@is_product_small_large", printer.is_product_small_large);
                cmd.Parameters.AddWithValue("@is_condiment_small_large", printer.is_condiment_small_large);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public clsPrinter Select(int? c_id, int? id, string conn)
        {
            clsPrinter printer = new clsPrinter();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Printer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@printer_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var portValue = rdr["port"];
                    printer.printer_id = Convert.ToInt32(rdr["printer_id"]);
                    printer.name = rdr["name"].ToString();
                    printer.Alias = rdr["Alias"].ToString();
                    if (portValue != DBNull.Value)
                    {
                        // Try to parse the value
                        if (int.TryParse(portValue.ToString(), out int port))
                        {
                            printer.port = port;
                        }
                        else
                        {
                            // Handle invalid format (e.g., set a default value or log the error)
                            printer.port = 0;  // Default value if the port value is not a valid integer
                        }
                    }
                    else
                    {
                        // Handle the case where the port is null or DBNull
                        printer.port = 0;  // Default value when the port is null or missing
                    }
                    printer.portValue = rdr["port"].ToString() == "Yes";
                    string portV = printer.portValue ? "Yes" : "No";
                    if (rdr["Group by"].ToString() == "Yes")
                    {
                        printer.group_by = true;
                    }
                    else
                    {
                        printer.group_by = false;
                    }

                    if (rdr["consile date"].ToString() == "Yes")
                    {
                        printer.consile_date = true;
                    }
                    else
                    {
                        printer.consile_date = false;
                    }
                    if (rdr["OrderFlag"].ToString() == "Yes")
                    {
                        printer.OrderFlag = true;
                    }
                    else
                    {
                        printer.OrderFlag = false;
                    }
                    if (rdr["is_active"].ToString() == "Yes")
                    {
                        printer.is_active = true;
                    }
                    else
                    {
                        printer.is_active = false;
                    }

                    //if (rdr["is_product_small_large"].ToString() == "Yes")
                    //{
                    //    printer.is_product_small_large = true;
                    //}
                    //else
                    //{
                    //    printer.is_product_small_large = false;
                    //}
                    printer.is_product_small_large = Convert.ToByte(rdr["is_product_small_large"].ToString());
                    printer.is_condiment_small_large = Convert.ToByte(rdr["is_condiment_small_large"].ToString());
                    //if (rdr["is_condiment_small_large"].ToString() == "Yes")
                    //{
                    //    printer.is_condiment_small_large = true;
                    //}
                    //else
                    //{
                    //    printer.is_condiment_small_large = false;
                    //}
                }
                con.Close();
            }
            return printer;
        }

        public void Delete(clsPrinter printer, string connStr, string ip_address)
        {
            printer.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Printer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", printer.cmp_id);
                cmd.Parameters.AddWithValue("@printer_id ", printer.printer_id);
                cmd.Parameters.AddWithValue("@name", printer.name);
                cmd.Parameters.AddWithValue("@Alias", printer.Alias);
                cmd.Parameters.AddWithValue("@is_active", printer.is_active);
                cmd.Parameters.AddWithValue("@login_id", printer.login_id);
                cmd.Parameters.AddWithValue("@ip_address", printer.ip_address);
                cmd.Parameters.AddWithValue("@printer_ip_address", 0);
                cmd.Parameters.AddWithValue("@port", printer.portValue);
                cmd.Parameters.AddWithValue("@network_type", 0);
                cmd.Parameters.AddWithValue("@vender_id", 0);
                cmd.Parameters.AddWithValue("@budrate", 0);
                cmd.Parameters.AddWithValue("@device_name", 0);
                cmd.Parameters.AddWithValue("@group_by_with", printer.group_by_with);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
