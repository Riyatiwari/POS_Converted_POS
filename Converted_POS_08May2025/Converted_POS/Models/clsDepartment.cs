using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS.Models
{
    public class clsDepartment
    {
        public int? Department_id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Value { get; set; }
        public bool Is_Active { get; set; }
        public int? Cmp_id { get; set; }
        public string Ip_Address { get; set; }
        public int? Login_id { get; set; }
        public int? Department_category_id { get; set; }
        public string department_category_name { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modify_date { get; set; }
        public string Tran_Type { get; set; }
        public int? cmp_id { get; set; }
        public bool IsDepartmentInserted { get; set; } = true;
    }

    //public class clsDepartment_DAL
    //{
        //public static IConfiguration _configuration;
        //string connectionString = _configuration.GetConnectionString("POS_Connection");

        //public List<clsDepartment> bindDptCat(int? c_id, string conn)
        //{
        //    List<clsDepartment> dept = new List<clsDepartment>();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Deparment_Category", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            dept.Add(new clsDepartment
        //            {
        //                department_category_name = rdr["department_category_name"].ToString(),
        //                Department_category_id = Convert.ToInt32(rdr["department_category_id"])
        //            });
        //        }
        //    }

        //    return dept;
        //}



        //To View all dept details    
        //public IEnumerable<clsDepartment> SelectAll(int? c_id, int? d_id, string conn)
        //{
        //    List<clsDepartment> list = new List<clsDepartment>();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);
        //        cmd.Parameters.AddWithValue("@Department_id", d_id);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            clsDepartment dept = new clsDepartment();

        //            dept.Department_id = Convert.ToInt32(rdr["Department_id"]);
        //            dept.Name = rdr["Name"].ToString();
        //            dept.Value = Convert.ToInt32(rdr["value"].ToString());
        //            dept.Department_category_id = Convert.ToInt32(rdr["department_category_id"]);
        //            dept.department_category_name = rdr["department_category_name"].ToString();

        //            list.Add(dept);
        //        }
        //        con.Close();
        //    }
        //    return list;
        //}


        //To View all dept details    
        //public clsDepartment Select(int? c_id, int? d_id, string conn)
        //{
        //    clsDepartment list = new clsDepartment();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);
        //        cmd.Parameters.AddWithValue("@Department_id", d_id);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            clsDepartment dept = new clsDepartment();

        //            dept.Department_id = Convert.ToInt32(rdr["Department_id"]);
        //            dept.Name = rdr["Name"].ToString();
        //            dept.Value = Convert.ToInt32(rdr["value"].ToString());
        //            dept.Department_category_id = Convert.ToInt32(rdr["department_category_id"]);
        //            dept.department_category_name = rdr["department_category_name"].ToString();
                    
        //        }
        //        con.Close();
        //    }
        //    return list;
        //}


        //To Add new record    
        //public void Insert(clsDepartment cat, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
        //        cmd.Parameters.AddWithValue("@Department_id ", cat.Department_id);
        //        cmd.Parameters.AddWithValue("@Name", cat.Name);
        //        cmd.Parameters.AddWithValue("@Value", cat.Value);
        //        cmd.Parameters.AddWithValue("@Login_id ", 0);
        //        cmd.Parameters.AddWithValue("@Ip_Address", "");
        //        cmd.Parameters.AddWithValue("@department_category_id", cat.Department_category_id);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "I");

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        //To Update record    
        //public void Update(clsDepartment cat, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
        //        cmd.Parameters.AddWithValue("@Department_id ", cat.Department_id);
        //        cmd.Parameters.AddWithValue("@Name", cat.Name);
        //        cmd.Parameters.AddWithValue("@Value", cat.Value);
        //        cmd.Parameters.AddWithValue("@Login_id ", cat.Login_id);
        //        cmd.Parameters.AddWithValue("@Ip_Address", cat.Ip_Address);
        //        cmd.Parameters.AddWithValue("@department_category_id", cat.Department_category_id);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "U");


        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        //To Delete record    
        //public void Delete(clsDepartment cat, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Department", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", cat.cmp_id);
        //        cmd.Parameters.AddWithValue("@Department_id ", cat.Department_id);
        //        cmd.Parameters.AddWithValue("@Name", cat.Name);
        //        cmd.Parameters.AddWithValue("@Value", cat.Value);
        //        cmd.Parameters.AddWithValue("@Login_id ", cat.Login_id);
        //        cmd.Parameters.AddWithValue("@Ip_Address", cat.Ip_Address);
        //        cmd.Parameters.AddWithValue("@department_category_id", cat.Department_category_id);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "D");


        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}


    //}
}
