<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileModal.ascx.cs" Inherits="AspNetWebformSample.ProfileModal" %>

    <script>
        function showModal() {
            $('#profileModal').modal('show');
        }
        function hideModal() {
            $('.modal').remove();
            $('.modal-backdrop').remove();
        }
    </script>

<div class="modal fade" id="profileModal" tabindex="-1" role="dialog" aria-labelledby="profileModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="profileModalLabel">
                    <asp:Label ID="lblModalContent" runat="server" Text="Initial Content" CssClass="form-label"></asp:Label></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <asp:HiddenField ID="hdnprofileId" runat="server" />
                <div class="form-group">
                    <label for="lblProfileId">Profile Id</label>
                    <asp:Label ID="lblProfileId" runat="server" readonly CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group">
                    <label for="txtName">Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtAddress">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
<%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic" />--%>
                </div>
                <div class="form-group">
                    <label for="txtMobile">Mobile</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtStatus">Status</label>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="SaveProfile" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>