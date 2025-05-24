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
    public class clsAllergy
    {
        
        public int? allergy_id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }
        
        public string description { get; set; }
       
        public DateTime? created_date { get; set; }
       
        public DateTime? modify_date { get; set; }
       
        public int? cmp_id { get; set; }
       
        public Byte[] Aimage { get; set; }
       
        public string Tran_Type { get; set; }
        public string ImageFileName { get; set; }
    }

    //public class clsAllergy_DAL
    //{
    //    public static IConfiguration _configuration;
    //    string connectionString = _configuration.GetConnectionString("POS_Connection");

        //To View all Allergy details    
        //public IEnumerable<clsAllergy> SelectAll(int? c_id, string conn)
        //{
        //    List<clsAllergy> list = new List<clsAllergy>();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_Allergy", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            clsAllergy allergy = new clsAllergy();

        //            allergy.allergy_id = Convert.ToInt32(rdr["allergy_id"]);
        //            allergy.name = rdr["name"].ToString();
        //            allergy.description = rdr["description"].ToString();

        //            list.Add(allergy);
        //        }
        //        con.Close();
        //    }
        //    return list;
        //}

        //Get the details of a particular Allergy  
        //public clsAllergy Select(int? c_id, int? id, string conn)
        //{
        //    clsAllergy alrgy = new clsAllergy();

        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("Get_M_AllergyInfo", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@allergy_id", id);
        //        cmd.Parameters.AddWithValue("@cmp_id", c_id);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            alrgy.allergy_id = Convert.ToInt32(rdr["allergy_id"]);
        //            alrgy.name = rdr["name"].ToString();
        //            alrgy.description = rdr["description"].ToString();

        //        }
        //        con.Close();
        //    }
        //    return alrgy;
        //}

        //To Add new Allergy record    
        //public void Insert(clsAllergy alrgy, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
        //        cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
        //        cmd.Parameters.AddWithValue("@name", alrgy.name);
        //        if (alrgy.description is null)
        //        {
        //            cmd.Parameters.AddWithValue("@description ", "");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@description ", alrgy.description);
        //        }

        //        cmd.Parameters.AddWithValue("@Aimage", null);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "I");

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        //To Update Allergy record    
        //public void Update(clsAllergy alrgy, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
        //        cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
        //        cmd.Parameters.AddWithValue("@name", alrgy.name);
        //        if (alrgy.description is null)
        //        {
        //            cmd.Parameters.AddWithValue("@description ", "");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@description ", alrgy.description);
        //        }
        //        cmd.Parameters.AddWithValue("@Aimage", null);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "U");

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}

        //To Delete Allergy record    
        //public void Delete(clsAllergy alrgy, string conn)
        //{
        //    using (SqlConnection con = new SqlConnection(conn))
        //    {
        //        SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
        //        cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
        //        cmd.Parameters.AddWithValue("@name", alrgy.name);
        //        if (alrgy.description is null)
        //        {
        //            cmd.Parameters.AddWithValue("@description ", "");
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@description ", alrgy.description);
        //        }
        //        cmd.Parameters.AddWithValue("@Aimage", null);
        //        cmd.Parameters.AddWithValue("@Tran_Type", "D");

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}


    //}
}
