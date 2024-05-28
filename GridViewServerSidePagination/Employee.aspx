<%@ Page Title="Employee Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="GridViewServerSidePagination.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
    <asp:UpdatePanel ID="upnEmployee" runat="server">
        <ContentTemplate>
            PageSize:
            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="50" Value="50" />
            </asp:DropDownList>
            <hr />

<%--            <asp:GridView ID="grdEmployee" runat="server" AllowPaging="True"
                AutoGenerateColumns="False"
                PagerSettings-Visible="false" PagerSettings-Mode="NextPreviousFirstLast" CssClass="table table-striped" Visible="false">
                <Columns>
                    <asp:BoundField DataField="EmployeeId" HeaderText="Employee ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="DepartmentId" HeaderText="Department ID" />
                    <asp:BoundField DataField="PhotoPath" HeaderText="Photo Path" />
                </Columns>
            </asp:GridView>--%>

            <asp:Panel ID="pnlPager" runat="server" CssClass="pager" Visible="false">
                <%--<asp:LinkButton ID="btnPrevious" runat="server" Text="Previous" OnClick="Page_Changed" CommandArgument="Prev" CssClass="btn btn-secondary" />--%>
<%--                <asp:Repeater ID="rptPager" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>--%>
                <%--<asp:LinkButton ID="btnNext" runat="server" Text="Next" OnClick="Page_Changed" CommandArgument="Next" CssClass="btn btn-secondary" />--%>

            </asp:Panel>

        <asp:GridView ID="gvProfile" DataSourceID="profileDataSource" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PagerSettings-Mode="NextPreviousFirstLast" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Prev" AllowSorting="true" CssClass="table table-striped">
            
            <Columns>
                <asp:BoundField DataField="ProfileId" HeaderText="Profile Id" SortExpression="ProfileId"
                    ItemStyle-Width="6%" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-Width="13%" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address"
                    ItemStyle-Width="18%" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-Width="8%" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" ItemStyle-Width="9%" />
                <asp:BoundField DataField="IsActive" HeaderText="Status" SortExpression="IsActive" ItemStyle-Width="4%" />
            </Columns>
        </asp:GridView>
<asp:ObjectDataSource ID="profileDataSource" runat="server" SelectMethod="GetProfileData" EnablePaging="true" MaximumRowsParameterName="pageSize"
    StartRowIndexParameterName="startRowIndex" TypeName="GridViewServerSidePagination.ProfileDataSource"  SelectCountMethod="TotalRowCount" 
    SortParameterName="sortExpression">
    <SelectParameters>
        <asp:Parameter Name="startRowIndex" Type="Int32" />
        <asp:Parameter Name="pageSize" Type="Int32"/>
        <asp:Parameter Name="sortExpression" Type="String" />            
    </SelectParameters>
</asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>