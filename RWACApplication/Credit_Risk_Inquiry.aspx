<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Credit_Risk_Inquiry.aspx.vb" Inherits="Credit_Risk_Inquiry" %>

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
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" />
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" />
                <asp:BoundField DataField="Contract_ID" HeaderText="Contract_ID" ReadOnly="True" SortExpression="Contract_ID" />
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" />
                <asp:BoundField DataField="COA" HeaderText="COA" SortExpression="COA" />
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" ReadOnly="True" SortExpression="Maturity_Date" />
                <asp:BoundField DataField="Remaining_Term" HeaderText="Remaining_Term" SortExpression="Remaining_Term" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" />
                <asp:BoundField DataField="Amount_THB" HeaderText="Amount_THB" SortExpression="Amount_THB" />
                <asp:BoundField DataField="Accrued" HeaderText="Accrued" SortExpression="Accrued" />
                <asp:BoundField DataField="Adjust_Accrued" HeaderText="Adjust_Accrued" SortExpression="Adjust_Accrued" />
                <asp:BoundField DataField="Specific_Provision" HeaderText="Specific_Provision" SortExpression="Specific_Provision" />
                <asp:BoundField DataField="Net_Balance" HeaderText="Net_Balance" SortExpression="Net_Balance" />
                <asp:CheckBoxField DataField="Trading_Book_Flag" HeaderText="Trading_Book_Flag" SortExpression="Trading_Book_Flag" />
                <asp:BoundField DataField="Credit_Exposure" HeaderText="Credit_Exposure" SortExpression="Credit_Exposure" />
                <asp:BoundField DataField="Credit_Type" HeaderText="Credit_Type" SortExpression="Credit_Type" />
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Cust_ID, Cust_Name, Contract_ID, Facility_ID, COA, format(Maturity_Date,'Short Date') AS Maturity_Date, Remaining_Term, Amount, [Currency], Amount_THB, Accrued, Adjust_Accrued, Specific_Provision, Net_Balance, Trading_Book_Flag, Credit_Exposure, Credit_Type, Credit_Risk_Subtype FROM tbl_RWAC_Master"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
