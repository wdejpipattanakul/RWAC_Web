Imports GemBox.Spreadsheet

Public Class CRSReportInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlReportDate.DataSource = clsUtility.getReportDateList
            ddlReportDate.DataTextField = "AOD"
            ddlReportDate.DataValueField = "AOD"
            ddlReportDate.DataBind()

            BindMainData(ddlReportDate.SelectedValue)
            BindTranData(ddlReportDate.SelectedValue)
        End If
    End Sub

    Protected Sub BindMainData(ByVal strDate As String)
        Dim ds As DataSet
        ds = clsCRSReportInquiry.getCRSMain(strDate)
        grdCRSMain.DataSource = ds
        grdCRSMain.DataBind()
    End Sub
    Protected Sub BindTranData(ByVal strDate As String)
        Dim ds As DataSet
        ds = clsCRSReportInquiry.getCRSTran(strDate)
        grdCRSTran.DataSource = ds
        grdCRSTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCRSMain.PageIndexChanging
        grdCRSMain.PageIndex = e.NewPageIndex
        BindMainData(ddlReportDate.SelectedValue)
    End Sub

    Protected Sub grdCRSTran_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCRSTran.PageIndexChanging
        grdCRSTran.PageIndex = e.NewPageIndex
        BindTranData(ddlReportDate.SelectedValue)
    End Sub

    Protected Sub ddlReportDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportDate.SelectedIndexChanged
        BindMainData(ddlReportDate.SelectedValue)
        BindTranData(ddlReportDate.SelectedValue)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExportMain.Click
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY")
        Dim ef As ExcelFile = ExcelFile.Load(Server.MapPath("/Template/CRS_Main_Report.xls"))
        Dim ws As ExcelWorksheet = ef.Worksheets(0)

        Dim ds As DataSet
        ds = clsCRSReportInquiry.getCRSMainExport(ddlReportDate.SelectedValue)

        ws.Cells("C3").Value = ddlReportDate.SelectedValue
        Dim i As Integer, j As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1 Step i + 1
            For j = 0 To ds.Tables(0).Columns.Count - 1 Step j + 1
                ws.Cells(5 + i, j).Value = ds.Tables(0).Rows(i)(j).ToString
            Next
            j = 0
        Next

        ef.Save(Me.Response, "CRS_Main_Report_" & Date.Now.ToString("yyyyMMddHHmmss") & ".xlsx")


    End Sub

    Protected Sub btnExportTran_Click(sender As Object, e As EventArgs) Handles btnExportTran.Click
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY")
        Dim ef2 As ExcelFile = ExcelFile.Load(Server.MapPath("/Template/CRS_Trans_Report.xls"))
        Dim ws2 As ExcelWorksheet = ef2.Worksheets(0)

        Dim ds2 As DataSet
        ds2 = clsCRSReportInquiry.getCRSTranExport(ddlReportDate.SelectedValue)

        ws2.Cells("C3").Value = ddlReportDate.SelectedValue
        Dim i2 As Integer, j2 As Integer
        For i2 = 0 To ds2.Tables(0).Rows.Count - 1 Step i2 + 1
            For j2 = 0 To ds2.Tables(0).Columns.Count - 1 Step j2 + 1
                ws2.Cells(5 + i2, j2).Value = ds2.Tables(0).Rows(i2)(j2).ToString
            Next
            j2 = 0
        Next

        ef2.Save(Me.Response, "CRS_Trans_Report_" & Date.Now.ToString("yyyyMMddHHmmss") & ".xlsx")
    End Sub
End Class