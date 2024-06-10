using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebFormBoostrap.BusinessLayer.Models;

namespace WebFormBoostrap.DataLayer
{
    public class ProfileRepository
    {
        public ProfileRepository()
        {
        }

        public int TotalRowCount(int startRowIndex, int pageSize, string sortExpression)
        {
            int intTotalProfile = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString))
            {
                SqlCommand cmdSelect = new SqlCommand("Profile_Total", conn);
                cmdSelect.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                dataReader.Read();
                intTotalProfile = Convert.ToInt32(dataReader[0]);
            }
            return intTotalProfile;
        }

        public List<UserProfile> GetProfiles(int startRowIndex, int pageSize, string sortExpression)
        {
            List<UserProfile> profiles = new List<UserProfile>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdSelect = new SqlCommand("Profile_GET", conn))
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;

                    startRowIndex = (startRowIndex / pageSize) + 1;
                    if (string.IsNullOrEmpty(sortExpression))
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

        public void CreateProfile(UserProfile profile)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdInsert = new SqlCommand("Profile_CREATE", conn))
                {
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.Parameters.AddWithValue("@Name", profile.Name);
                    cmdInsert.Parameters.AddWithValue("@Address", profile.Address);
                    cmdInsert.Parameters.AddWithValue("@Email", profile.Email);
                    cmdInsert.Parameters.AddWithValue("@Mobile", profile.Mobile);
                    cmdInsert.Parameters.AddWithValue("@IsActive", profile.IsActive);

                    conn.Open();
                    cmdInsert.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProfile(UserProfile profile)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdUpdate = new SqlCommand("Profile_UPDATE", conn))
                {
                    cmdUpdate.CommandType = CommandType.StoredProcedure;
                    cmdUpdate.Parameters.AddWithValue("@ProfileId", profile.ProfileId);
                    cmdUpdate.Parameters.AddWithValue("@Name", profile.Name);
                    cmdUpdate.Parameters.AddWithValue("@Address", profile.Address);
                    cmdUpdate.Parameters.AddWithValue("@Email", profile.Email);
                    cmdUpdate.Parameters.AddWithValue("@Mobile", profile.Mobile);
                    cmdUpdate.Parameters.AddWithValue("@IsActive", profile.IsActive);

                    conn.Open();
                    cmdUpdate.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProfile(int profileId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdDelete = new SqlCommand("Profile_Delete", conn))
                {
                    cmdDelete.CommandType = CommandType.StoredProcedure;
                    cmdDelete.Parameters.AddWithValue("@ProfileId", profileId);

                    conn.Open();
                    cmdDelete.ExecuteNonQuery();
                }
            }
        }

        public UserProfile GetProfileById(int profileId)
        {
            UserProfile profile = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ToString()))
            {
                using (SqlCommand cmdSelect = new SqlCommand("Profile_Get_By_Id", conn))
                {
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                    cmdSelect.Parameters.AddWithValue("@ProfileId", profileId);

                    conn.Open();
                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            profile = new UserProfile
                            {
                                ProfileId = Convert.ToInt32(reader["ProfileId"]),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Email = reader["Email"].ToString(),
                                Mobile = reader["Mobile"].ToString(),
                                IsActive = reader["IsActive"].ToString()
                            };
                        }
                    }
                }
            }
            return profile;
        }

        private static void GetProfileFromDatatable(List<UserProfile> profiles, DataTable profileDataTable)
        {
            foreach (DataRow row in profileDataTable.Rows)
            {
                UserProfile profile = new UserProfile
                {
                    ProfileId = Convert.ToInt32(row["ProfileId"]),
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Mobile = row["Mobile"].ToString(),
                    IsActive = row["IsActive"].ToString()
                };
                profiles.Add(profile);
            }
        }
    }
}