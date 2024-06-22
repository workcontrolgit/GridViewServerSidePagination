<%@ Page Title="Profile Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="AspNetWebformSample.Profile" %>

<%@ Register Src="~/Controls/ProfileModal.ascx" TagPrefix="uc" TagName="ProfileModal" %>
<%@ Register Src="~/Controls/DeleteModal.ascx" TagPrefix="uc" TagName="DeleteModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<nav class="navbar navbar-light bg-light">
    <div class="container-fluid d-flex justify-content-end">
        <div class="navbar-nav d-flex flex-row">
            <div class="nav-item mr-3">
                <asp:Button ID="btnAddProfile" runat="server" Text="Add Profile" OnClick="btnAddProfile_Click" CssClass="btn btn-link  nav-link" CausesValidation="false" />
            </div>
            <div class="nav-item">
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="btn btn-link  nav-link" CausesValidation="false" OnClick="btnExportToExcel_Click" />
            </div>
        </div>
    </div>
</nav>


    <asp:UpdatePanel ID="upnContent" runat="server">
        <ContentTemplate>


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
                                <asp:LinkButton ID="lnkFirst" runat="server" CommandName="Page" CommandArgument="First" CssClass="page-link" CausesValidation="false">
                                    <i class="fa fa-angle-double-left"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="page-link" CausesValidation="false">
                                    <i class="fa fa-angle-left"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged" CssClass="form-control d-inline-block w-auto"></asp:DropDownList>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CssClass="page-link" CausesValidation="false">
                                    <i class="fa fa-angle-right"></i>
                                </asp:LinkButton>
                            </li>
                            <li class="page-item">
                                <asp:LinkButton ID="lnkLast" runat="server" CommandName="Page" CommandArgument="Last" CssClass="page-link" CausesValidation="false">
                                    <i class="fa fa-angle-double-right"></i>
                                </asp:LinkButton>
                            </li>
                        </ul>
                        <div class="d-flex align-items-center">
                            <span class="ml-2">Total Records:
                                <asp:Label ID="lblTotalRecords" runat="server" /></span>
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
                            <asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Eval("ProfileId") %>' Text="Delete" CssClass="btn btn-danger" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <!-- Profile Modal -->
            <uc:ProfileModal ID="ProfileModal" runat="server" />

            <!-- Delete Confirmation Modal -->
            <uc:DeleteModal ID="DeleteModal" runat="server" />
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