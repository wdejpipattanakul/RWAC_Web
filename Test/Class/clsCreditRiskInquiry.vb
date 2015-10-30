Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCreditRiskInquiry
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Contract_ID, Facility_ID, COA, Maturity_Date, Remaining_Term, Amount, [Currency], Amount_THB, Accrued, Adjust_Accrued, Specific_Provision, Net_Balance, Trading_Book_Flag, Credit_Exposure, Credit_Type, Credit_Risk_Subtype FROM tbl_RWAC_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
