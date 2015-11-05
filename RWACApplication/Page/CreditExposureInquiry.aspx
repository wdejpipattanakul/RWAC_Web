<%@ Page Title="Credit Exposure Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreditExposureInquiry.aspx.vb" Inherits="RWACApplication.CreditExposureInquiry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
        <asp:Button ID="UpdateNetting" runat="server" Text="Update FX Netting Agreement" Width="215px" />
    &nbsp
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
            DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15"
            OnRowCancelingEdit ="grd_RowCancelEditing" OnRowEditing ="grd_RowEditing" OnRowUpdating ="grd_RowUpdating">  
            <Columns>
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" ReadOnly="true"/>
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" ReadOnly="True"/>
                <asp:BoundField DataField="Contract_ID" HeaderText="Contract_ID" SortExpression="Contract_ID" ReadOnly="True" />
                <asp:BoundField DataField="Facility_ID" HeaderText="Facility_ID" SortExpression="Facility_ID" ReadOnly="True"/>
                <asp:BoundField DataField="COA" HeaderText="COA" SortExpression="COA" ReadOnly="True"/>
                <asp:BoundField DataField="Maturity_Date" HeaderText="Maturity_Date" SortExpression="Maturity_Date" ReadOnly="True"/>
                <asp:BoundField DataField="Remaining_Term" HeaderText="Remaining_Term" SortExpression="Remaining_Term" ReadOnly="True"/>
                <asp:BoundField DataField="CCF" HeaderText="CCF" SortExpression="CCF" ReadOnly="True"/>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ReadOnly="True"/>
                <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" ReadOnly="True"/>
                <asp:BoundField DataField="Amount_THB" HeaderText="Amount_THB" SortExpression="Amount_THB" ReadOnly="True"/>
                <asp:BoundField DataField="Accrued" HeaderText="Accrued" SortExpression="Accrued" ReadOnly="True"/>
                <asp:BoundField DataField="Adjust_Accrued" HeaderText="Adjust_Accrued(*)" SortExpression="Adjust_Accrued" />
                <asp:BoundField DataField="Specific_Provision" HeaderText="Specific_Provision(*)" SortExpression="Specific_Provision" />
                <asp:BoundField DataField="CEA" HeaderText="CEA" SortExpression="CEA" ReadOnly="True"/>
                <asp:BoundField DataField="PCE" HeaderText="PCE" SortExpression="PCE" ReadOnly="True"/>
                <asp:BoundField DataField="Net_Balance" HeaderText="Net_Balance" SortExpression="Net_Balance" ReadOnly="True"/>
                <asp:CheckBoxField DataField="NPL_Flag" HeaderText="NPL_Flag" SortExpression="NPL_Flag" ReadOnly="True"/>
                <asp:CheckBoxField DataField="Trading_Book_Flag" HeaderText="Trading_Book_Flag(*)" SortExpression="Trading_Book_Flag" />
                <asp:CheckBoxField DataField="FX_Netting_Flag" HeaderText="FX_Netting_Flag(*)" SortExpression="FX_Netting_Flag" />
                <%--<asp:TemplateField DataField="Credit_Exposure" HeaderText="Credit_Exposure(*)" SortExpression="Credit_Exposure" />--%>
                
                <asp:TemplateField HeaderText="Credit_Exposure">
                <EditItemTemplate>
		            <asp:DropDownList ID="ddlCredit_Exposure" runat="server" SelectedValue='<%# Eval("Credit_Exposure")%>'>
			            <asp:ListItem>On-Bal</asp:ListItem>
                        <asp:ListItem>Off-Bal Non-Derivative</asp:ListItem>
                        <asp:ListItem>Derivative</asp:ListItem>
                        <asp:ListItem>Non-DVP</asp:ListItem>
                        <asp:ListItem>DVP</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
		            </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Credit_Exposure" runat="server" Text='<%# Eval("Credit_Exposure")%>'></asp:Label>
		           <%-- <asp:DropDownList ID="ddlCredit_Exposure" runat="server" SelectedValue='<%# Eval("Credit_Exposure")%>'>
			            <asp:ListItem>On-Bal</asp:ListItem>
                        <asp:ListItem>Off-Bal Non-Derivative</asp:ListItem>
                        <asp:ListItem>Derivative</asp:ListItem>
                        <asp:ListItem>Non-DVP</asp:ListItem>
                        <asp:ListItem>DVP</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
		            </asp:DropDownList>--%>
                </ItemTemplate>
                </asp:TemplateField>


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
