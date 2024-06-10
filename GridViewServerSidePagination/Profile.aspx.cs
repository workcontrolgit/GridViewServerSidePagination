using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormBoostrap.BusinessLayer.Models;
using WebFormBoostrap.BusinessLayer.Services;

namespace WebFormBoostrap
{
    public partial class Profile : System.Web.UI.Page
    {
        private readonly ProfileService _profileService;

        public Profile()
        {
            _profileService = new ProfileService();
        }

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            gvProfile.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        }

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
                _profileService.DeleteProfile(profileId);
                gvProfile.DataBind();
            }
        }

        protected void gvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Button lb = e.Row.FindControl("btnEdit") as Button;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
        }

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
            upnContent.Update();
        }

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
    }
}