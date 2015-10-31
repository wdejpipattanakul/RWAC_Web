Imports System.Data.SqlClient

Public Class CalculateRW
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsCalculateRW.getTransaction("30/09/2015")
        grdTran.DataSource = ds

        'DELETE FROM tbl_Summary_CRM_Calculation_Method
        SQLConnect.ExcNonQuery("DELETE FROM tbl_Summary_CRM_Calculation_Method")

        'Q_Update_RWA_Calculation_Method
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_RWAC_Master.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_RWAC_Master.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_RWAC_Master.Contract_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID) SET tbl_RWAC_Master.Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.[Calculation_Method],IIF(tbl_RWAC_Master.Trading_Book_Flag = True, 'Comprehensive', 'Simple'))")

        'Q_Update_CRM_Calculation_Method
        SQLConnect.ExcNonQuery("UPDATE (tbl_CRM_Allocated LEFT JOIN tbl_Summary_CRM_Calculation_Method ON (tbl_CRM_Allocated.Cust_ID = tbl_Summary_CRM_Calculation_Method.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_Summary_CRM_Calculation_Method.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_Summary_CRM_Calculation_Method.Transaction_ID)) LEFT JOIN tbl_RWAC_Master ON (tbl_CRM_Allocated.Cust_ID = tbl_RWAC_Master.Cust_ID) AND (tbl_CRM_Allocated.Facility_ID = tbl_RWAC_Master.Facility_ID) AND (tbl_CRM_Allocated.Transaction_ID = tbl_RWAC_Master.Contract_ID) SET tbl_CRM_Allocated.TRN_Calculation_Method = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method,IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple'))), tbl_CRM_Allocated.TRN_Calculation_Method_Ori = NZ(tbl_Summary_CRM_Calculation_Method.Calculation_Method, IIf(tbl_RWAC_Master.Trading_Book_Flag=True,'Comprehensive',IIf(tbl_CRM_Allocated.[TYPE]='On Balance','Comprehensive','Simple')))")


        'Q_Update_CRM_RW_Hc
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Allocated LEFT JOIN tbl_CRM_Master ON tbl_CRM_Allocated.ID = tbl_CRM_Master.ID SET tbl_CRM_Allocated.CRM_Hc_Value = [tbl_CRM_Master].[CRM_Hc_Value], tbl_CRM_Allocated.CRM_RW = [tbl_CRM_Master].[RW_Collateral]")



        grdTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTran.PageIndexChanging
        grdTran.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub Archive_Click(sender As Object, e As EventArgs) Handles Archive.Click
        Dim strSQL As String
        Dim Date_Filter As String


        Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
        oconn1.Open()

        strSQL = "SELECT aod from tbl_RWAC_Master where aod & '' <> ''"

        Dim objCommand As New SqlCommand(strSQL, oconn1)
        Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()



        If objDataReader.HasRows Then
            Date_Filter = objDataReader.Item("AOD")

        Else
            MsgBox("Can't Calculae RWA.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If



        SQLConnect.ExcNonQuery("Delete from tbl_RWAC_History where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_CRM_History where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_CRM_Allocated_History where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_DvP_History where aod = #" & Date_Filter & "#")

        SQLConnect.ExcNonQuery("Delete from tbl_CRS_Main_Report where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_CRS_Tran_Report where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_RWA_Report where aod = #" & Date_Filter & "#")
        SQLConnect.ExcNonQuery("Delete from tbl_RWA_Report_Max_CRM where aod = #" & Date_Filter & "#")

        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                         " SET aod = #" & Date_Filter & "#")

        SQLConnect.ExcNonQuery("INSERT INTO tbl_RWAC_History SELECT * FROM tbl_RWAC_Master")
        SQLConnect.ExcNonQuery("INSERT INTO tbl_CRM_History SELECT * FROM tbl_CRM_Master")
        SQLConnect.ExcNonQuery("INSERT INTO tbl_CRM_Allocated_History SELECT * FROM tbl_CRM_Allocated")
        SQLConnect.ExcNonQuery("INSERT INTO tbl_DvP_History SELECT * FROM tbl_DvP_Master")

        SQLConnect.ExcNonQuery("INSERT INTO tbl_CRS_Main_Report SELECT * FROM q_CRS_Main_Report")
        SQLConnect.ExcNonQuery("INSERT INTO tbl_CRS_Tran_Report SELECT * FROM q_CRS_Tran_Report")

        'Add Report in the Future

        MsgBox("Archive Data Successful")
    End Sub

    Protected Sub CalculateRWA_Click(sender As Object, e As EventArgs) Handles CalculateRWA.Click
        'Q_Update_RWA_OnBal_Simple
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.RWA = (([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[CRM_Allocated_Amount]-[Specific_Provision]+IIf(Memo='PCCMPRWK', Nz(MTM_Value,0), IIf(Credit_Type='Other assets',-1*Nz(Adjustment_Item,0), 0)))*[RW])+[RWA_Collateral]+[Funding_Allocated_Amount], tbl_RWAC_Master.RWA_No_Collateral = (([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[CRM_Allocated_Amount]-[Specific_Provision]+IIf(Memo='PCCMPRWK', Nz(MTM_Value,0), IIf(Credit_Type='Other assets',-1*Nz(Adjustment_Item,0), 0)))*[RW])+[Funding_Allocated_Amount]" & _
                                " WHERE (((tbl_RWAC_Master.Calculation_Method)='Simple') And ((tbl_RWAC_Master.[Credit_Exposure])='On-Bal'))")


        'Q_Update_RWA_OffBal_Simple
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.RWA = (([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[CRM_Allocated_Amount]-[Specific_Provision])*[CCF]*[RW])+([Funding_Allocated_Amount]*[CCF])+([RWA_Collateral]*[CCF]), tbl_RWAC_Master.RWA_No_Collateral = (([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[CRM_Allocated_Amount]-[Specific_Provision])*[CCF]*[RW])+([Funding_Allocated_Amount]*[CCF])" & _
                                " WHERE (((tbl_RWAC_Master.Calculation_Method)='Simple') And ((tbl_RWAC_Master.[Credit_Exposure])='Off-Bal Non-Derivative'))")


        'Q_Update_RWA_OnBal_Comprehensive
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.RWA = ((([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision])*(1+[Trans_He_Value]))-[RWA_Collateral])*[RW]+[Funding_Allocated_Amount], tbl_RWAC_Master.RWA_No_Collateral = ((([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision])*(1+[Trans_He_Value]))-[RWA_Collateral])*[RW]+[Funding_Allocated_Amount]" & _
                                " WHERE (((tbl_RWAC_Master.Calculation_Method)='Comprehensive') And ((tbl_RWAC_Master.[Credit_Exposure])='On-Bal'))")


        'Q_Update_RWA_OffBal_Comprehensive
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.RWA = (((([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision])*(1+[Trans_He_Value]))-[RWA_Collateral])*[RW])*[CCF]+([Funding_Allocated_Amount]*[CCF]), tbl_RWAC_Master.RWA_No_Collateral = (((([Amount_THB]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision])*(1+[Trans_He_Value]))-[RWA_Collateral])*[RW])*[CCF]+([Funding_Allocated_Amount]*[CCF])" & _
                                " WHERE (((tbl_RWAC_Master.Calculation_Method)='Comprehensive') And ((tbl_RWAC_Master.[Credit_Exposure])='Off-Bal Non-Derivative'))")

        'Q_Update_RWA_Deriv_Comprehensive
        SQLConnect.ExcNonQuery("UPDATE tbl_RWAC_Master SET tbl_RWAC_Master.RWA = ([CEA]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision]-[RWA_Collateral])*[RW]+[Funding_Allocated_Amount], tbl_RWAC_Master.RWA_No_Collateral = ([CEA]+IIF(Adjust_Accrued<>0,Adjust_Accrued,Accrued)-[Allocate_Asset_Amount]-[Specific_Provision]-[RWA_Collateral])*[RW]+[Funding_Allocated_Amount]" & _
                                " WHERE (((tbl_RWAC_Master.Calculation_Method)='Comprehensive') And ((tbl_RWAC_Master.[Credit_Exposure])='Derivative'))")


        MsgBox("Calculate RWA Successful")


    End Sub
End Class