Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsAssetManager
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Asset_Classification, NPL_Flag, General_Provision_Factor, CONVERT(nvarchar, NPL_Start_Date, 103) as NPL_Start_Date  FROM tbl_Customer_Master"

        ds = toSqlDB(cmd)
        Return ds
    End Function

    Public Overloads Shared Function getMasterInquiryTransaction(ByVal aod As String, ByVal Cust_ID As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = "Select Cust_ID, Cust_Name, Contract_ID, Asset_Classification, NPL_Flag, General_Provision_Factor, CONVERT(nvarchar, NPL_Start_Date, 103) as NPL_Start_Date From tbl_Asset_Transaction_Master WHERE [Cust_ID] = '" & Cust_ID & "' Order by Contract_ID"

        ds = toSqlDB(cmd)
        Return ds
    End Function

    Public Overloads Shared Function getMasterInquiryCollateral(ByVal aod As String, ByVal Cust_ID As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = "Select Counter_Party_ID, Collateral_ID, Counter_Party_Name, Owner_Guarantor_Name, Asset_Classification, NPL_Flag, General_Provision_Factor, CONVERT(nvarchar, NPL_Start_Date, 103) as NPL_Start_Date From tbl_Asset_Collateral_Master WHERE [Counter_Party_ID] = '" & Cust_ID & "' Order by Collateral_ID"

        ds = toSqlDB(cmd)
        Return ds
    End Function


End Class
