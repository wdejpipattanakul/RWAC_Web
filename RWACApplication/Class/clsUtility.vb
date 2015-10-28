Imports System.Data
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient


Public Class clsUtility
    Public Shared cn As New SqlConnection
    Shared ReadOnly Property SystemDate() As String
        Get
            Return Now.ToString("yyyyMMdd")
        End Get
    End Property
    Shared ReadOnly Property SystemTime() As String
        Get
            Return Now.ToString("HHmmss")
        End Get
    End Property

    Public Enum TypeDate
        [ddmmyyyy]
        [mmddyyyy]
        [yyyymmdd]
    End Enum
    Public Enum TypeYear
        Eng
        Thai
    End Enum
    Public Enum FirstLineComboT
        None
        Blang
        Text
    End Enum
    Public Enum Seperate
        No
        Yes
    End Enum


#Region " Utility "
    Public Shared Sub MsgBox(ByVal page As Page, ByVal str As String)
        Try
            page.RegisterStartupScript(str, "<script language='javascript'> alert('" & str & "'); </script>")
        Catch ex As Exception

        End Try
    End Sub
    Public Overloads Shared Sub InsertLineCombo(ByVal objCombo As Object, ByVal intIndex As Int16, ByVal strTextField As String, ByVal strValueField As String)
        Try
            Dim Combo As New DropDownList
            Combo = objCombo
            Combo.Items.Insert(intIndex, strTextField)
            Combo.Items.Item(intIndex).Value = strValueField
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Overloads Shared Sub BindCombo(ByVal objCombo As Object, _
                                            ByVal strTextField As String, _
                                            ByVal strValueField As String, _
                                            ByVal DataTable As DataTable, _
                                            ByVal strFirstLine As FirstLineComboT, _
                                            Optional ByVal ValueFirst As String = "", _
                                            Optional ByVal TextFirst As String = "")
        Try
            Dim Combo As New DropDownList
            Combo = objCombo
            Combo.DataSource = DataTable
            Combo.DataTextField = strTextField
            Combo.DataValueField = strValueField
            Combo.DataBind()

            If DataTable.Rows.Count > 0 Then
                If strFirstLine = FirstLineComboT.Blang Then
                    InsertLineCombo(Combo, 0, "", ValueFirst)
                ElseIf strFirstLine = FirstLineComboT.Text Then
                    InsertLineCombo(Combo, 0, TextFirst, ValueFirst)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Overloads Shared Sub BindCombo(ByVal DataSet As DataSet, ByVal strTextField As String, ByVal strValueField As String, ByVal strFirstLine As FirstLineComboT, ByVal ParamArray objCombo() As DropDownList)
        Try
            Dim Combo As DropDownList
            For iTable As Int16 = 0 To DataSet.Tables.Count - 1
                Combo = New DropDownList
                Combo = objCombo(iTable)
                Combo.DataSource = DataSet.Tables(iTable)
                Combo.DataTextField = strTextField
                Combo.DataValueField = strValueField
                Combo.DataBind()
                If DataSet.Tables(iTable).Rows.Count > 0 Then
                    If strFirstLine = FirstLineComboT.Blang Then
                        InsertLineCombo(Combo, 0, "", "")
                    ElseIf strFirstLine = FirstLineComboT.Text Then
                        InsertLineCombo(Combo, 0, "", "")
                    End If
                End If
                Combo = Nothing
            Next

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Function GenKey(ByVal Table As DataTable, Optional ByVal strKey As String = "Key") As DataTable
        Try
            Dim intTmp As Int16 = 0

            If Not IsNothing(Table) And Table.Rows.Count > 0 Then
                For i As Integer = 0 To Table.Columns.Count - 1
                    If Table.Columns(i).ToString = strKey Then
                        intTmp += 1
                        Exit For
                    End If
                Next

                If intTmp = 0 Then
                    Table.Columns.Add(strKey, GetType(Integer))
                End If

                For iRow As Integer = 0 To Table.Rows.Count - 1
                    Table.Rows(iRow)(strKey) = iRow + 1
                Next
            End If

            Return Table
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Shared Function FormatNumber(ByVal strNumber As String, Optional ByVal intPoint As Integer = 0, Optional ByVal strNoComma As String = "F") As String
        Dim strTmp1 As String = ""
        Dim strTmp2 As String = ""
        Dim strTmp3 As String = ""
        Dim strPoint As String = ""
        Dim intPoint1 As String = 0
        Try
            If strNoComma = "F" Then
                If InStr(strNumber, ",") > 0 Then
                    strNumber = strNumber.Replace(",", "")
                End If

                If InStr(strNumber, ".") > 0 Then
                    strPoint = Right(strNumber, strNumber.Length - (InStr(strNumber, ".")))
                    strNumber = Left(strNumber, InStr(strNumber, ".") - 1)
                End If

                For iPoint As Integer = 0 To strNumber.Trim.Length - 1
                    strTmp1 = Right(strNumber, iPoint + 1)
                    strTmp2 = Left(strTmp1, 1)
                    strTmp3 = strTmp2 & strTmp3
                    If strTmp1.Length Mod 3 = 0 Then
                        strTmp3 = "," & strTmp3
                    End If
                Next

                If Left(strTmp3, 1) = "," Then
                    strTmp3 = Right(strTmp3, strTmp3.Length - 1)
                End If

                If intPoint <> 0 Then
                    strTmp3 &= "."
                End If

                If strPoint.Trim <> "" Then
                    If strPoint.Length = intPoint Then
                        intPoint1 = 0
                        strTmp3 &= strPoint
                    ElseIf strPoint.Length > intPoint Then
                        intPoint1 = 0
                        strTmp3 &= Left(strPoint, intPoint)
                    ElseIf strPoint.Length < intPoint Then
                        intPoint1 = intPoint - strPoint.Length
                        strTmp3 &= strPoint
                    End If
                Else
                    intPoint1 = intPoint
                End If

                If intPoint1 <> 0 Then
                    For jPoint As Integer = 0 To intPoint1 - 1
                        strTmp3 &= "0"
                    Next
                End If
            Else
                strTmp3 = strNumber.Replace(",", "")
            End If

            Return strTmp3
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    'Public Overloads Shared Function ConvertDate(ByVal strDate As String, ByVal TypeDate As TypeDate, Optional ByVal strSybol As String = "/")
    '    Dim strTmpDate As String
    '    Dim strDay As String = Right("0" & Day(strDate), 2)
    '    Dim strMonth As String = Right("0" & Month(strDate), 2)
    '    Dim strYear As String = Year(strDate)
    '    Try
    '        'If strDate.Length = 8 Or strDate.Length = 10 Then
    '        If TypeDate = TypeDate.ddmmyyyy And strSybol = "" Then
    '            strTmpDate = strDay & strMonth & strYear
    '        ElseIf TypeDate = TypeDate.ddmmyyyy And strSybol <> "" Then
    '            strTmpDate = strDay & strSybol & strMonth & strSybol & strYear
    '        ElseIf TypeDate = TypeDate.mmddyyyy And strSybol = "" Then
    '            strTmpDate = strMonth & strDay & strYear
    '        ElseIf TypeDate = TypeDate.mmddyyyy And strSybol <> "" Then
    '            strTmpDate = strMonth & strSybol & strDay & strSybol & strYear
    '        ElseIf TypeDate = TypeDate.yyyymmdd And strSybol = "" Then
    '            strTmpDate = strYear & strMonth & strDay
    '        ElseIf TypeDate = TypeDate.yyyymmdd And strSybol <> "" Then
    '            strTmpDate = strYear & strSybol & strMonth & strSybol & strDay
    '        End If
    '        Return strTmpDate
    '    Catch ex As Exception
    '        Throw New Exception(ex.ToString)
    '    End Try
    'End Function
    Public Shared Function GetValue(ByVal Control As Object) As String
        Try
            If Control.GetType Is GetType(TextBox) Or _
                Control.GetType Is GetType(CheckBox) Or _
                Control.GetType Is GetType(RadioButton) Then
                If Control.text = "&nbsp;" Then
                    Return ""
                End If
                Return Control.text
            ElseIf Control.GetType Is GetType(DropDownList) Then
                If Control.SelectedValue = "&nbsp;" Then
                    Return ""
                End If
                Return Control.SelectedValue
            ElseIf Control.GetType Is GetType(HtmlInputText) Or _
                    Control.GetType Is GetType(HtmlInputFile) Or _
                    Control.GetType Is GetType(HtmlInputHidden) Or _
                    Control.GetType Is GetType(HtmlTextArea) Or _
                    Control.GetType Is GetType(HtmlInputCheckBox) Or _
                    Control.GetType Is GetType(HtmlInputRadioButton) Then
                If Control.Value = "&nbsp;" Then
                    Return ""
                End If
                Return Control.value
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Shared Sub GenScript(ByVal page As Page, ByVal strJavaScript As String)
        Try
            page.RegisterStartupScript(strJavaScript, "<script language='javascript'> " & strJavaScript & " </script>")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub GenHtml(ByVal page As Page, ByVal strJavaScript As String)
        Try
            page.RegisterStartupScript(strJavaScript, strJavaScript)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub SetPositionScoll(ByVal page As Page)
        Try
            Dim Str As String
            Str = "function GetPositionScoll(){" & vbCrLf
            Str &= "	document.getElementById('htxtGetPosition').value = document.body.scrollLeft + '|' + document.body.scrollTop;" & vbCrLf
            Str &= "}" & vbCrLf
            Str &= "function SetPositionScoll(){" & vbCrLf
            Str &= "	obj = document.getElementById('htxtGetPosition');" & vbCrLf
            Str &= "	var arrTmp = obj.value.split('|');" & vbCrLf
            Str &= "	if(arrTmp.length > 0){" & vbCrLf
            Str &= "		window.scrollTo(arrTmp[0],arrTmp[1]);" & vbCrLf
            Str &= "	}" & vbCrLf
            Str &= "}" & vbCrLf
            Str &= "window.onload = SetPositionScoll;" & vbCrLf
            Str &= "window.onscroll = GetPositionScoll;" & vbCrLf
            GenScript(page, Str)
            '************************************************************
            ' ??????????? hidden text realtime ???????? code beheigh ??????????
            ' ???????? : ???????????? control ??????????????????????????????????????????????
            '          ???? page ????
            '************************************************************
            If Not page.Request.Form("htxtGetPosition") Is Nothing Then '????????? hidden text ??????
                page.RegisterHiddenField("htxtGetPosition", page.Request.Form("htxtGetPosition"))
            Else
                page.RegisterHiddenField("htxtGetPosition", String.Empty)
            End If
            '************************************************************
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub OpenDialog(ByVal page As Page, ByVal ParamArray ctlReturnValue() As Object)
        Dim strTmp As String
        Try
            strTmp = " function OpenDialog(strURL,Width,Height){ " & vbCrLf
            strTmp &= " 			var strTmp, oTmp = new Object(); " & vbCrLf
            strTmp &= " 			var arrTmp; " & vbCrLf
            strTmp &= " 	    	oTmp.URL=strURL; " & vbCrLf
            strTmp &= " 			strTmp = window.showModalDialog('frmIframe.html',oTmp,'status:no;dialogWidth:' + Width + 'px;dialogHeight:' + Height + 'px;dialogHide:true;help:no;scroll:auto;center:true;'); " & vbCrLf

            If ctlReturnValue.Length > 0 Then
                strTmp &= " 			if (strTmp != null){ " & vbCrLf
                If ctlReturnValue.Length = 1 And (ctlReturnValue(0).GetType Is GetType(TextBox) Or _
                   ctlReturnValue(0).GetType Is GetType(HtmlInputHidden) Or _
                   ctlReturnValue(0).GetType Is GetType(HtmlInputText)) Then
                    strTmp &= " 				document.getElementById('" & ctlReturnValue(0).id.ToString & "').value = strTmp; " & vbCrLf
                Else
                    strTmp &= " 				arrTmp = strTmp.split('|'); " & vbCrLf
                    For i As Int16 = 0 To ctlReturnValue.Length - 1
                        If ctlReturnValue(i).GetType Is GetType(TextBox) Or _
                           ctlReturnValue(i).GetType Is GetType(HtmlInputHidden) Or _
                           ctlReturnValue(i).GetType Is GetType(HtmlInputText) Then
                            strTmp &= " 				document.getElementById('" & ctlReturnValue(i).id.ToString & "').value "
                        ElseIf ctlReturnValue(i).GetType Is GetType(Label) Or _
                               ctlReturnValue(i).GetType Is GetType(HtmlGenericControl) Then
                            strTmp &= " 				document.getElementById('" & ctlReturnValue(i).id.ToString & "').innerText "
                        End If
                        strTmp &= " = arrTmp[" & i & "]; " & vbCrLf
                    Next
                End If
            End If

            If ctlReturnValue.Length > 0 Then
                strTmp &= " 				} " & vbCrLf
            End If

            strTmp &= " 			return false;  " & vbCrLf
            strTmp &= " 		}  " & vbCrLf
            GenScript(page, strTmp)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub OpenWindow(ByVal page As Page, ByVal strUrl As String)
        Try
            GenScript(page, "window.open('" & strUrl & "')")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub ClearControl(ByVal ParamArray Control() As Object)
        Try
            If Control.Length > 0 Then
                For i As Int16 = 0 To Control.Length - 1
                    If Control(i).GetType Is GetType(TextBox) Then
                        CType(Control(i), TextBox).Text = ""
                    ElseIf Control(i).GetType Is GetType(DropDownList) Then
                        CType(Control(i), DropDownList).SelectedIndex = 0
                    ElseIf Control(i).GetType Is GetType(HtmlInputText) Or _
                            Control(i).GetType Is GetType(HtmlInputHidden) Then
                        Control(i).Value = ""
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub SetControl(ByVal Control As Object, ByVal strValue As String)
        Try
            If Control.GetType Is GetType(TextBox) Then
                CType(Control, TextBox).Text = strValue
            ElseIf Control.GetType Is GetType(DropDownList) Then
                CType(Control, DropDownList).SelectedValue = strValue
            ElseIf Control.GetType Is GetType(HtmlInputText) Or _
                    Control.GetType Is GetType(HtmlInputHidden) Then
                Control.Value = strValue
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Shared Sub MoveList(ByVal strAll As String, ByVal objForm As ListBox, ByVal objTo As ListBox)
        If strAll = "All" Then
            For i As Integer = 0 To objForm.Items.Count - 1
                objTo.Items.Add(objForm.Items(0).Text)
                objForm.Items.RemoveAt(0)
            Next
        Else
            If objForm.SelectedIndex > -1 Then
                objTo.Items.Add(objForm.Items(objForm.SelectedIndex).Text)
                objForm.Items.RemoveAt(objForm.SelectedIndex)
            End If
        End If
        objForm.SelectedIndex = 0
        objTo.SelectedIndex = 0
    End Sub
#End Region

#Region " dateFunction "

    Public Overloads Shared Function ConvertDate(ByVal itmDate As String)
        Try
            Dim RealDate As String
            If itmDate.Chars(2) <> "/" Then
                Dim dd As String = Left(itmDate, 2)
                Dim MM As String = Mid(itmDate, 3, 2)
                Dim yy As String = Right(itmDate, 4)
                If CInt(yy) < 2519 Then '-543
                    yy = yy + 543
                End If
                RealDate = dd & "/" & MM & "/" & yy
            End If

            If itmDate.Chars(2) = "/" Then
                Dim arr() As String = itmDate.Split("/")
                RealDate = arr(2) & arr(1) & arr(0)
            End If

            Return RealDate

        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Overloads Shared Function ConvertDate(ByVal strDate As String, ByVal TypeIN As TypeDate, Optional ByVal TypeOUT As TypeDate = TypeDate.yyyymmdd, Optional ByVal Year As TypeYear = TypeYear.Eng, Optional ByVal Seperate As Seperate = Seperate.No) As String
        Try
            Dim RealDate As String
            Dim dd As String
            Dim mm As String
            Dim yy As String
            If strDate.Trim = "" Then Return strDate
            If strDate.Chars(2) <> "/" Then
                Select Case TypeIN
                    Case TypeDate.ddmmyyyy
                        dd = Left(strDate, 2)
                        mm = Mid(strDate, 3, 2)
                        yy = Right(strDate, 4)
                    Case TypeDate.mmddyyyy
                        dd = Mid(strDate, 3, 2)
                        mm = Left(strDate, 2)
                        yy = Right(strDate, 4)
                    Case TypeDate.yyyymmdd
                        dd = Right(strDate, 2)
                        mm = Mid(strDate, 5, 2)
                        yy = Left(strDate, 4)
                End Select
            Else
                Dim arr() As String = strDate.Split("/")
                Select Case TypeIN
                    Case TypeDate.ddmmyyyy
                        dd = arr(0)
                        mm = arr(1)
                        yy = arr(2)
                    Case TypeDate.mmddyyyy
                        dd = arr(1)
                        mm = arr(0)
                        yy = arr(2)
                    Case TypeDate.yyyymmdd
                        dd = arr(2)
                        mm = arr(1)
                        yy = arr(0)
                End Select
            End If
            '--------- Year -------------------
            If Year = TypeYear.Eng Then
                If CInt(yy) > 2519 Then
                    yy = yy - 543
                End If
            ElseIf Year = TypeYear.Thai Then
                If CInt(yy) < 2519 Then '-543
                    yy = yy + 543
                End If
            End If
            '-----------------------------------
            If Seperate = Seperate.Yes Then
                Select Case TypeOUT
                    Case TypeDate.ddmmyyyy
                        RealDate = dd & "/" & mm & "/" & yy
                    Case TypeDate.mmddyyyy
                        RealDate = mm & "/" & dd & "/" & yy
                    Case TypeDate.yyyymmdd
                        RealDate = yy & "/" & mm & "/" & dd
                End Select
            ElseIf Seperate = Seperate.No Then
                Select Case TypeOUT
                    Case TypeDate.ddmmyyyy
                        RealDate = dd & mm & yy
                    Case TypeDate.mmddyyyy
                        RealDate = mm & dd & yy
                    Case TypeDate.yyyymmdd
                        RealDate = yy & mm & dd
                End Select
            End If
            Return RealDate
        Catch ex As Exception
            Return strDate
        End Try
    End Function
    Public Shared Function ConvertYear(ByVal itmDate As String)
        Try
            Dim yy As String = Left(itmDate, 4)
            If CInt(yy) < 2519 Then '-543
                yy = yy + 543
            End If
            Dim dd As String = Right(itmDate, 2)
            Dim MM As String = Mid(itmDate, 5, 2)
            Return yy & MM & dd
        Catch ex As Exception
            Return itmDate
        End Try
    End Function
    Public Shared Function ChkHoliday(ByVal itmDate As String) As Boolean
        Dim holiday(2) As String
        holiday(0) = "25491205"
        holiday(1) = "25491210"

        For i As Integer = 0 To holiday.Length - 1
            If ConvertDate(itmDate) = holiday(i) Then
                Return False
            End If
        Next
        Return True
    End Function



#End Region

#Region "DataBase"
    Public Shared Function connection() As SqlConnection
        Try
            cn = New SqlConnection(SQLConnect.getConnectionString())
            If cn.State = ConnectionState.Closed Then cn.Open()
            Return cn
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Shared Function getDataSet(ByVal strQuery As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim cmd As New SqlCommand
            Dim apd As SqlDataAdapter

            With cmd
                .Connection = connection()
                .CommandType = CommandType.Text
                .CommandText = strQuery
            End With

            apd = New SqlDataAdapter(cmd)
            apd.Fill(ds)
            cmd.Dispose()
            apd.Dispose()

            Return ds
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            cn.Close()
        End Try
    End Function
#End Region
End Class

