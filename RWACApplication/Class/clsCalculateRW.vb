Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCalculateRW
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT [Cust_ID], [Cust_Name], [Contract_ID], [Facility_ID], [Currency], [Amount], [Accrued], [Adjust_Accrued], [Specific_Provision], [Net_Balance], [DealDate], [Maturity_Date], [Credit_Exposure], [Credit_Type], [Credit_Risk_Subtype], [Trading_Book_Flag], [NPL_Flag], [Netting_Agreement_Flag], [RW], [RWA_Collateral], [RWA_No_Collateral], [RWA] "
        cmd += "  FROM tbl_RWAC_History "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
