Imports System.Data.SqlClient

Public Class DataInquiryTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If ddlTableName.SelectedValue <> -1 Then
                BindData(ddlTableName.SelectedValue)
            End If

        End If
    End Sub

    Protected Sub BindData(ByVal TableName As String)
        Dim ds As DataSet
        ds = clsDataInquiry.getMasterInquiry(TableName)
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTran.PageIndexChanging
        grdTran.PageIndex = e.NewPageIndex
        BindData(ddlTableName.SelectedValue)
    End Sub

    Protected Sub ddlTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTableName.SelectedIndexChanged
        BindData(ddlTableName.SelectedValue)
    End Sub

End Class