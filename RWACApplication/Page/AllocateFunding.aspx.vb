Imports System.Data.SqlClient

Public Class AllocateFunding
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then



            SQLConnect.ExcNonQuery("Delete from tbl_Funding_Master")
            SQLConnect.ExcNonQuery("INSERT INTO tbl_Funding_Master ( Cust_ID, Cust_Name, Contract_ID, FX_And_Original_Currency, CEA, Amount, Accrued, Adjust_Accrued, Specific_Provision, Credit_Exposure, Credit_Type, Remaining_Date, DealDate, Maturity_Date, Allocate_Asset_Amount, Customer_Location, Credit_Risk_Subtype, Remaining_Transaction_Amount, [Memo], Amount_THB, AOD )" & _
                                    " SELECT tbl_RWAC_Master.Cust_ID, tbl_RWAC_Master.Cust_Name, tbl_RWAC_Master.Contract_ID, tbl_RWAC_Master.FX_And_Original_Currency, tbl_RWAC_Master.CEA, tbl_RWAC_Master.Amount, tbl_RWAC_Master.Accrued, tbl_RWAC_Master.Adjust_Accrued, tbl_RWAC_Master.Specific_Provision, tbl_RWAC_Master.Credit_Exposure, tbl_RWAC_Master.Credit_Type, tbl_RWAC_Master.Remaining_Date, tbl_RWAC_Master.DealDate, tbl_RWAC_Master.Maturity_Date, tbl_RWAC_Master.Allocate_Asset_Amount, tbl_RWAC_Master.Customer_Location, tbl_RWAC_Master.Credit_Risk_Subtype, tbl_RWAC_Master.Remaining_Transaction_Amount, tbl_RWAC_Master.[Memo], tbl_RWAC_Master.Amount_THB, tbl_RWAC_Master.AOD" & _
                                    " FROM tbl_RWAC_Master INNER JOIN tbl_Currency_Country ON (tbl_RWAC_Master.Customer_Location = tbl_Currency_Country.Country_Code) AND (IIF(tbl_RWAC_Master.Memo='PCFXCOBK','THB',tbl_RWAC_Master.FX_And_Original_Currency) = tbl_Currency_Country.Currency)" & _
                                    " WHERE (((tbl_RWAC_Master.Credit_Exposure) Not In ('DVP','Non-DVP')) AND ((tbl_RWAC_Master.Remaining_Date)<=90) AND (ISNULL(tbl_RWAC_Master.Remaining_Date,'') <>'') AND ((tbl_RWAC_Master.Credit_Risk_Subtype) In ('2.1.1','4','5')) AND ((IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued]))<>0) AND ((tbl_RWAC_Master.Memo)<>'PCFXCOBK')) OR " & _
                                    " (((tbl_RWAC_Master.Credit_Exposure) Not In ('DVP','Non-DVP')) AND ((tbl_RWAC_Master.Remaining_Date)<=90) AND (ISNULL (tbl_RWAC_Master.Remaining_Date, '') <>'') AND ((tbl_RWAC_Master.Credit_Risk_Subtype) In ('2.1.1','4','5')) AND ((IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued]))<>0) AND ((tbl_RWAC_Master.Memo)='PCFXCOBK') AND ((tbl_RWAC_Master.Customer_Location)='TH')) OR " & _
                                    " (((tbl_RWAC_Master.Credit_Exposure) Not In ('DVP','Non-DVP')) AND ((tbl_RWAC_Master.Credit_Risk_Subtype) In ('1.2')) AND ((IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued]))<>0) AND ((tbl_RWAC_Master.Memo)<>'PCFXCOBK')) AND " & _
                                    " (ISNULL (tbl_RWAC_Master.Remaining_Date, '') <>'') OR " & _
                                    " (((tbl_RWAC_Master.Credit_Exposure) Not In ('DVP','Non-DVP')) AND ((tbl_RWAC_Master.Credit_Risk_Subtype) In ('1.2')) AND ((IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued]))<>0) AND ((tbl_RWAC_Master.Memo)='PCFXCOBK') AND ((tbl_RWAC_Master.Customer_Location)='TH') AND (ISNULL (tbl_RWAC_Master.Remaining_Date, '') <>''))")

            Dim strSQL As String
            Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
            oconn1.Open()
            strSQL = " SELECT Distinct(Cust_ID) as Cust_ID from tbl_Funding_Master"

            Dim objCommand As New SqlCommand(strSQL, oconn1)

            Dim ds As New DataSet()
            Dim adp As New SqlDataAdapter(objCommand)

            adp.Fill(ds)
            ddlCustID.DataSource = ds
            ddlCustID.DataTextField = "Cust_ID"
            ddlCustID.DataValueField = "Cust_ID"
            ddlCustID.DataBind()
            ds.Clear()


            strSQL = " SELECT Distinct(CCY) as Currency from tbl_Funding_List"
            objCommand = New SqlCommand(strSQL, oconn1)
            adp = New SqlDataAdapter(objCommand)

            adp.Fill(ds)
            ddlListCurrency.DataSource = ds
            ddlListCurrency.DataTextField = "Currency"
            ddlListCurrency.DataValueField = "Currency"
            ddlListCurrency.DataBind()
            ds.Clear()




            adp.Dispose()
            objCommand.Dispose()
            oconn1.Close()


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

    Protected Sub BindData(ByVal Cust_ID As String, ByVal Currency As String)
        Dim ds As DataSet
        ds = clsAllocateFunding.getSearch("30/09/2015", Cust_ID, Currency)
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
        If txtSearch.Text = "1" Then
            BindData(ddlCustID.SelectedValue, ddlCurrency.SelectedValue)
        Else
            BindData()
        End If

        Dim Contract_ID As String = grdTran.Rows(e.NewEditIndex()).Cells(2).Text
        Contract_ID = Left(Contract_ID, 2)
        Dim CCY As String = grdTran.Rows(e.NewEditIndex()).Cells(3).Text

        If Contract_ID = "FX" Then
            CCY = "THB"
        End If

        ddlListCurrency.ClearSelection()
        ddlListCurrency.Items.FindByValue(CCY).Selected = True

        ddlListCurrency.Enabled = False

        If ddlListCurrency.SelectedValue <> "Empthy" Then

            Dim strSQL As String
            Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
            oconn1.Open()
            strSQL = "SELECT Sum(bal)-ISNULL(Sum_Allocated_Amount,0) AS bal FROM tbl_Funding_List WHERE ccy = '" & ddlListCurrency.SelectedValue & "' GROUP BY Sum_Allocated_Amount"



            Dim ds As New DataSet()
            Dim objCommand As New SqlCommand(strSQL, oconn1)
            Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()

            If objDataReader.HasRows Then
                Do While objDataReader.Read()
                    txtRemainingAmount.Text = Format(objDataReader.Item("bal"), "#,##0.00")

                Loop

            Else

                Exit Sub
            End If


            txtRemainingAmount.DataBind()

            objDataReader.Close()
            objCommand.Dispose()
            oconn1.Close()

        End If


        grdTran.Rows(e.NewEditIndex).Cells(9).Controls(0).Focus()

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        If txtSearch.Text = "1" Then
            BindData(ddlCustID.SelectedValue, ddlCurrency.SelectedValue)
        Else
            BindData()
        End If
        
        ddlListCurrency.Enabled = True
    End Sub

    Protected Sub grd_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Contract_ID As String = grdTran.Rows(e.RowIndex).Cells(2).Text
        Dim Amount As String = grdTran.Rows(e.RowIndex).Cells(4).Text
        Dim Allocate_Amount As Double
        Allocate_Amount = e.NewValues("Allocate_Asset_Amount")

        If Allocate_Amount > CDbl(Amount) Then
            Response.Write("<script type=""text/javascript"">alert(""Allocated Amount cannot set to greater than Amount of Funding"");</script>")
            Exit Sub
        End If

        If Allocate_Amount > CDbl(txtRemainingAmount.Text) Then
            Response.Write("<script type=""text/javascript"">alert(""Allocated Amount cannot set to greater than Remaining Amount of Funding"");</script>")
            Exit Sub
        End If

        SQLConnect.ExcNonQuery("Update tbl_Funding_Master set Allocate_Asset_Amount = '" & e.NewValues("Allocate_Asset_Amount") & "'" & _
                               " where Contract_ID = '" & Contract_ID & "'")

        SQLConnect.ExcNonQuery("Update tbl_Funding_Master set Remaining_Transaction_Amount = Amount - Allocate_Asset_Amount" & _
                               " where Contract_ID = '" & Contract_ID & "'")


        Dim strSQL As String
        Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
        oconn1.Open()
        strSQL = "SELECT Currency, Sum_Allocated_Amount FROM Q_Sum_Allocated_Amount"


        Dim objCommand As New SqlCommand(strSQL, oconn1)
        Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then
            Do While objDataReader.Read()
                SQLConnect.ExcNonQuery("UPDATE tbl_Funding_List" & _
                            " SET Sum_Allocated_Amount = '" & objDataReader.Item("Sum_Allocated_Amount") & "'" & _
                            " WHERE ccy = '" & objDataReader.Item("Currency") & "'")
                Debug.Print(objDataReader.Item("Sum_Allocated_Amount") & "_" & objDataReader.Item("Currency"))
            Loop

        Else

            Exit Sub
        End If


        objDataReader.Close()
        objCommand.Dispose()
        oconn1.Close()




        ddlListCurrency.Enabled = True

        

        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master INNER JOIN tbl_Funding_Master ON tbl_Funding_Master.Contract_ID = tbl_RWAC_Master.Contract_ID SET tbl_RWAC_Master.Allocate_Asset_Amount = tbl_Funding_Master.Allocate_Asset_Amount, tbl_RWAC_Master.Remaining_Transaction_Amount = tbl_Funding_Master.Remaining_Transaction_Amount")

        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.Remaining_Transaction_Amount = tbl_RWAC_Master.FX_AND_Original_Amount_THB-tbl_RWAC_Master.Allocate_Asset_Amount, tbl_RWAC_Master.Funding_Allocated_Amount = tbl_RWAC_Master.[Allocate_Asset_Amount*tbl_RWAC_Master.RW_Funding")

        If ddlListCurrency.SelectedValue <> "Empthy" Then

            Dim strSQL2 As String
            Dim oconn2 As New SqlConnection(SQLConnect.getConnectionString())
            oconn2.Open()
            strSQL2 = "SELECT Sum(bal)-ISNULL(Sum_Allocated_Amount,0) AS bal FROM tbl_Funding_List WHERE ccy = '" & ddlListCurrency.SelectedValue & "' GROUP BY Sum_Allocated_Amount"



            Dim objCommand2 As New SqlCommand(strSQL2, oconn2)
            Dim objDataReader2 As SqlDataReader = objCommand2.ExecuteReader()

            If objDataReader2.HasRows Then
                Do While objDataReader2.Read()
                    txtRemainingAmount.Text = Format(objDataReader2.Item("bal"), "#,##0.00")

                Loop

            Else

                Exit Sub
            End If


            txtRemainingAmount.DataBind()

            objDataReader2.Close()
            objCommand2.Dispose()
            oconn2.Close()

        End If


        grdTran.EditIndex = -1
        If txtSearch.Text = "1" Then
            BindData(ddlCustID.SelectedValue, ddlCurrency.SelectedValue)
        Else
            BindData()
        End If
    End Sub



    Protected Sub ddlCustID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustID.SelectedIndexChanged
        If ddlCustID.SelectedValue <> "Empthy" Then

            Dim strSQL As String
            Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
            oconn1.Open()
            strSQL = "SELECT DISTINCT(tbl_Funding_Master.FX_And_Original_Currency) AS Currency FROM tbl_Funding_Master WHERE Cust_ID = '" & ddlCustID.SelectedValue & "'"

            Dim objCommand As New SqlCommand(strSQL, oconn1)

            Dim ds As New DataSet()
            Dim adp As New SqlDataAdapter(objCommand)

            adp.Fill(ds)
            ddlCurrency.Items.Clear()
            ddlCurrency.DataSource = ds
            ddlCurrency.DataTextField = "Currency"
            ddlCurrency.DataValueField = "Currency"
            ddlCurrency.DataBind()


            adp.Dispose()
            objCommand.Dispose()
            oconn1.Close()
        Else
            ddlCurrency.Items.Clear()
            ddlCurrency.DataBind()
        End If

    End Sub

    Protected Sub ddlListCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlListCurrency.SelectedIndexChanged
        If ddlListCurrency.SelectedValue <> "Empthy" Then

            Dim strSQL As String
            Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
            oconn1.Open()
            strSQL = "SELECT Sum(bal)-ISNULL(Sum_Allocated_Amount,0) AS bal FROM tbl_Funding_List WHERE ccy = '" & ddlListCurrency.SelectedValue & "' GROUP BY Sum_Allocated_Amount"



            Dim ds As New DataSet()
            Dim objCommand As New SqlCommand(strSQL, oconn1)
            Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()

            If objDataReader.HasRows Then
                Do While objDataReader.Read()
                    txtRemainingAmount.Text = Format(objDataReader.Item("bal"), "#,##0.00")

                Loop

            Else

                Exit Sub
            End If


            txtRemainingAmount.DataBind()

            objDataReader.Close()
            objCommand.Dispose()
            oconn1.Close()

        End If

        
    End Sub

    Protected Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click

        If ddlCustID.SelectedValue = "Empthy" Or ddlCurrency.SelectedValue = "Empthy" Then
            Response.Write("<script type=""text/javascript"">alert(""Please Select Cust_ID and Currency."");</script>")
        Else
            BindData(ddlCustID.SelectedValue, ddlCurrency.SelectedValue)
            txtSearch.Text = "1"
            txtSearch.DataBind()
        End If



    End Sub

    Protected Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        BindData()
        txtSearch.Text = "0"
        txtSearch.DataBind()
    End Sub


    Protected Sub grdCRSMain_PageIndexChanging2(sender As Object, e As GridViewPageEventArgs) Handles grdTran2.PageIndexChanging
        grdTran2.PageIndex = e.NewPageIndex
        BindData2()
    End Sub

End Class