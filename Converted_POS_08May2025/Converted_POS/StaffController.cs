using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class StaffController : Controller
    {
        public static IConfiguration _configuration;
        string connectionString = _configuration.GetConnectionString("POS_Connection");
        public IEnumerable<clsStaff> SelectAll(int? c_id, string conn)
        {
            List<clsStaff> list = new List<clsStaff>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsStaff staff = new clsStaff();

                    staff.staff_id = Convert.ToInt32(rdr["staff_id"]);
                    staff.staff_code = rdr["staff_code"].ToString();
                    staff.name = rdr["name"].ToString();
                    staff.email = rdr["has_email"].ToString();
                    staff.is_active = rdr["Active"].ToString() == "Yes";
                    string Active = staff.is_active ? "Yes" : "No";
                    //if (rdr["Active"].ToString() == "Yes")
                    //{
                    //    staff.is_active = true;
                    //}
                    //else
                    //{
                    //    staff.is_active = false;
                    //}
                    staff.role_name = rdr["Role_Name"].ToString();
                    //staff.description = rdr["description"].ToString();

                    list.Add(staff);
                }
                con.Close();
            }
            return list;
        }

        public IActionResult SyncTill(string connStr, int cmpID)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("P_M_Key_Map_Sync_all_tills", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cmp_id", cmpID);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        public List<SelectListItem> GetCity(int cmpId, string connStr, int stateId)
        {
            List<SelectListItem> city = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_City", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@state_id", stateId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    city.Add(new SelectListItem
                    {
                        Value = rdr["City_id"].ToString(),
                        Text = rdr["CityName"].ToString()
                    });
                }

                con.Close();
            }

            return city;
        }


        public List<SelectListItem> GetState(int cmpId, string connStr, int? country_id)
        {
            List<SelectListItem> state = new List<SelectListItem>();

            if (!country_id.HasValue)
            {
                return state; // Return an empty list or handle accordingly
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_States", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@country_id", country_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    state.Add(new SelectListItem
                    {
                        Value = rdr["State_Id"].ToString(),
                        Text = rdr["StateName"].ToString()
                    });
                }
                con.Close();
            }
            return state;
        }

        public List<SelectListItem> GetAccessRole(int? c_id, string conn)
        {
            List<SelectListItem> role = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Role_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    role.Add(new SelectListItem
                    {
                        Value = rdr["Role_Id"].ToString(),
                        Text = rdr["Role_Name"].ToString()
                    });
                }

                con.Close();
            }

            return role;
        }

        public List<SelectListItem> GetVenue(int? c_id, string conn)
        {
            List<SelectListItem> venue = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Venue_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    venue.Add(new SelectListItem
                    {
                        Value = rdr["venue_id"].ToString(),
                        Text = rdr["venue_name"].ToString()
                    });
                }

                con.Close();
            }

            return venue;
        }

        public List<SelectListItem> GetCountry(int? c_id, string conn)
        {
            List<SelectListItem> country = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Countries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    country.Add(new SelectListItem
                    {
                        Value = rdr["Country_id"].ToString(),
                        Text = rdr["CountryName"].ToString()
                    });
                }

                con.Close();
            }

            return country;
        }

        public void Insert(clsStaff staff, string conn, int? country_id, int? state_id, int? city_id)
        {
            DateTime? existingJoiningDate = null;
            if (staff.staff_id.HasValue)
            {
                existingJoiningDate = GetJoiningDate(staff.staff_id.Value, conn);
            }


            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", staff.cmp_id);
                cmd.Parameters.AddWithValue("@staff_id ", staff.staff_id);
                cmd.Parameters.AddWithValue("@Manager_code", staff.managercode);
                cmd.Parameters.AddWithValue("@staff_code", staff.till_code);
                cmd.Parameters.AddWithValue("@till_code", staff.till_code);
                cmd.Parameters.AddWithValue("@name", staff.name);
                DateTime joiningDate = staff.joining_date ?? DateTime.Now;
                cmd.Parameters.AddWithValue("@joining_date", joiningDate);
                cmd.Parameters.AddWithValue("@branch_id", staff.branch_id);
                cmd.Parameters.AddWithValue("@department_id", staff.department_id);
                cmd.Parameters.AddWithValue("@designation_id", staff.designation_id);
                if (string.IsNullOrEmpty(staff.address))
                {
                    cmd.Parameters.AddWithValue("@address", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@address", staff.address);
                }
                cmd.Parameters.AddWithValue("@country_id", country_id);
                cmd.Parameters.AddWithValue("@state_id", state_id);
                cmd.Parameters.AddWithValue("@city_id", staff.city_id);
                //cmd.Parameters.AddWithValue("@country_id", staff.country_id.HasValue ? (object)staff.country_id.Value : DBNull.Value);
                //cmd.Parameters.AddWithValue("@state_id", staff.state_id.HasValue ? (object)staff.state_id.Value : DBNull.Value);
                //cmd.Parameters.AddWithValue("@city_id", staff.city_id.HasValue ? (object)staff.city_id.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@national_id", staff.national_id);
                cmd.Parameters.AddWithValue("@contact_no", staff.contact_no);
                cmd.Parameters.AddWithValue("@email", staff.email);
                cmd.Parameters.AddWithValue("@role_id", staff.role_id);
                cmd.Parameters.AddWithValue("@is_active", staff.is_active);
                //cmd.Parameters.AddWithValue("@Authentication_Code", staff.Authentication_Code);
                //cmd.Parameters.AddWithValue("@other_id", staff.other_id);
                //cmd.Parameters.AddWithValue("@venue_id", staff.venue_id);

                cmd.Parameters.AddWithValue("@till_active", staff.till_active);
                cmd.Parameters.AddWithValue("@is_trainee", staff.is_trainee);
                cmd.Parameters.AddWithValue("@login_id", staff.login_id);
                cmd.Parameters.AddWithValue("@function_id", string.IsNullOrEmpty(staff.function_id) ? DBNull.Value : (object)staff.function_id);
                cmd.Parameters.AddWithValue("@ip_address", string.IsNullOrEmpty(staff.ip_address) ? DBNull.Value : (object)staff.ip_address);
                cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(staff.photo) ? DBNull.Value : (object)staff.photo);
                if (staff.last_working_date.HasValue)
                {
                    cmd.Parameters.AddWithValue("@last_working_date", staff.last_working_date.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@last_working_date", DBNull.Value);
                }
                //if (staff.description is null)
                //{
                //    cmd.Parameters.AddWithValue("@description ", "");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                //}
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void Update(clsStaff staff, string conn, int? country_id, int? state_id, int? city_id)
        {
            DateTime? existingJoiningDate = null;
            if (staff.staff_id.HasValue)
            {
                existingJoiningDate = GetJoiningDate(staff.staff_id.Value, conn);
            }


            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", staff.cmp_id);
                cmd.Parameters.AddWithValue("@staff_id ", staff.staff_id);
                cmd.Parameters.AddWithValue("@Manager_code", staff.managercode);
                cmd.Parameters.AddWithValue("@staff_code", staff.till_code);
                cmd.Parameters.AddWithValue("@till_code", staff.till_code);
                cmd.Parameters.AddWithValue("@name", staff.name);

                cmd.Parameters.AddWithValue("@joining_date", staff.joining_date.HasValue ? (object)staff.joining_date.Value : (object)existingJoiningDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@branch_id", staff.branch_id);
                cmd.Parameters.AddWithValue("@department_id", staff.department_id);
                cmd.Parameters.AddWithValue("@designation_id", staff.designation_id);
                cmd.Parameters.AddWithValue("@Authentication_Code", staff.Authentication_Code);
                cmd.Parameters.AddWithValue("@other_id", staff.other_id);
                if (string.IsNullOrEmpty(staff.address))
                {
                    cmd.Parameters.AddWithValue("@address", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@address", staff.address);
                }
                cmd.Parameters.AddWithValue("@venue_id", staff.venue_id);
                cmd.Parameters.AddWithValue("@country_id", country_id);
                cmd.Parameters.AddWithValue("@state_id", state_id);
                cmd.Parameters.AddWithValue("@city_id", city_id);
                cmd.Parameters.AddWithValue("@national_id", staff.national_id);
                cmd.Parameters.AddWithValue("@contact_no", string.IsNullOrWhiteSpace(staff.contact_no) ? DBNull.Value : (object)staff.contact_no);
                cmd.Parameters.AddWithValue("@email", staff.email);
                cmd.Parameters.AddWithValue("@role_id", staff.role_id);
                cmd.Parameters.AddWithValue("@is_active", staff.is_active);
                cmd.Parameters.AddWithValue("@till_active", staff.till_active);
                cmd.Parameters.AddWithValue("@is_trainee", staff.is_trainee);
                cmd.Parameters.AddWithValue("@login_id", staff.login_id);
                cmd.Parameters.AddWithValue("@function_id", string.IsNullOrEmpty(staff.function_id) ? DBNull.Value : (object)staff.function_id);
                cmd.Parameters.AddWithValue("@ip_address", string.IsNullOrEmpty(staff.ip_address) ? DBNull.Value : (object)staff.ip_address);
                cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(staff.photo) ? DBNull.Value : (object)staff.photo);
                if (staff.last_working_date.HasValue)
                {
                    cmd.Parameters.AddWithValue("@last_working_date", staff.last_working_date.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@last_working_date", DBNull.Value);
                }
                //if (staff.description is null)
                //{
                //    cmd.Parameters.AddWithValue("@description ", "");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                //}
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public DateTime? GetJoiningDate(int staff_id, string conn)
        {
            DateTime? joiningDate = null;

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("SELECT joining_date FROM M_Staff WHERE staff_id = @staff_id", con);
                cmd.Parameters.AddWithValue("@staff_id", staff_id);

                con.Open();
                var result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    joiningDate = Convert.ToDateTime(result);
                }
            }

            return joiningDate;
        }

        public clsStaff Select(int? c_id, int? id, string conn, int? country_id, int? state_id, int? city_id)
        {
            clsStaff staff = new clsStaff();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@staff_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);



                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    staff.staff_id = Convert.ToInt32(rdr["staff_id"]);
                    staff.till_code = rdr["staff_code"].ToString();
                    staff.name = rdr["name"].ToString();
                    staff.managercode = rdr["Manager_code"].ToString();
                    staff.email = rdr["email"].ToString();
                    staff.contact_no = rdr["contact_no"].ToString();
                    staff.address = rdr["address"].ToString();
                    staff.Authentication_Code = rdr["Authentication_Code"].ToString();
                    staff.other_id = rdr["other_id"].ToString();
                    staff.photo = rdr["photo"].ToString();
                    staff.is_trainee = Convert.ToBoolean(rdr["is_trainee"]);
                    staff.till_active = Convert.ToBoolean(rdr["till_active"]);
                    staff.is_active = Convert.ToBoolean(rdr["is_active"]);
                    staff.country_id = Convert.ToInt32(rdr["country_id"]);
                    staff.state_id = Convert.ToInt32(rdr["state_id"]);
                    staff.city_id = Convert.ToInt32(rdr["city_id"]);
                    //staff.description = rdr["description"].ToString();

                }
                con.Close();
            }
            return staff;
        }

        public void Delete(clsStaff staff, string conn)
        {
            DateTime? existingJoiningDate = null;
            if (staff.staff_id.HasValue)
            {
                existingJoiningDate = GetJoiningDate(staff.staff_id.Value, conn);
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Staff", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", staff.cmp_id);
                cmd.Parameters.AddWithValue("@staff_id ", staff.staff_id);
                cmd.Parameters.AddWithValue("@staff_code", staff.till_code);
                cmd.Parameters.AddWithValue("@till_code", staff.till_code);
                cmd.Parameters.AddWithValue("@name", staff.name);
                cmd.Parameters.AddWithValue("@joining_date", staff.joining_date.HasValue ? (object)staff.joining_date.Value : (object)existingJoiningDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@branch_id", staff.branch_id);
                cmd.Parameters.AddWithValue("@department_id", staff.department_id);
                cmd.Parameters.AddWithValue("@designation_id", staff.designation_id);
                if (string.IsNullOrEmpty(staff.address))
                {
                    cmd.Parameters.AddWithValue("@address", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@address", staff.address);
                }
                cmd.Parameters.AddWithValue("@country_id", staff.country_id);
                cmd.Parameters.AddWithValue("@state_id", staff.state_id);
                cmd.Parameters.AddWithValue("@Manager_code", staff.managercode);
                cmd.Parameters.AddWithValue("@city_id", staff.city_id);
                cmd.Parameters.AddWithValue("@national_id", staff.national_id);
                cmd.Parameters.AddWithValue("@contact_no", staff.contact_no);
                cmd.Parameters.AddWithValue("@email", staff.email);
                cmd.Parameters.AddWithValue("@role_id", staff.role_id);
                cmd.Parameters.AddWithValue("@is_active", staff.is_active);
                cmd.Parameters.AddWithValue("@till_active", staff.till_active);
                cmd.Parameters.AddWithValue("@is_trainee", staff.is_trainee);
                cmd.Parameters.AddWithValue("@login_id", staff.login_id);
                cmd.Parameters.AddWithValue("@function_id", string.IsNullOrEmpty(staff.function_id) ? DBNull.Value : (object)staff.function_id);
                cmd.Parameters.AddWithValue("@ip_address", string.IsNullOrEmpty(staff.ip_address) ? DBNull.Value : (object)staff.ip_address);
                cmd.Parameters.AddWithValue("@photo", string.IsNullOrEmpty(staff.photo) ? DBNull.Value : (object)staff.photo);
                if (staff.last_working_date.HasValue)
                {
                    cmd.Parameters.AddWithValue("@last_working_date", staff.last_working_date.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@last_working_date", DBNull.Value);
                }
                //if (staff.description is null)
                //{
                //    cmd.Parameters.AddWithValue("@description ", "");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@description ", alrgy.description);
                //}
                //cmd.Parameters.AddWithValue("@Aimage", null);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<SelectListItem> GetPOSRole(int? c_id, string conn)
        {
            List<SelectListItem> categories = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_staff_rules_master", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categories.Add(new SelectListItem
                    {
                        Value = rdr["m_staff_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }
                con.Close();
            }

            return categories;
        }
    }
}
