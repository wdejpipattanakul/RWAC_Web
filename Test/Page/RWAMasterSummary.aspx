<%@ Page Title="RWA Master Transaction Inquiry" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RWAMasterSummary.aspx.vb" Inherits="Test.RWAMasterSummary" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <div class="form-group">
        <label class="control-label col-sm-2" for="ddlReportDate" style="padding-top:5px">Report Date:</label>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddlReportDate" runat="server" AutoPostBack="true" class="form-control" Width="150px" ></asp:DropDownList>
        </div>
    </div>
    <br /><br />
    <div class="table-responsive">  
              
    </div>
    <div class="row">
    <div class="col-sm-4">
        <h4>Credit Exposure Type Summary</h4>
        <asp:Chart ID="chtSummary" runat="server">
		    <Series>
			    <asp:Series Name="ExposureType">
			    </asp:Series>
		    </Series>
		    <ChartAreas>
			    <asp:ChartArea Name="ChartArea1">
				    <Area3DStyle />
			    </asp:ChartArea>
		    </ChartAreas>
	    </asp:Chart> 
    </div>
    <div class="col-sm-8">
        <h4>Risk Weight Summary</h4>
        <asp:Chart ID="chtSummary2" runat="server" Width="700px">
		    <Series>
			    <asp:Series Name="RiskWeight">
			    </asp:Series>
		    </Series>
		    <ChartAreas>
			    <asp:ChartArea Name="ChartArea1">
				    <Area3DStyle />
			    </asp:ChartArea>
		    </ChartAreas>
	    </asp:Chart>
    </div>
  </div>
</asp:Content>
