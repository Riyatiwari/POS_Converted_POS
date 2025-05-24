using Converted_POS.Models;
using Converted_POS.Pages.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Converted_POS.Pages.Utility.Auto_Sync_RecoredModel;

namespace Converted_POS
{
    public class AutoSyncRecoredController
    {
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
                    //int venueId = Convert.ToInt32(rdr["venue_id"]);

                    // Call GetLocation for each venue_id to get its locations
                    //List<SelectListItem> location = GetLocation(c_id, conn, venueId);

                    // Add venue information
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

        public List<SelectListItem> GetLocation(int? c_id, string conn, int venue_id)
        {
            List<SelectListItem> location = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_Location_By_Venue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@venue_id", venue_id);
                //cmd.Parameters.AddWithValue("@location_id", location_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    location.Add(new SelectListItem
                    {
                        Value = rdr["location_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }

            return location;
        }

        public List<SelectListItem> GetMachineByLocation(int? c_id, int location_id, string connStr)
        {
            List<SelectListItem> machine = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Get_Machine_By_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);
                cmd.Parameters.AddWithValue("@location_id", location_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //int venueId = Convert.ToInt32(rdr["venue_id"]);

                    // Call GetLocation for each venue_id to get its locations
                    //List<SelectListItem> location = GetLocation(c_id, conn, venueId);

                    // Add venue information
                    machine.Add(new SelectListItem
                    {
                        Value = rdr["machine_id"].ToString(),
                        Text = rdr["name"].ToString()
                    });
                }

                con.Close();
            }
            return machine;
        }

        //public List<TillItem> GetTillList()
        //{
        //    throw new NotImplementedException();
        //}

        public List<Page> GetPageList(int? c_id, string conn)
        {
            List<Page> pages = new List<Page>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("Get_MasterPages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cmp_id", c_id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Assuming Page_ID is a numeric value and Page_name is a string.
                    pages.Add(new Page
                    {
                        Page_ID = Convert.ToInt32(rdr["Page_ID"]),// Value should be Page_ID
                        Page_name = rdr["Page_name"].ToString() // Text should be Page_name
                    });
                }
                con.Close();
            }
            return pages;
        }
        public void Insert(clsAutoSyncRecored autoSyncRecored, string conn, int? venue_id, int? location_id, int? till_id, List<int> SelectedMachineIds, List<Page> selectedPagesList,string pageIdsCsv, string pageNamesCsv)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                //var machineIdsTable = new DataTable();
                //machineIdsTable.Columns.Add("MachineId", typeof(int));  // Make sure the column name and type match the SQL type

                //// Add each machine ID to the DataTable
                //foreach (var machineId in SelectedMachineIds)
                //{
                //    machineIdsTable.Rows.Add(machineId);
                //}
                //string serializedMachineIds = string.Join(",", SelectedMachineIds);
                SqlCommand cmd = new SqlCommand("P_M_AutoSyncRecored", con);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var machineId in SelectedMachineIds)
                {
                    // Set parameters for the current machine ID
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@cmp_id", autoSyncRecored.cmp_id);
                    cmd.Parameters.AddWithValue("@Venue_Id", venue_id);
                    cmd.Parameters.AddWithValue("@Location_Id", location_id);
                    //cmd.Parameters.AddWithValue("@Till_Id", till_id);
                    //SqlParameter machineIdsParam = new SqlParameter("@Till_Id", SqlDbType.Structured)
                    //{
                    //    Value = machineIdsTable  // Pass the machineIdsTable as the TVP
                    //};
                    //cmd.Parameters.Add(machineIdsParam);
                    cmd.Parameters.AddWithValue("@Till_Id", machineId);
                    cmd.Parameters.AddWithValue("@SyncBtn_No", autoSyncRecored.SyncBtn_No);
                    cmd.Parameters.AddWithValue("@SyncFlag", autoSyncRecored.SyncFlag);
                    cmd.Parameters.AddWithValue("@login_id", autoSyncRecored.login_id);
                    cmd.Parameters.AddWithValue("@Page_list", pageIdsCsv);
                    cmd.Parameters.AddWithValue("@page_name", pageNamesCsv);

                    cmd.Parameters.AddWithValue("@Tran_Type", "I");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
