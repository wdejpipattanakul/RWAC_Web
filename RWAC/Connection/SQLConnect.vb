Imports System.Data.SqlClient
Imports System.Data

Public Module SQLConnect
    Private UserDB As String = ConfigurationManager.AppSettings.Item("UserDB").ToString
    Private PassDB As String = ConfigurationManager.AppSettings.Item("PassDB").ToString
    Private NameDB As String = ConfigurationManager.AppSettings.Item("NameDB").ToString
    Private DSN As String = ConfigurationManager.AppSettings.Item("DSNDB").ToString
    Public Function getUserDB() As String
        Return UserDB
    End Function
    Public Function getPassDB() As String
        Return PassDB
    End Function
    Public Function getNameDB() As String
        Return NameDB
    End Function
    Public Function getDSN() As String
        Return DSN
    End Function
    Public Function getConnectionString() As String
        Return "data source=" & getDSN() & ";initial catalog=" & getNameDB() & ";user id=" & getUserDB() & ";password=" & getPassDB() & ";Enlist=no;Persist Security Info=no;Connection Lifetime=0;Max Pool Size=1;Min Pool Size=0"
    End Function
#Region " Query "
    Public Function toSqlDB(ByVal query As String) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try

        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter(query, oconn1)

        Dim DS As New DataSet
        'oadap.SelectCommand = New SqlCommand(query, oconn)
        oadap1.Fill(DS)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal paramarr() As SqlParameter) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try

        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter

        Dim DS As New DataSet
        Dim ocomm1 = New SqlCommand(query, oconn1)
        AttachParameter(ocomm1, paramarr)
        oadap1.SelectCommand = ocomm1
        oadap1.Fill(DS)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal DS As DataSet) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter(query, oconn1)

        'oadap.SelectCommand = New SqlCommand(query, oconn)
        oadap1.Fill(DS)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal paramarr() As SqlParameter, ByVal DS As DataSet) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter

        Dim ocomm1 = New SqlCommand(query, oconn1)
        AttachParameter(ocomm1, paramarr)
        oadap1.SelectCommand = ocomm1
        oadap1.Fill(DS)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal TableName As String) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter(query, oconn1)

        Dim DS As New DataSet
        'oadap.SelectCommand = New SqlCommand(query, oconn)
        oadap1.Fill(DS, TableName)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal paramarr() As SqlParameter, ByVal TableName As String) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter

        Dim DS As New DataSet
        Dim ocomm1 = New SqlCommand(query, oconn1)
        AttachParameter(ocomm1, paramarr)
        oadap1.SelectCommand = ocomm1
        oadap1.Fill(DS, TableName)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal TableName As String, ByVal DS As DataSet) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter(query, oconn1)

        'oadap.SelectCommand = New SqlCommand(query, oconn)
        oadap1.Fill(DS, TableName)
        'oconn.Close()
        Return DS
    End Function
    Public Function toSqlDB(ByVal query As String, ByVal paramarr() As SqlParameter, ByVal TableName As String, ByVal DS As DataSet) As DataSet
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim oadap1 As New SqlDataAdapter

        Dim ocomm1 = New SqlCommand(query, oconn1)
        AttachParameter(ocomm1, paramarr)
        oadap1.SelectCommand = ocomm1
        oadap1.Fill(DS, TableName)
        'oconn.Close()
        Return DS
    End Function
#End Region
#Region " Excute None Query "
    Public Function ExcNonQuery(ByVal query As String) As Boolean
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim ocomm1 As New SqlCommand(query, oconn1)
        'ocomm1.CommandText = query
        'ocomm1.Connection = oconn1
        Try
            oconn1.Open()
            ocomm1.ExecuteNonQuery()
            Return True
        Catch ex As SqlException
            Return False
        Finally
            oconn1.Close()
        End Try
    End Function
    Public Function ExcNonQuery(ByVal query As String, ByVal paramarr() As SqlParameter) As Boolean
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim ocomm1 As New SqlCommand(query, oconn1)
        AttachParameter(ocomm1, paramarr)
        Try
            oconn1.Open()
            ocomm1.ExecuteNonQuery()
            Return True
        Catch ex As SqlException
            Return False
        Finally
            oconn1.Close()
        End Try
    End Function
    Public Function ExcSP(ByVal SP_Name As String, ByVal paramarr() As SqlParameter) As Boolean
        'Try
        '    oconn.Open()
        '    oconn.Close()
        'Catch ex As Exception
        '    newConnection()
        'End Try

        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim ocomm1 As New SqlCommand(SP_Name, oconn1)

        'If Not (oconn.State = ConnectionState.Open) Then
        '    oconn.Open()
        'End If

        'ocomm = New SqlCommand
        ExcSP = False
        ocomm1.CommandType = CommandType.StoredProcedure
        'ocomm1.CommandText = SP_Name
        'ocomm1.Connection = oconn1
        AttachParameter(ocomm1, paramarr)
        Try
            oconn1.Open()
            ocomm1.ExecuteNonQuery()
            ExcSP = True
        Catch ex As SqlException
            Throw New Exception(ex.ToString)
            'Return False
        Finally
            oconn1.Close()
        End Try
    End Function
    Public Function ExcSP(ByVal SP_Name As String, ByVal paramarr() As SqlParameter, ByVal IndexReturn As Integer) As String
        Dim oconn1 As New SqlConnection(getConnectionString())
        Dim ocomm1 As New SqlCommand(SP_Name, oconn1)

        'If Not (oconn.State = ConnectionState.Open) Then
        '    oconn.Open()
        'End If
        'ocomm = New SqlCommand
        ExcSP = ""
        ocomm1.CommandType = CommandType.StoredProcedure
        'ocomm.CommandText = SP_Name
        'ocomm.Connection = oconn
        AttachParameter(ocomm1, paramarr)
        Try
            oconn1.Open()
            ocomm1.ExecuteNonQuery()
            ExcSP = ocomm1.Parameters(IndexReturn).Value
        Catch ex As SqlException
            Throw New Exception(ex.ToString)
        Finally
            oconn1.Close()
        End Try
    End Function
#End Region
#Region " Function 4 Class "
    Private Sub AttachParameter(ByVal comm As SqlCommand, ByVal parameterarray() As SqlParameter)
        For Each parameter As SqlParameter In parameterarray
            Try
                If parameter.Value = "" Then
                    parameter.Value = System.DBNull.Value
                    comm.Parameters.Add(parameter)
                Else
                    comm.Parameters.Add(parameter)
                End If
            Catch ex As Exception
                comm.Parameters.Add(parameter)
            End Try
        Next
    End Sub
#End Region
End Module

