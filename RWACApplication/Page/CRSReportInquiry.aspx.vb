Public Class CRSReportInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindMainData()
            BindTranData()
        End If
    End Sub

    Protected Sub BindMainData()
        Dim ds As DataSet
        ds = clsCRSReportInquiry.getCRSMain("30/09/2015")
        grdCRSMain.DataSource = ds
        grdCRSMain.DataBind()
    End Sub
    Protected Sub BindTranData()
        Dim ds As DataSet
        ds = clsCRSReportInquiry.getCRSTran("30/09/2015")
        grdCRSTran.DataSource = ds
        grdCRSTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCRSMain.PageIndexChanging
        grdCRSMain.PageIndex = e.NewPageIndex
        BindMainData()
    End Sub

    Protected Sub grdCRSTran_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCRSTran.PageIndexChanging
        grdCRSTran.PageIndex = e.NewPageIndex
        BindTranData()
    End Sub

End Class