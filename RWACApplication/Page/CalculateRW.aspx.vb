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

        strSQL = "SELECT aod from tbl_RWAC_Master where aod + '' <> ''"

        Dim objCommand As New SqlCommand(strSQL, oconn1)
        Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()



        If objDataReader.HasRows Then
            Do While objDataReader.Read()
                Date_Filter = objDataReader.Item("AOD")

            Loop

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
        'Report Added

        SQLConnect.ExcNonQuery("INSERT INTO tbl_RWA_Report ( AOD, [Reporting month], [Ex rate EOM], [Asset classification type], [Exposure type], [BOT credit risk type code], [BOT credit risk item code], Facility_ID, Contract_ID, COA, Cust_Name, Customer_Country, Contract_Subtype, [Derivative Product Type], Amount_THB, Adjust_Accrued, Asset_Classification_Type, Q_RWA_Report_Transaction_Section_Currency, DealDate, Maturity_Date, [Original Maturity Date], [General provision], Specific_Provision, [Write-off amount], [Notional Amount], FX_Buy_Currency, FX_Buy_Amount, FX_Sell_Currency, FX_Sell_Amount, CCF, Customer_Location, [Country Ratings], Q_RWA_Report_Transaction_Section_ECAI_Name, [Long term rating/short term rating], [Issuer rating], [Issue specific rating], Q_RWA_Report_Transaction_Section_ECAI_Value, MTM_Value, MTM_Profit, MTM_Loss, Agross, RCgross, RCnet, Anet, Trading_Book_Flag, NPL_Flag, Netting_Agreement_Flag, [RWA rate], GCEA, [Adjustment Item], NCEA, Q_RWA_Report_Transaction_Section_RW, RWA_No_Collateral, RWA_Collateral, RWA, [CRM Method], [Decrease In CRM], [Increase IN CRM], CRM_ID, CRM_Type, CRM_Amount, CRM_Start_Date, CRM_Maturity_Date, [Residual maturity], CRM_RW, Owner_Guarantor_Name, [Country of guarantor], Q_RWA_Report_Collateral_Section_ECAI_Name, Q_RWA_Report_Collateral_Section_ECAI_Value, Amount, Q_RWA_Report_Collateral_Section_RW, Q_RWA_Report_Collateral_Section_Currency, [Maturity Mismatch Value], [CRM with On-Balance sheet Netting agreement], [E*], CRM_Hc_Value, CRM_Hfx_Value, [Haircut of holding period], [Currency mismatch adjustment for collateral], [BOT reporting risk weighted asset amount], [BOT ISIC code], [DvP/Non-DvP Type], [DvP/Non-DvP Counterparty name], [DvP/Non-DvP Trade date], [DvP/Non-DvP Receive CCY], [DvP/Non-DvP Received amount], [DvP/Non-DvP Pay CCY], [DvP/Non-DvP Paid Amount], [DvP/Non-DvP Positive current exposure], [DvP/Non-DvP Start fail settlement date], [DvP/Non-DvP End Date], [DvP/Non-DvP Issuer name], [DvP/Non-DvP Issuer rating], [DvP/Non-DvP Underwriter name], [DvP/Non-DvP Underwriter rating], [DvP/Non-DvP Subscription fee], [DvP/Non-DvP Subscription currency], [DvP/Non-DvP Date announcement (1st SD)], [DvP/Non-DvP Actual received securities date (2nd SD)], [DvP/Non-DvP Securities allocated (Y/N)], [DvP/Non-DvP Date receive subscription back], [DvP/Non-DvP exposure], [DvP/Non-DvP RW], [DvP/Non-DvP RWA], ID )" & _
                                " SELECT Q_RWA_Report.AOD, Q_RWA_Report.[Reporting month], Q_RWA_Report.[Ex rate EOM], Q_RWA_Report.[Asset classification type], Q_RWA_Report.[Exposure type], Q_RWA_Report.[BOT credit risk type code], Q_RWA_Report.[BOT credit risk item code], Q_RWA_Report.Facility_ID, Q_RWA_Report.Contract_ID, Q_RWA_Report.COA, Q_RWA_Report.Cust_Name, Q_RWA_Report.Customer_Country, Q_RWA_Report.Contract_Subtype, Q_RWA_Report.[Derivative Product Type], Q_RWA_Report.Amount_THB, Q_RWA_Report.Adjust_Accrued, Q_RWA_Report.Asset_Classification_Type, Q_RWA_Report.Q_RWA_Report_Transaction_Section.Currency, Q_RWA_Report.DealDate, Q_RWA_Report.Maturity_Date, Q_RWA_Report.[Original Maturity Date], Q_RWA_Report.[General provision], Q_RWA_Report.Specific_Provision, Q_RWA_Report.[Write-off amount], Q_RWA_Report.[Notional Amount], Q_RWA_Report.FX_Buy_Currency, Q_RWA_Report.FX_Buy_Amount, Q_RWA_Report.FX_Sell_Currency, Q_RWA_Report.FX_Sell_Amount, Q_RWA_Report.CCF, Q_RWA_Report.Customer_Location, Q_RWA_Report.[Country Ratings], Q_RWA_Report.Q_RWA_Report_Transaction_Section.ECAI_Name, Q_RWA_Report.[Long term rating/short term rating], Q_RWA_Report.[Issuer rating], Q_RWA_Report.[Issue specific rating], Q_RWA_Report.Q_RWA_Report_Transaction_Section.ECAI_Value, Q_RWA_Report.MTM_Value, Q_RWA_Report.MTM_Profit, Q_RWA_Report.MTM_Loss, Q_RWA_Report.Agross, Q_RWA_Report.RCgross, Q_RWA_Report.RCnet, Q_RWA_Report.Anet, Q_RWA_Report.Trading_Book_Flag, Q_RWA_Report.NPL_Flag, Q_RWA_Report.Netting_Agreement_Flag, Q_RWA_Report.[RWA rate], Q_RWA_Report.GCEA, Q_RWA_Report.[Adjustment Item], Q_RWA_Report.NCEA, Q_RWA_Report.Q_RWA_Report_Transaction_Section.RW, Q_RWA_Report.RWA_No_Collateral, Q_RWA_Report.RWA_Collateral, Q_RWA_Report.RWA, Q_RWA_Report.[CRM Method], Q_RWA_Report.[Decrease In CRM], Q_RWA_Report.[Increase IN CRM], Q_RWA_Report.CRM_ID, Q_RWA_Report.CRM_Type, Q_RWA_Report.CRM_Amount, Q_RWA_Report.CRM_Start_Date, Q_RWA_Report.CRM_Maturity_Date, Q_RWA_Report.[Residual maturity], Q_RWA_Report.CRM_RW, Q_RWA_Report.Owner_Guarantor_Name, Q_RWA_Report.[Country of guarantor], Q_RWA_Report.Q_RWA_Report_Collateral_Section.ECAI_Name, Q_RWA_Report.Q_RWA_Report_Collateral_Section.ECAI_Value, Q_RWA_Report.Amount, Q_RWA_Report.Q_RWA_Report_Collateral_Section.RW, Q_RWA_Report.Q_RWA_Report_Collateral_Section.Currency, Q_RWA_Report.[Maturity Mismatch Value], Q_RWA_Report.[CRM with On-Balance sheet Netting agreement], Q_RWA_Report.[E*], Q_RWA_Report.CRM_Hc_Value, Q_RWA_Report.CRM_Hfx_Value, Q_RWA_Report.[Haircut of holding period], Q_RWA_Report.[Currency mismatch adjustment for collateral], Q_RWA_Report.[BOT reporting risk weighted asset amount], Q_RWA_Report.[BOT ISIC code], Q_RWA_Report.[DvP/Non-DvP Type], Q_RWA_Report.[DvP/Non-DvP Counterparty name], Q_RWA_Report.[DvP/Non-DvP Trade date], Q_RWA_Report.[DvP/Non-DvP Receive CCY], Q_RWA_Report.[DvP/Non-DvP Received amount], Q_RWA_Report.[DvP/Non-DvP Pay CCY], Q_RWA_Report.[DvP/Non-DvP Paid Amount], Q_RWA_Report.[DvP/Non-DvP Positive current exposure], Q_RWA_Report.[DvP/Non-DvP Start fail settlement date], Q_RWA_Report.[DvP/Non-DvP End Date], Q_RWA_Report.[DvP/Non-DvP Issuer name], Q_RWA_Report.[DvP/Non-DvP Issuer rating], Q_RWA_Report.[DvP/Non-DvP Underwriter name], Q_RWA_Report.[DvP/Non-DvP Underwriter rating], Q_RWA_Report.[DvP/Non-DvP Subscription fee], Q_RWA_Report.[DvP/Non-DvP Subscription currency], Q_RWA_Report.[DvP/Non-DvP Date announcement (1st SD)], Q_RWA_Report.[DvP/Non-DvP Actual received securities date (2nd SD)], Q_RWA_Report.[DvP/Non-DvP Securities allocated (Y/N)], Q_RWA_Report.[DvP/Non-DvP Date receive subscription back], Q_RWA_Report.[DvP/Non-DvP exposure], Q_RWA_Report.[DvP/Non-DvP RW], Q_RWA_Report.[DvP/Non-DvP RWA], Q_RWA_Report.ID" & _
                                " FROM Q_RWA_Report")

        SQLConnect.ExcNonQuery("INSERT INTO tbl_RWA_Report ( AOD, [Reporting month], [Ex rate EOM], [Asset classification type], [Exposure type], [BOT credit risk type code], [BOT credit risk item code], Facility_ID, Contract_ID, COA, Cust_Name, Customer_Country, Contract_Subtype, [Derivative Product Type], Amount_THB, Adjust_Accrued, Asset_Classification_Type, Q_RWA_Report_Transaction_Section_Currency, DealDate, Maturity_Date, [Original Maturity Date], [General provision], Specific_Provision, [Write-off amount], [Notional Amount], FX_Buy_Currency, FX_Buy_Amount, FX_Sell_Currency, FX_Sell_Amount, CCF, Customer_Location, [Country Ratings], Q_RWA_Report_Transaction_Section_ECAI_Name, [Long term rating/short term rating], [Issuer rating], [Issue specific rating], Q_RWA_Report_Transaction_Section_ECAI_Value, MTM_Value, MTM_Profit, MTM_Loss, Agross, RCgross, RCnet, Anet, Trading_Book_Flag, NPL_Flag, Netting_Agreement_Flag, [RWA rate], GCEA, [Adjustment Item], NCEA, Q_RWA_Report_Transaction_Section_RW, RWA_No_Collateral, RWA_Collateral, RWA, [CRM Method], [Decrease In CRM], [Increase IN CRM], CRM_ID, CRM_Type, CRM_Amount, CRM_Start_Date, CRM_Maturity_Date, [Residual maturity], CRM_RW, Owner_Guarantor_Name, [Country of guarantor], Q_RWA_Report_Collateral_Section_ECAI_Name, Q_RWA_Report_Collateral_Section_ECAI_Value, Amount, Q_RWA_Report_Collateral_Section_RW, Q_RWA_Report_Collateral_Section_Currency, [Maturity Mismatch Value], [CRM with On-Balance sheet Netting agreement], [E*], CRM_Hc_Value, CRM_Hfx_Value, [Haircut of holding period], [Currency mismatch adjustment for collateral], [BOT reporting risk weighted asset amount], [BOT ISIC code], [DvP/Non-DvP Type], [DvP/Non-DvP Counterparty name], [DvP/Non-DvP Trade date], [DvP/Non-DvP Receive CCY], [DvP/Non-DvP Received amount], [DvP/Non-DvP Pay CCY], [DvP/Non-DvP Paid Amount], [DvP/Non-DvP Positive current exposure], [DvP/Non-DvP Start fail settlement date], [DvP/Non-DvP End Date], [DvP/Non-DvP Issuer name], [DvP/Non-DvP Issuer rating], [DvP/Non-DvP Underwriter name], [DvP/Non-DvP Underwriter rating], [DvP/Non-DvP Subscription fee], [DvP/Non-DvP Subscription currency], [DvP/Non-DvP Date announcement (1st SD)], [DvP/Non-DvP Actual received securities date (2nd SD)], [DvP/Non-DvP Securities allocated (Y/N)], [DvP/Non-DvP Date receive subscription back], [DvP/Non-DvP exposure], [DvP/Non-DvP RW], [DvP/Non-DvP RWA] )" & _
                                " SELECT Q_CRM_Summary_for_Report_RWA.AOD, Month([Q_CRM_Summary_for_Report_RWA].[AOD]) AS Expr4, '' AS [Ex rate EOM], '' AS [Asset classification type], 'CRM' AS [Exposure type], Q_CRM_Summary_for_Report_RWA.CreditRiskItem, Q_CRM_Summary_for_Report_RWA.CreditRiskType, Q_CRM_Summary_for_Report_RWA.Facility_ID, Q_CRM_Summary_for_Report_RWA.Collateral_ID, '' AS COA, Q_CRM_Summary_for_Report_RWA.Owner_Guarantor_Name, Q_CRM_Summary_for_Report_RWA.[Country of guarantor], Q_CRM_Summary_for_Report_RWA.Collateral_Type, '' AS [Derivative Product Type], Q_CRM_Summary_for_Report_RWA.CRM_Amount, 0 AS Adjust_Accrued, '' AS Asset_Classification_Type, Q_CRM_Summary_for_Report_RWA.Currency, Q_CRM_Summary_for_Report_RWA.Start_Date, Q_CRM_Summary_for_Report_RWA.Maturity_Date, Q_CRM_Summary_for_Report_RWA.Maturity_Date, 0 AS [General provision], 0 AS Specific_Provision, 0 AS [Write-off amount], '' AS [Notional Amount], '' AS FX_Buy_Currency, 0 AS FX_Buy_Amount, '' AS FX_Sell_Currency, 0 AS FX_Sell_Amount, 0 AS CCF, Q_CRM_Summary_for_Report_RWA.[Country of guarantor], Q_CRM_Summary_for_Report_RWA.Rating_Group, Q_CRM_Summary_for_Report_RWA.ECAI_Name, IIf(Q_CRM_Summary_for_Report_RWA.Rating_Group='Sovereign' Or Q_CRM_Summary_for_Report_RWA.Rating_Group='Corporate',Q_CRM_Summary_for_Report_RWA.ECAI_Value,'') AS [Long term rating/short term rating], IIf(Q_CRM_Summary_for_Report_RWA.Rating_Group='Sovereign' Or Q_CRM_Summary_for_Report_RWA.Rating_Group='Corporate',Q_CRM_Summary_for_Report_RWA.ECAI_Value,'') AS [Issuer rating], IIf(Q_CRM_Summary_for_Report_RWA.Rating_Group='SP',Q_CRM_Summary_for_Report_RWA.ECAI_Value,'') AS [Issue specific rating], Q_CRM_Summary_for_Report_RWA.ECAI_Value, Null AS MTM_Value, Null AS MTM_Profit, Null AS MTM_Loss, Null AS Agross, Null AS RCgross, Null AS RCnet, Null AS Anet, 'N' AS Trading_Book_Flag, 'N' AS NPL_Flag, 'N' AS Netting_Agreement_Flag, Q_CRM_Summary_for_Report_RWA.CRM_RW, 0 AS GCEA, Null AS [Adjustment Item], 0 AS NCEA, Q_CRM_Summary_for_Report_RWA.CRM_RW, 0 AS RWA_No_Collateral, Q_CRM_Summary_for_Report_RWA.[CRM_Allocated_Amount]*Q_CRM_Summary_for_Report_RWA.[CRM_RW] AS Expr1, Q_CRM_Summary_for_Report_RWA.[CRM_Allocated_Amount]*Q_CRM_Summary_for_Report_RWA.[CRM_RW] AS RWA, 'Simple' AS [CRM Method], 0 AS [Decrease In CRM], Q_CRM_Summary_for_Report_RWA.CRM_Allocated_Amount, Q_RWA_Report_Collateral_Section.CRM_ID, Q_RWA_Report_Collateral_Section.CRM_Type, Q_RWA_Report_Collateral_Section.CRM_Amount, Q_RWA_Report_Collateral_Section.CRM_Start_Date, Q_RWA_Report_Collateral_Section.CRM_Maturity_Date, Q_RWA_Report_Collateral_Section.[Residual maturity], Q_RWA_Report_Collateral_Section.CRM_RW, Q_RWA_Report_Collateral_Section.Owner_Guarantor_Name, Q_RWA_Report_Collateral_Section.[Country of guarantor], Q_RWA_Report_Collateral_Section.ECAI_Name, Q_RWA_Report_Collateral_Section.ECAI_Value, Q_RWA_Report_Collateral_Section.Amount, Q_RWA_Report_Collateral_Section.RW, Q_RWA_Report_Collateral_Section.Currency, Q_RWA_Report_Collateral_Section.[Maturity Mismatch Value], Nz([Q_RWA_Report_Collateral_Section].[CRM with On-Balance sheet Netting agreement],0) AS Expr5, 0 AS [E*], Q_RWA_Report_Collateral_Section.CRM_Hc_Value, Q_RWA_Report_Collateral_Section.CRM_Hfx_Value, Q_RWA_Report_Collateral_Section.[Haircut of holding period], Q_RWA_Report_Collateral_Section.[Currency mismatch adjustment for collateral], Q_CRM_Summary_for_Report_RWA.CRM_Allocated_Amount*Q_CRM_Summary_for_Report_RWA.CRM_RW AS Expr3, '' AS Expr2, tbl_DvP_History.Type AS [DvP/Non-DvP Type], tbl_DvP_History.[Counterparty name] AS [DvP/Non-DvP Counterparty name], tbl_DvP_History.[Trade date] AS [DvP/Non-DvP Trade date], tbl_DvP_History.[Receive CCY] AS [DvP/Non-DvP Receive CCY], tbl_DvP_History.[Received amount] AS [DvP/Non-DvP Received amount], tbl_DvP_History.[Pay CCY] AS [DvP/Non-DvP Pay CCY], tbl_DvP_History.[Paid Amount] AS [DvP/Non-DvP Paid Amount], tbl_DvP_History.[Positive current exposure] AS [DvP/Non-DvP Positive current exposure], tbl_DvP_History.[Start fail settlement date] AS [DvP/Non-DvP Start fail settlement date], tbl_DvP_History.[End Date] AS [DvP/Non-DvP End Date], tbl_DvP_History.[Issuer name] AS [DvP/Non-DvP Issuer name], tbl_DvP_History.[Issuer rating] AS [DvP/Non-DvP Issuer rating], tbl_DvP_History.[Underwriter name] AS [DvP/Non-DvP Underwriter name], tbl_DvP_History.[Underwriter rating] AS [DvP/Non-DvP Underwriter rating], tbl_DvP_History.[Subscription fee] AS [DvP/Non-DvP Subscription fee], tbl_DvP_History.[Subscription currency] AS [DvP/Non-DvP Subscription currency], tbl_DvP_History.[Date announcement (1st SD)] AS [DvP/Non-DvP Date announcement (1st SD)], tbl_DvP_History.[Actual received securities date (2nd SD)] AS [DvP/Non-DvP Actual received securities date (2nd SD)], tbl_DvP_History.[Securities allocated (Y/N)] AS [DvP/Non-DvP Securities allocated (Y/N)], tbl_DvP_History.[Date receive subscription back] AS [DvP/Non-DvP Date receive subscription back], tbl_DvP_History.exposure AS [DvP/Non-DvP exposure], Null AS [DvP/Non-DvP RW], Null AS [DvP/Non-DvP RWA]" & _
                                " FROM (Q_CRM_Summary_for_Report_RWA LEFT JOIN Q_RWA_Report_Collateral_Section ON (Q_CRM_Summary_for_Report_RWA.Facility_ID = Q_RWA_Report_Collateral_Section.Facility_ID) AND (Q_CRM_Summary_for_Report_RWA.Collateral_ID = Q_RWA_Report_Collateral_Section.Transaction_ID) AND (Q_CRM_Summary_for_Report_RWA.AOD = Q_RWA_Report_Collateral_Section.AOD)) LEFT JOIN tbl_DvP_History ON (Q_CRM_Summary_for_Report_RWA.AOD = tbl_DvP_History.AOD) AND (Q_CRM_Summary_for_Report_RWA.Collateral_ID = tbl_DvP_History.[Contract/Reference ID])" & _
                                " WHERE (((Q_CRM_Summary_for_Report_RWA.CRM_Allocated_Amount)<>0))")

        SQLConnect.ExcNonQuery("INSERT INTO tbl_RWA_Report_Max_CRM ( AOD, Facility_ID, Contract_ID, CRM_ID, ID )" & _
                                " SELECT tbl_RWA_Report.AOD, tbl_RWA_Report.Facility_ID, tbl_RWA_Report.Contract_ID, tbl_RWA_Report.CRM_ID, tbl_RWA_Report.ID" & _
                                " FROM tbl_RWA_Report" & _
                                " WHERE [tbl_RWA_Report].[ID] + '' = (SELECT TOP 1 tbl_CRM_Allocated_History.ID" & _
                                " FROM tbl_CRM_Allocated_History" & _
                                " WHERE tbl_CRM_Allocated_History.CRM_Allocated_Amount<>0 AND tbl_CRM_Allocated_History.Transaction_ID = tbl_RWA_Report.Contract_ID" & _
                                " ORDER BY tbl_CRM_Allocated_History.Cust_ID, tbl_CRM_Allocated_History.Transaction_ID, tbl_CRM_Allocated_History.TYPE, tbl_CRM_Allocated_History.CRM_Type)")


        SQLConnect.ExcNonQuery("UPDATE tbl_RWA_Report INNER JOIN tbl_RWA_Report_Max_CRM ON (tbl_RWA_Report.AOD = tbl_RWA_Report_Max_CRM.AOD) AND (tbl_RWA_Report.Facility_ID = tbl_RWA_Report_Max_CRM.Facility_ID) AND (tbl_RWA_Report.Contract_ID = tbl_RWA_Report_Max_CRM.Contract_ID) SET tbl_RWA_Report.[Notional Amount] = '', tbl_RWA_Report.GCEA = 0, tbl_RWA_Report.[Adjustment Item] = 0, tbl_RWA_Report.NCEA = 0, tbl_RWA_Report.RWA_No_Collateral = 0, tbl_RWA_Report.RWA_Collateral = 0, tbl_RWA_Report.RWA = 0, tbl_RWA_Report.[Decrease In CRM] = 0, tbl_RWA_Report.Amount_THB = 0, tbl_RWA_Report.Adjust_Accrued = 0, tbl_RWA_Report.[General provision] = 0, tbl_RWA_Report.Specific_Provision = 0, tbl_RWA_Report.[Write-off amount] = 0, tbl_RWA_Report.FX_Buy_Amount = 0, tbl_RWA_Report.FX_Sell_Amount = 0, tbl_RWA_Report.[RWA rate] = 0, tbl_RWA_Report.Q_RWA_Report_Transaction_Section_RW = 0" & _
                                " WHERE (((tbl_RWA_Report.ID)<>[tbl_RWA_Report_Max_CRM].[ID]) AND (([tbl_RWA_Report].[CRM_ID] + '')<>''))")









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