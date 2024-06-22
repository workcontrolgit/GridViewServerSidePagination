using AspNetWebformSample.BusinessLayer.Models;
using AspNetWebformSample.DataLayer;
using LargeXlsx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace AspNetWebformSample.BusinessLayer.Services
{
    /// <summary>
    /// Service class for managing user profiles.
    /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// Private field to store an instance of ProfileRepository class.
        /// </summary>
        private readonly ProfileRepository _repository;

        /// <summary>
        /// Constructor for ProfileService class.
        /// Initializes a new instance of ProfileRepository.
        /// </summary>
        public ProfileService()
        {
            _repository = new ProfileRepository();
        }

        /// <summary>
        /// Creates a new user profile by calling the CreateProfile method of the repository.
        /// </summary>
        /// <param name="profile">The user profile to be created</param>
        public void CreateProfile(UserProfile profile)
        {
            _repository.CreateProfile(profile);
        }

        /// <summary>
        /// Updates the user profile using the provided UserProfile object.
        /// </summary>
        /// <param name="profile">The UserProfile object containing the updated profile information.</param>
        public void UpdateProfile(UserProfile profile)
        {
            _repository.UpdateProfile(profile);
        }

        /// <summary>
        /// Retrieves a user profile by its ID from the repository.
        /// </summary>
        /// <param name="profileId">The ID of the user profile to retrieve</param>
        /// <returns>
        /// The user profile with the specified ID
        /// </returns>
        public UserProfile GetProfileById(int profileId)
        {
            return _repository.GetProfileById(profileId);
        }

        /// <summary>
        /// Deletes a profile based on the provided profileId.
        /// </summary>
        /// <param name="profileId">The unique identifier of the profile to be deleted.</param>
        public void DeleteProfile(int profileId)
        {
            _repository.DeleteProfile(profileId);
        }

        /// <summary>
        /// Gets the total number of profiles from the repository.
        /// </summary>
        /// <returns>
        /// The total number of profiles.
        /// </returns>
        public int GetTotalProfiles()
        {
            return _repository.TotalRowCount();
        }

        /// <summary>
        /// Retrieves a list of user profiles.
        /// </summary>
        /// <param name="startRowIndex">The starting index of the profiles to retrieve.</param>
        /// <param name="pageSize">The number of profiles to retrieve per page.</param>
        /// <param name="sortExpression">The column to use for sorting the profiles.</param>
        /// <returns>A list of user profiles based on the specified parameters.</returns>
        public List<UserProfile> GetProfiles(int startRowIndex, int pageSize, string sortExpression)
        {
            return _repository.GetProfiles(startRowIndex, pageSize, sortExpression);
        }

        /// <summary>
        /// Exports a list of user profiles to an Excel file and sends the file as a response to the client.
        /// </summary>
        /// <param name="profiles">The list of user profiles to be exported.</param>
        /// <param name="Response">The HttpResponse object to send the Excel file as a response.</param>
        public void ExportProfilesToExcel(List<UserProfile> profiles, HttpResponse response)
        {
            try
            {
                string fileName = "Profiles_" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".xlsx";

                using (var memoryStream = new MemoryStream())
                {
                    using (var xlsxWriter = new XlsxWriter(memoryStream))
                    {
                        var headerStyle = new XlsxStyle(
                            new XlsxFont("Segoe UI", 9, Color.White, bold: true),
                            new XlsxFill(Color.FromArgb(0, 0x45, 0x86)),
                            XlsxBorder.None,
                            XlsxNumberFormat.General,
                            XlsxAlignment.Default
                        );

                        var cellStyle = new XlsxStyle(
                            XlsxFont.Default,
                            XlsxFill.None,
                            XlsxBorder.None,
                            XlsxNumberFormat.General,
                            XlsxAlignment.Default
                        );

                        xlsxWriter.BeginWorksheet("Profiles");
                        // Use reflection to get the display names for the header row
                        var properties = typeof(UserProfile).GetProperties();

                        xlsxWriter.BeginRow();
                        foreach (var prop in properties)
                        {
                            var displayAttribute = prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                            string columnName = displayAttribute != null ? displayAttribute.Name : prop.Name;
                            xlsxWriter.Write(columnName, headerStyle);
                        }
                        // Write the profile data
                        foreach (var profile in profiles)
                        {
                            xlsxWriter.BeginRow()
                                .Write(profile.ProfileId, cellStyle)
                                .Write(profile.Name, cellStyle)
                                .Write(profile.Address, cellStyle)
                                .Write(profile.Email, cellStyle)
                                .Write(profile.Mobile, cellStyle)
                                .Write(profile.IsActive, cellStyle);
                        }
                    }

                    // Set the position of the memory stream to the beginning
                    memoryStream.Position = 0;

                    // Set the response to download the file
                    response.Clear();
                    response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                    // Write the memory stream to the response
                    memoryStream.WriteTo(response.OutputStream);
                    response.Flush();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (adjust this to use your logging framework)
                System.Diagnostics.Trace.TraceError("Error exporting profiles to Excel: " + ex.Message);

                // Optionally, show a user-friendly error message
                response.Clear();
                response.ContentType = "text/plain";
                response.Write("An error occurred while generating the Excel file. Please try again later.");
                response.StatusCode = 500;
            }
            finally
            {
                response.End();
            }
        }
    }
}