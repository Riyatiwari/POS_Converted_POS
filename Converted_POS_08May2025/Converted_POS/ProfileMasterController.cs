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
    public class ProfileMasterController : Controller
    {
        public IEnumerable<clsProfileMaster> SelectAll(int? c_id, string conn)
        {
            List<clsProfileMaster> list = new List<clsProfileMaster>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string active = "";
                    clsProfileMaster DType = new clsProfileMaster();

                    DType.profile_id = Convert.ToInt32(rdr["profile_id"]);
                    DType.profile_name = rdr["profile_name"].ToString();


                    list.Add(DType);
                }
                con.Close();
            }
            return list;
        }

        public clsProfileMaster Select(int? c_id, int? id, string conn)
        {
            clsProfileMaster profile = new clsProfileMaster();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Profile_info", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@profile_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    profile.profile_id = Convert.ToInt32(rdr["profile_id"]);
                    profile.profile_name = rdr["profile_name"].ToString();
                    profile.bonus_point = Convert.ToDecimal(rdr["bonus_point"]);
                    profile.amount = Convert.ToDecimal(rdr["amount"]);
                    profile.discount_percent = Convert.ToDecimal(rdr["discount_percent"]);
                    profile.earn_bonus = Convert.ToDecimal(rdr["earn_bonus"]);
                    profile.purchase_amount = Convert.ToDecimal(rdr["purchase_amount"]);

                    if (rdr["Is_Default"].ToString() == "1")
                    {
                        profile.IsDefault = true;
                    }
                    else
                    {
                        profile.IsDefault = false;
                    }

                }

                con.Close();
            }
            return profile;
        }

        public void Insert(clsProfileMaster profile, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", profile.cmp_id);
                cmd.Parameters.AddWithValue("@profile_id", profile.profile_id);
                cmd.Parameters.AddWithValue("@profile_name", profile.profile_name);
                cmd.Parameters.AddWithValue("@bonus_point", profile.bonus_point);
                cmd.Parameters.AddWithValue("@amount", profile.amount);
                cmd.Parameters.AddWithValue("@purchase_amount", profile.purchase_amount);
                cmd.Parameters.AddWithValue("@earn_bonus", profile.earn_bonus);
                cmd.Parameters.AddWithValue("@discount_percent", profile.discount_percent);
                cmd.Parameters.AddWithValue("@IsDefaul", profile.IsDefault);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsProfileMaster profile, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", profile.cmp_id);
                cmd.Parameters.AddWithValue("@profile_id", profile.profile_id);
                cmd.Parameters.AddWithValue("@profile_name", profile.profile_name);
                cmd.Parameters.AddWithValue("@bonus_point", profile.bonus_point);
                cmd.Parameters.AddWithValue("@amount", profile.amount);
                cmd.Parameters.AddWithValue("@purchase_amount", profile.purchase_amount);
                cmd.Parameters.AddWithValue("@earn_bonus", profile.earn_bonus);
                cmd.Parameters.AddWithValue("@discount_percent", profile.discount_percent);
                cmd.Parameters.AddWithValue("@IsDefaul", profile.IsDefault);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Delete(clsProfileMaster profile, string conn)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Profile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", profile.cmp_id);
                cmd.Parameters.AddWithValue("@profile_id", profile.profile_id);
                cmd.Parameters.AddWithValue("@profile_name", profile.profile_name);
                cmd.Parameters.AddWithValue("@bonus_point", profile.bonus_point);
                cmd.Parameters.AddWithValue("@amount", profile.amount);
                cmd.Parameters.AddWithValue("@purchase_amount", profile.purchase_amount);
                cmd.Parameters.AddWithValue("@earn_bonus", profile.earn_bonus);
                cmd.Parameters.AddWithValue("@discount_percent", profile.discount_percent);
                cmd.Parameters.AddWithValue("@IsDefaul", profile.IsDefault);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
