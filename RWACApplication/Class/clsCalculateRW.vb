Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCalculateRW
    Public Overloads Shared Function getTransaction(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Facility_ID, Contract_ID, DealDate, Maturity_Date, Credit_Exposure, Calculation_Method, CCF, PCE, Trans_He_Value, RW, [Currency], Amount, Amount_THB, Adjust_Accrued, Specific_Provision, Allocate_Asset_Amount, RW_Funding, Funding_Allocated_Amount, CRM_Allocated_Amount, RWA_Collateral, RWA_No_Collateral, RWA "
        cmd += "  FROM tbl_RWAC_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds



    End Function
End Class
