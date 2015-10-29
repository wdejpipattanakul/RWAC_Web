Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsRWAReportInquiry
    Public Overloads Shared Function getRWAReportInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT [Asset classification type], [Exposure type], [BOT credit risk type code], [BOT credit risk item code], [Facility_ID], [Contract_ID], [Cust_Name], [Q_RWA_Report_Transaction_Section_Currency], [Amount_THB], [Adjust_Accrued], [Specific_Provision], [Notional Amount], [DealDate], [Maturity_Date], [CCF], [RWA rate], [GCEA], [Adjustment Item], [NCEA], [RWA_No_Collateral], [RWA_Collateral], [RWA], [CRM Method], [Decrease In CRM], [Increase IN CRM] "
        cmd += "  FROM tbl_RWA_Report "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function

    Public Overloads Shared Function getRWAReportExport(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim sSql1 As String
        sSql1 = " SELECT TOP 148 [Reporting month], [Ex rate EOM], [Asset classification type], [Exposure type], [BOT credit risk type code], [BOT credit risk item code], Facility_ID, Contract_ID, COA, Cust_Name, Customer_Country, Contract_Subtype, [Derivative Product Type], Amount_THB, Adjust_Accrued, Asset_Classification_Type, Q_RWA_Report_Transaction_Section_Currency, DealDate, Maturity_Date, [Original Maturity Date], [General provision], Specific_Provision, [Write-off amount], [Notional Amount], FX_Buy_Currency, FX_Buy_Amount, FX_Sell_Currency, FX_Sell_Amount, CCF, Customer_Location, [Country Ratings], Q_RWA_Report_Transaction_Section_ECAI_Name"
        sSql1 += "     , [Long term rating/short term rating] , [Issuer rating], [Issue specific rating], Q_RWA_Report_Transaction_Section_ECAI_Value, MTM_Value, MTM_Profit, MTM_Loss, Agross, RCgross, RCnet, Anet, Trading_Book_Flag, NPL_Flag, Netting_Agreement_Flag, [RWA rate], GCEA, [Adjustment Item], NCEA, Q_RWA_Report_Transaction_Section_RW, RWA_No_Collateral, RWA_Collateral, RWA, [CRM Method], [Decrease In CRM], [Increase IN CRM], CRM_ID, CRM_Type, CRM_Amount, CRM_Start_Date, CRM_Maturity_Date, [Residual maturity], CRM_RW, Owner_Guarantor_Name, [Country of guarantor]"
        sSql1 += "     , Q_RWA_Report_Collateral_Section_ECAI_Name , Q_RWA_Report_Collateral_Section_ECAI_Value, Amount, Q_RWA_Report_Collateral_Section_RW, Q_RWA_Report_Collateral_Section_Currency, [Maturity Mismatch Value], [CRM with On-Balance sheet Netting agreement], [E*], CRM_Hc_Value, CRM_Hfx_Value, [Haircut of holding period], [Currency mismatch adjustment for collateral], [BOT reporting risk weighted asset amount], [BOT ISIC code], [DvP/Non-DvP Type], [DvP/Non-DvP Counterparty name], [DvP/Non-DvP Trade date], [DvP/Non-DvP Receive CCY], [DvP/Non-DvP Received amount], [DvP/Non-DvP Pay CCY], [DvP/Non-DvP Paid Amount]"
        sSql1 += "     , [DvP/Non-DvP Positive current exposure], [DvP/Non-DvP Start fail settlement date], [DvP/Non-DvP End Date],[DvP/Non-DvP Issuer name] , [DvP/Non-DvP Issuer rating], [DvP/Non-DvP Underwriter name], [DvP/Non-DvP Underwriter rating], [DvP/Non-DvP Subscription fee], [DvP/Non-DvP Subscription currency], [DvP/Non-DvP Date announcement (1st SD)], [DvP/Non-DvP Actual received securities date (2nd SD)], [DvP/Non-DvP Securities allocated (Y/N)], [DvP/Non-DvP Date receive subscription back], [DvP/Non-DvP exposure], [DvP/Non-DvP RW], [DvP/Non-DvP RWA] "
        sSql1 += "  FROM tbl_RWA_Report"
        sSql1 += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(sSql1)
        Return ds
    End Function
End Class
