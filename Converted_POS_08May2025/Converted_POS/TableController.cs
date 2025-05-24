using Converted_POS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Converted_POS
{
    public class TableController
    {
        public IEnumerable<clsTable> SelectAll(int? c_id, string conn)
        {
            List<clsTable> list = new List<clsTable>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Table", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    clsTable table = new clsTable();

                    table.table_id = Convert.ToInt32(rdr["Table_id"]);
                    table.name = rdr["Table_name"].ToString();
                    table.min_cover = Convert.ToInt32(rdr["MinCover"]);
                    table.max_cover = Convert.ToInt32(rdr["MaxCover"]);
                    table.location_name = rdr["location_name"].ToString();
                    list.Add(table);
                }
                con.Close();
            }
            return list;
        }

        public clsTable Select(int? c_id, int? id, string conn)
        {
            clsTable table = new clsTable();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_M_Table", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@table_id", id);
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    table.table_id = Convert.ToInt32(rdr["table_id"]);
                    table.name = rdr["Table_name"].ToString();
                    table.min_cover = Convert.ToInt32(rdr["MinCover"]);
                    table.max_cover = Convert.ToInt32(rdr["MaxCover"]);
                    table.location_id = Convert.ToInt32(rdr["location_id"]);
                    table.location_name = rdr["location_name"].ToString();
                    table.SortingNo = Convert.ToInt32(rdr["SortingNo"]);
                }
                con.Close();
            }
            return table;
        }

        public void Delete(clsTable table, string connStr, string ip_address)
        {
            table.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("P_M_Table", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", table.cmp_id);
                cmd.Parameters.AddWithValue("@table_id ", table.table_id);
                cmd.Parameters.AddWithValue("@name", table.name);
                cmd.Parameters.AddWithValue("@Location_id", table.location_id);
                cmd.Parameters.AddWithValue("@min_cover", table.min_cover);
                cmd.Parameters.AddWithValue("@max_cover", table.max_cover);
                cmd.Parameters.AddWithValue("@is_open", table.is_open);
                cmd.Parameters.AddWithValue("@shorting_no", table.SortingNo);
                cmd.Parameters.AddWithValue("@ip_address", table.ip_address);
                cmd.Parameters.AddWithValue("@login_id", table.login_id);
                
                cmd.Parameters.AddWithValue("@Tran_Type", "D");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<SelectListItem> GetLocation(int? c_id, string conn)
        {
            List<SelectListItem> role = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Cmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    role.Add(new SelectListItem
                    {
                        Value = rdr["location_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return role;
        }

        public void Insert(clsTable table, string conn, string ip_address)
        {
            table.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Table", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", table.cmp_id);
                cmd.Parameters.AddWithValue("@table_id", table.table_id);
                cmd.Parameters.AddWithValue("@name", table.name);
                cmd.Parameters.AddWithValue("@Location_id", table.location_id);
                cmd.Parameters.AddWithValue("@min_cover", table.min_cover);
                cmd.Parameters.AddWithValue("@max_cover", table.max_cover);
                cmd.Parameters.AddWithValue("@is_open", table.is_open);
                cmd.Parameters.AddWithValue("@shorting_no", table.SortingNo);
                cmd.Parameters.AddWithValue("@ip_address", table.ip_address);
                cmd.Parameters.AddWithValue("@login_id", table.login_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "I");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(clsTable table, string conn, string ip_address)
        {
            table.ip_address = ip_address;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("P_M_Table", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cmp_id", table.cmp_id);
                cmd.Parameters.AddWithValue("@table_id", table.table_id);
                cmd.Parameters.AddWithValue("@name", table.name);
                cmd.Parameters.AddWithValue("@Location_id", table.location_id);
                cmd.Parameters.AddWithValue("@min_cover", table.min_cover);
                cmd.Parameters.AddWithValue("@max_cover", table.max_cover);
                cmd.Parameters.AddWithValue("@is_open", table.is_open);
                cmd.Parameters.AddWithValue("@shorting_no", table.SortingNo);
                cmd.Parameters.AddWithValue("@ip_address", table.ip_address);
                cmd.Parameters.AddWithValue("@login_id", table.login_id);
                cmd.Parameters.AddWithValue("@Tran_Type", "U");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
