Public Class AllocateFunding
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
            BindData2()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsAllocateFunding.getMasterInquiry("30/09/2015")
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub BindData2()
        Dim ds As DataSet
        ds = clsAllocateFunding.getMasterInquiry2("30/09/2015")
        grdTran2.DataSource = ds
        grdTran2.DataBind()
    End Sub


    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTran.PageIndexChanging
        grdTran.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub grd_RowEditing(sender As Object, e As GridViewEditEventArgs)
        grdTran.EditIndex = e.NewEditIndex
        BindData()
        grdTran.Rows(e.NewEditIndex).Cells(18).Controls(0).Focus()

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        BindData()
    End Sub

    Protected Sub grd_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Contract_ID As String = grdTran.Rows(e.RowIndex).Cells(2).Text

        SQLConnect.ExcNonQuery("Update tbl_Funding_Master set Allocate_Asset_Amount = '" & e.NewValues("Allocate_Asset_Amount") & "'" & _
                               " where Contract_ID = '" & Contract_ID & "'")



        grdTran.EditIndex = -1
        BindData()
    End Sub



End Class