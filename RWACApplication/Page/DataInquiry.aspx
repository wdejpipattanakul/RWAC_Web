<%@ Page Title="Data Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DataInquiry.aspx.vb" Inherits="RWACApplication.DataInquiryTest" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
        <label class="control-label col-sm-2" for="ddlTableName" style="padding-top:5px">Table Name :</label>
        <div class="col-sm-2"><asp:DropDownList ID="ddlTableName" runat="server" AutoPostBack="true" class="form-control" Width="150px">                             
                    <asp:ListItem Enabled="true" Text="Select Table Name" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCACTBWK" Value="ATLAS_PCACTBWK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCCMPRWK" Value="ATLAS_PCCMPRWK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCCUSTBK" Value="ATLAS_PCCUSTBK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCFACLWK" Value="ATLAS_PCFACLWK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCFXCOBK" Value="ATLAS_PCFXCOBK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCLDCOBK" Value="ATLAS_PCLDCOBK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCSWPTWK" Value="ATLAS_PCSWPTWK"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCACCWK1" Value="ATLAS_PCACCWK1"></asp:ListItem>
                    <asp:ListItem Text="ATLAS_PCCMPOW2" Value="ATLAS_PCCMPOW2"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_Dvp_FX" Value="tbl_Temp_Dvp_FX"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_Dvp_Bond_Primary" Value="tbl_Temp_Dvp_Bond_Primary"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_Dvp_Bonds_Investment" Value="tbl_Temp_Dvp_Bonds_Investment"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_Sovereign" Value="tbl_Temp_Sovereign"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_Corperate" Value="tbl_Temp_Corperate"></asp:ListItem>
                    <asp:ListItem Text="tbl_Temp_SP" Value="tbl_Temp_SP"></asp:ListItem>
                    <asp:ListItem Text="tbl_Accrued" Value="tbl_Accrued"></asp:ListItem>
                    <asp:ListItem Text="tbl_Collateral" Value="tbl_Collateral"></asp:ListItem>
                    <asp:ListItem Text="tbl_ECAI_Rating" Value="tbl_ECAI_Rating"></asp:ListItem>               
                    </asp:DropDownList>
            </div>
    <br/>
    &nbsp
    <div class="table-responsive">  
        <asp:GridView ID="grdTran" runat="server" RowStyle-Wrap="false" Font-Size="XX-Small" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" 
            EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="15">  
             
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
