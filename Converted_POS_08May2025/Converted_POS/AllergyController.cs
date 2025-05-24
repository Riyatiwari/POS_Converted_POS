using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Converted_POS
{
    [Route("api/[controller]")]
    public class AllergyController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");

        public IEnumerable<clsAllergy> SelectAll(int? c_id, string conn)
        {
            List<clsAllergy> list = new List<clsAllergy>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Allergy", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsAllergy allergy = new clsAllergy();

                    allergy.allergy_id = Convert.ToInt32(rdr["allergy_id"]);
                    allergy.name = rdr["name"].ToString();
                    allergy.description = rdr["description"].ToString();

                    list.Add(allergy);
                }
                con.Close();
            }
            return list;
        }

        public clsAllergy Select(int? c_id, int? id, string conn)
        {
            clsAllergy alrgy = new clsAllergy();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_AllergyInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@allergy_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    alrgy.allergy_id = Convert.ToInt32(rdr["allergy_id"]);
                    alrgy.name = rdr["name"].ToString();
                    alrgy.description = rdr["description"].ToString();
                    alrgy.Aimage = rdr["Aimage"] as byte[];
                }
                con.Close();
            }
            return alrgy;
        }

        public void Insert(clsAllergy alrgy, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
                cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
                cmd.Parameters.AddWithValue("@name", alrgy.name);
                if (alrgy.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                }

                cmd.Parameters.Add("@Aimage", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@Aimage"].Value = (object)alrgy.Aimage ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsAllergy alrgy, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
                cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
                cmd.Parameters.AddWithValue("@name", alrgy.name);
                if (alrgy.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                }
                cmd.Parameters.Add("@Aimage", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@Aimage"].Value = (object)alrgy.Aimage ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Delete(clsAllergy alrgy, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Allergy", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", alrgy.cmp_id);
                cmd.Parameters.AddWithValue("@allergy_id ", alrgy.allergy_id);
                cmd.Parameters.AddWithValue("@name", alrgy.name);
                if (alrgy.description is null)
                {
                    cmd.Parameters.AddWithValue("@description ", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                }
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
