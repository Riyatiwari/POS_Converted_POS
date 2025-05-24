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
    public class ProductPriceController : Controller
    {
        public IEnumerable<clsProductPrice> SelectAll(int? c_id, string conn)
        {
            List<clsProductPrice> list = new List<clsProductPrice>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsProductPrice price = new clsProductPrice();

                    price.Product_Price_Id = Convert.ToInt32(rdr["Product_Price_Id"]);
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        price.is_active = true;
                    }
                    else
                    {
                        price.is_active = false;
                    }
                    price.PPrice = rdr["PPrice"].ToString();

                    list.Add(price);
                }
                con.Close();
            }
            return list;
        }

        public clsProductPrice Select(int? c_id, int? id, string conn)
        {
            clsProductPrice price = new clsProductPrice();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@Product_Price_Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    price.Product_Price_Id = Convert.ToInt32(rdr["Product_Price_Id"]);
                    price.PPrice = rdr["PPrice"].ToString();
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        price.is_active = true;
                    }
                    else
                    {
                        price.is_active = false;
                    }
                }
                con.Close();
            }
            return price;
        }

        public clsProductPrice Select(int? c_id, int? id, string conn, int? country_id, int? state_id)
        {
            clsProductPrice price = new clsProductPrice();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Product_Price_Id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // customer.AccountID = Convert.ToInt32(rdr["AccountID"]);
                    price.Product_Price_Id = Convert.ToInt32(rdr["Product_Price_Id"]);
                    if (rdr["Active"].ToString() == "Yes")
                    {
                        price.is_active = true;
                    }
                    else
                    {
                        price.is_active = false;
                    }
                    price.PPrice = rdr["PPrice"].ToString();
                    
                }
                con.Close();
            }
            return price;
        }

        public void Insert(clsProductPrice price, string connStr, string ip_address)
        {
            price.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Product_Price_Id", price.Product_Price_Id);
                cmd.Parameters.AddWithValue("@PPrice", price.PPrice);
                cmd.Parameters.AddWithValue("@cmp_id", price.cmp_id);
                cmd.Parameters.AddWithValue("@ip_address", price.ip_address);
                cmd.Parameters.AddWithValue("@login_id", price.login_id);
                cmd.Parameters.AddWithValue("@is_active", price.is_active);
                cmd.Parameters.AddWithValue("@is_default", price.is_default);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsProductPrice price, string connStr, string ip_address)
        {
            price.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Product_Price_Id", price.Product_Price_Id);
                cmd.Parameters.AddWithValue("@PPrice", price.PPrice);
                cmd.Parameters.AddWithValue("@cmp_id", price.cmp_id);
                cmd.Parameters.AddWithValue("@ip_address", price.ip_address);
                cmd.Parameters.AddWithValue("@login_id", price.login_id);
                cmd.Parameters.AddWithValue("@is_active", price.is_active);
                cmd.Parameters.AddWithValue("@is_default", price.is_default);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Delete(clsProductPrice price, string connStr, string ip_address)
        {
            price.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Product_Price", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Product_Price_Id", price.Product_Price_Id);
                cmd.Parameters.AddWithValue("@PPrice", price.PPrice);
                cmd.Parameters.AddWithValue("@cmp_id", price.cmp_id);
                cmd.Parameters.AddWithValue("@ip_address", price.ip_address);
                cmd.Parameters.AddWithValue("@login_id", price.login_id);
                cmd.Parameters.AddWithValue("@is_active", price.is_active);
                cmd.Parameters.AddWithValue("@is_default", price.is_default);
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
