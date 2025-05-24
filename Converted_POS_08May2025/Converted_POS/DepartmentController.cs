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
    public class DepartmentController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");

        public List<clsDepartment> bindDptCat(int? c_id, string conn)
        {
            List<clsDepartment> dept = new List<clsDepartment>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Deparment_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dept.Add(new clsDepartment
                    {
                        department_category_name = rdr["department_category_name"].ToString(),
                        Department_category_id = Convert.ToInt32(rdr["department_category_id"])
                    });
                }
            }

            return dept;
        }

        public IEnumerable<clsDepartment> SelectAll(int? c_id, int? d_id, string conn)
        {
            List<clsDepartment> list = new List<clsDepartment>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@Department_id", d_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsDepartment dept = new clsDepartment();

                    dept.Department_id = Convert.ToInt32(rdr["Department_id"]);
                    dept.Name = rdr["Name"].ToString();
                    dept.Value = rdr.IsDBNull(rdr.GetOrdinal("value")) ? 0 : Convert.ToInt32(rdr["value"]);
                    dept.Department_category_id = Convert.ToInt32(rdr["department_category_id"]);
                    dept.department_category_name = rdr["department_category_name"].ToString();

                    list.Add(dept);
                }
                con.Close();
            }
            return list;
        }


        public clsDepartment Select(int? c_id, int? d_id, string conn)
        {
            clsDepartment list = new clsDepartment();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@Department_id", d_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    list.Department_id = Convert.ToInt32(rdr["Department_id"]);
                    list.Name = rdr["Name"].ToString();
                    list.Value = Convert.ToInt32(rdr["value"].ToString());
                    list.Department_category_id = Convert.ToInt32(rdr["department_category_id"]);
                    list.department_category_name = rdr["department_category_name"].ToString();

                }
                con.Close();
            }
            return list;
        }
        public bool Insert(clsDepartment cat, string conn)
        {


            
          
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM M_departments WHERE Name = @Name", con);
                checkCmd.Parameters.AddWithValue("@Name", cat.Name);

                int categoryExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (categoryExists > 0)
                {
                    return false;
                }

                SqlCommand cmd = new SqlCommand("P_M_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
                cmd.Parameters.AddWithValue("@Department_id ", cat.Department_id ?? 0);
                cmd.Parameters.AddWithValue("@Name", cat.Name);
                cmd.Parameters.AddWithValue("@Value", cat.Value);
                cmd.Parameters.AddWithValue("@Login_id ", 0);
                cmd.Parameters.AddWithValue("@Ip_Address", "");
                cmd.Parameters.AddWithValue("@department_category_id", cat.Department_category_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public bool Update(clsDepartment cat, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
                cmd.Parameters.AddWithValue("@Department_id ", cat.Department_id);
                cmd.Parameters.AddWithValue("@Name", cat.Name);
                cmd.Parameters.AddWithValue("@Value", cat.Value);
                cmd.Parameters.AddWithValue("@Login_id ", cat.Login_id??0);
                cmd.Parameters.AddWithValue("@Ip_Address", cat.Ip_Address??"");
                cmd.Parameters.AddWithValue("@department_category_id", cat.Department_category_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public void Delete(clsDepartment dept, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", dept.Cmp_id);
                cmd.Parameters.AddWithValue("@Department_id ", dept.Department_id);
                cmd.Parameters.AddWithValue("@Name", dept.Name);
                cmd.Parameters.AddWithValue("@Value", dept.Value);
                cmd.Parameters.AddWithValue("@Login_id ", 0);
                cmd.Parameters.AddWithValue("@Ip_Address", "");
                cmd.Parameters.AddWithValue("@department_category_id", dept.Department_category_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
