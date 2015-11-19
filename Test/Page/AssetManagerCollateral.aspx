<%@ Page Title="Asset Manager" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssetManagerCollateral.aspx.vb" Inherits="Test.AssetManagerCollateral" EnableEventValidation="false"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <h4><%: Title %></h4>
        
    &nbsp
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False"
            EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15"
            OnRowCancelingEdit ="grd_RowCancelEditing" OnRowEditing ="grd_RowEditing" OnRowUpdating ="grd_RowUpdating"
            OnSelectedIndexChanged = "grd_OnSelectedIndexChanged" OnRowDataBound = "grd_OnRowDataBound"
           >    
            <Columns>
                <asp:BoundField DataField="Cust_ID" HeaderText="Cust_ID" SortExpression="Cust_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" SortExpression="Cust_Name" ReadOnly="True"/>
                
                <asp:TemplateField HeaderText="Asset_Classification">
                <EditItemTemplate>
		            <asp:DropDownList ID="ddlAsset_Classification" runat="server" SelectedValue='<%# Eval("Asset_Classification")%>'>
                        <asp:ListItem Value ="020001">020001</asp:ListItem>
                        <asp:ListItem Value ="020002">020002</asp:ListItem>
                        <asp:ListItem Value ="020003">020003</asp:ListItem>
                        <asp:ListItem Value ="020004">020004</asp:ListItem>
                        <asp:ListItem Value ="020005">020005</asp:ListItem>
                        <asp:ListItem Value ="020006">020006</asp:ListItem>
			            <%--<asp:ListItem Value ="020001">Normal</asp:ListItem>
                        <asp:ListItem Value ="020002">Special Mention</asp:ListItem>
                        <asp:ListItem Value ="020003">Substandard</asp:ListItem>
                        <asp:ListItem Value ="020004">Doubtful</asp:ListItem>
                        <asp:ListItem Value ="020005">Doubtful of Loss</asp:ListItem>
                        <asp:ListItem Value ="020006">Loss</asp:ListItem>--%>
                        <asp:ListItem></asp:ListItem>
		            </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Asset_Classification" runat="server" Text='<%# Eval("Asset_Classification")%>'></asp:Label>
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

                <asp:CheckBoxField DataField="NPL_Flag" HeaderText="NPL_Flag" SortExpression="NPL_Flag"/>
                <asp:BoundField DataField="General_Provision_Factor" HeaderText="General_Provision_Factor" SortExpression="General_Provision_Factor"/>
                <asp:BoundField DataField="NPL_Start_Date" HeaderText="NPL_Start_Date" SortExpression="NPL_Start_Date"/>
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

    <asp:TextBox ID="txtSearch" runat="server" style="text-align: right" Visible="False"></asp:TextBox>
    List : <asp:DropDownList ID="ddlType" runat="server" Width ="200px" AutoPostBack ="True" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
              <asp:ListItem Text="Transaction" Value="Transaction" />
              <asp:ListItem Text="Collateral" Value="Collateral" Selected="true"/>
              </asp:DropDownList>  
    <br/><br/>
    <div class="table-responsive">  
        <asp:GridView ID="grdTran2" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false" 
            EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15"
            OnRowCancelingEdit ="grd_RowCancelEditing2" OnRowEditing ="grd_RowEditing2" OnRowUpdating ="grd_RowUpdating2"
            OnRowCreated ="grd_RowCreated2" OnRowDataBound ="grd_RowDataBound2"
           >    
            <Columns>
                <asp:BoundField DataField="Counter_Party_ID" HeaderText="Counter_Party_ID" SortExpression="Counter_Party_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Collateral_ID" HeaderText="Collateral_ID" SortExpression="Collateral_ID" ReadOnly="True"/>
                <asp:BoundField DataField="Counter_Party_Name" HeaderText="Counter_Party_Name" SortExpression="Counter_Party_Name" ReadOnly="True"/>
                <asp:BoundField DataField="Owner_Guarantor_Name" HeaderText="Owner_Guarantor_Name" SortExpression="Owner_Guarantor_Name" ReadOnly="True"/>
                <asp:TemplateField HeaderText="Asset_Classification">
                <EditItemTemplate>
		            <asp:DropDownList ID="ddlAsset_Classification" runat="server" SelectedValue='<%# Eval("Asset_Classification")%>'>
                        <asp:ListItem Value ="020001">020001</asp:ListItem>
                        <asp:ListItem Value ="020002">020002</asp:ListItem>
                        <asp:ListItem Value ="020003">020003</asp:ListItem>
                        <asp:ListItem Value ="020004">020004</asp:ListItem>
                        <asp:ListItem Value ="020005">020005</asp:ListItem>
                        <asp:ListItem Value ="020006">020006</asp:ListItem>
			            <%--<asp:ListItem Value ="020001">Normal</asp:ListItem>
                        <asp:ListItem Value ="020002">Special Mention</asp:ListItem>
                        <asp:ListItem Value ="020003">Substandard</asp:ListItem>
                        <asp:ListItem Value ="020004">Doubtful</asp:ListItem>
                        <asp:ListItem Value ="020005">Doubtful of Loss</asp:ListItem>
                        <asp:ListItem Value ="020006">Loss</asp:ListItem>--%>
                        <asp:ListItem></asp:ListItem>
		            </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Asset_Classification" runat="server" Text='<%# Eval("Asset_Classification")%>'></asp:Label>
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

                <asp:CheckBoxField DataField="NPL_Flag" HeaderText="NPL_Flag" SortExpression="NPL_Flag"/>
                <asp:BoundField DataField="General_Provision_Factor" HeaderText="General_Provision_Factor" SortExpression="General_Provision_Factor"/>
                <asp:BoundField DataField="NPL_Start_Date" HeaderText="NPL_Start_Date" SortExpression="NPL_Start_Date"/>
                <asp:CommandField ShowEditButton="True" />                         
                
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




</asp:Content>
