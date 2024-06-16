<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteModal.ascx.cs" Inherits="AspNetWebformSample.Controls.DeleteModal" %>

<script>
    function showDeleteModal() {
        $('#deleteModal').modal('show');
    }
    function hideDeleteModal() {
        $('#deleteModal').modal('hide');
        $('.modal-backdrop').remove();
    }
</script>

<div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="deleteModalLabel">Delete Confirmation</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
<asp:Label ID="lblProfileId" runat="server" CssClass="font-weight-bold"></asp:Label>
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="Are you sure you want to delete this profile?"></asp:Label>
                
                <asp:HiddenField ID="hdnDeleteProfileId" runat="server" />
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" CausesValidation="false" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>