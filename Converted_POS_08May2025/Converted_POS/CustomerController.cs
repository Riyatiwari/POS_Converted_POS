using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class CustomerController : Controller
    {
        public IEnumerable<clsCustomer> SelectAll(int? c_id, string conn)
        {
            List<clsCustomer> list = new List<clsCustomer>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsCustomer customer = new clsCustomer();

                    customer.customer_id = Convert.ToInt32(rdr["customer_id"]);
                    customer.first_name = rdr["first_name"].ToString();
                    customer.email = rdr["email"].ToString();
                    customer.contact_no = rdr["contact_no"].ToString();
                    customer.DateTimeExpiry = rdr["DateTimeExpiry"] != DBNull.Value

                   ? DateTime.TryParseExact(rdr["DateTimeExpiry"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiry)
                   ? expiry
                    : DateTime.MinValue
                    : DateTime.MinValue;
                    // Handle DBNull
                    customer.bounus_point = rdr["bounus_point"] != DBNull.Value ? Convert.ToSingle(rdr["bounus_point"]) : 0f; // Use Convert.ToSingle
                    customer.CardNumber = rdr["CardNumber"] != DBNull.Value
                ? long.TryParse(rdr["CardNumber"].ToString(), out long cardNum) ? cardNum : 0
                : 0;
                    customer.Is_credit = rdr["Is_credit"] != DBNull.Value ? Convert.ToBoolean(rdr["Is_credit"]) : false; // Handle DBNull

                    list.Add(customer);
                }
                con.Close();
            }
            return list;
        }

        public List<SelectListItem> GetPriceLevel(int? c_id, string conn)
        {
            List<SelectListItem> price = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_PricesBy_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    price.Add(new SelectListItem
                    {
                        Value = rdr["Product_Price_Id"].ToString(),
                        Text = rdr["PPrice"].ToString()
                    });
                }
                con.Close();
            }
            return price;
        }

        public List<SelectListItem> GetState(int? c_id, string conn, int? country_id)
        {
            List<SelectListItem> state = new List<SelectListItem>();
            if (!country_id.HasValue)
            {
                return state;
            }
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_States", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@country_id", country_id);
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

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

        public List<SelectListItem> GetCity(int? c_id, string conn, int? state_id)
        {
            List<SelectListItem> city = new List<SelectListItem>();
            if (!state_id.HasValue)
            {
                return city; // Return an empty list or handle accordingly
            }
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_City", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@state_id", state_id);
                //cmd.Parameters.AddWithValue("@cmp_id", c_id);

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

        public clsCustomer Select(int? c_id, int? id, string conn, int? country_id, int? state_id)
        {
            clsCustomer customer = new clsCustomer();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AccountID", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // customer.AccountID = Convert.ToInt32(rdr["AccountID"]);
                    customer.customer_id = Convert.ToInt32(rdr["customer_id"]);
                    customer.first_name = rdr["first_name"].ToString();
                    customer.last_name = rdr["last_name"].ToString();
                    customer.address = rdr["address"].ToString();
                    customer.postal_code = rdr["postal_code"].ToString();
                    customer.country_id = Convert.ToInt32(rdr["country_id"]);
                    customer.state_id = Convert.ToInt32(rdr["state_id"]);
                    customer.city_id = Convert.ToInt32(rdr["city_id"]);
                    customer.CardNumber = rdr["CardNumber"] != DBNull.Value
                ? long.TryParse(rdr["CardNumber"].ToString(), out long cardNum) ? cardNum : 0
                : 0;
                    customer.Is_credit = Convert.ToBoolean(rdr["Is_credit"]);
                    customer.other_id = rdr["other_id"].ToString();
                    customer.bounus_point = rdr["bounus_point"] != DBNull.Value ? Convert.ToSingle(rdr["bounus_point"]) : 0f; // Use Convert.ToSingle
                    customer.email = rdr["email"].ToString();
                    customer.contact_no = rdr["contact_no"].ToString();
                    customer.CustomerProfile = rdr["CustomerProfile"] as byte[];
                    customer.DateTimeExpiry = rdr["DateTimeExpiry"] != DBNull.Value ? Convert.ToDateTime(rdr["DateTimeExpiry"]) : DateTime.MinValue; // Handle DBNull
                }
                con.Close();
            }
            return customer;
        }

        public List<SelectListItem> GetProfile(int? c_id, string conn)
        {
            List<SelectListItem> profile = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    profile.Add(new SelectListItem
                    {
                        Value = rdr["profile_id"].ToString(),
                        Text = rdr["profile_name"].ToString()
                    });
                }
                con.Close();
            }
            return profile;
        }

        public void Delete(clsCustomer customer, string connStr, string ip_address)
        {
            customer.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_Account", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customer_id", customer.customer_id);
                cmd.Parameters.AddWithValue("@AccountID", customer.customer_id);
                cmd.Parameters.AddWithValue("@cmp_id", customer.cmp_id);
                cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@contact_no", customer.contact_no);
                cmd.Parameters.AddWithValue("@address", customer.address);
                cmd.Parameters.AddWithValue("@country_id", customer.country_id);
                cmd.Parameters.AddWithValue("@state_id", customer.state_id);
                cmd.Parameters.AddWithValue("@city_id", customer.city_id);
                cmd.Parameters.AddWithValue("@postal_code", customer.postal_code);
                //cmd.Parameters.AddWithValue("@is_active", customer.is_active);
                //cmd.Parameters.AddWithValue("@ip_address", customer.ip_address);
                //cmd.Parameters.AddWithValue("@login_id", customer.login_id);
                cmd.Parameters.AddWithValue("@other_id", customer.other_id);
                //cmd.Parameters.AddWithValue("@machine_id", customer.machine_id);
                //cmd.Parameters.AddWithValue("@profile_id", customer.profile_id);
                //cmd.Parameters.AddWithValue("@AccountID", customer.AccountID);
                //cmd.Parameters.AddWithValue("@AccountID", customer);
                cmd.Parameters.AddWithValue("@Is_credit", customer.Is_credit);
                cmd.Parameters.AddWithValue("@CardNumber", customer.CardNumber);
                //cmd.Parameters.AddWithValue("@Price_level_id", customer.Price_level_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Insert(clsCustomer customer, string connStr, int? country_id, int? state_id, int? city_id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_Account", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", customer.cmp_id);
                cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@contact_no", customer.contact_no);
                cmd.Parameters.AddWithValue("@other_id", customer.other_id);
                cmd.Parameters.AddWithValue("@profile_id", customer.profile_id);
                cmd.Parameters.AddWithValue("@address", customer.address);
                cmd.Parameters.AddWithValue("@country_id", country_id);
                cmd.Parameters.AddWithValue("@state_id", state_id);
                cmd.Parameters.AddWithValue("@city_id", customer.city_id);
                cmd.Parameters.AddWithValue("@postal_code", customer.postal_code);
                cmd.Parameters.AddWithValue("@Is_credit", customer.Is_credit);
                cmd.Parameters.AddWithValue("@CardNumber", customer.CardNumber);
                cmd.Parameters.AddWithValue("@Price_level_id", customer.Price_level_id);
                //cmd.Parameters.AddWithValue("@CustomerProfile", customer.CustomerProfile);
                cmd.Parameters.Add("@CustomerProfile", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@CustomerProfile"].Value = (object)customer.CustomerProfile ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@Tran_Type", "I");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsCustomer customer, string connStr, int? country_id, int? state_id, int? city_id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_Account", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@customer_id", customer.customer_id);
                cmd.Parameters.AddWithValue("@AccountID", customer.customer_id);
                cmd.Parameters.AddWithValue("@cmp_id", customer.cmp_id);
                cmd.Parameters.AddWithValue("@first_name", customer.first_name);
                cmd.Parameters.AddWithValue("@last_name", customer.last_name);
                cmd.Parameters.AddWithValue("@email", customer.email);
                cmd.Parameters.AddWithValue("@contact_no", customer.contact_no);
                cmd.Parameters.AddWithValue("@other_id", customer.other_id);
                cmd.Parameters.AddWithValue("@profile_id", customer.profile_id);
                cmd.Parameters.AddWithValue("@address", customer.address);
                cmd.Parameters.AddWithValue("@country_id", customer.country_id);
                cmd.Parameters.AddWithValue("@state_id", customer.state_id);
                cmd.Parameters.AddWithValue("@city_id", customer.city_id);
                cmd.Parameters.AddWithValue("@postal_code", customer.postal_code);
                cmd.Parameters.AddWithValue("@Is_credit", customer.Is_credit);
                cmd.Parameters.AddWithValue("@CardNumber", customer.CardNumber);
                cmd.Parameters.AddWithValue("@Price_level_id", customer.Price_level_id);
                cmd.Parameters.Add("@CustomerProfile", SqlDbType.VarBinary); // Ensure you're using the right SQL type
                cmd.Parameters["@CustomerProfile"].Value = (object)customer.CustomerProfileFile ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@Tran_Type", "U");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
