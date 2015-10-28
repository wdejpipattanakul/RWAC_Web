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
        cmd += "  FROM tbl_RWAC_Report "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
