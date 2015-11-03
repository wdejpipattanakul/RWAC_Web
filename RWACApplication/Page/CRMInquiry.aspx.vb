Public Class CRMInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsCRMInquiry.getMasterInquiry("30/09/2015")
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTran.PageIndexChanging
        grdTran.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub grd_RowEditing(sender As Object, e As GridViewEditEventArgs)


        grdTran.EditIndex = e.NewEditIndex
        BindData()
        grdTran.Rows(e.NewEditIndex).Cells(12).Controls(0).Focus()

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        BindData()
    End Sub

    Protected Sub grd_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Counter_Party_ID As String = grdTran.Rows(e.RowIndex).Cells(0).Text
        Dim Collateral_ID As String = grdTran.Rows(e.RowIndex).Cells(2).Text
        Dim Facility_ID As String = grdTran.Rows(e.RowIndex).Cells(3).Text
        Dim Owner_Guarantor_Name As String = grdTran.Rows(e.RowIndex).Cells(4).Text

        If Facility_ID = "&nbsp;" Then
            Facility_ID = ""
        End If

        If Owner_Guarantor_Name = "&nbsp;" Then
            Owner_Guarantor_Name = ""
        End If


        Debug.Print(Counter_Party_ID & "_" & Collateral_ID & "_" & Facility_ID & "_" & Owner_Guarantor_Name)

        SQLConnect.ExcNonQuery("Update tbl_CRM_Master set CRM_Use_Flag = '" & e.NewValues("CRM_Use_Flag") & "', CRM_Revalue_Term = '" & e.NewValues("CRM_Revalue_Term") & "', CRM_Holding_Period = '" & e.NewValues("CRM_Holding_Period") & "'" & _
                               " where Counter_Party_ID = '" & Counter_Party_ID & "' And Collateral_ID = '" & Collateral_ID & "' And Facility_ID = '" & Facility_ID & "' And Owner_Guarantor_Name = '" & Owner_Guarantor_Name & "'")


        grdTran.EditIndex = -1
        BindData()


    End Sub

    
End Class