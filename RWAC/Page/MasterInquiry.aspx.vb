Public Class MasterInquiry
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsMasterInquiry.getMasterInquiry("30/09/2015")
        grdRWAMaster.DataSource = ds
        grdRWAMaster.DataBind()
    End Sub

    Protected Sub grdRWAMaster_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdRWAMaster.PageIndexChanging
        grdRWAMaster.PageIndex = e.NewPageIndex
        BindData()
    End Sub
End Class