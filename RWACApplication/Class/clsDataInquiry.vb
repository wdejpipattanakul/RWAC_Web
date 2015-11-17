Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Public Class clsDataInquiry
    Public Overloads Shared Function getMasterInquiry(ByVal TableName As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT * FROM " & TableName & ""
        ds = toSqlDB(cmd)
        Return ds
    End Function

   
End Class
