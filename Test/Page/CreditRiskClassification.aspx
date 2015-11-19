<%@ Page Title="CRM Allocation" Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreditRiskClassification.aspx.vb" Inherits="Test.CreditRiskClassification" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="form-group">
        <div class="row">
            <div class="col-sm-2">
                <p style="padding-top:5px">Credit Risk Item Classification</p>
            </div>
            <div class="col-sm-10">
                <asp:Button ID="btnAT" runat="server" Text="Customer" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnLG" runat="server" Text="Owner/Guarantor" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnOA" runat="server" Text="Other Assets" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnDP" runat="server" Text="DvP/Non-DvP" class="btn btn-primary" Width="150px" />
            </div>
        </div>
        <div class="row" style="height:400px">
            <div class="col-sm-4">
                <div class="table-responsive" >
                    <asp:GridView ID="grdCustList" runat="server" SelectedRowStyle-BorderColor="Black" SelectedRowStyle-BorderWidth="2px" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="10">  
                        <Columns>  
                            <asp:BoundField DataField="Cust_Name" HeaderText="Customer Name" ReadOnly="True" SortExpression="Cust_Name" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="BOT_Code">
                                <ItemTemplate>
                                    <asp:Label ID="BOT_Credit_Risk_Item" runat="server" Font-Size="X-Small" Text='<%# Bind("BOT_Credit_Type")%>' style="text-align:center"></asp:Label>
                                    <asp:HiddenField ID="txtID" Value='<%# Eval("ID")%>' runat="server" />
                                    <asp:HiddenField ID="txtCreditType" Value='<%# Eval("Credit_Type")%>' runat="server" />
                                    <asp:HiddenField ID="txtCreditSubtype" Value='<%# Eval("Credit_Subtype")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>  
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
                        <RowStyle Wrap="False"></RowStyle>
                        <HeaderStyle BackColor="CadetBlue" />
                    </asp:GridView> 
                </div>
            </div>
            <div class="col-sm-8">
                <div class="table-responsive" style="overflow:auto; height:380px;">
                    <asp:TreeView ID="tvwBOT" SkinId="Simple" ExpandDepth="0" runat="server" Font-Size="X-Small" OnSelectedNodeChanged="tvwBOT_SelectedNodeChanged">
                    </asp:TreeView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
