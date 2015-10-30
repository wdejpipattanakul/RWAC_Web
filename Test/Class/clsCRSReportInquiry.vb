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

    Public Overloads Shared Function getCRSMainExport(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim sSql1 As String
        sSql1 = " SELECT TOP 140 CreditRiskMethod, [CRM Method], CreditRiskType, CreditRiskItem, FtdAdjustMent, CcfRate, Reference "
        sSql1 += "  FROM tbl_CRS_Main_Report"
        sSql1 += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(sSql1)
        Return ds
    End Function

    Public Overloads Shared Function getCRSTranExport(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim sSql1 As String
        sSql1 = " SELECT TOP 140 CreditRiskMethod, [CRM Method], CreditRiskType, CreditRiskItem, RW, [Notional Principle Amount], [Gross Credit Equivalent Amount], [Specific Provision], [Adjustment Item], [Net Credit Equivalent Amount], [Decrease in CRM], [Increase in CRM], [Potential Loss], [Risk Weighted Asset Outstanding Amount], Reference "
        sSql1 += "  FROM tbl_CRS_Tran_Report"
        sSql1 += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(sSql1)
        Return ds
    End Function
End Class
