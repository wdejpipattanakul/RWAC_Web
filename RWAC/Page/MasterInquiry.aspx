<%@ Page Title="RWA Master Transaction Inquiry" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterInquiry.aspx.vb" Inherits="RWAC.MasterInquiry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="table-responsive">  
                                <asp:GridView ID="grdRWAMaster" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Contract_ID" EmptyDataText="There are no data records to display." AllowPaging="true" PageSize="15">  
                                    <Columns>  
                                        <asp:BoundField DataField="Cust_ID" HeaderText="CustomerID" ReadOnly="True" SortExpression="Cust_ID" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" >  
<HeaderStyle CssClass="hidden-xs"></HeaderStyle>

<ItemStyle CssClass="hidden-xs"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cust_Name" HeaderText="CustomerName" SortExpression="Cust_Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
<HeaderStyle CssClass="visible-lg"></HeaderStyle>

<ItemStyle CssClass="visible-lg"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Contract_ID" HeaderText="ContractID" SortExpression="Contract_ID" />  
                                        <asp:BoundField DataField="Facility_ID" HeaderText="FacilityID" SortExpression="Facility_ID" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Currency" HeaderText="CCY" SortExpression="Currency" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" ItemStyle-HorizontalAlign="Center" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Accrued" HeaderText="Accrued" SortExpression="Accrued" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Adjust_Accrued" HeaderText="AdjustAccrued" SortExpression="Adjust_Accrued" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Specific_Provision" HeaderText="SpecificProvision" SortExpression="Specific_Provision" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Net_Balance" HeaderText="NetBalance" SortExpression="Net_Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >   
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DealDate" HeaderText="DealDate" SortExpression="DealDate" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Maturity_Date" HeaderText="MaturityDate" SortExpression="Maturity_Date" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" DataFormatString="{0:dd/MM/yyyy}">  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Credit_Exposure" HeaderText="CreditExposure" SortExpression="Credit_Exposure" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Credit_Type" HeaderText="CreditType" SortExpression="Credit_Type" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Credit_Risk_Subtype" HeaderText="CreditSubtype" SortExpression="Credit_Risk_Subtype" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Trading_Book_Flag" HeaderText="TradingBookFlag" SortExpression="Trading_Book_Flag" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NPL_Flag" HeaderText="NPLFlag" SortExpression="NPL_Flag" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Netting_Agreement_Flag" HeaderText="NettingAgreementFlag" SortExpression="Netting_Agreement_Flag" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" > 
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RW" HeaderText="RW" SortExpression="RW" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RWA_Collateral" HeaderText="RWANoCRM" SortExpression="RWA_Collateral" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RWA_No_Collateral" HeaderText="RWACRM" SortExpression="RWA_No_Collateral" HeaderStyle-CssClass="hidden-md" ItemStyle-CssClass="hidden-md" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" >  
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RWA" HeaderText="RWATotal" SortExpression="RWA" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" > 
                                        </asp:BoundField>
                                    </Columns>  
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="TopAndBottom" />

<RowStyle Wrap="False"></RowStyle>
                                </asp:GridView>
                                <i>You are viewing page
                                <%=grdRWAMaster.PageIndex + 1%>
                                of
                                <%=grdRWAMaster.PageCount%>
                                </i> 
                            </div>
    <div id="mainContainer" class="container">  
        <div class="shadowBox">  
            <div class="page-container">  
                <div class="container">  
                    <div class="row">  
                        <div class="col-lg-12 ">  
                              
                        </div>  
                    </div>  
                </div>  
            </div>  
        </div>  
    </div>  
</asp:Content>
