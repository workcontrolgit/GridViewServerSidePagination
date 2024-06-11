using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormBootstrap.BusinessLayer.Models;
using WebFormBootstrap.BusinessLayer.Services;

namespace WebFormBootstrap
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

        /// <summary>
        /// Event handler for when the page size dropdown selection is changed.
        /// Sets the page size of the gridview to the selected value from the dropdown list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void PageSize_Changed(object sender, EventArgs e)
        {
            gvProfile.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
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
                    lblModalContent.Text = "Edit Profile";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "showModal()", true);
                    hdnprofileId.Value = profile.ProfileId.ToString();
                    lblProfileId.Text = profile.ProfileId.ToString();
                    txtName.Text = profile.Name;
                    txtAddress.Text = profile.Address;
                    txtEmail.Text = profile.Email;
                    txtMobile.Text = profile.Mobile;
                    txtStatus.Text = profile.IsActive;
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
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Button btnEdit = e.Row.FindControl("btnEdit") as Button;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnEdit);

            Button btnDelete = e.Row.FindControl("btnDelete") as Button;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnDelete);
        }

        /// <summary>
        /// Event handler for saving a user profile. Creates a new UserProfile object with data from input fields, then either creates or updates the profile using a ProfileService based on the ProfileId. Finally, refreshes the GridView and hides the modal popup using JavaScript.
        /// </summary>
        protected void SaveProfile(object sender, EventArgs e)
        {
            var profile = new UserProfile
            {
                ProfileId = string.IsNullOrEmpty(hdnprofileId.Value) ? 0 : Convert.ToInt32(hdnprofileId.Value),
                Name = txtName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                Mobile = txtMobile.Text,
                IsActive = txtStatus.Text
            };

            if (profile.ProfileId == 0)
            {
                _profileService.CreateProfile(profile);
            }
            else
            {
                _profileService.UpdateProfile(profile);
            }

            gvProfile.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "hideModal()", true);
        }

        /// <summary>
        /// Event handler for the button click to add a new profile.
        /// Clears the input fields and sets the modal content to "Add Profile".
        /// Shows the modal dialog using JavaScript.
        /// </summary>
        protected void btnAddProfile_Click(object sender, EventArgs e)
        {
            lblModalContent.Text = "Add Profile";
            hdnprofileId.Value = string.Empty;
            lblProfileId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtStatus.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "showModal()", true);
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
    }
}