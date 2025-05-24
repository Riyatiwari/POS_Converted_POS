using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class CourseController : Controller
    {
        public IEnumerable<clsCourse> SelectAll(int? c_id, string conn)
        {
            List<clsCourse> list = new List<clsCourse>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsCourse course = new clsCourse();

                    course.Course_id = Convert.ToInt32(rdr["course_id"]);
                    course.Name = rdr["name"].ToString();
                    course.courses_category_id = Convert.ToInt32(rdr["course_category_id"]);
                    course.courses_category_name = rdr["cname"].ToString();
                    course.Value = Convert.ToInt32(rdr["value"]);
                    list.Add(course);
                }
                con.Close();
            }
                return list;
        }

        public List<SelectListItem> GetCourseCategory(int? c_id, string conn, int course_category_id)
        {
            List<SelectListItem> CourseCategory = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Course_Category", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                //cmd.Parameters.AddWithValue("@course_category_id", course_category_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CourseCategory.Add(new SelectListItem
                    {
                        Value = rdr["course_category_id"].ToString(),
                        Text = rdr["Name"].ToString()
                    });
                }

                con.Close();
            }

            return CourseCategory;
        }

        public clsCourse Select(int? c_id, int? id, string conn, int? courses_category_id)
        {
            clsCourse course = new clsCourse();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@Course_id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    course.Course_id = Convert.ToInt32(rdr["course_id"]);
                    course.Name = rdr["name"].ToString();
                    course.courses_category_id = Convert.ToInt32(rdr["course_category_id"]);
                    course.courses_category_name = rdr["cname"].ToString();
                    course.Value = Convert.ToInt32(rdr["value"]);

                    course.is_CheckSlot = Convert.ToBoolean(rdr["is_CheckSlot"]);
                }
                con.Close();
            }
            return course;
        }

        public void Insert(clsCourse course, string connStr, string ip_address, int? courses_category_id)
        {
            course.Ip_Address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Course_id", course.Course_id);
                cmd.Parameters.AddWithValue("@Name ", course.Name);
                cmd.Parameters.AddWithValue("@Value", course.Value);
                cmd.Parameters.AddWithValue("@Course_Category_id", courses_category_id ?? (object)DBNull.Value); // Handle null case
                cmd.Parameters.AddWithValue("@cmp_id", course.Cmp_id);
                cmd.Parameters.AddWithValue("@Ip_Address", course.Ip_Address);
                cmd.Parameters.AddWithValue("@Login_id", course.Login_id);
                cmd.Parameters.AddWithValue("@is_checkslot", course.is_CheckSlot);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsCourse course, string connStr, string ip_address, int? courses_category_id)
        {
            course.Ip_Address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Course_id", course.Course_id);
                cmd.Parameters.AddWithValue("@Name ", course.Name);
                cmd.Parameters.AddWithValue("@Value", course.Value);
                cmd.Parameters.AddWithValue("@Course_Category_id", courses_category_id ?? (object)DBNull.Value); // Handle null case
                cmd.Parameters.AddWithValue("@cmp_id", course.Cmp_id);
                cmd.Parameters.AddWithValue("@Ip_Address", course.Ip_Address);
                cmd.Parameters.AddWithValue("@Login_id", course.Login_id);
                cmd.Parameters.AddWithValue("@is_checkslot", course.is_CheckSlot);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Delete(clsCourse course, string connStr, string ip_address)
        {
            course.Ip_Address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Course", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Course_id", course.Course_id);
                cmd.Parameters.AddWithValue("@Name ", course.Name);
                cmd.Parameters.AddWithValue("@Value", course.Value);
                cmd.Parameters.AddWithValue("@Course_Category_id", course.courses_category_id);
                cmd.Parameters.AddWithValue("@cmp_id", course.Cmp_id);
                cmd.Parameters.AddWithValue("@Ip_Address", course.Ip_Address);
                cmd.Parameters.AddWithValue("@Login_id", course.Login_id);
                cmd.Parameters.AddWithValue("@is_checkslot", course.is_CheckSlot);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
