Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCreditExposureInquiry
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Contract_ID, Facility_ID, COA, Maturity_Date, Remaining_Term, CCF, Amount, [Currency], Amount_THB, Accrued, Adjust_Accrued, Specific_Provision, CEA, PCE, Net_Balance, NPL_Flag, Trading_Book_Flag, FX_Netting_Flag , Credit_Exposure  FROM tbl_RWAC_Master"
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
