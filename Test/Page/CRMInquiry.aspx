<%@ Page Title="CRM Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRMInquiry.aspx.vb" Inherits="Test.CRMInquiry" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type = "text/javascript" src = "scripts/jquery-1.3.2.min.js"></script>
    <script type = "text/javascript" src = "scripts/jquery.blockUI.js"></script>
    <script type = "text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<table align = "center"><tr><td>' +
                     '<img src="images/loadingAnim.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: {
                        backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB'
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        $(document).ready(function () {

            BlockUI("Grid");
            $.blockUI.defaults.css = {};
        });
    </script>



    

    <h4><%: Title %>
    </h4>
    
    <div class="table-responsive">  
        
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15"
            OnRowCancelingEdit ="grd_RowCancelEditing" OnRowEditing ="grd_RowEditing" OnRowUpdating ="grd_RowUpdating">  
            <Columns>
                <asp:BoundField DataField="Counter_Party_ID" HeaderText="Counter_Party_ID" SortExpression="Counter_Party_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Counter_Party_Name" HeaderText="Counter_Party_Name" SortExpression="Counter_Party_Name" ReadOnly="True"/>
                <asp:BoundField DataField="Collateral_ID" HeaderText="Collateral_ID" SortExpression="Collateral_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Owner_Guarantor_Name" HeaderText="Owner_Guarantor_Name" SortExpression="Owner_Guarantor_Name" ReadOnly="True"/>
                <asp:BoundField DataField="Collateral_Type" HeaderText="Collateral_Type" SortExpression="Collateral_Type" ReadOnly="True"/>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ReadOnly="True"/>
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" ReadOnly="True"/>
                <asp:BoundField DataField="Start_Date" HeaderText="Start_Date" SortExpression="Start_Date" ReadOnly="True"/>
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" SortExpression="Maturity_Date" ReadOnly="True"/>
                <asp:BoundField DataField="Remaining_Term" HeaderText="Remaining_Term" SortExpression="Remaining_Term" ReadOnly="True"/>
                <asp:BoundField DataField="Credit_Risk_Type" HeaderText="Credit_Risk_Type" SortExpression="Credit_Risk_Type" ReadOnly="True"/>
                <asp:CheckBoxField DataField="CRM_Use_Flag" HeaderText="CRM_Use_Flag(*)" SortExpression="CRM_Use_Flag" />
                <asp:BoundField DataField="CRM_Revalue_Term" HeaderText="CRM_Revalue_Term(*)" SortExpression="CRM_Revalue_Term" />
                <asp:BoundField DataField="CRM_Holding_Period" HeaderText="CRM_Holding_Period(*)" SortExpression="CRM_Holding_Period" />
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" ReadOnly="True"/>
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

