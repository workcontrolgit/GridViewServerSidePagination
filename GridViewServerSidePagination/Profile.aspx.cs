using AspNetWebformSample.BusinessLayer.Events;
using AspNetWebformSample.BusinessLayer.Models;
using AspNetWebformSample.BusinessLayer.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetWebformSample
{
    /// <summary>
    /// Represents the Profile page class that handles user profile operations.
    /// </summary>
    public partial class Profile : Page
    {
        private readonly ProfileService _profileService;

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
        }

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
            gvProfile.DataBind();
        }

        /// <summary>
        /// Event handler for when the page size dropdown selection is changed.
        /// Sets the page size of the gridview to the selected value from the dropdown list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void PageSize_Changed(object sender, EventArgs e)
        {
            DropDownList ddlPageSize = (DropDownList)sender;
            gvProfile.PageSize = int.Parse(ddlPageSize.SelectedValue);
            //gvProfile.PageIndex = 0;
            //gvProfile.DataBind();
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
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                int profileId = Convert.ToInt32(e.CommandArgument);
                hdnDeleteProfileId.Value = profileId.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDeleteModal", "showDeleteModal()", true);
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
                    //lblTotalRecords.Text = gv.DataSource as ObjectDataSource != null
                    //    ? (gv.DataSource as ObjectDataSource).SelectCountMethod
                    //    : "0";
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
        }

        /// <summary>
        /// Event handler for confirming the deletion of a profile. Retrieves the profile ID from a hidden field, deletes the profile using the profile service, updates the grid view, and then closes the delete modal using a script.
        /// </summary>
        protected void ConfirmDeleteProfile(object sender, EventArgs e)
        {
            int profileId = Convert.ToInt32(hdnDeleteProfileId.Value);
            _profileService.DeleteProfile(profileId);
            gvProfile.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseDeleteModal", "hideDeleteModal()", true);
        }

        protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPages = (DropDownList)sender;
            int pageIndex = int.Parse(ddlPages.SelectedValue);
            gvProfile.PageIndex = pageIndex;
            gvProfile.DataBind();
        }
    }
}