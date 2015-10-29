Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCRMInquiry
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Counter_Party_ID, Counter_Party_Name, Collateral_ID, Facility_ID, Owner_Guarantor_Name, Collateral_Type, Amount, [Currency], Start_Date, Maturity_Date, Remaining_Term, Credit_Risk_Type, CRM_Use_Flag, CRM_Revalue_Term, CRM_Holding_Period, Credit_Risk_Subtype FROM tbl_CRM_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
