<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RW_CRM_Calculation.aspx.vb" Inherits="RW_CRM_Calculation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="3200px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Counter_Party_ID" HeaderText="Counter_Party_ID" SortExpression="Counter_Party_ID" />
                <asp:BoundField DataField="Counter_Party_Name" HeaderText="Counter_Party_Name" SortExpression="Counter_Party_Name" />
                <asp:BoundField DataField="Collateral_ID" HeaderText="Collateral_ID" SortExpression="Collateral_ID" />
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" />
                <asp:BoundField DataField="Collateral_Type" HeaderText="Collateral_Type" SortExpression="Collateral_Type" />
                <asp:BoundField DataField="Owner_Guarantor_Name" HeaderText="Owner_Guarantor_Name" SortExpression="Owner_Guarantor_Name" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                <asp:BoundField DataField="Remaining_Date" HeaderText="Remaining_Date" SortExpression="Remaining_Date" />
                <asp:BoundField DataField="Start_Date" HeaderText="Start_Date" ReadOnly="True" SortExpression="Start_Date" />
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" ReadOnly="True" SortExpression="Maturity_Date" />
                <asp:BoundField DataField="Credit_Risk_Type" HeaderText="Credit_Risk_Type" SortExpression="Credit_Risk_Type" />
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" />
                <asp:BoundField DataField="Rating_Group" HeaderText="Rating_Group(*)" SortExpression="Rating_Group" />
                <asp:BoundField DataField="ECAI_Currency" HeaderText="ECAI_Currency(*)" SortExpression="ECAI_Currency" />
                <asp:BoundField DataField="ECAI_Term" HeaderText="ECAI_Term(*)" SortExpression="ECAI_Term" />
                <asp:BoundField DataField="ECAI_Name" HeaderText="ECAI_Name(*)" SortExpression="ECAI_Name" />
                <asp:BoundField DataField="ECAI_Value" HeaderText="ECAI_Value(*)" SortExpression="ECAI_Value" />
                <asp:BoundField DataField="Rating_Grade" HeaderText="Rating_Grade(*)" SortExpression="Rating_Grade" />
                <asp:BoundField DataField="CRM_Hc_Value" HeaderText="CRM_Hc_Value" SortExpression="CRM_Hc_Value" />
                <asp:BoundField DataField="RW_Collateral" HeaderText="RW_Collateral(*)" SortExpression="RW_Collateral" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Counter_Party_ID, Counter_Party_Name, Collateral_ID, Facility_ID, Collateral_Type, Owner_Guarantor_Name, Amount, [Currency], Remaining_Date, format(Start_Date, 'Short Date') AS Start_Date, format(Maturity_Date, 'Short Date') AS Maturity_Date, Credit_Risk_Type, Credit_Risk_Subtype, Rating_Group, ECAI_Currency, ECAI_Term, ECAI_Name, ECAI_Value, Rating_Grade, CRM_Hc_Value, RW_Collateral FROM tbl_CRM_Master"></asp:SqlDataSource>
    </form>
</body>
</html>
