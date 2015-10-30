
Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsRWDebtCalculation
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = "SELECT AOD, Cust_ID, Cust_Name, Contract_ID, COA, Maturity_Date, Remaining_Term, Remaining_Date, Amount, [Currency], Amount_THB, Accrued, Adjust_Accrued, Specific_Provision, Credit_Exposure, NPL_Flag, Credit_Type, Credit_Risk_Subtype, Rating_Group, Term, ECAI_Currency, ECAI_Name, ECAI_Value, Rating_Grade, RW, CEA, PCE, RWA FROM tbl_RWAC_Master"
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
