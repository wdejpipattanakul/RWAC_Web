<%@ Page Title="Allocate Funding" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AllocateFunding.aspx.vb" Inherits="RWACApplication.AllocateFunding" MaintainScrollPositionOnPostBack = "true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    
    Cust_ID : <asp:DropDownList ID="ddlCustID" runat="server" Width ="400px" AutoPostBack ="True" AppendDataBoundItems="true">
              <asp:ListItem Text="" Value="Empthy" />
              </asp:DropDownList>  
              Currrency : <asp:DropDownList ID="ddlCurrency" runat="server" Width ="75px" AutoPostBack ="True" AppendDataBoundItems="false"> 
              <asp:ListItem Text="" Value="Empthy" />
              </asp:DropDownList> &nbsp
              <asp:Button ID="Search" runat="server" Text="Search" /> &nbsp
              <asp:Button ID="Clear" runat="server" Text="Clear" />
    <br/>
    
    <br/>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
            DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15"
            OnRowCancelingEdit ="grd_RowCancelEditing" OnRowEditing ="grd_RowEditing" OnRowUpdating ="grd_RowUpdating">  
            <Columns>
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" ReadOnly="true"/>
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" ReadOnly="True"/>
                <asp:BoundField DataField="Contract_ID" HeaderText="Contract_ID" SortExpression="Contract_ID" ReadOnly="True" />
                <asp:BoundField DataField="FX_AND_Original_Currency" HeaderText="CCY" SortExpression="FX_AND_Original_Currency" ReadOnly="True"/>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ReadOnly="True"/>
                <asp:BoundField DataField="Credit_Exposure" HeaderText="Credit_Exposure" SortExpression="Credit_Exposure" ReadOnly="True"/>
                <asp:BoundField DataField="Credit_Type" HeaderText="Credit_Type" SortExpression="Credit_Type" ReadOnly="True"/>
                <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="Credit_Risk_Subtype" SortExpression="Credit_Risk_Subtype" ReadOnly="True"/>
                <asp:BoundField DataField="Remaining_Date" HeaderText="Contract_Prd" SortExpression="Remaining_Date" ReadOnly="True"/>
                <asp:BoundField DataField="Allocate_Asset_Amount" HeaderText="Allocate_Asset_Amount" SortExpression="Allocate_Asset_Amount"/>
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
        
    <br/>
    <h4>Funding List</h4>
    
    Currrency : <asp:DropDownList ID="ddlListCurrency" runat="server" Width ="75px" AutoPostBack ="True" AppendDataBoundItems="true"> 
              <asp:ListItem Text="" Value="Empthy" />
              </asp:DropDownList> Remaining Amount : <asp:TextBox ID="txtRemainingAmount" runat="server" style="text-align: right" ReadOnly="True"></asp:TextBox>
              <asp:TextBox ID="txtSearch" runat="server" style="text-align: right" Visible="False"></asp:TextBox>
    <br/><br/>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran2" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="50%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
            EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15"
            >  
            <Columns>
                <asp:BoundField DataField="AcctID" HeaderText="Account_ID" SortExpression="AcctID" ReadOnly="true"/>
                <asp:BoundField DataField="CCY" HeaderText="Currency" SortExpression="CCY" ReadOnly="True"/>
                <asp:BoundField DataField="COA" HeaderText="COA" SortExpression="COA" ReadOnly="True" />
                <asp:BoundField DataField="bal" HeaderText="Amount" SortExpression="bal" ReadOnly="True"/>
                
                
                                                
            </Columns>  
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>
        <div style="font-size:x-small" hidden="hidden">
            <i>You are viewing page
            <%=grdTran2.PageIndex + 1%>
            of
            <%=grdTran2.PageCount%>
            </i> 
        </div>       
    </div>
        
    <br/>

</asp:Content>
