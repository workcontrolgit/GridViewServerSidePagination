using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewServerSidePagination
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
                string profileId = e.CommandArgument.ToString();

                // Retrieve the data for the selected profile
                // You can use your data access method to get the profile details
                // For example:
                var profile = GetProfileById(profileId);

                if (profile != null)
                {
                    // Set the modal content
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", $"showModal('{profile.ProfileId}', '{profile.Name}', '{profile.Address}', '{profile.Email}', '{profile.Mobile}', '{profile.IsActive}');", true);
                }
            }
        }

        protected void gvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Button lb = e.Row.FindControl("btnMoreInfo") as Button;
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
        }

        private App_Code.Profile GetProfileById(string profileId)
        {
            // Replace this with your actual data access code
            // This is just a placeholder
            return new App_Code.Profile
            {
                ProfileId = profileId,
                Name = "John Doe",
                Address = "123 Main St",
                Email = "john.doe@example.com",
                Mobile = "123-456-7890",
                IsActive = "Active"
            };
        }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsPermanent { get; set; }
        public int RegdNo { get; set; }
        public int Salary { get; set; }
        public string ProfileURL { get; set; }
    }
}