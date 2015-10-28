<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRM_Inquiry.aspx.vb" Inherits="CRM_Inquiry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Counter_Party_ID" HeaderText="Counter_Party_ID" SortExpression="Counter_Party_ID" />
                <asp:BoundField DataField="Counter_Party_Name" HeaderText="Counter_Party_Name" SortExpression="Counter_Party_Name" />
                <asp:BoundField DataField="Collateral_ID" HeaderText="Collateral_ID" SortExpression="Collateral_ID" />
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" />
                <asp:BoundField DataField="Owner_Guarantor_Name" HeaderText="Owner_Guarantor_Name" SortExpression="Owner_Guarantor_Name" />
                <asp:BoundField DataField="Collateral_Type" HeaderText="Collateral_Type" SortExpression="Collateral_Type" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                <asp:BoundField DataField="Start_Date" HeaderText="Start_Date" SortExpression="Start_Date" />
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" ReadOnly="True" SortExpression="Maturity_Date" />
                <asp:BoundField DataField="Remaining_Term" HeaderText="Remaining_Term" SortExpression="Remaining_Term" />
                <asp:BoundField DataField="Credit_Risk_Type" HeaderText="Credit_Risk_Type" SortExpression="Credit_Risk_Type" />
                <asp:CheckBoxField DataField="CRM_Use_Flag" HeaderText="CRM_Use_Flag(*)" SortExpression="CRM_Use_Flag" />
                <asp:BoundField DataField="CRM_Revalue_Term" HeaderText="CRM_Revalue_Term(*)" SortExpression="CRM_Revalue_Term" />
                <asp:BoundField DataField="CRM_Holding_Period" HeaderText="CRM_Holding_Period(*)" SortExpression="CRM_Holding_Period" />
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" />
                <asp:CommandField ShowEditButton="True" />

            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Counter_Party_ID, Counter_Party_Name, Collateral_ID, Facility_ID, Owner_Guarantor_Name, Collateral_Type, Amount, [Currency], Start_Date, format(Maturity_Date,'Short Date') as Maturity_Date, Remaining_Term, Credit_Risk_Type, CRM_Use_Flag, CRM_Revalue_Term, CRM_Holding_Period, Credit_Risk_Subtype FROM tbl_CRM_Master"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
