using AspNetWebformSample.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AspNetWebformSample.DataLayer
{
    /// <summary>
    /// Repository class for handling operations related to user profiles in the database.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// Default constructor for ProfileRepository class.
        /// </summary>
        public ProfileRepository()
        {
        }

        /// <summary>
        /// Retrieves the total row count from the database based on the provided startRowIndex, pageSize, and sortExpression.  This is for use by the Object Data Source
        /// </summary>
        /// <param name="startRowIndex">The starting index of the row.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="sortExpression">The expression used for sorting.</param>
        /// <returns>The total row count retrieved from the database.</returns>
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

        /// <summary>
        /// Retrieves the total row count from the database based on the provided startRowIndex, pageSize, and sortExpression.
        /// </summary>
        /// <returns>The total row count retrieved from the database.</returns>
        public int TotalRowCount()
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

        /// <summary>
        /// Retrieves a list of user profiles based on the provided start index, page size, and sort expression.
        /// </summary>
        /// <param name="startRowIndex">The starting index of the profiles to retrieve.</param>
        /// <param name="pageSize">The number of profiles to retrieve per page.</param>
        /// <param name="sortExpression">The column to use for sorting the profiles.</param>
        /// <returns>A list of user profiles based on the specified parameters.</returns>
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

        /// <summary>
        /// Creates a new user profile by inserting the provided UserProfile object into the database using a stored procedure.
        /// </summary>
        /// <param name="profile">The UserProfile object containing the details to be inserted into the database.</param>
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

        /// <summary>
        /// Updates a user profile in the database using a stored procedure named "Profile_UPDATE".
        /// </summary>
        /// <param name="profile">The user profile object containing the updated information.</param>
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

        /// <summary>
        /// Deletes a profile from the database using the specified profileId.
        /// </summary>
        /// <param name="profileId">The unique identifier of the profile to be deleted.</param>
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

        /// <summary>
        /// Retrieves a user profile by the specified profile ID from the database.
        /// </summary>
        /// <param name="profileId">The ID of the profile to retrieve.</param>
        /// <returns>The UserProfile object corresponding to the provided profile ID, or null if no profile is found.</returns>
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

        /// <summary>
        /// Extracts user profiles from a DataTable and populates a list of UserProfile objects.
        /// </summary>
        /// <param name="profiles">List of UserProfile objects to populate</param>
        /// <param name="profileDataTable">DataTable containing profile data</param>
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