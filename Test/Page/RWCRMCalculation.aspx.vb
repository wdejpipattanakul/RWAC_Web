Imports System.Data.SqlClient

Public Class RWCRMCalculation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub BindData()
        Dim ds As DataSet
        ds = clsRWCRMCalculation.getMasterInquiry("30/09/2015")
        grdTran.DataSource = ds
        grdTran.DataBind()
    End Sub

    Protected Sub grdCRSMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdTran.PageIndexChanging
        grdTran.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub RWCRMCalculate_Click(sender As Object, e As EventArgs) Handles RWCRMCalculate.Click

        Dim UserDB As String
        Dim PassDB As String
        Dim NameDB As String
        Dim DSN As String
        Dim Query As String
        Dim Query2 As String
        Dim strSQL As String
        Dim strSQL2 As String
        Dim rowCount As Integer
        Dim Collateral_Name_Array() As String
        Dim ECAI_ID_Array() As Long
        Dim ECAI_Name_Array() As String
        Dim Rating_Value_Array() As String
        Dim ECAI_Value_Priority() As Long
        Dim Rating_Debt() As Long
        Dim Rating_Group_Array() As String
        Dim ECAI_CounterPartyName_Array() As String

        Dim Original_Asset As Double
        Dim Remaining_Transaction As Double
        Dim Allocate_Asset_Amount As Double
        Dim RWA_Value As Double
        Dim ECAI_ID As Long
        Dim ECAI_Name_Save As String
        Dim Rating_Value_Save As String
        Dim Rating_Group_Save As String
        Dim Rating_Grade As Long
        Dim RW_Value As Double
        Dim RW_Case_Count As Long
        Dim Remaining_Date_Count As Long
        Dim Remaining_Term_Count As Long
        Dim Hc_Value As Double
        Dim Collateral_Type As String
        Dim record_count As Long
        Dim record_count2 As Long
        Dim i As Long = 0
        Dim k As Long = 0
        Dim Credit_Type_Case1 = New String() {"1.2", "2.1.1", "2.1.2", "2.1.3", "3.2", "4", "5", "6"}


        Dim LF_Currency As String
        Dim LS_Term As String

        UserDB = SQLConnect.getUserDB
        PassDB = SQLConnect.getPassDB
        NameDB = SQLConnect.getNameDB
        DSN = SQLConnect.getDSN



        Dim oconn1 As New SqlConnection(SQLConnect.getConnectionString())
        oconn1.Open()
        Dim oconn2 As New SqlConnection(SQLConnect.getConnectionString())
        oconn2.Open()

        strSQL = " SELECT tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name, tbl_ECAI_Rating.Rating_Group"
        strSQL += " FROM (tbl_ECAI_Rating INNER JOIN tbl_Mapping_CounterPartyName ON tbl_ECAI_Rating.Counter_Party_Name = tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name) INNER JOIN tbl_BOT_Rating_Type ON (Replace(tbl_ECAI_Rating.ECAI_Name,'''','') = tbl_BOT_Rating_Type.ECAI_Name) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term) AND (ISNULL(tbl_BOT_Rating_Type.ECAI_Value,'') = ISNULL(tbl_ECAI_Rating.Rating_Value,''))"
        strSQL += " GROUP BY tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name, tbl_ECAI_Rating.Rating_Group"
        strSQL += " HAVING ((([tbl_Mapping_CounterPartyName].[RWA_Counter_Party_Name])<>''));"




        Dim dt As New DataTable()
        Dim dt2 As New DataTable()


        'objCommand.Connection.Open()

        Dim objCommand As New SqlCommand(strSQL, oconn1)
        Dim objCommand2 As New SqlCommand(strSQL, oconn2)
        Dim objDataReader As SqlDataReader = objCommand.ExecuteReader()
        Dim objDataReader2 As SqlDataReader = objCommand2.ExecuteReader()

        objCommand2.Dispose()
        objDataReader2.Close()

        dt.Load(objDataReader)

        record_count = dt.Rows.Count
        Debug.Print("row = " & record_count)
        dt.Clear()
        dt.Rows.Clear()
        dt.Columns.Clear()
        objDataReader = objCommand.ExecuteReader()





        i = 0
        If objDataReader.HasRows Then
            objDataReader.Close()
            objDataReader = objCommand.ExecuteReader()
            dt.Load(objDataReader)
            record_count = dt.Rows.Count
            dt.Clear()
            dt.Rows.Clear()
            dt.Columns.Clear()
            objDataReader = objCommand.ExecuteReader()

            
            ReDim Preserve Collateral_Name_Array(0 To record_count - 1)
            ReDim Preserve Rating_Group_Array(0 To record_count - 1)
            ReDim Preserve ECAI_CounterPartyName_Array(0 To record_count - 1)

            

            Do While objDataReader.Read()

                SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                " SET RW_Collateral = '', RWA_Collateral = '', ECAI_Value = '', ECAI_Name = '', Rating_Grade = '',Rating_Group = '', CRM_Hc_Value = 0, ECAI_Term = '', Collateral_TYP = '', ECAI_Counter_Party_Name = '',ECAI_Currency = '' WHERE Collateral_Name = '" & objDataReader.Item("RWA_Counter_Party_Name") & "'")


                Rating_Group_Array(i) = objDataReader.Item("Rating_Group")
                Collateral_Name_Array(i) = objDataReader.Item("RWA_Counter_Party_Name")
                ECAI_CounterPartyName_Array(i) = objDataReader.Item("ECAI_Counter_Party_Name")

                i = i + 1

            Loop
            objDataReader.Close()
            objCommand.Dispose()
        Else
            MsgBox("Incomplete Data Can't Calculate RW" & vbNewLine & "Please Mapping ECAI_Name", vbCritical)
            objDataReader.Close()
            objCommand.Dispose()


            Exit Sub

        End If

        For x = 0 To UBound(Credit_Type_Case1)

            'Check Rating Group
            If Credit_Type_Case1(x) = "2.1.2" Or Credit_Type_Case1(x) = "3.2" Then
                Rating_Group_Save = "Corperate"
            Else
                Rating_Group_Save = "Sovereign"
            End If



            For i = 0 To UBound(Collateral_Name_Array)

                strSQL = "SELECT ID, Collateral_Name, Remaining_Date, Collateral_ID, Currency,Remaining_Term, [Country of guarantor] AS Location FROM  Q_Collateral_Name " & _
                    " WHERE Collateral_Name = '" & Collateral_Name_Array(i) & "' And Credit_Risk_Subtype = '" & Credit_Type_Case1(x) & "' AND Collateral_Type <> 'Stock'"
                
                objCommand = New SqlCommand(strSQL, oconn1)
                objDataReader.Close()
                objDataReader = objCommand.ExecuteReader()

                dt.Load(objDataReader)
                record_count = dt.Rows.Count
                dt.Clear()
                dt.Rows.Clear()
                dt.Columns.Clear()


                objDataReader = objCommand.ExecuteReader()

                Rating_Group_Save = Rating_Group_Array(i)
                If objDataReader.HasRows Then

                    Do While objDataReader.Read()
                        

                        strSQL2 = "SELECT Currency FROM tbl_Currency_Country WHERE Country_Code = '" & (objDataReader.Item("Location")) & "'"
                        objCommand2 = New SqlCommand(strSQL2, oconn2)
                        objDataReader2.Close()
                        objDataReader2 = objCommand2.ExecuteReader()




                        If objDataReader2.HasRows Then
                            dt2.Load(objDataReader2)
                            objDataReader2 = objCommand2.ExecuteReader()
                            dt2.Clear()
                            dt2.Rows.Clear()
                            dt2.Columns.Clear()
                            Do While objDataReader2.Read()
                                If objDataReader2.Item("Currency") = objDataReader.Item("Currency") Then
                                    LF_Currency = "Local Currency"
                                Else
                                    LF_Currency = "Foreign Currency"
                                End If
                            Loop

                        Else
                            LF_Currency = "Local Currency"
                        End If

                        'Fix Loan use long term only User Requirement No.3
                        LS_Term = "Long Term"


                        If LS_Term = "Long Term" Then
                            'strSQL2 = "SELECT RWA_Counter_Party_Name,ECAI_Name, Trim(Replace(Replace(Replace(Replace(IIF(tbl_ECAI_Rating.Rating_Value & '' <>'', tbl_ECAI_Rating.Rating_Value ,''),'/*',''),'*-',''),'*+',''),'*','')) AS Rating_Value FROM Q_Mapping_CounterPartyName2 " & _
                            '          " WHERE RWA_Counter_Party_Name = '" & objDataReader.Item("Cust_Name") & "' And Rating_Value & '' NOT IN('SD','WD','WR','RD','NR') And ISNULL(Rating_Value,'') <> '' And Term = 'Long Term' And Currency_Type = '" & LF_Currency & "'"
                            strSQL2 = "SELECT tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, Replace(tbl_ECAI_Rating.ECAI_Name,'''','') AS ECAI_Name, RTrim(LTrim(Replace(Replace(Replace(Replace(ISNULL(tbl_ECAI_Rating.Rating_Value,''),'/*',''),'*-',''),'*+',''),'*',''))) AS Rating_Value"
                            strSQL2 += " FROM (tbl_ECAI_Rating INNER JOIN tbl_Mapping_CounterPartyName ON tbl_ECAI_Rating.Counter_Party_Name = tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name) INNER JOIN tbl_BOT_Rating_Type ON  (ISNULL(tbl_BOT_Rating_Type.ECAI_Value,'') = ISNULL(tbl_ECAI_Rating.Rating_Value,'')) AND (tbl_ECAI_Rating.ECAI_Name = tbl_BOT_Rating_Type.ECAI_Name) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term)"
                            strSQL2 += " WHERE tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name = '" & objDataReader.Item("Collateral_Name") & "' And (([tbl_ECAI_Rating].[Rating_Value] + '') Not In ('SD','WD','WR','RD','NR','B','C','D')) And ISNULL(Rating_Value,'') <> '' And tbl_ECAI_Rating.Term = 'Long Term' And tbl_ECAI_Rating.Currency_Type = '" & LF_Currency & "'"

                        ElseIf LS_Term = "Short Term" Then
                            'strSQL2 = "SELECT RWA_Counter_Party_Name,ECAI_Name, Trim(Replace(Replace(Replace(Replace(IIF(tbl_ECAI_Rating.Rating_Value & '' <>'', tbl_ECAI_Rating.Rating_Value ,''),'/*',''),'*-',''),'*+',''),'*','')) AS Rating_Value FROM Q_Mapping_CounterPartyName2 " & _
                            '          " WHERE RWA_Counter_Party_Name = '" & objDataReader.Item("Cust_Name") & "' And Rating_Value & '' NOT IN('SD','WD','WR','RD','NR','B','C','D') And ISNULL(Rating_Value,'') <> '' And Term = 'Short Term' And Currency_Type = '" & LF_Currency & "'"
                            strSQL2 = "SELECT tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, Replace(tbl_ECAI_Rating.ECAI_Name,'''','') AS ECAI_Name, RTrim(LTrim(Replace(Replace(Replace(Replace(ISNULL(tbl_ECAI_Rating.Rating_Value,''),'/*',''),'*-',''),'*+',''),'*',''))) AS Rating_Value"
                            strSQL2 += " FROM (tbl_ECAI_Rating INNER JOIN tbl_Mapping_CounterPartyName ON tbl_ECAI_Rating.Counter_Party_Name = tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name) INNER JOIN tbl_BOT_Rating_Type ON  (ISNULL(tbl_BOT_Rating_Type.ECAI_Value,'') = ISNULL(tbl_ECAI_Rating.Rating_Value,'')) AND (tbl_ECAI_Rating.ECAI_Name = tbl_BOT_Rating_Type.ECAI_Name) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term)"
                            strSQL2 += " WHERE tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name = '" & objDataReader.Item("Collateral_Name") & "' And (([tbl_ECAI_Rating].[Rating_Value] + '') Not In ('SD','WD','WR','RD','NR','B','C','D')) And ISNULL(Rating_Value,'') <> '' And tbl_ECAI_Rating.Term = 'Short Term' And tbl_ECAI_Rating.Currency_Type = '" & LF_Currency & "'"
                        End If

                        objCommand2 = New SqlCommand(strSQL2, oconn2)
                        objDataReader2.Close()
                        objDataReader2 = objCommand2.ExecuteReader()

                        If objDataReader2.HasRows Then

                            dt2.Load(objDataReader2)
                            record_count2 = dt2.Rows.Count
                            dt2.Clear()
                            dt2.Rows.Clear()
                            dt2.Columns.Clear()
                            objDataReader2 = objCommand2.ExecuteReader()

                            ReDim Preserve ECAI_Name_Array(0 To record_count2 - 1)
                            ReDim Preserve Rating_Value_Array(0 To record_count2 - 1)


                            k = 0
                            Do While objDataReader2.Read()
                                If Right((objDataReader2.Item("Rating_Value")), 1) = "u" Then
                                    Rating_Value_Array(k) = Left((objDataReader2.Item("Rating_Value")), Len((objDataReader2.Item("Rating_Value"))) - 1)
                                ElseIf Left((objDataReader2.Item("Rating_Value")), 3) = "(P)" Then
                                    Rating_Value_Array(k) = Right((objDataReader2.Item("Rating_Value")), Len((objDataReader2.Item("Rating_Value"))) - 3)
                                Else

                                    Rating_Value_Array(k) = (objDataReader2.Item("Rating_Value"))
                                End If

                                ECAI_Name_Array(k) = (objDataReader2.Item("ECAI_Name"))




                                k = k + 1
                            Loop

                            ReDim Preserve ECAI_ID_Array(0 To UBound(ECAI_Name_Array))
                            ReDim Preserve ECAI_Value_Priority(0 To UBound(ECAI_Name_Array))
                            ReDim Preserve Rating_Debt(0 To UBound(ECAI_Name_Array))


                            For k = 0 To UBound(ECAI_Name_Array)

                                strSQL2 = "SELECT ID, Rating_Debt, ECAI_Value_Priority FROM tbl_BOT_Rating_Type " & _
                                          " WHERE ECAI_Name = '" & ECAI_Name_Array(k) & "' And ECAI_Value = '" & Rating_Value_Array(k) & "' And Term = '" & LS_Term & "'"

                                objCommand2 = New SqlCommand(strSQL2, oconn2)
                                objDataReader2.Close()
                                objDataReader2 = objCommand2.ExecuteReader()


                                If objDataReader2.HasRows Then

                                    dt2.Load(objDataReader2)
                                    record_count2 = dt2.Rows.Count
                                    dt2.Clear()
                                    dt2.Rows.Clear()
                                    dt2.Columns.Clear()
                                    objDataReader2 = objCommand2.ExecuteReader()

                                    Do While objDataReader2.Read()
                                        ECAI_ID_Array(k) = (objDataReader2.Item("ID"))
                                        ECAI_Value_Priority(k) = (objDataReader2.Item("ECAI_Value_Priority"))
                                        Rating_Debt(k) = (objDataReader2.Item("Rating_Debt"))

                                    Loop

                                Else

                                End If


                            Next k

                            ECAI_ID_Array = ArraySrt(ECAI_ID_Array, True)
                            ECAI_Value_Priority = ArraySrt(ECAI_Value_Priority, True)
                            Rating_Debt = ArraySrt(Rating_Debt, True)

                            'Delete NP When Case =2 -> UBoundArray = 1

                            If UBound(ECAI_Value_Priority) = 1 Then
                                For k = 1 To UBound(ECAI_Value_Priority)
                                    If ECAI_Value_Priority(1) = 99 Then
                                        ReDim Preserve ECAI_ID_Array(0 To UBound(ECAI_Value_Priority) - 1)
                                        ReDim Preserve Rating_Debt(0 To UBound(ECAI_Value_Priority) - 1)
                                        ReDim Preserve ECAI_Value_Priority(0 To UBound(ECAI_Value_Priority) - 1)
                                        RW_Case_Count = RW_Case_Count - 1
                                    End If
                                Next k
                            End If


                            RW_Value = Map_RW(Rating_Debt, ECAI_Value_Priority, Credit_Type_Case1(x), RW_Case_Count, Remaining_Date_Count, LS_Term)
                            Debug.Print(RW_Value & "_" & Credit_Type_Case1(x) & "_" & Remaining_Date_Count)
                            If UBound(Rating_Debt) + 1 < 2 Then
                                Rating_Value_Save = Rating_Debt(0)
                                ECAI_ID = ECAI_ID_Array(0)

                            ElseIf UBound(Rating_Debt) + 1 >= 2 Then
                                Rating_Value_Save = Rating_Debt(1)
                                ECAI_ID = ECAI_ID_Array(1)

                            End If


                            'Update ECAI_Name Back to Master
                            strSQL2 = "SELECT ECAI_Name, ECAI_Value, Rating_Debt FROM tbl_BOT_Rating_Type " & _
                                      " WHERE ID = " & ECAI_ID & ""

                            objCommand2 = New SqlCommand(strSQL2, oconn2)
                            objDataReader2.Close()
                            objDataReader2 = objCommand2.ExecuteReader()

                            If objDataReader2.HasRows Then

                                dt2.Load(objDataReader2)
                                record_count2 = dt2.Rows.Count
                                dt2.Clear()
                                dt2.Rows.Clear()
                                dt2.Columns.Clear()
                                objDataReader2 = objCommand2.ExecuteReader()

                                Do While objDataReader2.Read()
                                    ECAI_Name_Save = (objDataReader2.Item("ECAI_Name"))
                                    Rating_Value_Save = (objDataReader2.Item("ECAI_Value"))
                                    Rating_Grade = (objDataReader2.Item("Rating_Debt"))
                                Loop

                            Else


                            End If

                            Debug.Print("Csub = " + Credit_Type_Case1(x))

                            SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                                " SET RW_Collateral = '" & RW_Value & "', ECAI_Value = '" & Rating_Value_Save & "', ECAI_Name = '" & ECAI_Name_Save & "', Rating_Grade = '" & Rating_Grade & "', Rating_Group = '" & Rating_Group_Save & "', ECAI_Counter_Party_Name = '" & ECAI_CounterPartyName_Array(i) & "', ECAI_Currency = '" & LF_Currency & "', ECAI_Term = '" & LS_Term & "'" & _
                                " WHERE ID = '" & objDataReader.Item("ID").ToString() & "' And Credit_Risk_SubType = '" & Credit_Type_Case1(x).ToString() & "'")




                        Else
                            If LS_Term = "Long Term" Then
                                RW_Value = 1
                            ElseIf LS_Term = "Short Term" Then
                                RW_Value = 1.5
                            End If


                            SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                                     " SET RW_Collateral = '" & RW_Value & "', ECAI_Counter_Party_Name = '', ECAI_Currency = '', ECAI_Term = '', Rating_Group = '', ECAI_Value = '', ECAI_Name = 'NO RATING'" & _
                                     " WHERE ID = '" & objDataReader.Item("ID").ToString() & "'  And Credit_Risk_SubType = '" & Credit_Type_Case1(x).ToString() & "'")

                        End If







                    Loop

                Else

                End If

            Next i

        Next x


        'Calculate RW WHERE Collateral_Type = 'Stock'
        For x = 0 To UBound(Credit_Type_Case1)

            'Check Rating Group
            If Credit_Type_Case1(x) = "2.1.2" Or Credit_Type_Case1(x) = "3.2" Then
                Rating_Group_Save = "Corperate"
            Else
                Rating_Group_Save = "Sovereign"
            End If



            For i = 0 To UBound(Collateral_Name_Array)

                strSQL = "SELECT ID, Collateral_Name, Remaining_Date, Collateral_ID, Currency,Remaining_Term, [Country of guarantor] AS Location FROM  Q_Collateral_Name " & _
                    " WHERE Collateral_Name = '" & Collateral_Name_Array(i) & "' And Credit_Risk_Subtype = '" & Credit_Type_Case1(x) & "' AND Collateral_Type = 'Stock'"

                objCommand = New SqlCommand(strSQL, oconn1)
                objDataReader.Close()
                objDataReader = objCommand.ExecuteReader()

                dt.Load(objDataReader)
                record_count = dt.Rows.Count
                dt.Clear()
                dt.Rows.Clear()
                dt.Columns.Clear()


                objDataReader = objCommand.ExecuteReader()

                Rating_Group_Save = Rating_Group_Array(i)
                If objDataReader.HasRows Then

                    Do While objDataReader.Read()

                        'use to check long or short term
                        If (objDataReader.Item("Remaining_Date")) & "" = "" Then
                            LS_Term = "Short Term"
                            Remaining_Date_Count = 0 ' Use to check 3 month
                        ElseIf (objDataReader.Item("Remaining_Date")) > 365 Then
                            LS_Term = "Long Term"
                            Remaining_Date_Count = (objDataReader.Item("Remaining_Date")) ' Use to check 3 month
                        Else
                            LS_Term = "Short Term"
                            Remaining_Date_Count = (objDataReader.Item("Remaining_Date")) ' Use to check 3 month
                        End If



                        strSQL2 = "SELECT Currency FROM tbl_Currency_Country WHERE Country_Code = '" & (objDataReader.Item("Location")) & "'"
                        objCommand2 = New SqlCommand(strSQL2, oconn2)
                        objDataReader2.Close()
                        objDataReader2 = objCommand2.ExecuteReader()




                        If objDataReader2.HasRows Then
                            dt2.Load(objDataReader2)
                            objDataReader2 = objCommand2.ExecuteReader()
                            dt2.Clear()
                            dt2.Rows.Clear()
                            dt2.Columns.Clear()
                            Do While objDataReader2.Read()
                                If objDataReader2.Item("Currency") = objDataReader.Item("Currency") Then
                                    LF_Currency = "Local Currency"
                                Else
                                    LF_Currency = "Foreign Currency"
                                End If
                            Loop

                        Else
                            LF_Currency = "Local Currency"
                        End If



                        If LS_Term = "Long Term" Then
                            'strSQL2 = "SELECT RWA_Counter_Party_Name,ECAI_Name, Trim(Replace(Replace(Replace(Replace(IIF(tbl_ECAI_Rating.Rating_Value & '' <>'', tbl_ECAI_Rating.Rating_Value ,''),'/*',''),'*-',''),'*+',''),'*','')) AS Rating_Value FROM Q_Mapping_CounterPartyName2 " & _
                            '          " WHERE RWA_Counter_Party_Name = '" & objDataReader.Item("Cust_Name") & "' And Rating_Value & '' NOT IN('SD','WD','WR','RD','NR') And ISNULL(Rating_Value,'') <> '' And Term = 'Long Term' And Currency_Type = '" & LF_Currency & "'"
                            strSQL2 = "SELECT tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, Replace(tbl_ECAI_Rating.ECAI_Name,'''','') AS ECAI_Name, RTrim(LTrim(Replace(Replace(Replace(Replace(ISNULL(tbl_ECAI_Rating.Rating_Value,''),'/*',''),'*-',''),'*+',''),'*',''))) AS Rating_Value"
                            strSQL2 += " FROM (tbl_ECAI_Rating INNER JOIN tbl_Mapping_CounterPartyName ON tbl_ECAI_Rating.Counter_Party_Name = tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name) INNER JOIN tbl_BOT_Rating_Type ON  (ISNULL(tbl_BOT_Rating_Type.ECAI_Value,'') = ISNULL(tbl_ECAI_Rating.Rating_Value,'')) AND (tbl_ECAI_Rating.ECAI_Name = tbl_BOT_Rating_Type.ECAI_Name) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term)"
                            strSQL2 += " WHERE tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name = '" & objDataReader.Item("Collateral_Name") & "' And (([tbl_ECAI_Rating].[Rating_Value] + '') Not In ('SD','WD','WR','RD','NR','B','C','D')) And ISNULL(Rating_Value,'') <> '' And tbl_ECAI_Rating.Term = 'Long Term' And tbl_ECAI_Rating.Currency_Type = '" & LF_Currency & "'"

                        ElseIf LS_Term = "Short Term" Then
                            'strSQL2 = "SELECT RWA_Counter_Party_Name,ECAI_Name, Trim(Replace(Replace(Replace(Replace(IIF(tbl_ECAI_Rating.Rating_Value & '' <>'', tbl_ECAI_Rating.Rating_Value ,''),'/*',''),'*-',''),'*+',''),'*','')) AS Rating_Value FROM Q_Mapping_CounterPartyName2 " & _
                            '          " WHERE RWA_Counter_Party_Name = '" & objDataReader.Item("Cust_Name") & "' And Rating_Value & '' NOT IN('SD','WD','WR','RD','NR','B','C','D') And ISNULL(Rating_Value,'') <> '' And Term = 'Short Term' And Currency_Type = '" & LF_Currency & "'"
                            strSQL2 = "SELECT tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name, Replace(tbl_ECAI_Rating.ECAI_Name,'''','') AS ECAI_Name, RTrim(LTrim(Replace(Replace(Replace(Replace(ISNULL(tbl_ECAI_Rating.Rating_Value,''),'/*',''),'*-',''),'*+',''),'*',''))) AS Rating_Value"
                            strSQL2 += " FROM (tbl_ECAI_Rating INNER JOIN tbl_Mapping_CounterPartyName ON tbl_ECAI_Rating.Counter_Party_Name = tbl_Mapping_CounterPartyName.ECAI_Counter_Party_Name) INNER JOIN tbl_BOT_Rating_Type ON  (ISNULL(tbl_BOT_Rating_Type.ECAI_Value,'') = ISNULL(tbl_ECAI_Rating.Rating_Value,'')) AND (tbl_ECAI_Rating.ECAI_Name = tbl_BOT_Rating_Type.ECAI_Name) AND (tbl_ECAI_Rating.Term = tbl_BOT_Rating_Type.Term)"
                            strSQL2 += " WHERE tbl_Mapping_CounterPartyName.RWA_Counter_Party_Name = '" & objDataReader.Item("Collateral_Name") & "' And (([tbl_ECAI_Rating].[Rating_Value] + '') Not In ('SD','WD','WR','RD','NR','B','C','D')) And ISNULL(Rating_Value,'') <> '' And tbl_ECAI_Rating.Term = 'Short Term' And tbl_ECAI_Rating.Currency_Type = '" & LF_Currency & "'"
                        End If

                        objCommand2 = New SqlCommand(strSQL2, oconn2)
                        objDataReader2.Close()
                        objDataReader2 = objCommand2.ExecuteReader()

                        If objDataReader2.HasRows Then

                            dt2.Load(objDataReader2)
                            record_count2 = dt2.Rows.Count
                            dt2.Clear()
                            dt2.Rows.Clear()
                            dt2.Columns.Clear()
                            objDataReader2 = objCommand2.ExecuteReader()

                            ReDim Preserve ECAI_Name_Array(0 To record_count2 - 1)
                            ReDim Preserve Rating_Value_Array(0 To record_count2 - 1)


                            k = 0
                            Do While objDataReader2.Read()
                                If Right((objDataReader2.Item("Rating_Value")), 1) = "u" Then
                                    Rating_Value_Array(k) = Left((objDataReader2.Item("Rating_Value")), Len((objDataReader2.Item("Rating_Value"))) - 1)
                                ElseIf Left((objDataReader2.Item("Rating_Value")), 3) = "(P)" Then
                                    Rating_Value_Array(k) = Right((objDataReader2.Item("Rating_Value")), Len((objDataReader2.Item("Rating_Value"))) - 3)
                                Else

                                    Rating_Value_Array(k) = (objDataReader2.Item("Rating_Value"))
                                End If

                                ECAI_Name_Array(k) = (objDataReader2.Item("ECAI_Name"))




                                k = k + 1
                            Loop

                            ReDim Preserve ECAI_ID_Array(0 To UBound(ECAI_Name_Array))
                            ReDim Preserve ECAI_Value_Priority(0 To UBound(ECAI_Name_Array))
                            ReDim Preserve Rating_Debt(0 To UBound(ECAI_Name_Array))


                            For k = 0 To UBound(ECAI_Name_Array)

                                strSQL2 = "SELECT ID, Rating_Debt, ECAI_Value_Priority FROM tbl_BOT_Rating_Type " & _
                                          " WHERE ECAI_Name = '" & ECAI_Name_Array(k) & "' And ECAI_Value = '" & Rating_Value_Array(k) & "' And Term = '" & LS_Term & "'"

                                objCommand2 = New SqlCommand(strSQL2, oconn2)
                                objDataReader2.Close()
                                objDataReader2 = objCommand2.ExecuteReader()


                                If objDataReader2.HasRows Then

                                    dt2.Load(objDataReader2)
                                    record_count2 = dt2.Rows.Count
                                    dt2.Clear()
                                    dt2.Rows.Clear()
                                    dt2.Columns.Clear()
                                    objDataReader2 = objCommand2.ExecuteReader()

                                    Do While objDataReader2.Read()
                                        ECAI_ID_Array(k) = (objDataReader2.Item("ID"))
                                        ECAI_Value_Priority(k) = (objDataReader2.Item("ECAI_Value_Priority"))
                                        Rating_Debt(k) = (objDataReader2.Item("Rating_Debt"))

                                    Loop

                                Else

                                End If


                            Next k

                            ECAI_ID_Array = ArraySrt(ECAI_ID_Array, True)
                            ECAI_Value_Priority = ArraySrt(ECAI_Value_Priority, True)
                            Rating_Debt = ArraySrt(Rating_Debt, True)

                            'Delete NP When Case =2 -> UBoundArray = 1

                            If UBound(ECAI_Value_Priority) = 1 Then
                                For k = 1 To UBound(ECAI_Value_Priority)
                                    If ECAI_Value_Priority(1) = 99 Then
                                        ReDim Preserve ECAI_ID_Array(0 To UBound(ECAI_Value_Priority) - 1)
                                        ReDim Preserve Rating_Debt(0 To UBound(ECAI_Value_Priority) - 1)
                                        ReDim Preserve ECAI_Value_Priority(0 To UBound(ECAI_Value_Priority) - 1)
                                        RW_Case_Count = RW_Case_Count - 1
                                    End If
                                Next k
                            End If


                            RW_Value = Map_RW(Rating_Debt, ECAI_Value_Priority, Credit_Type_Case1(x), RW_Case_Count, Remaining_Date_Count, LS_Term)
                            Debug.Print(RW_Value & "_" & Credit_Type_Case1(x) & "_" & Remaining_Date_Count)
                            If UBound(Rating_Debt) + 1 < 2 Then
                                Rating_Value_Save = Rating_Debt(0)
                                ECAI_ID = ECAI_ID_Array(0)

                            ElseIf UBound(Rating_Debt) + 1 >= 2 Then
                                Rating_Value_Save = Rating_Debt(1)
                                ECAI_ID = ECAI_ID_Array(1)

                            End If


                            'Update ECAI_Name Back to Master
                            strSQL2 = "SELECT ECAI_Name, ECAI_Value, Rating_Debt FROM tbl_BOT_Rating_Type " & _
                                      " WHERE ID = " & ECAI_ID & ""

                            objCommand2 = New SqlCommand(strSQL2, oconn2)
                            objDataReader2.Close()
                            objDataReader2 = objCommand2.ExecuteReader()

                            If objDataReader2.HasRows Then

                                dt2.Load(objDataReader2)
                                record_count2 = dt2.Rows.Count
                                dt2.Clear()
                                dt2.Rows.Clear()
                                dt2.Columns.Clear()
                                objDataReader2 = objCommand2.ExecuteReader()

                                Do While objDataReader2.Read()
                                    ECAI_Name_Save = (objDataReader2.Item("ECAI_Name"))
                                    Rating_Value_Save = (objDataReader2.Item("ECAI_Value"))
                                    Rating_Grade = (objDataReader2.Item("Rating_Debt"))
                                Loop

                            Else


                            End If

                            Debug.Print("Csub = " + Credit_Type_Case1(x))

                            SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                                " SET RW_Collateral = '" & 1 & "', ECAI_Value = '" & Rating_Value_Save & "', ECAI_Name = '" & ECAI_Name_Save & "', Rating_Grade = '" & Rating_Grade & "', Rating_Group = '" & Rating_Group_Save & "', ECAI_Counter_Party_Name = '" & ECAI_CounterPartyName_Array(i) & "', ECAI_Currency = '" & LF_Currency & "', ECAI_Term = '" & LS_Term & "'" & _
                                " WHERE ID = '" & objDataReader.Item("ID").ToString() & "' And Credit_Risk_SubType = '" & Credit_Type_Case1(x).ToString() & "'")




                        Else
                            If LS_Term = "Long Term" Then
                                RW_Value = 1
                            ElseIf LS_Term = "Short Term" Then
                                RW_Value = 1.5
                            End If


                            SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                                     " SET RW_Collateral = '" & RW_Value & "', ECAI_Counter_Party_Name = '', ECAI_Currency = '', ECAI_Term = '', Rating_Group = '', ECAI_Value = '', ECAI_Name = 'NO RATING'" & _
                                     " WHERE ID = '" & objDataReader.Item("ID").ToString() & "'  And Credit_Risk_SubType = '" & Credit_Type_Case1(x).ToString() & "'")

                        End If







                    Loop

                Else

                End If

            Next i

        Next x



        strSQL = "SELECT  ID, Collateral_ID, Remaining_Term, Rating_Grade, Collateral_Type,Credit_Risk_Subtype FROM  tbl_CRM_Master " & _
                " WHERE Collateral_Type = 'Stock' And ISNULL(RW_Collateral, '') <> ''"

        objCommand = New SqlCommand(strSQL, oconn1)
        objDataReader.Close()
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then

            Do While objDataReader.Read()
                Hc_Value = Cal_Hc(objDataReader.Item("Remaining_Term"), objDataReader.Item("Rating_Grade"), objDataReader.Item("Credit_Risk_Subtype"))
               
                SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                            " SET CRM_Hc_Value = '" & Hc_Value & "'" & _
                            " WHERE ID = '" & objDataReader.Item("ID").ToString() & "'")
            Loop

        End If

        'Update CRM_Hc_Value = 0 Not Bond
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master " & _
                     " SET CRM_Hc_Value = 0" & _
                     " WHERE Collateral_Type <> 'Stock' And RW_Collateral & '' <> ''")

        'Update RW No Mapping Case

        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                     " SET RW_Collateral = 1, ECAI_Name = 'NO RATING'" & _
                     " WHERE Credit_Risk_Subtype IN('1.1','1.2','2.1.1','2.1.2','2.1.3','4','5') And (RW_Collateral & '' = '' OR ECAI_Value & '' ='')")

        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                     " SET RW_Collateral = 0.5, ECAI_Name = 'NO RATING'" & _
                     " WHERE Credit_Risk_Subtype IN('3.1','3.2') And (RW_Collateral & '' = '' OR ECAI_Value & '' ='')")

        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                     " SET RW_Collateral = 1, ECAI_Name = 'NO RATING'" & _
                     " WHERE Credit_Risk_Subtype IN('6') And (RW_Collateral & '' = '' OR ECAI_Value & '' ='')")




        '1.1 1.3 RW_Collateral = 0% Calculate RWA
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                     " SET RW_Collateral = 0, ECAI_Counter_Party_Name = '', ECAI_Currency = '', ECAI_Term = '', Rating_Group = '', ECAI_Name = '', Rating_Grade = '', ECAI_Value = ''" & _
                     " WHERE Credit_Risk_Subtype IN('1.1','3.1')")

        'Update Cash RW = 0
        SQLConnect.ExcNonQuery("UPDATE tbl_CRM_Master" & _
                     " SET RW_Collateral = 0, ECAI_Counter_Party_Name = '', ECAI_Currency = '', ECAI_Term = '', Rating_Group = '', ECAI_Name = '', Rating_Grade = '', ECAI_Value = ''" & _
                     " WHERE Collateral_Type IN('Saving Deposit','Current Deposit','Fixed Deposit')")








        objDataReader.Close()
        objCommand.Dispose()
        oconn1.Close()
        objDataReader2.Close()
        objCommand2.Dispose()
        oconn2.Close()





        BindData()

        MsgBox("Re-Calculate RW Successful")

    End Sub


    Public Function ArraySrt(ArrayIn As Array, Ascending As Boolean)

        Dim SrtTemp As Object
        Dim i As Long
        Dim j As Long


        If Ascending = True Then
            For i = LBound(ArrayIn) To UBound(ArrayIn)
                For j = i + 1 To UBound(ArrayIn)
                    If ArrayIn(i) > ArrayIn(j) Then
                        SrtTemp = ArrayIn(j)
                        ArrayIn(j) = ArrayIn(i)
                        ArrayIn(i) = SrtTemp

                    End If
                Next j
            Next i
        Else
            For i = LBound(ArrayIn) To UBound(ArrayIn)
                For j = i + 1 To UBound(ArrayIn)
                    If ArrayIn(i) < ArrayIn(j) Then
                        SrtTemp = ArrayIn(j)
                        ArrayIn(j) = ArrayIn(i)
                        ArrayIn(i) = SrtTemp

                    End If
                Next j
            Next i
        End If

        ArraySrt = ArrayIn

    End Function

    Public Function Map_RW(Rating, Priority, Credit_Subtype, RW_Case_Count, Remaining_Term, LS_Term) As Double
        Dim i As Long
        Dim case_check As Long



        If RW_Case_Count < 2 Then
            case_check = Rating(0)

        ElseIf RW_Case_Count = 2 Then
            case_check = Rating(1)

        ElseIf RW_Case_Count > 2 Then
            case_check = Rating(1)
        End If



        If LS_Term = "Long Term" Then
            If Credit_Subtype = "1.2" Then

                If (case_check = 1) Then
                    Map_RW = 0
                ElseIf (case_check = 2) Then
                    Map_RW = 0.2
                ElseIf (case_check = 3) Then
                    Map_RW = 0.5
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If


            ElseIf Credit_Subtype = "2.1.1" And Remaining_Term > 90 Then

                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If


            ElseIf Credit_Subtype = "2.1.2" Then

                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

                Debug.Print("Rating = " & case_check)
                Debug.Print("Map_RW = " & Map_RW)

            ElseIf Credit_Subtype = "2.1.1" And Remaining_Term <= 90 Then
                If (case_check = 1) Then
                    Map_RW = 0
                ElseIf (case_check = 2) Then
                    Map_RW = 0.2
                ElseIf (case_check = 3) Then
                    Map_RW = 0.5
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

            ElseIf Credit_Subtype = "2.1.3" Then
                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1.5
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

            ElseIf Credit_Subtype = "3.2" Then
                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1.5
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If


            ElseIf Credit_Subtype = "4" Or Credit_Subtype = "5" And Remaining_Term > 90 Then

                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

            ElseIf Credit_Subtype = "4" Or Credit_Subtype = "5" And Remaining_Term <= 90 Then

                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

            ElseIf Credit_Subtype = "6" Then
                If (case_check = 1) Then
                    Map_RW = 0.2
                ElseIf (case_check = 2) Then
                    Map_RW = 0.5
                ElseIf (case_check = 3) Then
                    Map_RW = 1
                ElseIf (case_check = 4) Then
                    Map_RW = 1
                ElseIf (case_check = 5) Then
                    Map_RW = 1.5
                ElseIf (case_check = 6) Then
                    Map_RW = 1.5
                End If

            End If

        ElseIf LS_Term = "Short Term" Then
            If (case_check = 1) Then
                Map_RW = 0.2
            ElseIf (case_check = 2) Then
                Map_RW = 0.5
            ElseIf (case_check = 3) Then
                Map_RW = 1
            ElseIf (case_check = 4) Then
                Map_RW = 1.5
            End If

        End If



    End Function


    Public Function Cal_Hc(Remaining_Term, Rating_Grade, Collateral_Type) As Double
        If Rating_Grade = 1 Then
            If Remaining_Term <= 365 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.005
                Else
                    Cal_Hc = 0.01
                End If

            ElseIf Remaining_Term > 365 And Remaining_Term <= 1825 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.02
                Else
                    Cal_Hc = 0.04
                End If

            ElseIf Remaining_Term > 1825 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.04
                Else
                    Cal_Hc = 0.08
                End If

            End If



        ElseIf Rating_Grade = 4 And (Collateral_Type = "1.2") Then
            Cal_Hc = 0.15

        Else
            If Remaining_Term <= 365 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.01
                Else
                    Cal_Hc = 0.02
                End If

            ElseIf Remaining_Term > 365 And Remaining_Term <= 1825 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.03
                Else
                    Cal_Hc = 0.06
                End If

            ElseIf Remaining_Term > 1825 Then
                If Collateral_Type = "1.2" Then
                    Cal_Hc = 0.06
                Else
                    Cal_Hc = 0.12
                End If

            End If

        End If


    End Function
End Class