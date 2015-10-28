Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCRSReportInquiry
    Public Overloads Shared Function getCRSMain(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT [CreditRiskMethod], [CRM Method], [CreditRiskType], [CreditRiskItem], [FtdAdjustMent], [CcfRate], [Reference] "
        cmd += "  FROM tbl_CRS_Main_Report "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function

    Public Overloads Shared Function getCRSTran(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT [CreditRiskMethod], [CRM Method], [CreditRiskType], [CreditRiskItem], [RW], [Notional Principle Amount], [Gross Credit Equivalent Amount], [Specific Provision], [Adjustment Item], [Net Credit Equivalent Amount], [Decrease in CRM], [Increase in CRM], [Potential Loss], [Risk Weighted Asset Outstanding Amount], [Reference] "
        cmd += "  FROM tbl_CRS_Tran_Report "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
