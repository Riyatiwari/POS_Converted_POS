using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class Device_TypeController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");

        public void Insert(clsDevice_Type DType, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", DType.cmp_id);
                cmd.Parameters.AddWithValue("@Device_Type_id ", DType.Device_Type_id);
                cmd.Parameters.AddWithValue("@Device_Type", DType.Device_Type);
                cmd.Parameters.AddWithValue("@is_active", DType.is_active);
                cmd.Parameters.AddWithValue("@ip_address", "::1");
                cmd.Parameters.AddWithValue("@login_id", 0);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsDevice_Type DType, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", DType.cmp_id);
                cmd.Parameters.AddWithValue("@Device_Type_id ", DType.Device_Type_id);
                cmd.Parameters.AddWithValue("@Device_Type", DType.Device_Type);
                cmd.Parameters.AddWithValue("@is_active", DType.is_active);
                cmd.Parameters.AddWithValue("@ip_address", "::1");
                cmd.Parameters.AddWithValue("@login_id", 0);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Delete(clsDevice_Type DType, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Device_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", DType.cmp_id);
                cmd.Parameters.AddWithValue("@Device_Type_id ", DType.Device_Type_id);
                cmd.Parameters.AddWithValue("@Device_Type", DType.Device_Type);
                cmd.Parameters.AddWithValue("@is_active", DType.is_active);
                cmd.Parameters.AddWithValue("@ip_address", "::1");
                cmd.Parameters.AddWithValue("@login_id", 0);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public IEnumerable<clsDevice_Type> SelectAll(int? c_id, string conn)
        {
            List<clsDevice_Type> list = new List<clsDevice_Type>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Device_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string active = "";
                    clsDevice_Type DType = new clsDevice_Type();

                    DType.Device_Type_id = Convert.ToInt32(rdr["Device_Type_id"]);
                    DType.Device_Type = rdr["Device_Type"].ToString();
                    active = rdr["Active"].ToString();

                    if (active == "Yes")
                    {
                        DType.is_active = true;
                    }
                    else
                    {
                        DType.is_active = false;
                    }

                    list.Add(DType);
                }
                con.Close();
            }
            return list;
        }

        public clsDevice_Type Select(int? c_id, int? id, string conn)
        {
            clsDevice_Type device = new clsDevice_Type();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Device_Type", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Device_Type_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string active = "";
                    device.Device_Type_id = Convert.ToInt32(rdr["Device_Type_id"]);
                    device.Device_Type = rdr["Device_Type"].ToString();
                    active = rdr["Active"].ToString();

                    if (active == "Yes")
                    {
                        device.is_active = true;
                    }
                    else
                    {
                        device.is_active = false;
                    }

                }
                con.Close();
            }
            return device;
        }
    }
}
