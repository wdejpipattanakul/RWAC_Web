<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalculateRWA.aspx.vb" Inherits="CalculateRWA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Contract_ID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" />
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" />
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" />
                <asp:BoundField DataField="Contract_ID" HeaderText="Contract_ID" ReadOnly="True" SortExpression="Contract_ID" />
                <asp:BoundField DataField="DealDate" HeaderText="DealDate" ReadOnly="True" SortExpression="DealDate" />
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" ReadOnly="True" SortExpression="Maturity_Date" />
                <asp:BoundField DataField="Credit_Exposure" HeaderText="Credit_Exposure" SortExpression="Credit_Exposure" />
                <asp:BoundField DataField="Calculation_Method" HeaderText="Calculation_Method" SortExpression="Calculation_Method" />
                <asp:BoundField DataField="CCF" HeaderText="CCF" SortExpression="CCF" />
                <asp:BoundField DataField="PCE" HeaderText="PCE" SortExpression="PCE" />
                <asp:BoundField DataField="Trans_He_Value" HeaderText="Trans_He_Value" SortExpression="Trans_He_Value" />
                <asp:BoundField DataField="RW" HeaderText="RW" SortExpression="RW" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                <asp:BoundField DataField="Amount_THB" HeaderText="Amount_THB" SortExpression="Amount_THB" />
                <asp:BoundField DataField="Adjust_Accrued" HeaderText="Adjust_Accrued" SortExpression="Adjust_Accrued" />
                <asp:BoundField DataField="Specific_Provision" HeaderText="Specific_Provision" SortExpression="Specific_Provision" />
                <asp:BoundField DataField="Allocate_Asset_Amount" HeaderText="Allocate_Asset_Amount" SortExpression="Allocate_Asset_Amount" />
                <asp:BoundField DataField="RW_Funding" HeaderText="RW_Funding" SortExpression="RW_Funding" />
                <asp:BoundField DataField="Funding_Allocated_Amount" HeaderText="Funding_Allocated_Amount" SortExpression="Funding_Allocated_Amount" />
                <asp:BoundField DataField="CRM_Allocated_Amount" HeaderText="CRM_Allocated_Amount" SortExpression="CRM_Allocated_Amount" />
                <asp:BoundField DataField="RWA_Collateral" HeaderText="RWA_Collateral" SortExpression="RWA_Collateral" />
                <asp:BoundField DataField="RWA_No_Collateral" HeaderText="RWA_No_Collateral" SortExpression="RWA_No_Collateral" />
                <asp:BoundField DataField="RWA" HeaderText="RWA" SortExpression="RWA" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Cust_ID, Cust_Name, Facility_ID, Contract_ID, format(DealDate,'Short Date') AS DealDate, format(Maturity_Date,'Short Date') AS Maturity_Date, Credit_Exposure, Calculation_Method, CCF, PCE, Trans_He_Value, RW, [Currency], Amount, Amount_THB, Adjust_Accrued, Specific_Provision, Allocate_Asset_Amount, RW_Funding, Funding_Allocated_Amount, CRM_Allocated_Amount, RWA_Collateral, RWA_No_Collateral, RWA FROM tbl_RWAC_Master"></asp:SqlDataSource>
    </form>
</body>
</html>
