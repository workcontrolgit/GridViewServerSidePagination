using System;
using System.Web.UI;

namespace AspNetWebformSample.Controls
{
    public partial class DeleteModal : UserControl
    {
        /// <summary>
        /// Event that is raised when a delete action is confirmed.
        /// </summary>
        public event EventHandler DeleteConfirmed;

        /// <summary>
        /// Gets or sets the ProfileID value stored in a hidden field.
        /// </summary>
        public string ProfileID
        {
            get { return hdnDeleteProfileId.Value; }
            set { hdnDeleteProfileId.Value = value; }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            // Raise the event to notify the parent page
            DeleteConfirmed?.Invoke(this, EventArgs.Empty);
            CloseModal();
        }

        /// <summary>
        /// Closes a modal dialog by registering a startup script to hide the modal using ScriptManager.
        /// </summary>
        public void CloseModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModal", "hideDeleteModal()", true);
        }

        /// <summary>
        /// Sets the profile ID value to the hidden field and displays it in a label.
        /// </summary>
        /// <param name="profileId">The profile ID to be set.</param>
        public void SetProfileId(string profileId)
        {
            hdnDeleteProfileId.Value = profileId;
            lblProfileId.Text = "Profile ID: " + profileId;
        }

        /// <summary>
        /// Opens a modal dialog by registering a startup script to show the delete modal using ScriptManager.
        /// </summary>
        public void OpenModal()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "showDeleteModal()", true);
        }
    }
}