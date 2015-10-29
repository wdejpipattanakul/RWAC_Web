Public Class RWAMasterInquiry
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
        ds = clsMasterInquiry.getMasterInquiry(strDate)
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

End Class