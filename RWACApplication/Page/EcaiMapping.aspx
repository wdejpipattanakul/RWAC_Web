<%@ Page Title="ECAI Mapping" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EcaiMapping.aspx.vb" Inherits="RWACApplication.EcaiMapping" EnableEventValidation="false" MaintainScrollPositionOnPostBack = "true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %>       
    </h4>
    
    <asp:LinkButton ID="Transaction_Mapping" runat="server">Transaction_Mapping</asp:LinkButton>&nbsp
        <asp:LinkButton ID="Collateral_Mapping" runat="server">Collateral_Mapping</asp:LinkButton>&nbsp
        <asp:LinkButton ID="Bond_Mapping" runat="server">Bond_Mapping</asp:LinkButton>&nbsp
        <asp:LinkButton ID="NON_DVP_Mapping" runat="server">NON-DVP_Mapping</asp:LinkButton>&nbsp

    <br/>
    <br />
    <asp:TextBox ID="txtLabel" runat="server" BorderWidth="0px" Font-Bold="True" MaxLength="300"
             style="width:100%;resize:none;" ReadOnly="True" TextMode="MultiLine" Rows="1"></asp:TextBox>

    
    



    <br />
    <br/>


    <asp:Button ID="btn_Sovereign" runat="server" Text="Sovereign" />&nbsp<asp:Button ID="btn_Coporate" runat="server" Text="Coporate" />&nbsp<asp:Button ID="btn_Specific" runat="server" Text="Specific" />
    <br/><br/>


    
    
    <%--<div style ="height:30px;width:450px; margin:0;padding:0">
        <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader"
         style="font-family:Arial;font-size:10pt;width:450px;color:black;
         border-collapse:collapse;height:100%;" runat="server">
            <tr>
               <td style ="width:126px;text-align:left"> Counter_Party_Name</td>
               <td style ="width:275px;text-align:left;"> COMP_Name</td>
            </tr>
        </table>
    </div>--%>

    <table>
            <tr>
               <td>
                   <div class="table-responsive" style ="height:400px; width:467px; overflow:auto;">  
        
                    <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" 
                         EmptyDataText="There are no data records to display."
                        OnSelectedIndexChanged = "grd_OnSelectedIndexChanged"
                        OnRowDataBound = "grd_OnRowDataBound"
                        >  
                        <Columns>
                            <asp:BoundField ItemStyle-Width="200px" DataField="Counter_Party_Name" HeaderText="Counter_Party_Name" SortExpression="Counter_Party_Name" ReadOnly="true"/>
                            <asp:BoundField ItemStyle-Width="250px" DataField="COMP_Name" HeaderText="COMP_Name" SortExpression="COMP_Name" ReadOnly="true" Visible ="false"/>
                
                
                
                        </Columns>  
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
                        <RowStyle Wrap="False"></RowStyle>
                        <HeaderStyle BackColor="CadetBlue" />
                    </asp:GridView>
                    <%--<div style="font-size:x-small" hidden="hidden">
                        <i>You are viewing page
                        <%=grdTran.PageIndex + 1%>
                        of
                        <%=grdTran.PageCount%>
                        </i> 
                    </div> --%>      
                </div>
                   

               </td>
               <td> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <asp:Button ID="btn_Mapping" runat="server" Text="Mapping" Font-Names="Arial" Font-Size="11pt" Height="28px" /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
               <td>
                   <div class="table-responsive" style ="height:400px; width:467px; overflow:auto;">  
        
                    <asp:GridView ID="grdTran2" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="true" 
                         EmptyDataText="There are no data records to display."
                        OnSelectedIndexChanged = "grd_OnSelectedIndexChanged2"
                        OnRowDataBound = "grd_OnRowDataBound2"
                        >  
                        <Columns>
                            
                
                
                
                        </Columns>  
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" Position="Bottom" />
                        <RowStyle Wrap="False"></RowStyle>
                        <HeaderStyle BackColor="CadetBlue" />
                    </asp:GridView>
                    <%--<div style="font-size:x-small" hidden="hidden">
                        <i>You are viewing page
                        <%=grdTran.PageIndex + 1%>
                        of
                        <%=grdTran.PageCount%>
                        </i> 
                    </div> --%>      
                </div>


               </td>
            </tr>
    </table>

    <asp:TextBox ID="txtECAI" runat="server" style="text-align: right" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtCustName" runat="server" style="text-align: right" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtMenu" runat="server" style="text-align: right" Visible="False"></asp:TextBox>
    
        
    

</asp:Content>
