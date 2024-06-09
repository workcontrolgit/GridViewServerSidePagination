using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormBoostrap.Business;

namespace WebFormBoostrap
{
    public partial class Profile : System.Web.UI.Page
    {
        //private string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            gvProfile.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        }

        protected void gvProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowMoreInfo")
            {
                // Get the ProfileId from the CommandArgument
                int profileId = Convert.ToInt32(e.CommandArgument);

                // Retrieve the data for the selected profile
                // You can use your data access method to get the profile details
                // For example:
                var profile = GetProfileById(profileId);

                if (profile != null)
                {
                    // Set the modal content
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "$('#profileModal').modal('show');", true);
                    hdnprofileId.Value = profile.ProfileId.ToString();
                    txtName.Text = profile.Name;
                    txtAddress.Text = profile.Address;
                    txtEmail.Text = profile.Email;
                    txtMobile.Text = profile.Mobile;
                    txtStatus.Text = profile.IsActive;
                    //upnEmployee.Update();
                }
            }
        }

        protected void gvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Button lb = e.Row.FindControl("btnMoreInfo") as Button;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
        }

        protected void SaveProfile(object sender, EventArgs e)
        {
            var _profileId = hdnprofileId.Value;
            var _name = txtName.Text;
            var _address = txtAddress.Text;
            var _email = txtEmail.Text;
            var _mobile = txtMobile.Text;
            var _status = txtStatus.Text;

            if (string.IsNullOrEmpty(_profileId))
            {
                CreateProfile(_name, _address, _email, _mobile, _status);
            }
            else
            {
                UpdateProfile(Convert.ToInt32(hdnprofileId.Value), _name, _address, _email, _mobile, _status);
            }

            gvProfile.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "$('#profileModal').modal('hide');", true);
        }

        private void CreateProfile(string name, string address, string email, string mobile, string status)
        {
            var profile = new Business.UserProfile
            {
                Name = name,
                Address = address,
                Email = email,
                Mobile = mobile,
                IsActive = status
            };
            new ProfileRepository().CreateProfile(profile);
        }

        private void UpdateProfile(int profileId, string name, string address, string email, string mobile, string status)
        {
            var profile = new Business.UserProfile
            {
                ProfileId = profileId,
                Name = name,
                Address = address,
                Email = email,
                Mobile = mobile,
                IsActive = status
            };
            new ProfileRepository().UpdateProfile(profile);
        }

        private UserProfile GetProfileById(int profileId)
        {
            return new ProfileRepository().GetProfileById(profileId);
        }
    }
}