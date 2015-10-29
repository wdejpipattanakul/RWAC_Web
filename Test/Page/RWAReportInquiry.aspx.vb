Imports GemBox.Spreadsheet

Public Class RWAReportInquiry
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
        ds = clsRWAReportInquiry.getRWAReportInquiry(strDate)
        grdRWAMaster.DataSource = ds
        grdRWAMaster.DataBind()
    End Sub

    Protected Sub grdRWAMaster_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdRWAMaster.PageIndexChanging
        grdRWAMaster.PageIndex = e.NewPageIndex
        BindData(ddlReportDate.SelectedValue)
    End Sub

    Protected Sub ddlReportDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportDate.SelectedIndexChanged
        BindData(ddlReportDate.SelectedValue)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY")
        Dim ef As ExcelFile = ExcelFile.Load(Server.MapPath("/Template/RWA_Report.xlsx"))
        Dim ws As ExcelWorksheet = ef.Worksheets(0)

        Dim ds As DataSet
        ds = clsRWAReportInquiry.getRWAReportExport(ddlReportDate.SelectedValue)

        Dim i As Integer, j As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1 Step i + 1
            For j = 0 To ds.Tables(0).Columns.Count - 1 Step j + 1
                ws.Cells(2 + i, j).Value = ds.Tables(0).Rows(i)(j).ToString
            Next
            j = 0
        Next

        'ef.Save(Server.MapPath("/Template/TemplateUse.xlsx"))
        ef.Save(Me.Response, "RWA_Report_" & Date.Now.ToString("yyyyMMddHHmmss") & ".xlsx")
    End Sub
End Class