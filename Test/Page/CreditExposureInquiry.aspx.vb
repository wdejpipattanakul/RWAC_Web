Imports System.Data.SqlClient

Public Class CreditExposureInquiry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsCreditExposureInquiry.getMasterInquiry("30/09/2015")
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
        grdTran.Rows(e.NewEditIndex).Cells(18).Controls(0).Focus()

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        BindData()
    End Sub

    Protected Sub grd_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Contract_ID As String = grdTran.Rows(e.RowIndex).Cells(2).Text

        'Get hold of combobox
        Dim row As GridViewRow = DirectCast(sender, 
        GridView).Rows(e.RowIndex)
        Dim cbo As DropDownList =
        DirectCast(row.FindControl("ddlCredit_Exposure"), DropDownList)

        'Set the New value of the object
        e.NewValues("Credit_Exposure") = cbo.SelectedValue



        'MsgBox(e.NewValues("Credit_Exposure") + "_" + grdTran.Rows(e.RowIndex).Cells(20).Text)

        SQLConnect.ExcNonQuery("Update tbl_RWAC_Master set Adjust_Accrued = '" & e.NewValues("Adjust_Accrued") & "', Specific_Provision = '" & e.NewValues("Specific_Provision") & "', Trading_Book_Flag = '" & e.NewValues("Trading_Book_Flag") & "', FX_Netting_Flag = '" & e.NewValues("FX_Netting_Flag") & "', Credit_Exposure = '" & e.NewValues("Credit_Exposure") & "'" & _
                               " where Contract_ID = '" & Contract_ID & "'")


        grdTran.EditIndex = -1
        BindData()


    End Sub


    Protected Sub UpdateNetting_Click(sender As Object, e As EventArgs) Handles UpdateNetting.Click

        Dim UserDB As String
        Dim PassDB As String
        Dim NameDB As String
        Dim DSN As String
        Dim Query As String
        Dim Query2 As String
        Dim strSQL As String
        Dim strSQL2 As String
        Dim record_count As Long
        Dim record_count2 As Long
        Dim i As Long = 0
        Dim k As Long = 0

        Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
        oconn1.Open()

        Dim dt As New DataTable()

        strSQL = "SELECT Cust_ID, Contract_ID, CEA_No_Netting, NotionalxCCF  FROM  Q_CEA2"

        Dim objCommand As New SqlCommand(strSQL, oconn1)
        Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()

        'dt.Load(objDataReader)

        If objDataReader.HasRows Then
            objDataReader.Close()
            objDataReader = objCommand.ExecuteReader()

            Do While objDataReader.Read()
                SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                         " SET CEA = '" & objDataReader.Item("CEA_No_Netting") & "'" & _
                         " WHERE Contract_ID = '" & objDataReader.Item("Contract_ID") & "' AND Memo = 'PCFXCOBK' AND FX_Netting_Flag = False")



            Loop




        End If




        strSQL = "SELECT Cust_ID, CountOfCust_ID, CEA_Netting, NotionalxCCF  FROM  Q_CEA_Netting"

        objCommand = New SqlCommand(strSQL, oconn1)
        objDataReader.Close()
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then
            objDataReader.Close()
            objDataReader = objCommand.ExecuteReader()

            Do While objDataReader.Read()

                If objDataReader.Item("NotionalxCCF") = 0 Then

                    SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                            " SET CEA = 0" & _
                            " WHERE Cust_ID = '" & objDataReader.Item("Cust_ID") & "' AND Memo = 'PCFXCOBK' AND FX_Netting_Flag = True")
                Else
                    SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                            " SET CEA = Amount_THB*CCF*'" & objDataReader.Item("CEA_Netting") / objDataReader.Item("NotionalxCCF") & "'" & _
                            " WHERE Cust_ID = '" & objDataReader.Item("Cust_ID") & "' AND Memo = 'PCFXCOBK' AND FX_Netting_Flag = True")

                End If
            Loop

        End If
        


        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master " & _
                     " SET Net_Balance = Amount_THB + IIF(Adjust_Accrued<>Accrued And Adjust_Accrued<>0, Adjust_Accrued,Accrued) - Specific_Provision" & _
                     " WHERE Credit_Exposure = 'On-Bal'")

        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master " & _
                     " SET Net_Balance = (Amount_THB + IIF(Adjust_Accrued<>Accrued And Adjust_Accrued<>0, Adjust_Accrued,Accrued) - Specific_Provision)*CCF" & _
                     " WHERE Credit_Exposure = 'Off-Bal Non-Derivative'")

        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master " & _
                     " SET Net_Balance = (CEA - Specific_Provision)" & _
                     " WHERE Credit_Exposure = 'Derivative'")

        'Update Anet RCnet Later ###############
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master " & _
                     " SET MTM_Value = '', MTM_Loss = '' , MTM_Profit = '' , RCgross = '', Agross = '' " & _
                     " WHERE Memo = 'PCFXCOBK' AND FX_Netting_Flag = True")

        Response.Write("<script type=""text/javascript"">alert(""Update FX Netting Agreement Successful"");</script>")
        'MsgBox("Update FX Netting Agreement Successful")

        BindData()
    End Sub
End Class