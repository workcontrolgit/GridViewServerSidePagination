<%@ Page Title="Employee Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebFormBoostrap.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function showModal() {
            $('#profileModal').modal('show');
        }
        function hideModal() {
            $('.modal').remove();
            $('.modal-backdrop').remove();
        }    
        function confirmDelete() {
            return confirm("Are you sure you want to delete this record?");
        }
    </script>
    <asp:UpdatePanel ID="upnContent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="d-flex justify-content-between align-items-center mb-3">
                <asp:Button ID="btnAddProfile" runat="server" Text="Add Profile" OnClick="btnAddProfile_Click" CssClass="btn btn-primary" />
                <div>
                    <label class="mr-2">Page Size:</label>
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed" CssClass="form-control d-inline-block w-auto">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="25" Value="25" />
                        <asp:ListItem Text="50" Value="50" />
                    </asp:DropDownList>
                </div>
            </div>

                    <asp:GridView ID="gvProfile" DataSourceID="profileDataSource" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProfile_RowDataBound" OnRowCommand="gvProfile_RowCommand"
                        AllowPaging="true" PagerSettings-Mode="NextPreviousFirstLast" AllowSorting="true" CssClass="table table-striped table-bordered table-hover mt-3" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Previous" PagerSettings-Visible="True">

                        <Columns>
                            <asp:BoundField DataField="ProfileId" HeaderText="Profile Id" SortExpression="ProfileId" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-info" CausesValidation="false"
                                        CommandName="EditRow" CommandArgument='<%# Eval("ProfileId") %>' />
<asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("ProfileId") %>' Text="Delete" CssClass="btn btn-danger" OnClientClick="return confirmDelete();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
<!-- Modal -->
<div class="modal fade" id="profileModal" tabindex="-1" role="dialog" aria-labelledby="profileModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="profileModalLabel"><asp:Label ID="lblModalContent" runat="server" Text="Initial Content" CssClass="form-label"></asp:Label></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                
                <asp:HiddenField ID="hdnprofileId" runat="server" />
                <div class="form-group">
                    <label for="lblProfileId">Profile Id</label>
                    <asp:Label ID="lblProfileId" runat="server" readonly  CssClass="form-control"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:ObjectDataSource ID="profileDataSource" runat="server" SelectMethod="GetProfiles" EnablePaging="true" MaximumRowsParameterName="pageSize"
        StartRowIndexParameterName="startRowIndex" TypeName="WebFormBoostrap.DataLayer.ProfileRepository" SelectCountMethod="TotalRowCount" 
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
