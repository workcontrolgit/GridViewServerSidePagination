<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EffectivePaging.aspx.cs"
    Inherits="EffectivePaging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <center>
    <h4 style="font-family:Verdana;color:Purple">Effective Paging</h4>
    <div>
        <asp:GridView ID="gvProfile" DataSourceID="profileDataSource" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" AllowSorting="true" PageSize="5" HeaderStyle-Font-Names="Verdana" 
            Font-Size="Small" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Underline="false"
            Width="55%" HeaderStyle-BackColor="BurlyWood" HeaderStyle-ForeColor="Navy">
            <AlternatingRowStyle BackColor="Aquamarine" />
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
    </div>
    </center>
    <asp:ObjectDataSource ID="profileDataSource" runat="server" SelectMethod="GetProfileData" EnablePaging="true" MaximumRowsParameterName="pageSize"
        StartRowIndexParameterName="startRowIndex" TypeName="WebFormBoostrap.ProfileDataSource"  SelectCountMethod="TotalRowCount" 
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="pageSize" Type="Int32"/>
            <asp:Parameter Name="sortExpression" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
