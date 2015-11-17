Imports System.Web.UI.DataVisualization.Charting

Public Class RWAMasterSummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlReportDate.DataSource = clsUtility.getReportDateList
            ddlReportDate.DataTextField = "AOD"
            ddlReportDate.DataValueField = "AOD"
            ddlReportDate.DataBind()

            BindData(ddlReportDate.SelectedValue)
        End If
    End Sub

    Protected Sub BindData(ByVal strDate As String)
        Dim ds As DataSet
        ds = clsMasterInquiry.getRWAReportSummaryExposure(strDate)
        chtSummary.Series("ExposureType").Points.DataBind(ds.Tables(0).AsEnumerable(), "Credit_Exposure", "NumberofTrans", String.Empty)

        chtSummary.Series("ExposureType").ChartTypeName = "Doughnut"
        chtSummary.ChartAreas(0).Area3DStyle.Enable3D = False

        ds = clsMasterInquiry.getRWAReportSummaryRW(strDate)
        chtSummary2.Series("RiskWeight").Points.DataBind(ds.Tables(0).AsEnumerable(), "RW", "NumberofTrans", String.Empty)

        chtSummary2.Series("RiskWeight").ChartTypeName = "Area"
        chtSummary2.ChartAreas(0).Area3DStyle.Enable3D = False
    End Sub

    Protected Sub ddlReportDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportDate.SelectedIndexChanged
        BindData(ddlReportDate.SelectedValue)
    End Sub
End Class