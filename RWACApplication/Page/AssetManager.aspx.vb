Imports System.Data.SqlClient

Public Class AssetManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()

        End If

    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsAssetManager.getMasterInquiry("30/09/2015")
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

    End Sub

    Protected Sub grd_RowCancelEditing(sender As Object, e As GridViewCancelEditEventArgs)
        grdTran.EditIndex = -1
        BindData()
    End Sub

    Protected Sub grd_OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Me.Page.ClientScript.GetPostBackClientHyperlink(Me.grdTran, "Select$" & e.Row.RowIndex)

        End If


    End Sub


    Protected Sub grd_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim Cust_ID As String = grdTran.SelectedRow.Cells(1).Text

        BindData2(Cust_ID)
        txtSearch.Text = Cust_ID


    End Sub


    Protected Sub grd_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim Cust_ID As String = grdTran.Rows(e.RowIndex).Cells(0).Text


        'Get hold of combobox
        Dim row As GridViewRow = DirectCast(sender, 
        GridView).Rows(e.RowIndex)
        Dim cbo As DropDownList =
        DirectCast(row.FindControl("ddlAsset_Classification"), DropDownList)
        Dim NPL_Start_Date As String
        NPL_Start_Date = e.NewValues("NPL_Start_Date")

        Dim NPL_Flag As Boolean


        'Set the New value of the object
        e.NewValues("Asset_Classification") = cbo.SelectedValue

        If e.NewValues("Asset_Classification") = "020001" Or e.NewValues("Asset_Classification") = "020002" Or e.NewValues("Asset_Classification") = "" Then
            NPL_Flag = False
            SQLConnect.ExcNonQuery("Update tbl_Customer_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = Null" & _
                                   " where Cust_ID = '" & Cust_ID & "'")

        Else
            NPL_Flag = True
            If NPL_Start_Date = "" Then
                Response.Write("<script type=""text/javascript"">alert(""NPL_Start_Date Can't Be Null"");</script>")
                Exit Sub
            End If

            SQLConnect.ExcNonQuery("Update tbl_Customer_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = '" & e.NewValues("NPL_Start_Date") & "'" & _
                               " where Cust_ID = '" & Cust_ID & "'")
        End If



        'MsgBox(e.NewValues("Credit_Exposure") + "_" + grdTran.Rows(e.RowIndex).Cells(20).Text)

        SQLConnect.ExcNonQuery("UPDATE (((tbl_RWAC_Master LEFT JOIN tbl_Customer_Master ON tbl_RWAC_Master.Cust_ID = tbl_Customer_Master.Cust_ID) LEFT JOIN tbl_Other_Asset ON tbl_RWAC_Master.Contract_ID = tbl_Other_Asset.Transaction_ID) LEFT JOIN tbl_DvP_Non_DvP ON (tbl_RWAC_Master.Cust_ID = tbl_DvP_Non_DvP.Cust_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_DvP_Non_DvP.Transaction_ID)) LEFT JOIN tbl_Asset_Transaction_Master ON (tbl_RWAC_Master.Cust_ID = tbl_Asset_Transaction_Master.Cust_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Asset_Transaction_Master.Contract_ID) SET tbl_RWAC_Master.BOT_Credit_Risk_Classification = ISNULL(tbl_DvP_Non_DvP.BOT_Credit_Type,ISNULL(tbl_Other_Asset.BOT_Credit_Type,tbl_Customer_Master.BOT_Credit_Risk_Item)), tbl_RWAC_Master.Credit_Type = ISNULL(tbl_Other_Asset.Credit_Type,tbl_Customer_Master.Credit_Risk_Type), tbl_RWAC_Master.Credit_Risk_Subtype = ISNULL(tbl_Other_Asset.Credit_Subtype,tbl_Customer_Master.Credit_Risk_Subtype), tbl_RWAC_Master.RW_Funding = IIf(tbl_Customer_Master.Credit_Risk_Type='Banks' And tbl_RWAC_Master.Remaining_Date<90,0.2,0), tbl_RWAC_Master.RW = IIf(tbl_RWAC_Master.Credit_Risk_Subtype<>tbl_Customer_Master.Credit_Risk_Subtype OR tbl_Asset_Transaction_Master.NPL_Flag <> tbl_RWAC_Master.NPL_Flag,Null,tbl_RWAC_Master.RW)")
        SQLConnect.ExcNonQuery("UPDATE (tbl_CRM_Master LEFT JOIN tbl_Customer_Master ON tbl_CRM_Master.Counter_Party_ID = tbl_Customer_Master.Cust_ID) LEFT JOIN tbl_Owner_Guarantor ON tbl_CRM_Master.Owner_Guarantor_Name = tbl_Owner_Guarantor.Owner_Guarantor_Name SET tbl_CRM_Master.Credit_Risk_Type = ISNULL([tbl_Owner_Guarantor].[Credit_Type],[tbl_Customer_Master].[Credit_Risk_Type]), tbl_CRM_Master.Credit_Risk_Subtype = ISNULL([tbl_Owner_Guarantor].[Credit_Subtype],[tbl_Customer_Master].[Credit_Risk_Subtype]), tbl_CRM_Master.RW_Collateral = IIF(tbl_CRM_Master.Credit_Risk_Subtype = ISNULL([tbl_Owner_Guarantor].[Credit_Subtype],[tbl_Customer_Master].[Credit_Risk_Subtype]),tbl_CRM_Master.RW_Collateral,null)")
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master LEFT JOIN tbl_Asset_Collateral_Master ON (tbl_CRM_Master.Collateral_ID = tbl_Asset_Collateral_Master.Collateral_ID) AND (tbl_CRM_Master.Counter_Party_ID = tbl_Asset_Collateral_Master.Counter_Party_ID) SET tbl_CRM_Master.Credit_Risk_Type = IIf(tbl_Asset_Collateral_Master.NPL_Flag=1,'NPL',tbl_CRM_Master.Credit_Risk_Type), tbl_CRM_Master.Credit_Risk_SubType = IIf(tbl_Asset_Collateral_Master.NPL_Flag=1,'10',tbl_CRM_Master.Credit_Risk_SubType), tbl_CRM_Master.Collateral_ID = tbl_CRM_Master.Collateral_ID, tbl_CRM_Master.Counter_Party_ID = tbl_CRM_Master.Counter_Party_ID")
        SQLConnect.ExcNonQuery("UPDATE (tbl_RWAC_Master INNER JOIN tbl_Asset_Transaction_Master ON tbl_RWAC_Master.Contract_ID = tbl_Asset_Transaction_Master.Contract_ID) LEFT JOIN tbl_Typelist ON tbl_Asset_Transaction_Master.Asset_Classification = tbl_Typelist.NPL_Type_Code SET tbl_RWAC_Master.BOT_Category = [tbl_Typelist].[NPL_Type], tbl_RWAC_Master.NPL_Flag = [tbl_Asset_Transaction_Master].NPL_Flag, tbl_RWAC_Master.NPL_Start_Date = [tbl_Asset_Transaction_Master].NPL_Start_Date, tbl_RWAC_Master.Asset_Classification_Type = [tbl_Typelist].[NPL_Type_Code], tbl_RWAC_Master.Provision_Factor = [tbl_Asset_Transaction_Master].General_Provision_Factor, tbl_RWAC_Master.Asset_Type = [tbl_Typelist].[NPL_Type], tbl_RWAC_Master.Credit_Type = IIf(tbl_Asset_Transaction_Master.NPL_Flag=1,'NPL',tbl_RWAC_Master.Credit_Type), tbl_RWAC_Master.Credit_Risk_SubType = IIf(tbl_Asset_Transaction_Master.NPL_Flag=1,'10',tbl_RWAC_Master.Credit_Risk_SubType)")



        grdTran.EditIndex = -1
        BindData()


    End Sub


    Protected Sub BindData2(ByVal Cust_ID As String)
        Dim ds As DataSet
        If ddlType.SelectedValue = "Transaction" Then
            ds = clsAssetManager.getMasterInquiryTransaction("30/09/2015", Cust_ID)
            grdTran2.DataSource = ds
            grdTran2.DataBind()
        ElseIf ddlType.SelectedValue = "Collateral" Then
            ds = clsAssetManager.getMasterInquiryCollateral("30/09/2015", Cust_ID)
            grdTran2.DataSource = ds
            grdTran2.DataBind()
        End If

        

    End Sub

    Protected Sub grdCRSMain_PageIndexChanging2(sender As Object, e As GridViewPageEventArgs) Handles grdTran2.PageIndexChanging
        grdTran2.PageIndex = e.NewPageIndex
        BindData2(txtSearch.Text)
    End Sub

    Protected Sub grd_RowEditing2(sender As Object, e As GridViewEditEventArgs)
        grdTran2.EditIndex = e.NewEditIndex
        BindData2(txtSearch.Text)
        'grdTran2.Rows(e.NewEditIndex).Cells(5).Controls(0).Focus()

    End Sub

    Protected Sub grd_RowCancelEditing2(sender As Object, e As GridViewCancelEditEventArgs)

        grdTran2.EditIndex = -1
        BindData2(txtSearch.Text)


    End Sub

    Protected Sub grd_RowCreated2(sender As Object, e As GridViewRowEventArgs)
        'Dim row As GridViewRow = e.Row
        'Dim columns As New List(Of TableCell)()

        'For Each column As DataControlField In grdTran2.Columns
        '    'Get the first Cell /Column
        '    Dim cell As TableCell = row.Cells(0)
        '    ' Then Remove it after
        '    row.Cells.Remove(cell)
        '    'And Add it to the List Collections
        '    columns.Add(cell)
        'Next

        'row.Cells.AddRange(columns.ToArray())

        'If ddlType.SelectedValue = "Transaction" Then
        '    If (e.Row.RowType = DataControlRowType.DataRow) Then
        '        e.Row.Cells(0).Enabled = False
        '        e.Row.Cells(1).Enabled = False
        '        e.Row.Cells(2).Enabled = False
        '    End If
        'ElseIf ddlType.SelectedValue = "Collateral" Then
        '    If (e.Row.RowType = DataControlRowType.DataRow) Then
        '        e.Row.Cells(0).Enabled = False
        '        e.Row.Cells(1).Enabled = False
        '        e.Row.Cells(2).Enabled = False
        '        e.Row.Cells(3).Enabled = False
        '    End If
        'End If
        


    End Sub


    Protected Sub grd_RowUpdating2(sender As Object, e As GridViewUpdateEventArgs)
        If ddlType.SelectedValue = "Transaction" Then

            Dim Cust_ID As String = grdTran2.Rows(e.RowIndex).Cells(0).Text


            'Get hold of combobox
            Dim row As GridViewRow = DirectCast(sender, 
            GridView).Rows(e.RowIndex)
            Dim cbo As DropDownList =
            DirectCast(row.FindControl("ddlAsset_Classification"), DropDownList)
            Dim NPL_Start_Date As String
            NPL_Start_Date = e.NewValues("NPL_Start_Date")

            Dim NPL_Flag As Boolean


            'Set the New value of the object            
            e.NewValues("Asset_Classification") = cbo.SelectedValue




            If e.NewValues("Asset_Classification") = "020001" Or e.NewValues("Asset_Classification") = "020002" Or e.NewValues("Asset_Classification") = "" Then
                NPL_Flag = False

                SQLConnect.ExcNonQuery("Update tbl_Asset_Transaction_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = Null" & _
                                   " where Cust_ID = '" & Cust_ID & "'")

            Else
                NPL_Flag = True
                If NPL_Start_Date = "" Then
                    Response.Write("<script type=""text/javascript"">alert(""NPL_Start_Date Can't Be Null"");</script>")
                    Exit Sub
                End If

                SQLConnect.ExcNonQuery("Update tbl_Asset_Transaction_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = '" & e.NewValues("NPL_Start_Date") & "'" & _
                               " where Cust_ID = '" & Cust_ID & "'")


            End If
        ElseIf ddlType.SelectedValue = "Collateral" Then
            Dim Cust_ID As String = grdTran2.Rows(e.RowIndex).Cells(0).Text
            Dim Collateral_ID As String = grdTran2.Rows(e.RowIndex).Cells(1).Text
            Dim Counter_Party_Name As String = grdTran2.Rows(e.RowIndex).Cells(2).Text
            Dim Owner_Guarantor_Name As String = grdTran2.Rows(e.RowIndex).Cells(3).Text

            'Get hold of combobox
            Dim row As GridViewRow = DirectCast(sender, 
            GridView).Rows(e.RowIndex)
            Dim cbo As DropDownList =
            DirectCast(row.FindControl("ddlAsset_Classification"), DropDownList)
            Dim NPL_Start_Date As String
            NPL_Start_Date = e.NewValues("NPL_Start_Date")

            Dim NPL_Flag As Boolean


            'Set the New value of the object
            e.NewValues("Asset_Classification") = cbo.SelectedValue

            If e.NewValues("Asset_Classification") = "020001" Or e.NewValues("Asset_Classification") = "020002" Or e.NewValues("Asset_Classification") = "" Then
                NPL_Flag = False

                SQLConnect.ExcNonQuery("Update tbl_Asset_Collateral_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = Null" & _
                               " where Counter_Party_ID = '" & Cust_ID & "' And Collateral_ID = '" & Collateral_ID & "' And Counter_Party_Name = '" & Counter_Party_Name & "' And Counter_Party_Name = '" & Counter_Party_Name & "' And Owner_Guarantor_Name = '" & Owner_Guarantor_Name & "'")

            Else
                NPL_Flag = True
                If NPL_Start_Date = "" Then
                    Response.Write("<script type=""text/javascript"">alert(""NPL_Start_Date Can't Be Null"");</script>")
                    Exit Sub
                End If

                SQLConnect.ExcNonQuery("Update tbl_Asset_Collateral_Master set Asset_Classification = '" & e.NewValues("Asset_Classification") & "', NPL_Flag = '" & NPL_Flag & "', General_Provision_Factor = '" & e.NewValues("General_Provision_Factor") & "', NPL_Start_Date = = '" & e.NewValues("NPL_Start_Date") & "'" & _
                               " where Counter_Party_ID = '" & Cust_ID & "' And Collateral_ID = '" & Collateral_ID & "' And Counter_Party_Name = '" & Counter_Party_Name & "' And Counter_Party_Name = '" & Counter_Party_Name & "' And Owner_Guarantor_Name = '" & Owner_Guarantor_Name & "'")

            End If

        End If


        'MsgBox(e.NewValues("Credit_Exposure") + "_" + grdTran.Rows(e.RowIndex).Cells(20).Text)

        SQLConnect.ExcNonQuery("UPDATE (((tbl_RWAC_Master LEFT JOIN tbl_Customer_Master ON tbl_RWAC_Master.Cust_ID = tbl_Customer_Master.Cust_ID) LEFT JOIN tbl_Other_Asset ON tbl_RWAC_Master.Contract_ID = tbl_Other_Asset.Transaction_ID) LEFT JOIN tbl_DvP_Non_DvP ON (tbl_RWAC_Master.Cust_ID = tbl_DvP_Non_DvP.Cust_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_DvP_Non_DvP.Transaction_ID)) LEFT JOIN tbl_Asset_Transaction_Master ON (tbl_RWAC_Master.Cust_ID = tbl_Asset_Transaction_Master.Cust_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Asset_Transaction_Master.Contract_ID) SET tbl_RWAC_Master.BOT_Credit_Risk_Classification = ISNULL(tbl_DvP_Non_DvP.BOT_Credit_Type,ISNULL(tbl_Other_Asset.BOT_Credit_Type,tbl_Customer_Master.BOT_Credit_Risk_Item)), tbl_RWAC_Master.Credit_Type = ISNULL(tbl_Other_Asset.Credit_Type,tbl_Customer_Master.Credit_Risk_Type), tbl_RWAC_Master.Credit_Risk_Subtype = ISNULL(tbl_Other_Asset.Credit_Subtype,tbl_Customer_Master.Credit_Risk_Subtype), tbl_RWAC_Master.RW_Funding = IIf(tbl_Customer_Master.Credit_Risk_Type='Banks' And tbl_RWAC_Master.Remaining_Date<90,0.2,0), tbl_RWAC_Master.RW = IIf(tbl_RWAC_Master.Credit_Risk_Subtype<>tbl_Customer_Master.Credit_Risk_Subtype OR tbl_Asset_Transaction_Master.NPL_Flag <> tbl_RWAC_Master.NPL_Flag,Null,tbl_RWAC_Master.RW)")
        SQLConnect.ExcNonQuery("UPDATE (tbl_CRM_Master LEFT JOIN tbl_Customer_Master ON tbl_CRM_Master.Counter_Party_ID = tbl_Customer_Master.Cust_ID) LEFT JOIN tbl_Owner_Guarantor ON tbl_CRM_Master.Owner_Guarantor_Name = tbl_Owner_Guarantor.Owner_Guarantor_Name SET tbl_CRM_Master.Credit_Risk_Type = ISNULL([tbl_Owner_Guarantor].[Credit_Type],[tbl_Customer_Master].[Credit_Risk_Type]), tbl_CRM_Master.Credit_Risk_Subtype = ISNULL([tbl_Owner_Guarantor].[Credit_Subtype],[tbl_Customer_Master].[Credit_Risk_Subtype]), tbl_CRM_Master.RW_Collateral = IIF(tbl_CRM_Master.Credit_Risk_Subtype = ISNULL([tbl_Owner_Guarantor].[Credit_Subtype],[tbl_Customer_Master].[Credit_Risk_Subtype]),tbl_CRM_Master.RW_Collateral,null)")
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master LEFT JOIN tbl_Asset_Collateral_Master ON (tbl_CRM_Master.Collateral_ID = tbl_Asset_Collateral_Master.Collateral_ID) AND (tbl_CRM_Master.Counter_Party_ID = tbl_Asset_Collateral_Master.Counter_Party_ID) SET tbl_CRM_Master.Credit_Risk_Type = IIf(tbl_Asset_Collateral_Master.NPL_Flag=1,'NPL',tbl_CRM_Master.Credit_Risk_Type), tbl_CRM_Master.Credit_Risk_SubType = IIf(tbl_Asset_Collateral_Master.NPL_Flag=1,'10',tbl_CRM_Master.Credit_Risk_SubType), tbl_CRM_Master.Collateral_ID = tbl_CRM_Master.Collateral_ID, tbl_CRM_Master.Counter_Party_ID = tbl_CRM_Master.Counter_Party_ID")
        SQLConnect.ExcNonQuery("UPDATE (tbl_RWAC_Master INNER JOIN tbl_Asset_Transaction_Master ON tbl_RWAC_Master.Contract_ID = tbl_Asset_Transaction_Master.Contract_ID) LEFT JOIN tbl_Typelist ON tbl_Asset_Transaction_Master.Asset_Classification = tbl_Typelist.NPL_Type_Code SET tbl_RWAC_Master.BOT_Category = [tbl_Typelist].[NPL_Type], tbl_RWAC_Master.NPL_Flag = [tbl_Asset_Transaction_Master].NPL_Flag, tbl_RWAC_Master.NPL_Start_Date = [tbl_Asset_Transaction_Master].NPL_Start_Date, tbl_RWAC_Master.Asset_Classification_Type = [tbl_Typelist].[NPL_Type_Code], tbl_RWAC_Master.Provision_Factor = [tbl_Asset_Transaction_Master].General_Provision_Factor, tbl_RWAC_Master.Asset_Type = [tbl_Typelist].[NPL_Type], tbl_RWAC_Master.Credit_Type = IIf(tbl_Asset_Transaction_Master.NPL_Flag=1,'NPL',tbl_RWAC_Master.Credit_Type), tbl_RWAC_Master.Credit_Risk_SubType = IIf(tbl_Asset_Transaction_Master.NPL_Flag=1,'10',tbl_RWAC_Master.Credit_Risk_SubType)")



        grdTran2.EditIndex = -1
        BindData2(txtSearch.Text)


    End Sub


    Protected Sub ddlType_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If ddlType.SelectedValue = "Collateral" Then
            Response.Redirect("~/Page/AssetManagerCollateral.aspx")

        End If

    End Sub

    
   
    Protected Sub grd_RowDataBound2(sender As Object, e As GridViewRowEventArgs) Handles grdTran2.RowDataBound
        Dim rowCount As Long
        rowCount = grdTran2.Rows.Count

        'If (rowCount <> 0) Then

        '    e.Row.Cells(0).Enabled = False
        '    e.Row.Cells(1).Enabled = False
        '    e.Row.Controls("Cust_ID").e()
        'End If

        'If (e.Row.RowType = DataControlRowType.DataRow) Then

        '    Dim txtBox As TextBox
        '    txtBox = DirectCast(e.Row.FindControl("Cust_ID"), TextBox)
        '    txtBox.ReadOnly = True




        'End If
        'e.Row.Cells(1).Attributes.Add("readonly", "true")
        'e.Row.Cells(2).Attributes.Add("readonly", "true")
    End Sub

End Class