Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsCreditRiskClassification
    Public Overloads Shared Function getTransactionList(ByVal type As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String = ""

        Select Case type
            Case "Customer"
                cmd = " SELECT Cust_ID AS ID, Cust_Name, BOT_Credit_Risk_Item AS BOT_Credit_Type, Credit_Risk_Type AS Credit_Type, Credit_Risk_Subtype AS Credit_Subtype FROM tbl_Customer_Master "
            Case "Guarantor"
                cmd = " SELECT Owner_Guarantor_Name AS ID, Owner_Guarantor_Name AS Cust_Name, BOT_Credit_Type, Credit_Type, Credit_Subtype FROM tbl_Owner_Guarantor "
            Case "OtherAssets"
                cmd = " SELECT Transaction_ID AS ID, Transaction_ID AS Cust_Name, BOT_Credit_Type, Credit_Type, Credit_Subtype FROM tbl_Other_Asset "
            Case "DvP"
                cmd = " SELECT Transaction_ID AS ID, Transaction_ID AS Cust_Name, BOT_Credit_Type, Credit_Type, Credit_Subtype FROM tbl_DvP_Non_DvP "
            Case Else
                cmd = " SELECT Cust_ID AS ID, Cust_Name, BOT_Credit_Risk_Item AS BOT_Credit_Type, Credit_Risk_Type AS Credit_Type, Credit_Risk_Subtype AS Credit_Subtype FROM tbl_Customer_Master "
        End Select

        ds = toSqlDB(cmd)
        Return ds
    End Function
End Class
