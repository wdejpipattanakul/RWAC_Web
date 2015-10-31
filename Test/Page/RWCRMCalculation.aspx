<%@ Page Title="RW CRM Calculation" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RWCRMCalculation.aspx.vb" Inherits="Test.RWCRMCalculation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>

    <asp:Button ID="RWCRMCalculate" runat="server" Text="Re-Calculate RW" Width="131px" Height="30px" />
    &nbsp
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">  
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
