Imports System.Data.SqlClient

Public Class EcaiMapping
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
            BindData2()
            'asd
            'Dim tb As HtmlTable
            'tb = tblHeader
            'Dim rowscount As Long
            'rowscount = tblHeader.Rows.Count

            'For i = 0 To rowscount - 1

            '    Dim tcs As HtmlTableCellCollection = tb.Rows(i).Cells
            '    tcs(1).Visible = True
            'Next i
            txtLabel.Text = "Transaction Mapping Counterparty Name"
            txtMenu.Text = "Transaction"
            grdTran.Columns(1).Visible = False
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsEcaiMapping.getSovereign()
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub BindDataTransaction()
        Dim ds As DataSet
        ds = clsEcaiMapping.getTransaction()
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub BindData2()
        Dim ds As DataSet
        ds = clsEcaiMapping.getTransaction()
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

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        BindData()
        
    End Sub

    



    

    Protected Sub btn_Sovereign_Click(sender As Object, e As EventArgs) Handles btn_Sovereign.Click

        'Dim tb As HtmlTable
        'tb = tblHeader
        'Dim rowscount As Long
        'rowscount = tblHeader.Rows.Count

        'For i = 0 To rowscount - 1

        '    Dim tcs As HtmlTableCellCollection = tb.Rows(i).Cells
        '    tcs(1).Visible = False
        'Next i

        grdTran.Columns(1).Visible = False

        Dim ds As DataSet
        ds = clsEcaiMapping.getSovereign()
        grdTran.DataSource = ds
        grdTran.DataBind()

    End Sub

    Protected Sub btn_Coporate_Click(sender As Object, e As EventArgs) Handles btn_Coporate.Click
        'Dim tb As HtmlTable
        'tb = tblHeader
        'Dim rowscount As Long
        'rowscount = tblHeader.Rows.Count

        'For i = 0 To rowscount - 1

        '    Dim tcs As HtmlTableCellCollection = tb.Rows(i).Cells
        '    tcs(1).Visible = False
        'Next i

        grdTran.Columns(1).Visible = False

        Dim ds As DataSet
        ds = clsEcaiMapping.getCoporate()
        grdTran.DataSource = ds
        grdTran.DataBind()



    End Sub

    Protected Sub btn_Specific_Click(sender As Object, e As EventArgs) Handles btn_Specific.Click
        'Dim tb As HtmlTable
        'tb = tblHeader
        'Dim rowscount As Long
        'rowscount = tblHeader.Rows.Count

        'For i = 0 To rowscount - 1

        '    Dim tcs As HtmlTableCellCollection = tb.Rows(i).Cells
        '    tcs(1).Visible = True
        'Next i

        grdTran.Columns(1).Visible = True

        Dim ds As DataSet
        ds = clsEcaiMapping.getSpecific()
        grdTran.DataSource = ds
        grdTran.DataBind()




    End Sub



    Protected Sub grd_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim ECAI_Name As String = grdTran.SelectedRow.Cells(0).Text

        txtECAI.Text = ECAI_Name


    End Sub

    Protected Sub grd_OnSelectedIndexChanged2(ByVal sender As Object, ByVal e As EventArgs)
        Dim Cust_Name As String = grdTran2.SelectedRow.Cells(0).Text

        txtCustName.Text = Cust_Name


    End Sub

    Protected Sub btn_Mapping_Click(sender As Object, e As EventArgs) Handles btn_Mapping.Click
        If txtCustName.Text = "" Or txtECAI.Text = "" Then
            Response.Write("<script type=""text/javascript"">alert(""Please Select Both Side."");</script>")
            Exit Sub
        Else
            If txtMenu.Text = "Transaction" Then
                'Delete when tbl_Mapping_Counterparty_Name already has one
                SQLConnect.ExcNonQuery("DELETE FROM tbl_Mapping_CounterPartyName " & _
                         "WHERE RWA_Counter_Party_Name = '" & txtCustName.Text & "'")

                'Insert Before Mapping because Master will change permanantly

                SQLConnect.ExcNonQuery("INSERT INTO tbl_Mapping_CounterPartyName (ECAI_Counter_Party_Name, RWA_Counter_Party_Name)  " & _
                                 " VALUES ('" & txtECAI.Text & "','" & txtCustName.Text & "')")

                'Clear RW
                SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                            " SET RW = NULL" & _
                            " WHERE Cust_Name = '" & txtCustName.Text & "'")

                Dim ds As DataSet
                ds = clsEcaiMapping.getTransaction()
                grdTran2.DataSource = ds
                grdTran2.DataBind()

            ElseIf txtMenu.Text = "Collateral" Then
                'Delete when tbl_Mapping_Counterparty_Name already has one
                SQLConnect.ExcNonQuery("DELETE FROM tbl_Mapping_CounterPartyName " & _
                         "WHERE RWA_Counter_Party_Name = '" & txtCustName.Text & "'")

                'Insert Before Mapping because Master will change permanantly

                SQLConnect.ExcNonQuery("INSERT INTO tbl_Mapping_CounterPartyName (ECAI_Counter_Party_Name, RWA_Counter_Party_Name)  " & _
                                 " VALUES ('" & txtECAI.Text & "','" & txtCustName.Text & "')")

                'Clear RW
                SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                            " SET RW_Collateral = NULL" & _
                            " WHERE Counter_Party_Name = '" & txtCustName.Text & "'")

                SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                            " SET RW_Collateral = NULL" & _
                            " WHERE Owner_Guarantor_Name = '" & txtCustName.Text & "'")

                Dim ds As DataSet
                ds = clsEcaiMapping.getCollateral()
                grdTran2.DataSource = ds
                grdTran2.DataBind()

            ElseIf txtMenu.Text = "Bond" Then
                'Delete when tbl_Mapping_Counterparty_Name already has one
                SQLConnect.ExcNonQuery("DELETE FROM tbl_Mapping_CounterPartyName " & _
                         "WHERE RWA_Counter_Party_Name = '" & txtCustName.Text & "'")

                'Insert Before Mapping because Master will change permanantly

                SQLConnect.ExcNonQuery("INSERT INTO tbl_Mapping_CounterPartyName (ECAI_Counter_Party_Name, RWA_Counter_Party_Name)  " & _
                                 " VALUES ('" & txtECAI.Text & "','" & txtCustName.Text & "')")

                'Clear RW
                SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                            " SET RW = NULL" & _
                            " WHERE Contract_ID = '" & txtCustName.Text & "'")

                Dim ds As DataSet
                ds = clsEcaiMapping.getBond()
                grdTran2.DataSource = ds
                grdTran2.DataBind()

            ElseIf txtMenu.Text = "NON_DVP" Then
                'Delete when tbl_Mapping_Counterparty_Name already has one
                SQLConnect.ExcNonQuery("DELETE FROM tbl_Mapping_CounterPartyName " & _
                         "WHERE RWA_Counter_Party_Name = '" & txtCustName.Text & "'")

                'Insert Before Mapping because Master will change permanantly

                SQLConnect.ExcNonQuery("INSERT INTO tbl_Mapping_CounterPartyName (ECAI_Counter_Party_Name, RWA_Counter_Party_Name)  " & _
                                 " VALUES ('" & txtECAI.Text & "','" & txtCustName.Text & "')")

                'Clear RW
                SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master" & _
                            " SET RW = NULL" & _
                            " WHERE Cust_Name = '" & txtCustName.Text & "'")

                Dim ds As DataSet
                ds = clsEcaiMapping.getNonDVP()
                grdTran2.DataSource = ds
                grdTran2.DataBind()

            End If
            

            Response.Write("<script type=""text/javascript"">alert(""Mapping Completed"");</script>")
            

        End If



    End Sub

    Protected Sub grd_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(Me.grdTran, "Select$" & e.Row.RowIndex)

        End If


    End Sub

    Protected Sub grd_OnRowDataBound2(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(Me.grdTran2, "Select$" & e.Row.RowIndex)

        End If


    End Sub



    Protected Sub Transaction_Mapping_Click(sender As Object, e As EventArgs) Handles Transaction_Mapping.Click
        Dim ds As DataSet
        ds = clsEcaiMapping.getTransaction()
        grdTran2.DataSource = ds
        grdTran2.DataBind()
        txtMenu.Text = "Transaction"
    End Sub

    Protected Sub Collateral_Mapping_Click(sender As Object, e As EventArgs) Handles Collateral_Mapping.Click
        Dim ds As DataSet
        ds = clsEcaiMapping.getCollateral()
        grdTran2.DataSource = ds
        grdTran2.DataBind()
        txtMenu.Text = "Collateral"
    End Sub

    Protected Sub Bond_Mapping_Click(sender As Object, e As EventArgs) Handles Bond_Mapping.Click
        Dim ds As DataSet
        ds = clsEcaiMapping.getBond()
        grdTran2.DataSource = ds
        grdTran2.DataBind()
        txtMenu.Text = "Bond"
    End Sub

    Protected Sub NON_DVP_Mapping_Click(sender As Object, e As EventArgs) Handles NON_DVP_Mapping.Click
        Dim ds As DataSet
        ds = clsEcaiMapping.getNonDVP()
        grdTran2.DataSource = ds
        grdTran2.DataBind()
        txtMenu.Text = "NON_DVP"
    End Sub
End Class