<%@ Page Title="Employee Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="AspNetWebformSample.Profile" %>

<%@ Register Src="~/Controls/ProfileModal.ascx" TagPrefix="uc" TagName="ProfileModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function showDeleteModal() {
            $('#deleteModal').modal('show');
        }
        function hideDeleteModal() {
            $('#deleteModal').modal('hide');
            $('.modal-backdrop').remove();
        }
    </script>
    <asp:UpdatePanel ID="upnContent" runat="server">
        <ContentTemplate>
            <div class="d-flex justify-content-end align-items-center mb-3">
                <asp:Button ID="btnAddProfile" runat="server" Text="Add Profile" OnClick="btnAddProfile_Click" CssClass="btn btn-primary" />
            </div>

            <asp:GridView ID="gvProfile" DataSourceID="profileDataSource" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProfile_RowDataBound" OnRowCommand="gvProfile_RowCommand"
                AllowPaging="true" PagerSettings-Mode="NextPreviousFirstLast" AllowSorting="true" CssClass="table table-striped table-bordered table-hover mt-3" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Previous" PagerSettings-Visible="True">

                <PagerTemplate>
                    <div class="d-flex justify-content-between align-items-center w-100">
                        <div class="d-flex align-items-center">
                            <label class="mr-2">Page Size:</label>
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed" CssClass="form-control d-inline-block w-auto">
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="25" Value="25" />
                                <asp:ListItem Text="50" Value="50" />
                            </asp:DropDownList>
                        </div>
                        <ul class="pagination mb-0">
                            <li class="page-item">
                                <asp:LinkButton ID="lnkFirst" runat="server" CommandName="Page" CommandArgument="First" CssClass="page-link">First</asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="page-link">Previous</asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged" CssClass="form-control d-inline-block w-auto"></asp:DropDownList>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CssClass="page-link">Next</asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkLast" runat="server" CommandName="Page" CommandArgument="Last" CssClass="page-link">Last</asp:LinkButton>
                            </li>
                        </ul>
                        <div class="d-flex align-items-center">
                            <span class="ml-2">Total Records: <asp:Label ID="lblTotalRecords" runat="server" /></span>
                        </div>
                    </div>
                </PagerTemplate>

                <Columns>
                    <asp:BoundField DataField="ProfileId" HeaderText="Profile Id" SortExpression="ProfileId" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-info" CausesValidation="false"
                                CommandName="EditRow" CommandArgument='<%# Eval("ProfileId") %>' />
                            <asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("ProfileId") %>' Text="Delete" CssClass="btn btn-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <!-- Profile Modal -->
            <uc:ProfileModal ID="ProfileModal" runat="server" />

            <!-- Delete Confirmation Modal -->
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
                            Are you sure you want to delete this profile?
                            <asp:HiddenField ID="hdnDeleteProfileId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="ConfirmDeleteProfile" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddProfile" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvProfile" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:ObjectDataSource ID="profileDataSource" runat="server" SelectMethod="GetProfiles" EnablePaging="true" MaximumRowsParameterName="pageSize"
        StartRowIndexParameterName="startRowIndex" TypeName="AspNetWebformSample.DataLayer.ProfileRepository" SelectCountMethod="TotalRowCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>