Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCRMAllocation
    Public Overloads Shared Function getCRMTransactionList(Optional type As String = "") As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Facility_ID, Contract_ID, Asset_Classification_Type, NPL_Flag, Maturity_Date, Remaining_Term, Currency, Remaining_Transaction_Amount AS Amount, Remaining_Transaction_Amount - CRM_Allocated_Amount AS Remaining_Amount, Calculation_Method, Credit_Exposure "
        cmd += "  FROM Q_CRM_Allocated_Transaction_List "
        If type = "CRM" Then
            cmd += " WHERE AOD IS NOT NULL"
        ElseIf type = "NoCRM" Then
            cmd += " WHERE AOD IS NULL"
        End If
        ds = toSqlDB(cmd)
        Return ds
    End Function

    Public Overloads Shared Function getCRMList(ByVal custID As String, ByVal facilID As String, ByVal tranID As String, ByVal type As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Facility_ID, Transaction_ID, CRM_ID, CRM_Type, CRM_Maturity_Date, CRM_Currency, CRM_Amount, CRM_Remaining_Amount, CRM_Allocated_Amount, CRM_Allocated_Amount_Ori, ID "
        cmd += "  FROM tbl_CRM_Allocated "
        cmd += " WHERE [Cust_ID] = '" & custID & "' AND [Facility_ID] = '" & facilID & "' AND [Transaction_ID] = '" & tranID & "' "

        If type <> "" Then cmd += "   AND [TYPE] = '" & type & "' "

        cmd += " ORDER BY [CRM_ID], [ID]"

        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
