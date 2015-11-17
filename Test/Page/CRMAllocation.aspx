<%@ Page Title="CRM Allocation" Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRMAllocation.aspx.vb" Inherits="Test.CRMAllocation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="form-group">
        <div class="row">
            <div class="col-sm-2">
                <p style="padding-top:5px">Master Transaction</p>
            </div>
            <div class="col-sm-6">
                <asp:Button ID="btnAll" runat="server" Text="ALL" class="btn btn-primary" Width="100px" />
                <asp:Button ID="btnCRM" runat="server" Text="CRM" class="btn btn-primary" Width="100px" />
                <asp:Button ID="btnNoCRM" runat="server" Text="NO CRM" class="btn btn-primary" Width="100px" />
            </div>
            <div class="col-sm-4" style="display:block;float:right;">
                <asp:Button ID="btnAutoAllocate" runat="server" Text="Automate Allocate" class="btn btn-primary" Visible="false" />
                <asp:Button ID="btnClear" runat="server" Text="Clear Allocated Value" class="btn btn-primary" />
            </div>
        </div>
    </div>
    <div class="table-responsive">  
        <asp:GridView ID="grdTRNList" runat="server" SelectedRowStyle-BorderColor="Black" SelectedRowStyle-BorderWidth="2px" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5">  
            <Columns>  
                <asp:BoundField DataField="Cust_ID" HeaderText="CustID" ReadOnly="True" SortExpression="Cust_ID" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="Credit_Exposure" HeaderText="ExposureType" SortExpression="Credit_Exposure" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false"  >  
                </asp:BoundField> 
                <asp:BoundField DataField="Calculation_Method" HeaderText="CalculationMethod" SortExpression="Calculation_Method" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false"  >  
                </asp:BoundField> 
                <asp:BoundField DataField="Facility_ID" HeaderText="FacilityID" SortExpression="Facility_ID" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                </asp:BoundField>
                <asp:BoundField DataField="Contract_ID" HeaderText="ContractID" SortExpression="Contract_ID" />  
                <asp:BoundField DataField="Asset_Classification_Type" HeaderText="AssetType" SortExpression="Asset_Classification_Type" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                </asp:BoundField>
                <asp:CheckBoxField DataField="NPL_Flag" HeaderText="NPL" SortExpression="NPL_Flag" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" ItemStyle-HorizontalAlign="Center" >  
                </asp:CheckBoxField>
                <asp:BoundField DataField="Maturity_Date" HeaderText="MaturityDate" SortExpression="Maturity_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                </asp:BoundField>
                <asp:BoundField DataField="Remaining_Term" HeaderText="RemainingTerm" SortExpression="Remaining_Term" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Currency" HeaderText="CCY" SortExpression="Currency" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" ItemStyle-HorizontalAlign="Center" >  
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Remaining_Amount" HeaderText="RemainingAmount" SortExpression="Remaining_Amount" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
            </Columns>  
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>      
    </div>
    <div class="form-group">
        <div class="row">
            <div class="col-sm-2">
                <p style="padding-top:5px">CRM Transaction</p>
            </div>
            <div class="col-sm-10" style="font-size:xx-small">
                <asp:Button ID="btnCAll" runat="server" Text="ALL" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnCOnbal" runat="server" Text="On-bal Netting" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnCFinan" runat="server" Text="Financial" class="btn btn-primary" Width="150px" />
                <asp:Button ID="btnCGuara" runat="server" Text="Guarantee" class="btn btn-primary" Width="150px" />
            </div>
        </div>
    </div>
    <div class="table-responsive">  
        <asp:GridView ID="grdCRMList" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Transaction_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5" ShowFooter="True" ShowHeaderWhenEmpty="True">  
            <Columns>  
                <asp:BoundField DataField="Cust_ID" HeaderText="CustID" ReadOnly="True" SortExpression="Cust_ID" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" Visible="false" >  
                </asp:BoundField>
                <asp:BoundField DataField="Transaction_ID" HeaderText="Transaction_ID" SortExpression="Transaction_ID" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false"  >  
                </asp:BoundField> 
                <asp:BoundField DataField="Facility_ID" HeaderText="FacilityID" SortExpression="Facility_ID" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" Visible="false" >  
                </asp:BoundField>
                <asp:BoundField DataField="CRM_ID" HeaderText="Collateral ID" SortExpression="CRM_ID" />  
                <asp:BoundField DataField="CRM_Type" HeaderText="Type" SortExpression="CRM_Type" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CRM_Maturity_Date" HeaderText="MaturityDate" SortExpression="CRM_Maturity_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                </asp:BoundField>
                <asp:BoundField DataField="CRM_Currency" HeaderText="CCY" SortExpression="CRM_Currency" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" ItemStyle-HorizontalAlign="Center" >  
                </asp:BoundField>
                <asp:BoundField DataField="CRM_Amount" HeaderText="Amount" SortExpression="CRM_Amount" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:TemplateField HeaderText="RemainingAmount">
                    <ItemTemplate>
                        <asp:Label ID="lblRemaining" runat="server" class="form-control" Font-Size="X-Small" Text='<%# Bind("CRM_Remaining_Amount", "{0:N2}")%>' style="text-align:right"></asp:Label>
                        <asp:HiddenField ID="txtAllocatedOri" Value='<%# Eval("CRM_Allocated_Amount_Ori")%>' runat="server" />
                        <asp:HiddenField ID="txtID" Value='<%# Eval("ID")%>' runat="server" />
                        <asp:HiddenField ID="txtCustID" Value='<%# Eval("Cust_ID")%>' runat="server" />
                        <asp:HiddenField ID="txtFacilID" Value='<%# Eval("Facility_ID")%>' runat="server" />
                        <asp:HiddenField ID="txtTransID" Value='<%# Eval("Transaction_ID")%>' runat="server" />
                        <asp:HiddenField ID="txtCrmID" Value='<%# Eval("CRM_ID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AllocatedAmount">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAllocated" runat="server" class="form-control" Font-Size="X-Small" BackColor="LightYellow" Text='<%# Bind("CRM_Allocated_Amount", "{0:N2}") %>' style="text-align:right" OnTextChanged="txtAllocated_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
<%--                    <FooterTemplate>
                        <asp:Label ID="lblTotalAllocated" runat="server" class="form-control" Font-Size="X-Small" Text='' style="text-align:right"></asp:Label> 
                    </FooterTemplate>--%>
                </asp:TemplateField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
            <FooterStyle BackColor="CadetBlue" />
        </asp:GridView>      
    </div> 
</asp:Content>
