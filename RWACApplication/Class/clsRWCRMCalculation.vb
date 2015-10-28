Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsRWCRMCalculation
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Counter_Party_ID, Counter_Party_Name, Collateral_ID, Facility_ID, Collateral_Type, Owner_Guarantor_Name, Amount, Currency, Remaining_Date, Start_Date, Maturity_Date, Credit_Risk_Type, Credit_Risk_Subtype, Rating_Group, ECAI_Currency, ECAI_Term, ECAI_Name, ECAI_Value, Rating_Grade, CRM_Hc_Value, RW_Collateral"
        cmd += "  FROM tbl_CRM_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
