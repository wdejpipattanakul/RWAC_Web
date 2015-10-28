<%@ Page Title="CRS Report Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CalculateRW.aspx.vb" Inherits="RWACApplication.CalculateRW" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Reference" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5">  
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
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>
        <div style="font-size:x-small" hidden="hidden">
            <i>You are viewing page
            <%=grdTran.PageIndex + 1%>
            of
            <%=grdTran.PageCount%>
            </i> 
        </div>       
    </div>
</asp:Content>
