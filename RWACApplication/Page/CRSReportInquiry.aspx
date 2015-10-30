<%@ Page Title="CRS Report Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CRSReportInquiry.aspx.vb" Inherits="RWACApplication.CRSReportInquiry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="form-group">
        <label class="control-label col-sm-2" for="ddlReportDate" style="padding-top:5px">Report Date:</label>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddlReportDate" runat="server" AutoPostBack="true" class="form-control" Width="150px" ></asp:DropDownList>
        </div>
    </div>
    <br /><br />
    <div class="form-group">
        <div class="col-sm-2">
            <p style="padding-top:5px">Main Report</p>
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnExportMain" runat="server" Text="Export CRS Main Report" class="btn btn-primary" />
        </div>
    </div>
    <br />
    <div class="table-responsive">  
        <asp:GridView ID="grdCRSMain" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Reference" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5">  
            <Columns>  
                <asp:BoundField DataField="CreditRiskMethod" HeaderText="CreditRiskMethod" ReadOnly="True" SortExpression="CreditRiskMethod" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CRM Method" HeaderText="CRMMethod" SortExpression="CRM Method" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CreditRiskType" HeaderText="CreditRiskType" SortExpression="CreditRiskType" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CreditRiskItem" HeaderText="CreditRiskItem" SortExpression="CreditRiskItem" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="FtdAdjustMent" HeaderText="FtdAdjustMent" SortExpression="FtdAdjustMent" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="CcfRate" HeaderText="CcfRate" SortExpression="CcfRate" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" >  
                </asp:BoundField>
            </Columns>  
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>
        <div style="font-size:x-small" hidden="hidden">
            <i>You are viewing page
            <%=grdCRSMain.PageIndex + 1%>
            of
            <%=grdCRSMain.PageCount%>
            </i> 
        </div>       
    </div>   
    <div class="form-group">
        <div class="col-sm-2">
            <p style="padding-top:5px">Tran Report</p>
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnExportTran" runat="server" Text="Export CRS Tran Report" class="btn btn-primary" />
        </div>
    </div>
    <br />
    <div class="table-responsive">  
        <asp:GridView ID="grdCRSTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Reference" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="5">  
            <Columns>  
                <asp:BoundField DataField="CreditRiskMethod" HeaderText="CreditRiskMethod" ReadOnly="True" SortExpression="CreditRiskMethod" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CRM Method" HeaderText="CRMMethod" SortExpression="CRM Method" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CreditRiskType" HeaderText="CreditRiskType" SortExpression="CreditRiskType" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="CreditRiskItem" HeaderText="CreditRiskItem" SortExpression="CreditRiskItem" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="RW" HeaderText="RW" SortExpression="RW" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Notional Principle Amount" HeaderText="NotionalAmount" SortExpression="Notional Principle Amount" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Gross Credit Equivalent Amount" HeaderText="GCEA" SortExpression="Gross Credit Equivalent Amount" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Specific Provision" HeaderText="SP" SortExpression="Specific Provision" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Adjustment Item" HeaderText="ADJ" SortExpression="Adjustment Item" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Net Credit Equivalent Amount" HeaderText="NCEA" SortExpression="Net Credit Equivalent Amount" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Decrease in CRM" HeaderText="DecreaseCRM" SortExpression="Decrease in CRM" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Increase in CRM" HeaderText="IncreaseCRM" SortExpression="Increase in CRM" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Potential Loss" HeaderText="PotentialLoss" SortExpression="Potential Loss" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Risk Weighted Asset Outstanding Amount" HeaderText="RWA" SortExpression="Risk Weighted Asset Outstanding Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Reference" HeaderText="Reference" SortExpression="Reference" >  
                </asp:BoundField>
            </Columns>  
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>
        <div style="font-size:x-small" hidden="hidden">
            <i>You are viewing page
            <%=grdCRSTran.PageIndex + 1%>
            of
            <%=grdCRSTran.PageCount%>
            </i> 
        </div>       
    </div>
</asp:Content>
