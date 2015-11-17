Public Class CreditRiskClassification
    Inherits System.Web.UI.Page

    Private CRMType As String, TRNType As String
    Dim totalAllocatedAmount As Decimal = 0
    Dim isUpdate As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sql As String

            'DoCmd.OpenQuery "Q_Insert_Asset_Transaction_Master"
            sql = " INSERT INTO tbl_Asset_Transaction_Master ( Cust_ID, Cust_Name, Contract_ID )"
            sql += " SELECT tbl_RWAC_Master.Cust_ID, tbl_RWAC_Master.Cust_Name, tbl_RWAC_Master.Contract_ID"
            sql += " FROM tbl_RWAC_Master LEFT JOIN tbl_Asset_Transaction_Master ON tbl_RWAC_Master.Contract_ID=[tbl_Asset_Transaction_Master].Contract_ID"
            sql += " WHERE ((([tbl_Asset_Transaction_Master].Contract_ID) Is Null)) And tbl_RWAC_Master.Cust_Name<>'';"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Insert_Customer_Master"
            sql = "INSERT INTO tbl_Customer_Master ( Cust_ID, Cust_Name )"
            sql += " SELECT DISTINCT tbl_RWAC_Master.Cust_ID, tbl_RWAC_Master.Cust_Name"
            sql += " FROM tbl_RWAC_Master LEFT JOIN tbl_Customer_Master ON tbl_RWAC_Master.Cust_ID = tbl_Customer_Master.Cust_ID"
            sql += " WHERE (((tbl_Customer_Master.Cust_ID) Is Null)) AND tbl_RWAC_Master.Cust_Name <> '';"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Insert_Other_Asset"
            sql = "INSERT INTO tbl_Other_Asset ( Transaction_ID, COA, Credit_Type, Credit_Subtype )"
            sql += " SELECT DISTINCT tbl_RWAC_Master.Contract_ID, tbl_RWAC_Master.COA, 'Other assets', '9'"
            sql += " FROM tbl_RWAC_Master LEFT JOIN tbl_Other_Asset ON tbl_RWAC_Master.Contract_ID = tbl_Other_Asset.Transaction_ID"
            sql += " WHERE tbl_RWAC_Master.Credit_Risk_Subtype = '9' AND tbl_Other_Asset.Transaction_ID IS NULL;"
            SQLConnect.ExcNonQuery(sql)

            'DoCmd.OpenQuery "Q_Insert_Owner_Guarantor"
            sql = "INSERT INTO tbl_Owner_Guarantor ( Owner_Guarantor_Name )"
            sql += " SELECT DISTINCT tbl_CRM_Master.Owner_Guarantor_Name"
            sql += " FROM tbl_Owner_Guarantor RIGHT JOIN tbl_CRM_Master ON tbl_Owner_Guarantor.Owner_Guarantor_Name = tbl_CRM_Master.Owner_Guarantor_Name"
            sql += " WHERE (((tbl_CRM_Master.Owner_Guarantor_Name)<>""));"
            SQLConnect.ExcNonQuery(sql)

            TRNType = "Customer"
            BindData(TRNType)
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
            isUpdate = False
        End If
    End Sub

    Protected Sub BindData(ByVal strType As String)
        Dim ds As DataSet
        ds = clsCreditRiskClassification.getTransactionList(strType)
        grdCustList.DataSource = ds
        grdCustList.DataBind()

        Select Case strType
            Case "Customer"
                grdCustList.Columns(0).HeaderText = "Customer Name"
                generateTreeview(strType)
            Case "Guarantor"
                grdCustList.Columns(0).HeaderText = "Owner / Guarantor"
                generateTreeview(strType)
            Case Else
                grdCustList.Columns(0).HeaderText = "Transaction ID"
                generateTreeview(strType)
        End Select

        grdCustList.SelectedIndex = -1
    End Sub

    Protected Sub grdRWAMaster_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCustList.PageIndexChanging
        grdCustList.PageIndex = e.NewPageIndex
        BindData(TRNType)
    End Sub

    Protected Sub grdTRNList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCustList.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    'e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#93A3B0'; this.style.color='White'; this.style.cursor='pointer'")
                    'If e.Row.RowState = DataControlRowState.Alternate Then

                    '    e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='{0}';", grdCRMList.AlternatingRowStyle.BackColor.ToKnownColor()))
                    'Else
                    '    e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='{0}';", grdCRMList.RowStyle.BackColor.ToKnownColor()))
                    'End If
                    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdCustList, "Select$" + e.Row.RowIndex.ToString()))
                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdTRNList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCustList.SelectedIndexChanged
        Try
            'ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + grdTRNList.SelectedRow.Cells(4).Text + "')</script>")
            'BindCRMData(grdTRNList.SelectedRow.Cells(0).Text, grdTRNList.SelectedRow.Cells(3).Text, grdTRNList.SelectedRow.Cells(4).Text, CRMType)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAT_Click(sender As Object, e As EventArgs) Handles btnAT.Click
        TRNType = "Customer"
        BindData(TRNType)
    End Sub

    Protected Sub btnLG_Click(sender As Object, e As EventArgs) Handles btnLG.Click
        TRNType = "Guarantor"
        BindData(TRNType)
    End Sub

    Protected Sub btnOA_Click(sender As Object, e As EventArgs) Handles btnOA.Click
        TRNType = "OtherAssets"
        BindData(TRNType)
    End Sub

    Protected Sub btnDP_Click(sender As Object, e As EventArgs) Handles btnDP.Click
        TRNType = "DvP"
        BindData(TRNType)
    End Sub

    Protected Sub generateTreeview(ByVal type As String)
        tvwBOT.Nodes.Clear()
        Dim ds As DataSet = SQLConnect.toSqlDB("SELECT DISTINCT [Code], [Name] FROM tbl_Credit_Risk_Item WHERE [Parent_Code] = '-'")
        For Each dr In ds.Tables(0).Rows
            Dim newNode As TreeNode = New TreeNode(dr("Code") & " : " & dr("Name"), dr("Code"))

            newNode.SelectAction = TreeNodeSelectAction.Expand
            'newNode.PopulateOnDemand = True
            tvwBOT.Nodes.Add(newNode)

            generateChildrenNode(dr("Code"), newNode, type)
        Next

        tvwBOT.ExpandAll()
    End Sub

    Protected Sub generateChildrenNode(ByVal code As String, ByVal node As TreeNode, ByVal type As String)
        Dim sql As String

        Select Case type
            Case "Customer"
                sql = "SELECT DISTINCT [Code], [Name] FROM tbl_Credit_Risk_Item WHERE [Parent_Code] = '" & code & "' AND [Use_Flag] = 1 AND ([Credit_Risk_Subtype] <> '9' OR Credit_Risk_Subtype IS NULL) ORDER BY [Name]"
            Case "Guarantor"
                sql = "SELECT DISTINCT [Code], [Name] FROM tbl_Credit_Risk_Item WHERE [Parent_Code] = '" & code & "' AND [Use_Flag] = 1 AND ([Credit_Risk_Subtype] <> '9' OR Credit_Risk_Subtype IS NULL) ORDER BY [Name]"
            Case "OtherAssets"
                sql = "SELECT DISTINCT [Code], [Name] FROM tbl_Credit_Risk_Item WHERE [Parent_Code] = '" & code & "' AND [Use_Flag] = 1 AND ([Credit_Risk_Subtype] = '9' OR Credit_Risk_Subtype IS NULL) ORDER BY [Name]"
            Case "DvP"
                sql = "SELECT DISTINCT [Code], [Name] FROM tbl_Credit_Risk_Item WHERE [Parent_Code] = '" & code & "' AND [Use_Flag] = 1 AND ([Credit_Risk_Subtype] = '11' OR Credit_Risk_Subtype IS NULL) ORDER BY [Name]"
        End Select

        Dim ds As DataSet = SQLConnect.toSqlDB(sql)
        For Each dr In ds.Tables(0).Rows
            Dim newNode As TreeNode = New TreeNode(dr("Code") & " : " & dr("Name"), dr("Code"))

            newNode.SelectAction = TreeNodeSelectAction.Expand
            'newNode.PopulateOnDemand = True
            node.ChildNodes.Add(newNode)

            generateChildrenNode(dr("Code"), newNode, type)
        Next
    End Sub

    Protected Sub tvwBOT_SelectedNodeChanged(sender As Object, e As EventArgs) Handles tvwBOT.SelectedNodeChanged
        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + tvwBOT.SelectedValue + "')</script>")
    End Sub
End Class