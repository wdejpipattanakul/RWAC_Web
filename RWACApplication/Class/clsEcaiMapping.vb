Public Class clsEcaiMapping
    Public Overloads Shared Function getSovereign() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Counter_Party_Name "
        cmd += "  FROM Q_Bloomberg_Sovereign "

        ds = toSqlDB(cmd)
        Return ds



    End Function

    Public Overloads Shared Function getCoporate() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Counter_Party_Name "
        cmd += "  FROM Q_Bloomberg_Corperate "

        ds = toSqlDB(cmd)
        Return ds



    End Function

    Public Overloads Shared Function getSpecific() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Counter_Party_Name, Comp_Name "
        cmd += "  FROM Q_Bloomberg_SP "

        ds = toSqlDB(cmd)
        Return ds

    End Function

    Public Overloads Shared Function getTransaction() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_Name, ECAI_Counter_Party_Name "
        cmd += "  FROM Q_Transaction_Mapping "

        ds = toSqlDB(cmd)
        Return ds

    End Function

    Public Overloads Shared Function getCollateral() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Collateral_Name, ECAI_Counter_Party_Name "
        cmd += "  FROM Q_Collateral_Mapping "

        ds = toSqlDB(cmd)
        Return ds

    End Function


    Public Overloads Shared Function getBond() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Contract_ID, ECAI_Counter_Party_Name "
        cmd += "  FROM Q_Bond_Mapping "

        ds = toSqlDB(cmd)
        Return ds

    End Function

    Public Overloads Shared Function getNonDVP() As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_Name, ECAI_Counter_Party_Name "
        cmd += "  FROM [Q_DVP-NONDVP_Mapping]"

        ds = toSqlDB(cmd)
        Return ds

    End Function





End Class
