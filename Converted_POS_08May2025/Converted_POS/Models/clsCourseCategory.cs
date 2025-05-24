using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
 
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsCourseCategory
    {
        public int? course_category_id { get; set; }
        public int cmp_id { get; set; }
        public bool is_active { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modify_date { get; set; }
        public String Tran_Type { get; set; }
        [Required]
        public string name { get; set; }
        public String active { get; set; }

    }

    //public class clsCourseCategory_DAL
    //{
    //To View all  details    
    //public IEnumerable<clsCourseCategory> SelectAll(int? c_id, string conn)
    //{
    //    List<clsCourseCategory> list = new List<clsCourseCategory>();

    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_M_Course_Category", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@cmp_id", c_id);

    //        con.Open();
    //        SqlDataReader rdr = cmd.ExecuteReader();

    //        while (rdr.Read())
    //        {
    //            clsCourseCategory cat = new clsCourseCategory();

    //            cat.course_category_id = Convert.ToInt32(rdr["course_category_id"]);
    //            cat.name = rdr["Name"].ToString();
    //            cat.active = rdr["Active"].ToString();

    //            if (rdr["Active"].ToString() == "Yes")
    //            {
    //                cat.is_active = true;
    //            }
    //            else
    //            {
    //                cat.is_active = false;
    //            }

    //            list.Add(cat);
    //        }
    //        con.Close();
    //    }
    //    return list;
    //}

    //Get the details of a particular Category  
    //public clsCourseCategory Select(int? c_id, int? id, string conn)
    //{
    //    clsCourseCategory cat = new clsCourseCategory();

    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_M_Course_CategoryInfo", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@course_category_id", id);
    //        cmd.Parameters.AddWithValue("@cmp_id", c_id);

    //        con.Open();
    //        SqlDataReader rdr = cmd.ExecuteReader();

    //        while (rdr.Read())
    //        {
    //            cat.course_category_id = Convert.ToInt32(rdr["course_category_id"]);
    //            cat.name = rdr["Name"].ToString();
    //            cat.active = rdr["is_active"].ToString();

    //            if (rdr["is_active"].ToString() == "1")
    //            {
    //                cat.is_active = true;
    //            }
    //            else
    //            {
    //                cat.is_active = false;
    //            }
    //        }

    //        con.Close();
    //    }
    //    return cat;
    //}

    //To Add new Category record    
    //public void Insert(clsCourseCategory cat, string conn)
    //{
    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        SqlCommand cmd = new SqlCommand("P_M_Course_Category", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
    //        cmd.Parameters.AddWithValue("@course_category_id ", cat.course_category_id);
    //        cmd.Parameters.AddWithValue("@name", cat.name);
    //        cmd.Parameters.AddWithValue("@is_active", cat.is_active);
    //        cmd.Parameters.AddWithValue("@Tran_Type", "I");

    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //}

    //To Update Category record    
    //public void Update(clsCourseCategory cat, string conn)
    //{
    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        SqlCommand cmd = new SqlCommand("P_M_Course_Category", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
    //        cmd.Parameters.AddWithValue("@course_category_id ", cat.course_category_id);
    //        cmd.Parameters.AddWithValue("@name", cat.name);
    //        cmd.Parameters.AddWithValue("@is_active", cat.is_active);
    //        cmd.Parameters.AddWithValue("@Tran_Type", "U");

    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //}

    //To Delete Category record    
    //public void Delete(clsCourseCategory cat, string conn)
    //{
    //    using (SqlConnection con = new SqlConnection(conn))
    //    {
    //        SqlCommand cmd = new SqlCommand("P_M_Course_Category", con);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
    //        cmd.Parameters.AddWithValue("@course_category_id ", cat.course_category_id);
    //        cmd.Parameters.AddWithValue("@name", cat.name);
    //        cmd.Parameters.AddWithValue("@is_active", cat.is_active);
    //        cmd.Parameters.AddWithValue("@Tran_Type", "D");

    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //}

    //}

}
