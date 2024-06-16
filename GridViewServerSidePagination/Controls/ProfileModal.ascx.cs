using AspNetWebformSample.BusinessLayer.Events;
using AspNetWebformSample.BusinessLayer.Models;
using System;
using System.Web.UI;

namespace AspNetWebformSample
{
    public partial class ProfileModal : UserControl
    {
        /// <summary>
        /// Event that is raised when a profile is saved.
        /// </summary>
        public event EventHandler<ProfileEventArgs> ProfileSaved;

        /// <summary>
        /// Gets or sets the UserProfile object for the user profile information.
        /// </summary>
        /// <returns>
        /// A UserProfile object containing the profile information.
        /// </returns>
        public UserProfile Profile
        {
            get
            {
                int profileId = 0;
                if (!string.IsNullOrEmpty(lblProfileId.Text))
                {
                    int.TryParse(lblProfileId.Text, out profileId);
                }
                return new UserProfile
                {
                    ProfileId = profileId,
                    Name = txtName.Text,
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text,
                    IsActive = txtStatus.Text
                };
            }
            set
            {
                lblProfileId.Text = value.ProfileId.ToString();
                txtName.Text = value.Name;
                txtAddress.Text = value.Address;
                txtEmail.Text = value.Email;
                txtMobile.Text = value.Mobile;
                txtStatus.Text = value.IsActive;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Saves the profile and raises the ProfileSaved event.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void SaveProfile(object sender, EventArgs e)
        {
            // Raise the ProfileSaved event
            ProfileSaved?.Invoke(this, new ProfileEventArgs(Profile));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "hideModal()", true);
        }

        /// <summary>
        /// Opens a modal to edit a user profile with the provided profile details.
        /// </summary>
        public void OpenProfileModal(UserProfile profile)
        {
            if (profile != null)
            {
                lblModalContent.Text = "Edit Profile";
                hdnprofileId.Value = profile.ProfileId.ToString();
                lblProfileId.Text = profile.ProfileId.ToString();
                txtName.Text = profile.Name;
                txtAddress.Text = profile.Address;
                txtEmail.Text = profile.Email;
                txtMobile.Text = profile.Mobile;
                txtStatus.Text = profile.IsActive;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "showModal()", true);
            }
        }

        /// <summary>
        /// Initializes the Add Profile modal by setting default values for the controls and showing the modal.
        /// </summary>
        public void InitializeAddProfileModal()
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
    }
}