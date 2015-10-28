<%@ Page Title="CRM Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRMInquiry.aspx.vb" Inherits="RWACApplication.CRMInquiry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">  
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
