<%@ Page Title="RWA Report Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RWAReportInquiry.aspx.vb" Inherits="RWACApplication.RWAReportInquiry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="table-responsive">  
        <asp:GridView ID="grdRWAMaster" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">  
            <Columns>  
                <asp:BoundField DataField="Asset classification type" HeaderText="AssetType" ReadOnly="True" SortExpression="Asset classification type" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
                </asp:BoundField>
                <asp:BoundField DataField="Exposure type" HeaderText="ExposureType" SortExpression="Exposure type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                </asp:BoundField> 
                <asp:BoundField DataField="BOT credit risk type code" HeaderText="CreditRiskType" SortExpression="BOT credit risk type code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                </asp:BoundField> 
                <asp:BoundField DataField="EBOT credit risk item code" HeaderText="CreditRiskItem" SortExpression="BOT credit risk item code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                </asp:BoundField> 
                <asp:BoundField DataField="Contract_ID" HeaderText="ContractID" SortExpression="Contract_ID" />  
                <asp:BoundField DataField="Facility_ID" HeaderText="FacilityID" SortExpression="Facility_ID" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                </asp:BoundField>
                <asp:BoundField DataField="Cust_Name" HeaderText="CustName" SortExpression="Cust_Name" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                </asp:BoundField>
                <asp:BoundField DataField="Q_RWA_Report_Transaction_Section_Currency" HeaderText="CCY" SortExpression="Q_RWA_Report_Transaction_Section_Currency" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" ItemStyle-HorizontalAlign="Center" >  
                </asp:BoundField>
                <asp:BoundField DataField="Amount_THB" HeaderText="Amount" SortExpression="Amount_THB" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Adjust_Accrued" HeaderText="AdjustAccrued" SortExpression="Adjust_Accrued" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Specific_Provision" HeaderText="SpecificProvision" SortExpression="Specific_Provision" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Notional Amount" HeaderText="NotionalAmount" SortExpression="Notional Amount" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >   
                </asp:BoundField>
                <asp:BoundField DataField="DealDate" HeaderText="DealDate" SortExpression="DealDate" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                </asp:BoundField>
                <asp:BoundField DataField="Maturity_Date" HeaderText="MaturityDate" SortExpression="Maturity_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                </asp:BoundField>
                <asp:BoundField DataField="CCF" HeaderText="CCF" SortExpression="CCF" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="GCEA" HeaderText="GCEA" SortExpression="GCEA" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Adjustment Item" HeaderText="ADJ" SortExpression="Adjustment Item" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="NCEA" HeaderText="NCEA" SortExpression="NCEA" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="RWA rate" HeaderText="RW" SortExpression="RWA rate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="RWA_Collateral" HeaderText="RWANoCRM" SortExpression="RWA_Collateral" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="RWA_No_Collateral" HeaderText="RWACRM" SortExpression="RWA_No_Collateral" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="RWA" HeaderText="RWATotal" SortExpression="RWA" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" > 
                </asp:BoundField>
                <asp:BoundField DataField="CRM Method" HeaderText="CRMMethod" SortExpression="CRM Method" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                </asp:BoundField>
                <asp:BoundField DataField="Decrease In CRM" HeaderText="DecreaseCRM" SortExpression="Decrease In CRM" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                </asp:BoundField>
                <asp:BoundField DataField="Increase IN CRM" HeaderText="IncreaseCRM" SortExpression="Increase IN CRM" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" > 
                </asp:BoundField>
            </Columns>  
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
            <RowStyle Wrap="False"></RowStyle>
            <HeaderStyle BackColor="CadetBlue" />
        </asp:GridView>
        <div style="font-size:x-small" hidden="hidden">
            <i>You are viewing page
            <%=grdRWAMaster.PageIndex + 1%>
            of
            <%=grdRWAMaster.PageCount%>
            </i> 
        </div>       
    </div> 
</asp:Content>
