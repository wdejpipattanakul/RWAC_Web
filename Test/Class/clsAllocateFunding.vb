Public Class clsAllocateFunding
    Public Overloads Shared Function getMasterInquiry(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Contract_ID, FX_AND_Original_Currency, IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued])-[Specific_Provision] AS Amount, Credit_Exposure, Credit_Type, Credit_Risk_Subtype, Remaining_Date, Allocate_Asset_Amount "
        cmd += "  FROM tbl_Funding_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds



    End Function

    Public Overloads Shared Function getMasterInquiry2(ByVal aod As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT AcctID, CCY, COA, bal "
        cmd += "  FROM tbl_Funding_List "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "'"
        ds = toSqlDB(cmd)
        Return ds



    End Function

    Public Overloads Shared Function getSearch(ByVal aod As String, ByVal Cust_ID As String, ByVal Currency As String) As DataSet
        Dim ds As New DataSet
        Dim cmd As String
        cmd = " SELECT Cust_ID, Cust_Name, Contract_ID, FX_AND_Original_Currency, IIf([Credit_Exposure]='Derivative',[CEA],[Amount_THB])+IIf([Adjust_Accrued]<>0,[Adjust_Accrued],[Accrued])-[Specific_Provision] AS Amount, Credit_Exposure, Credit_Type, Credit_Risk_Subtype, Remaining_Date, Allocate_Asset_Amount "
        cmd += "  FROM tbl_Funding_Master "
        cmd += " WHERE CONVERT(nvarchar, AOD, 103) = '" & aod & "' And Cust_ID = '" & Cust_ID & "' And FX_AND_Original_Currency = '" & Currency & "'"
        ds = toSqlDB(cmd)
        Return ds



    End Function


End Class
