using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class DepartmentCategoryController : Controller
    {
        public IEnumerable<clsDepartmentCategory> SelectAll(int? c_id, string conn)
        {
            List<clsDepartmentCategory> list = new List<clsDepartmentCategory>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Deparment_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsDepartmentCategory Dept_cat = new clsDepartmentCategory();

                    Dept_cat.department_category_id = Convert.ToInt32(rdr["department_category_id"]);
                    Dept_cat.department_category_name = rdr["department_category_name"].ToString();
                    Dept_cat.active = rdr["Active"].ToString();

                    if (rdr["Active"].ToString() == "Yes")
                    {
                        Dept_cat.is_active = true;
                    }
                    else
                    {
                        Dept_cat.is_active = false;
                    }

                    list.Add(Dept_cat);
                }
                con.Close();
            }
            return list;
        }
        public clsDepartmentCategory Select(int? c_id, int? id, string conn)
        {
            clsDepartmentCategory Dept_cat = new clsDepartmentCategory();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Deparment_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@department_category_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Dept_cat.department_category_id = Convert.ToInt32(rdr["department_category_id"]);
                    Dept_cat.department_category_name = rdr["department_category_name"].ToString();
                    Dept_cat.AcCode = Convert.ToDecimal(rdr["AcCode"].ToString());
                    if (rdr["is_active"].ToString() == "1")
                    {
                        Dept_cat.is_active = true;
                    }
                    else
                    {
                        Dept_cat.is_active = false;
                    }
                }

                con.Close();
            }
            return Dept_cat;
        }
        public void Insert(clsDepartmentCategory Dept_cat, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("M_Dep_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", Dept_cat.cmp_id);
                cmd.Parameters.AddWithValue("@department_category_id ", Dept_cat.department_category_id);
                cmd.Parameters.AddWithValue("@department_category_name", Dept_cat.department_category_name);
                cmd.Parameters.AddWithValue("@AcCode", Dept_cat.AcCode);
                cmd.Parameters.AddWithValue("@is_active", Dept_cat.is_active);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsDepartmentCategory Dept_cat, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("M_Dep_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", Dept_cat.cmp_id);
                cmd.Parameters.AddWithValue("@department_category_id ", Dept_cat.department_category_id);
                cmd.Parameters.AddWithValue("@department_category_name", Dept_cat.department_category_name);
                cmd.Parameters.AddWithValue("@AcCode", Dept_cat.AcCode);
                cmd.Parameters.AddWithValue("@is_active", Dept_cat.is_active);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Delete(clsDepartmentCategory Dept_cat, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("M_Dep_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", Dept_cat.cmp_id);
                cmd.Parameters.AddWithValue("@department_category_id ", Dept_cat.department_category_id);
                cmd.Parameters.AddWithValue("@department_category_name", Dept_cat.department_category_name);
                cmd.Parameters.AddWithValue("@AcCode", Dept_cat.AcCode);
                cmd.Parameters.AddWithValue("@is_active", Dept_cat.is_active);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
