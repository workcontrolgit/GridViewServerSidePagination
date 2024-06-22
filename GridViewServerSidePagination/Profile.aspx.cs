using AspNetWebformSample.BusinessLayer.Events;
using AspNetWebformSample.BusinessLayer.Models;
using AspNetWebformSample.BusinessLayer.Services;
using LargeXlsx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetWebformSample
{
    /// <summary>
    /// Represents the Profile page class that handles user profile operations.
    /// </summary>
    public partial class Profile : Page
    {
        /// <summary>
        /// Private field to store an instance of the ProfileService class.
        /// </summary>
        private readonly ProfileService _profileService;

        /// <summary>
        /// Constructor for the Profile class.
        /// Initializes a new instance of the Profile class and creates a new ProfileService object.
        /// </summary>
        public Profile()
        {
            _profileService = new ProfileService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// Subscribes the ProfileSaved event of ProfileModal to the ProfileModal_SaveProfileClicked method.
            /// </summary>
            ProfileModal.ProfileSaved += ProfileModal_SaveProfileClicked;
            // Subscribe to the DeleteConfirmed event
            DeleteModal.DeleteConfirmed += DeleteModal_DeleteConfirmed;
        }

        /// <summary>
        /// Event handler for saving a user profile data.
        /// Creates a new profile if ProfileId is 0, otherwise updates the existing profile using ProfileService.
        /// Closes the profile modal and rebinds the grid view with updated data.
        /// </summary>
        protected void ProfileModal_SaveProfileClicked(object sender, ProfileEventArgs e)
        {
            // Handle the profile data
            UserProfile profile = e.Profile;

            if (profile.ProfileId == 0)
            {
                _profileService.CreateProfile(profile);
            }
            else
            {
                _profileService.UpdateProfile(profile);
            }
            ProfileModal.CloseModal();
            gvProfile.DataBind();
        }

        /// <summary>
        /// Event handler for changing the page size in a GridView
        /// </summary>
        /// <param name="sender">The DropDownList control that triggered the event</param>
        /// <param name="e">The event arguments</param>
        protected void PageSize_Changed(object sender, EventArgs e)
        {
            DropDownList ddlPageSize = (DropDownList)sender;
            gvProfile.PageSize = int.Parse(ddlPageSize.SelectedValue);
            gvProfile.PageIndex = 0;
        }

        /// <summary>
        /// Handles the RowCommand event of the GridView control for editing or deleting a profile.
        /// If the command is to edit a row, retrieves the profile details and populates the modal form for editing.
        /// If the command is to delete a row, sets the profile ID to be deleted and shows the delete confirmation modal.
        /// </summary>
        protected void gvProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int profileId = Convert.ToInt32(e.CommandArgument);
                var profile = _profileService.GetProfileById(profileId);

                if (profile != null)
                {
                    ProfileModal.OpenProfileModal(profile);
                    ProfileModal.OpenModal();
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                var profileId = Convert.ToString(e.CommandArgument);
                DeleteModal.SetProfileId(profileId);
                DeleteModal.OpenModal();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gvProfile GridView to register async postback controls for edit and delete buttons in each row.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A GridViewRowEventArgs that contains the event data.</param>
        protected void gvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnEdit);

                Button btnDelete = e.Row.FindControl("btnDelete") as Button;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnDelete);
            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                GridView gv = (GridView)sender;

                // Get the pager row and find the LinkButton controls
                LinkButton lnkFirst = (LinkButton)e.Row.FindControl("lnkFirst");
                LinkButton lnkPrev = (LinkButton)e.Row.FindControl("lnkPrev");
                LinkButton lnkNext = (LinkButton)e.Row.FindControl("lnkNext");
                LinkButton lnkLast = (LinkButton)e.Row.FindControl("lnkLast");

                // Disable "First" and "Previous" buttons if on the first page
                if (gv.PageIndex == 0)
                {
                    if (lnkFirst != null) lnkFirst.Enabled = false;
                    if (lnkPrev != null) lnkPrev.Enabled = false;
                }

                // Disable "Next" and "Last" buttons if on the last page
                if (gv.PageIndex == gv.PageCount - 1)
                {
                    if (lnkNext != null) lnkNext.Enabled = false;
                    if (lnkLast != null) lnkLast.Enabled = false;
                }

                // Display total records
                Label lblTotalRecords = (Label)e.Row.FindControl("lblTotalRecords");
                if (lblTotalRecords != null)
                {
                    lblTotalRecords.Text = Convert.ToString(_profileService.GetTotalProfiles());
                }

                // Set page size to match grid page size
                DropDownList ddlPageSize = (DropDownList)e.Row.FindControl("ddlPageSize");
                ddlPageSize.SelectedValue = Convert.ToString(gv.PageSize);

                // Populate page numbers in the dropdown
                DropDownList ddlPages = (DropDownList)e.Row.FindControl("ddlPages");
                if (ddlPages != null)
                {
                    ddlPages.Items.Clear();
                    for (int i = 0; i < gv.PageCount; i++)
                    {
                        ListItem item = new ListItem($"Page {i + 1} of {gv.PageCount}", i.ToString());
                        if (i == gv.PageIndex)
                        {
                            item.Selected = true;
                        }
                        ddlPages.Items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for the click event of the btnAddProfile button.
        /// Initializes the add profile modal by calling the InitializeAddProfileModal method of the ProfileModal class.
        /// </summary>
        protected void btnAddProfile_Click(object sender, EventArgs e)
        {
            ProfileModal.InitializeAddProfileModal();
            ProfileModal.OpenModal();
        }

        /// <summary>
        /// Event handler for confirming deletion in a modal dialog. Deletes a profile based on the profile ID obtained from the modal, then rebinds the GridView to reflect the changes.
        /// </summary>
        private void DeleteModal_DeleteConfirmed(object sender, EventArgs e)
        {
            var profileId = Convert.ToInt32(DeleteModal.ProfileID);
            // Implement your delete logic here
            _profileService.DeleteProfile(profileId);
            gvProfile.DataBind();
        }

        /// <summary>
        /// Event handler for the SelectedIndexChanged event of the ddlPages DropDownList.
        /// Sets the PageIndex of the gvProfile GridView to the selected value of ddlPages.
        /// </summary>
        /// <param name="sender">The object that raised the event (ddlPages DropDownList).</param>
        /// <param name="e">The event arguments.</param>
        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPages = (DropDownList)sender;
            int pageIndex = int.Parse(ddlPages.SelectedValue);
            gvProfile.PageIndex = pageIndex;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int startRowIndex = 0; // Adjust this as necessary
                int pageSize = 10;     // Adjust this as necessary
                string sortExpression = "ProfileId"; // Adjust this as necessary

                List<UserProfile> profiles = _profileService.GetProfiles(startRowIndex, pageSize, sortExpression);

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

                        // Write the header row
                        xlsxWriter.BeginRow()
                            .Write("ProfileId", headerStyle)
                            .Write("Name", headerStyle)
                            .Write("Address", headerStyle)
                            .Write("Email", headerStyle)
                            .Write("Mobile", headerStyle)
                            .Write("IsActive", headerStyle);

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
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                    // Write the memory stream to the response
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (adjust this to use your logging framework)
                System.Diagnostics.Trace.TraceError("Error exporting profiles to Excel: " + ex.Message);

                // Optionally, show a user-friendly error message
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("An error occurred while generating the Excel file. Please try again later.");
                Response.StatusCode = 500;
            }
            finally
            {
                Response.End();
            }
        }
    }
}