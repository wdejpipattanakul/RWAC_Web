
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security
Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub submir_Click(sender As Object, e As EventArgs)
        Dim userId As Integer = 0
        Dim constr As String = SQLConnect.getConnectionString() 'ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Validate_User")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Username", uname.Text)
                cmd.Parameters.AddWithValue("@Password", upass.Text)
                cmd.Connection = con
                con.Open()
                userId = Convert.ToInt32(cmd.ExecuteScalar())
                con.Close()
            End Using
            Select Case userId
                Case -1
                    'Login1.FailureText = "Username and/or password is incorrect."
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Username and/or password is incorrect.')</script>")
                    Exit Select
                Case -2
                    'Login1.FailureText = "Account has not been activated."
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Account has not been activated.')</script>")
                    Exit Select
                Case Else
                    FormsAuthentication.RedirectFromLoginPage(uname.Text, False)
                    Exit Select
            End Select
        End Using
    End Sub
End Class
