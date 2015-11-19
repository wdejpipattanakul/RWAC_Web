Public Class CRMAllocation
    Inherits System.Web.UI.Page

    Private CRMType As String, TRNType As String
    Dim totalAllocatedAmount As Decimal = 0
    Dim isUpdate As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sql As String
            'DELETE FROM tbl_Summary_CRM_Calculation_Method
            SQLConnect.ExcNonQuery("DELETE FROM tbl_Summary_CRM_Calculation_Method")

            'DoCmd.OpenQuery "Q_Insert_Summary_CRM_Calculation_Method"
            sql = " INSERT INTO tbl_Summary_CRM_Calculation_Method ( Cust_ID, Facility_ID, Transaction_ID, Calculation_Method )"
            sql += "SELECT T.Cust_ID, T.Facility_ID, T.Transaction_ID, Min(T.TRN_Calculation_Method_Ori) AS Calculation_Method"
            sql += "  FROM tbl_CRM_Allocated AS T"
            sql += " WHERE T.CRM_Allocated_Amount <> 0"
            sql += " GROUP BY T.Cust_ID, T.Facility_ID, T.Transaction_ID"
            SQLConnect.ExcNonQuery(sql)

            'Q_Update_RWA_Calculation_Method
            SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_RWAC_Master.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_RWAC_Master.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID) SET tbl_RWAC_Master.Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.[Calculation_Method],IIF(tbl_RWAC_Master.Trading_Book_Flag = True, 'Comprehensive', 'Simple'))")

            'Q_Update_CRM_Calculation_Method
            SQLConnect.ExcNonQuery("UPDATE (tbl_CRM_Allocated LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_CRM_Allocated.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID)) LEFT JOIN tbl_RWAC_Master ON (tbl_CRM_Allocated.Cust_ID = tbl_RWAC_Master.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_RWAC_Master.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_RWAC_Master.Contract_ID) SET tbl_CRM_Allocated.TRN_Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method,IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple'))), tbl_CRM_Allocated.TRN_Calculation_Method_Ori = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method, IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple')))")


            'Q_Update_CRM_RW_Hc
            SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Allocated LEFT JOIN tbl_CRM_Master ON tbl_CRM_Allocated.ID = tbl_CRM_Master.ID SET tbl_CRM_Allocated.CRM_Hc_Value = [tbl_CRM_Master].[CRM_Hc_Value], tbl_CRM_Allocated.CRM_RW = [tbl_CRM_Master].[RW_Collateral]")

            TRNType = ""
            CRMType = ""
            BindData(TRNType)
            BindCRMData("", "", "", "")
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Unload
        If isUpdate Then
            Dim sql As String = ""
            '' Update Calculation Method
            'DoCmd.RunSQL "DELETE FROM tbl_Summary_CRM_Calculation_Method"
            SQLConnect.ExcNonQuery("DELETE FROM tbl_Summary_CRM_Calculation_Method")

            'DoCmd.OpenQuery "Q_Insert_Summary_CRM_Calculation_Method"
            sql = "  INSERT INTO tbl_Summary_CRM_Calculation_Method ( Cust_ID, Facility_ID, Transaction_ID, Calculation_Method )"
            sql += " SELECT T.Cust_ID, T.Facility_ID, T.Transaction_ID, Min(T.TRN_Calculation_Method_Ori) AS Calculation_Method"
            sql += "   FROM tbl_CRM_Allocated AS T"
            sql += "  WHERE T.CRM_Allocated_Amount <> 0"
            sql += "  GROUP BY T.Cust_ID, T.Facility_ID, T.Transaction_ID"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Calculation_Method"
            sql = "UPDATE (tbl_CRM_Allocated LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_CRM_Allocated.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID)) LEFT JOIN tbl_RWAC_Master ON (tbl_CRM_Allocated.Cust_ID = tbl_RWAC_Master.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_RWAC_Master.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_RWAC_Master.Contract_ID) SET tbl_CRM_Allocated.TRN_Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method,IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple'))), tbl_CRM_Allocated.TRN_Calculation_Method_Ori = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method, IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple')))"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_RWA_Calculation_Method"
            sql = "UPDATE tbl_RWAC_Master LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_RWAC_Master.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_RWAC_Master.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID) SET tbl_RWAC_Master.Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.[Calculation_Method],IIF(tbl_RWAC_Master.Trading_Book_Flag = True, 'Comprehensive', 'Simple'));"
            SQLConnect.ExcNonQuery(sql)

            '' Update CRM Collateral Amount
            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Bond_Simple_Currency_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]-[CRM_Hfx_Value]) WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Simple') AND ((tbl_CRM_Allocated.CRM_Type)='Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Other_Simple_Currency_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]-[CRM_Hfx_Value]) WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Simple') AND ((tbl_CRM_Allocated.CRM_Type)<>'Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Bond_Comprehensive_Currency_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10)-[CRM_Hfx_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10)), tbl_CRM_Allocated.CRM_RW = 1 WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Comprehensive') AND ((tbl_CRM_Allocated.CRM_Type)='Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Other_Comprehensive_Currency_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]-[CRM_Hfx_Value]), tbl_CRM_Allocated.CRM_RW = 1 WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Comprehensive') AND ((tbl_CRM_Allocated.CRM_Type)<>'Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Bond_Comprehensive_Maturity_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10)-[CRM_Hfx_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10))*((([CRM_Remaining_Term]/365)-0.25)/(([TRN_Remaining_Term]/365)-0.25)), tbl_CRM_Allocated.CRM_RW = 1 WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Comprehensive') AND ((tbl_CRM_Allocated.CRM_Is_Maturity_Mismatch)=True) AND ((tbl_CRM_Allocated.CRM_Type)='Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Other_Comprehensive_Maturity_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]-[CRM_Hfx_Value])*((([CRM_Remaining_Term]/365)-0.25)/(([TRN_Remaining_Term]/365)-0.25)), tbl_CRM_Allocated.CRM_RW = 1 WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Comprehensive') AND ((tbl_CRM_Allocated.CRM_Is_Maturity_Mismatch)=True) AND ((tbl_CRM_Allocated.CRM_Type)<>'Stock'));"
            SQLConnect.ExcNonQuery(sql)


            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Bond_Simple_Maturity_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10)-[CRM_Hfx_Value]*Sqr(([CRM_Revalue_Term]+([CRM_Holding_Period]-1))/10))*((([CRM_Remaining_Term]/365)-0.25)/(([TRN_Remaining_Term]/365)-0.25)) WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Simple') AND ((tbl_CRM_Allocated.CRM_Is_Maturity_Mismatch)=True) AND ((tbl_CRM_Allocated.CRM_Type)='Stock'));"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_CRM_Amount_Other_Simple_Maturity_Mismatch"
            sql = "UPDATE tbl_CRM_Allocated SET tbl_CRM_Allocated.CRM_C_Value = [CRM_Allocated_Amount]*(1-[CRM_Hc_Value]-[CRM_Hfx_Value])*((([CRM_Remaining_Term]/365)-0.25)/(([TRN_Remaining_Term]/365)-0.25)) WHERE (((tbl_CRM_Allocated.TRN_Calculation_Method)='Simple') AND ((tbl_CRM_Allocated.CRM_Is_Maturity_Mismatch)=True) AND ((tbl_CRM_Allocated.CRM_Type)<>'Stock'));"
            SQLConnect.ExcNonQuery(sql)

            '' Update RWA Master Collateral Amount
            'DoCmd.RunSQL "DELETE FROM tbl_Summary_CRM_Allocated"
            SQLConnect.ExcNonQuery("DELETE FROM tbl_Summary_CRM_Allocated")

            'DoCmd.OpenQuery "Q_Insert_Summary_CRM_Allocated"
            sql = "  INSERT INTO tbl_Summary_CRM_Allocated ( Cust_ID, Facility_ID, Contract_ID, Allocated_Amount, RWA )"
            sql += " SELECT tbl_RWAC_Master.Cust_ID, tbl_RWAC_Master.Facility_ID, tbl_RWAC_Master.Contract_ID, Sum(Nz([tbl_CRM_Allocated].[CRM_C_Value],0)) AS Allocated_Amount, Sum(Nz([tbl_CRM_Allocated].[CRM_C_Value],0)*Nz([tbl_CRM_Allocated].[CRM_RW],0)) AS RWA"
            sql += "   FROM tbl_RWAC_Master LEFT JOIN tbl_CRM_Allocated ON (tbl_RWAC_Master.Facility_ID = tbl_CRM_Allocated.Facility_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_CRM_Allocated.Transaction_ID) AND (tbl_RWAC_Master.Cust_ID = tbl_CRM_Allocated.Cust_ID)"
            sql += "  GROUP BY tbl_RWAC_Master.Cust_ID, tbl_RWAC_Master.Facility_ID, tbl_RWAC_Master.Contract_ID;"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Update_Master_CRM_Allocated"
            sql = "UPDATE (SELECT * FROM tbl_RWAC_Master LEFT JOIN tbl_Summary_CRM_Allocated ON (tbl_RWAC_Master.Contract_ID = tbl_Summary_CRM_Allocated.Contract_ID) AND (tbl_RWAC_Master.Facility_ID = tbl_Summary_CRM_Allocated.Facility_ID) AND (tbl_RWAC_Master.Cust_ID = tbl_Summary_CRM_Allocated.Cust_ID))  AS CRM SET CRM.CRM_Allocated_Amount = NZ( tbl_Summary_CRM_Allocated.Allocated_Amount,0), CRM.RWA_Collateral = NZ(tbl_Summary_CRM_Allocated.RWA,0);"
            SQLConnect.ExcNonQuery(sql)

            isUpdate = False
        End If
    End Sub

    Protected Sub BindData(ByVal strType As String)
        Dim ds As DataSet
        ds = clsCRMAllocation.getCRMTransactionList(strType)
        grdTRNList.DataSource = ds
        grdTRNList.DataBind()

        grdTRNList.SelectedIndex = -1
    End Sub

    Protected Sub BindCRMData(ByVal custID As String, ByVal facilID As String, ByVal tranID As String, ByVal type As String)
        Dim ds As DataSet
        ds = clsCRMAllocation.getCRMList(custID, facilID, tranID, type)
        grdCRMList.DataSource = ds
        grdCRMList.DataBind()
    End Sub

    Protected Sub grdRWAMaster_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTRNList.PageIndexChanging
        grdTRNList.PageIndex = e.NewPageIndex
        BindData(TRNType)
        BindCRMData("", "", "", "")
    End Sub

    Protected Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        TRNType = ""
        BindData(TRNType)
        BindCRMData("", "", "", "")
    End Sub

    Protected Sub btnCRM_Click(sender As Object, e As EventArgs) Handles btnCRM.Click
        TRNType = "CRM"
        BindData(TRNType)
        BindCRMData("", "", "", "")
    End Sub

    Protected Sub btnNoCRM_Click(sender As Object, e As EventArgs) Handles btnNoCRM.Click
        TRNType = "NoCRM"
        BindData(TRNType)
        BindCRMData("", "", "", "")
    End Sub

    Protected Sub grdTRNList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdTRNList.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#93A3B0'; this.style.color='White'; this.style.cursor='pointer'")
                    'If e.Row.RowState = DataControlRowState.Alternate Then

                    '    e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='{0}';", grdCRMList.AlternatingRowStyle.BackColor.ToKnownColor()))
                    'Else
                    '    e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='{0}';", grdCRMList.RowStyle.BackColor.ToKnownColor()))
                    'End If
                    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdTRNList, "Select$" + e.Row.RowIndex.ToString()))
                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdTRNList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdTRNList.SelectedIndexChanged
        Try
            'ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + grdTRNList.SelectedRow.Cells(4).Text + "')</script>")
            BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdCRMList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCRMList.PageIndexChanging
        grdCRMList.PageIndex = e.NewPageIndex
        BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
    End Sub

    Protected Sub btnCAll_Click(sender As Object, e As EventArgs) Handles btnCAll.Click
        CRMType = ""
        BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
    End Sub

    Protected Sub btnCOnbal_Click(sender As Object, e As EventArgs) Handles btnCOnbal.Click
        CRMType = "On Balance"
        BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
    End Sub

    Protected Sub btnCFinan_Click(sender As Object, e As EventArgs) Handles btnCFinan.Click
        CRMType = "Financial"
        BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
    End Sub

    Protected Sub btnCGuara_Click(sender As Object, e As EventArgs) Handles btnCGuara.Click
        CRMType = "Guarantee"
        BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
    End Sub

    Protected Sub txtAllocated_TextChanged(sender As Object, e As EventArgs)
        Dim errMsg As String = ""
        Dim currentRow As GridViewRow = CType(CType(sender, TextBox).Parent.Parent, GridViewRow)
        Dim txtRemaining As Label = CType(currentRow.FindControl("lblRemaining"), Label)
        Dim txtAllocate As TextBox = CType(currentRow.FindControl("txtAllocated"), TextBox)
        Dim txtAllocateOri As HiddenField = CType(currentRow.FindControl("txtAllocatedOri"), HiddenField)

        Dim trnRemainingAmount As Double = CDbl(grdTRNList.SelectedRow.Cells(11).Text)
        Try
            Dim crmAllocatedAmount As Double = CDbl(txtAllocate.Text)
            Dim crmRemainingAmount As Double = CDbl(txtRemaining.Text)
            Dim crmAllocatedAmountOri As Double = CDbl(txtAllocateOri.Value)

            If totalAllocatedAmount + crmAllocatedAmount > trnRemainingAmount Then
                errMsg = "Allocated Amount cannot set to greater than Transaction Remaining Amount"
            Else
                If crmAllocatedAmount < 0 Then
                    errMsg = "Allocated Amount cannot set to negative value"
                ElseIf crmAllocatedAmount = 0 Then
                    crmRemainingAmount = crmRemainingAmount + crmAllocatedAmountOri
                    crmAllocatedAmountOri = 0
                    txtAllocateOri.Value = 0
                ElseIf crmAllocatedAmount <> crmAllocatedAmountOri Then
                    If crmAllocatedAmount > crmRemainingAmount + crmAllocatedAmountOri Then
                        errMsg = "Allocated Amount cannot set to greater than CRM Remaining Amount"
                    End If
                End If

                crmRemainingAmount = crmRemainingAmount + (crmAllocatedAmountOri - crmAllocatedAmount)
            End If

            If errMsg = "" Then
                txtAllocate.Text = FormatNumber(crmAllocatedAmount, 2)
                txtRemaining.Text = FormatNumber(crmRemainingAmount, 2)
                txtAllocateOri.Value = FormatNumber(crmAllocatedAmountOri, 2)

                totalAllocatedAmount = totalAllocatedAmount - (crmAllocatedAmountOri - crmAllocatedAmount)
                grdCRMList.FooterRow.Cells(9).Text = "Total: " & FormatNumber(totalAllocatedAmount, 2)
                grdTRNList.SelectedRow.Cells(11).Text = FormatNumber(trnRemainingAmount + (crmAllocatedAmountOri - crmAllocatedAmount), 2)

                'DoCmd.OpenQuery "Q_Insert_Summary_CRM_Calculation_Method"
                Dim sql As String
                sql = " UPDATE tbl_CRM_Allocated "
                sql += "   SET CRM_Allocated_Amount = " & crmAllocatedAmount.ToString() & ", CRM_Allocated_Amount_Ori = " & crmAllocatedAmountOri.ToString()
                sql += " WHERE [Cust_ID] = '" & CType(currentRow.FindControl("txtCustID"), HiddenField).Value & "' AND [Facility_ID] = '" & CType(currentRow.FindControl("txtFacilID"), HiddenField).Value & "' AND [Transaction_ID] = '" & CType(currentRow.FindControl("txtTransID"), HiddenField).Value & "' AND [ID] = '" & CType(currentRow.FindControl("txtID"), HiddenField).Value & "' "
                SQLConnect.ExcNonQuery(sql)

                sql = " UPDATE tbl_CRM_Allocated "
                sql += "   SET CRM_Remaining_Amount = " & crmRemainingAmount.ToString()
                sql += " WHERE [ID] = '" & CType(currentRow.FindControl("txtID"), HiddenField).Value & "'"
                SQLConnect.ExcNonQuery(sql)

                isUpdate = True
            Else
                txtAllocate.Text = FormatNumber(crmAllocatedAmountOri, 2)
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" & errMsg & "')</script>")
            End If           
        Catch ex As Exception
            Dim val As String
            If txtAllocateOri.Value = "" Then val = 0 Else val = txtAllocateOri.Value
            txtAllocate.Text = FormatNumber(CDbl(val), 2)
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please enter only numeric value')</script>")
        End Try

        'ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + txt.Text + "')</script>")
    End Sub

    Protected Sub grdCRMList_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles grdCRMList.RowUpdating
        Dim AllocatedAmount As Double = CDbl(CType(grdCRMList.Rows(e.RowIndex).FindControl("txtAllocated"), TextBox).Text)
        Dim RemainingAmount As Double = CDbl(CType(grdCRMList.Rows(e.RowIndex).FindControl("lblRemaining"), TextBox).Text)

        e.NewValues("CRM_Remaining_Amount") = RemainingAmount - AllocatedAmount
    End Sub

    Protected Sub grdCRMList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCRMList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            totalAllocatedAmount += CDbl(CType(e.Row.FindControl("txtAllocated"), TextBox).Text)
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(9).Text = "Total: " & FormatNumber(totalAllocatedAmount, 2)
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim sql As String = ""
        ''clear CRM table
        'DoCmd.RunSQL "DELETE FROM tbl_CRM_Allocated"
        SQLConnect.ExcNonQuery("DELETE FROM tbl_CRM_Allocated")
        'DoCmd.RunSQL "DELETE FROM tbl_CRM_Allocated_Working"
        SQLConnect.ExcNonQuery("DELETE FROM tbl_CRM_Allocated_Working")
        'DoCmd.OpenQuery "Q_Generate_CRM_List Query"

        '' Update Calculation Method
        'DoCmd.RunSQL "DELETE FROM tbl_Summary_CRM_Calculation_Method"
        sql = "  INSERT INTO tbl_CRM_Allocated ( AOD, Cust_ID, Facility_ID, Transaction_ID, CRM_ID, CRM_Start_Date, CRM_Maturity_Date, CRM_Currency, CRM_Amount, CRM_Remaining_Amount, CRM_Allocated_Amount, CRM_Allocated_Amount_Ori, CRM_RW, CRM_Type, TRN_Calculation_Method, TRN_Calculation_Method_Ori, CRM_Is_Currency_Mismatch, CRM_Is_Maturity_Mismatch, CRM_Hc_Value, CRM_Hfx_Value, TRN_Remaining_Term, CRM_Remaining_Term, CRM_Revalue_Term, CRM_Holding_Period, CRM_Use_Flag, CRM_Contract_Term, TYPE, ID )"
        sql += " SELECT Q_Generate_CRM_List.AOD, Q_Generate_CRM_List.Cust_ID, Q_Generate_CRM_List.Facility_ID, Q_Generate_CRM_List.Contract_ID, Q_Generate_CRM_List.Collateral_ID, Q_Generate_CRM_List.Start_Date, Q_Generate_CRM_List.CRM_Maturity, Q_Generate_CRM_List.CRM_Currency, Q_Generate_CRM_List.Amount, Q_Generate_CRM_List.CRM_Remaining_Amount, Q_Generate_CRM_List.CRM_Allocated_Amount, Q_Generate_CRM_List.CRM_Allocated_Amount_Ori, Q_Generate_CRM_List.RW_Collateral, Q_Generate_CRM_List.Collateral_Type, Q_Generate_CRM_List.[Calculation Method], Q_Generate_CRM_List.[Calculation Method], Q_Generate_CRM_List.[Currency Mismatch], Q_Generate_CRM_List.[Maturity Mismatch], Q_Generate_CRM_List.CRM_Hc_Value, Q_Generate_CRM_List.CRM_Hfx_Value, Q_Generate_CRM_List.TRN_Remaining_Term, Q_Generate_CRM_List.CRM_Remaining_Term, Q_Generate_CRM_List.CRM_Revalue_Term, Q_Generate_CRM_List.CRM_Holding_Period, Q_Generate_CRM_List.CRM_Use_Flag, Q_Generate_CRM_List.CRM_Contract_Term, Q_Generate_CRM_List.TYPE, Q_Generate_CRM_List.ID"
        sql += " FROM Q_Generate_CRM_List;"
        SQLConnect.ExcNonQuery(sql)

        'DoCmd.OpenQuery "Q_Insert_Summary_CRM_Calculation_Method"
        sql = "  INSERT INTO tbl_Summary_CRM_Calculation_Method ( Cust_ID, Facility_ID, Transaction_ID, Calculation_Method )"
        sql += " SELECT T.Cust_ID, T.Facility_ID, T.Transaction_ID, Min(T.TRN_Calculation_Method_Ori) AS Calculation_Method"
        sql += "   FROM tbl_CRM_Allocated AS T"
        sql += "  WHERE T.CRM_Allocated_Amount <> 0"
        sql += "  GROUP BY T.Cust_ID, T.Facility_ID, T.Transaction_ID"
        SQLConnect.ExcNonQuery(sql)

        'DoCmd.OpenQuery "Q_Update_CRM_Calculation_Method"
        sql = "UPDATE (tbl_CRM_Allocated LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_CRM_Allocated.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID)) LEFT JOIN tbl_RWAC_Master ON (tbl_CRM_Allocated.Cust_ID = tbl_RWAC_Master.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_RWAC_Master.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_RWAC_Master.Contract_ID) SET tbl_CRM_Allocated.TRN_Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method,IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple'))), tbl_CRM_Allocated.TRN_Calculation_Method_Ori = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method, IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple')))"
        SQLConnect.ExcNonQuery(sql)

        'DoCmd.OpenQuery "Q_Update_RWA_Calculation_Method"
        sql = "UPDATE tbl_RWAC_Master LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_RWAC_Master.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_RWAC_Master.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID) SET tbl_RWAC_Master.Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.[Calculation_Method],IIF(tbl_RWAC_Master.Trading_Book_Flag = True, 'Comprehensive', 'Simple'));"
        SQLConnect.ExcNonQuery(sql)

        '' Update CRM allocated from Master
        'DoCmd.OpenQuery "Q_Update_CRM_List"
        sql = "UPDATE tbl_CRM_Allocated INNER JOIN tbl_CRM_Master ON tbl_CRM_Allocated.ID = tbl_CRM_Master.ID SET tbl_CRM_Allocated.CRM_Use_Flag = IIF(tbl_CRM_Allocated.CRM_Use_Flag=false, false,IIf(TRN_Calculation_Method='Simple' AND tbl_CRM_Allocated.[TYPE] = 'Financial' And (CRM_Is_Currency_Mismatch=TRUE OR CRM_Is_Maturity_Mismatch= TRUE), FALSE, IIf(CRM_Remaining_Term IS NULL,FALSE,IIf(CRM_Contract_Term<365 OR CRM_Remaining_Term<90,FALSE,IIf(TRN_Calculation_Method='Simple' AND TRN_Remaining_Term>CRM_Remaining_Term,True,[tbl_CRM_Master].[CRM_Use_Flag]))))), tbl_CRM_Allocated.CRM_Revalue_Term = [tbl_CRM_Master].[CRM_Revalue_Term], tbl_CRM_Allocated.CRM_Holding_Period = [tbl_CRM_Master].[CRM_Holding_Period], tbl_CRM_Allocated.CRM_RW = [tbl_CRM_Master].[RW_Collateral], tbl_CRM_Allocated.Unused_Reason = IIF(tbl_CRM_Allocated.CRM_Use_Flag=false, '[CRM_Use_Flag] = False',IIf(TRN_Calculation_Method='Simple' AND tbl_CRM_Allocated.[TYPE] = 'Financial' AND(CRM_Is_Currency_Mismatch=TRUE OR CRM_Is_Maturity_Mismatch= TRUE), 'Simple approach not allow currency and maturity mismatch', IIf(CRM_Remaining_Term IS NULL,'Cannot find remaining term',IIf(CRM_Contract_Term<365 OR CRM_Remaining_Term<90,'Contract term must be greater than 365 days and remaining term must be greater than 90 days',IIf(TRN_Calculation_Method='Simple' AND TRN_Remaining_Term>CRM_Remaining_Term,'',IIf([tbl_CRM_Master].[CRM_Use_Flag]=FALSE,'[CRM_Use_Flag] = False',''))))));"
        SQLConnect.ExcNonQuery(sql)

        ''clear CRM Allocate on Master
        'DoCmd.RunSQL "UPDATE tbl_RWAC_Master SET RWA_Collateral = 0, CRM_Allocated_Amount = 0"
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET RWA_Collateral = 0, CRM_Allocated_Amount = 0")

        TRNType = ""
        BindData(TRNType)
        BindCRMData("", "", "", "")
        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Cleare All CRM Allocated Amount successfully.')</script>")
    End Sub
End Class