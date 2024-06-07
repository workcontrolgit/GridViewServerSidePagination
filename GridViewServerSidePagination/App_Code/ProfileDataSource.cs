using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebFormBoostrap.App_Code
{
    [DataObject(true)]
    public class ProfileDataSource
    {
        private readonly string _connectionString;

        public ProfileDataSource(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ProfileDataSource()
        {
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Int32 TotalRowCount(Int32 startRowIndex, Int32 pageSize, String sortExpression)
        {
            Int32 intTotalProfile = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString))
            {
                SqlCommand cmdSelect = new SqlCommand();

                conn.Open();
                cmdSelect.CommandText = "Profile_Total";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                cmdSelect.Connection = conn;

                SqlDataReader dataReader = cmdSelect.ExecuteReader();

                dataReader.Read();
                intTotalProfile = Convert.ToInt32(dataReader[0]);
            }
            return intTotalProfile;
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Profile> GetProfiles(int startRowIndex, int pageSize, string sortExpression)
        {
            List<Profile> profiles = new List<Profile>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdSelect = new SqlCommand("Profile_GET", conn))
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;

                    startRowIndex = (startRowIndex / pageSize) + 1;

                    if (String.IsNullOrEmpty(sortExpression))
                        sortExpression = "ProfileId";

                    cmdSelect.Parameters.AddWithValue("@CurrentPage", startRowIndex);
                    cmdSelect.Parameters.AddWithValue("@PageSize", pageSize);
                    cmdSelect.Parameters.AddWithValue("@SortExpression", sortExpression);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdSelect);
                    DataTable profileDataTable = new DataTable();
                    dataAdapter.Fill(profileDataTable);

                    GetProfileFromDatatable(profiles, profileDataTable);
                }
            }

            return profiles;
        }

        private static void GetProfileFromDatatable(List<Profile> profiles, DataTable profileDataTable)
        {
            foreach (DataRow row in profileDataTable.Rows)
            {
                Profile profile1 = new Profile
                {
                    ProfileId = Convert.ToInt32(row["ProfileId"]),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Mobile = row["Mobile"].ToString(),
                    IsActive = row["IsActive"].ToString()
                };
                Profile profile = profile1;

                profiles.Add(profile);
            }
        }
    }
}