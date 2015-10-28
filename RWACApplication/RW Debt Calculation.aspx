<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RW Debt Calculation.aspx.vb" Inherits="RW_Debt_Calculation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Contract_ID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="AOD" HeaderText="AOD" ReadOnly="True" SortExpression="AOD" />
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" />
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" />
                <asp:BoundField DataField="Contract_ID" HeaderText="Contract_ID" ReadOnly="True" SortExpression="Contract_ID" />
                <asp:BoundField DataField="COA" HeaderText="COA" SortExpression="COA" />
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" ReadOnly="True" SortExpression="Maturity_Date" />
                <asp:BoundField DataField="Remaining_Term" HeaderText="Remaining_Term" SortExpression="Remaining_Term" />
                <asp:BoundField DataField="Remaining_Date" HeaderText="Contract_Period" SortExpression="Remaining_Date" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                <asp:BoundField DataField="Amount_THB" HeaderText="Amount_THB" SortExpression="Amount_THB" />
                <asp:BoundField DataField="Accrued" HeaderText="Accrued" SortExpression="Accrued" />
                <asp:BoundField DataField="Adjust_Accrued" HeaderText="Adjust_Accrued" SortExpression="Adjust_Accrued" />
                <asp:BoundField DataField="Specific_Provision" HeaderText="Specific_Provision" SortExpression="Specific_Provision" />
                <asp:BoundField DataField="Credit_Exposure" HeaderText="Credit_Exposure" SortExpression="Credit_Exposure" />
                <asp:CheckBoxField DataField="NPL_Flag" HeaderText="NPL_Flag" SortExpression="NPL_Flag" />
                <asp:BoundField DataField="Credit_Type" HeaderText="Credit_Type" SortExpression="Credit_Type" />
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" />
                <asp:BoundField DataField="Rating_Group" HeaderText="Rating_Group(*)" SortExpression="Rating_Group" />
                <asp:BoundField DataField="Term" HeaderText="ECAI_Term(*)" SortExpression="Term" />
                <asp:BoundField DataField="ECAI_Currency" HeaderText="ECAI_Currency(*)" SortExpression="ECAI_Currency" />
                <asp:BoundField DataField="ECAI_Name" HeaderText="ECAI_Name(*)" SortExpression="ECAI_Name" />
                <asp:BoundField DataField="ECAI_Value" HeaderText="ECAI_Value(*)" SortExpression="ECAI_Value" />
                <asp:BoundField DataField="Rating_Grade" HeaderText="Rating_Grade(*)" SortExpression="Rating_Grade" />
                <asp:BoundField DataField="RW" HeaderText="RW" SortExpression="RW" />
                <asp:BoundField DataField="CEA" HeaderText="CEA" SortExpression="CEA" />
                <asp:BoundField DataField="PCE" HeaderText="PCE" SortExpression="PCE" />
                <asp:BoundField DataField="RWA" HeaderText="RWA" SortExpression="RWA" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT format(AOD,'Short Date') AS AOD, Cust_ID, Cust_Name, Contract_ID, COA, format(Maturity_Date ,'Short Date') AS Maturity_Date, Remaining_Term, Remaining_Date, Amount, [Currency], Amount_THB, Accrued, Adjust_Accrued, Specific_Provision, Credit_Exposure, NPL_Flag, Credit_Type, Credit_Risk_Subtype, Rating_Group, Term, ECAI_Currency, ECAI_Name, ECAI_Value, Rating_Grade, RW, CEA, PCE, RWA FROM tbl_RWAC_Master"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
