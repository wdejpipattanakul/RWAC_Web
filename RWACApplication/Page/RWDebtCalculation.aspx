<%@ Page Title="Calculate RWA" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RWDebtCalculation.aspx.vb" Inherits="RWACApplication.RWDebtCalculation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">  
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

