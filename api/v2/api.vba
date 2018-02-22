Option Compare Database
Option Explicit
Dim LastTime As Date
Function GET_CEPABERTO(CEP As String) As String
    Const Token As String = "Token token=YOUR_TOKEN"
    Dim xml As MSXML2.XMLHTTP60
    Dim BASE_URL As String
    BASE_URL = "http://www.cepaberto.com/api/v2/ceps.xml?cep=" & CEP
    Set xml = CreateObject("MSXML2.XMLHTTP.6.0")      
    With xml
        .Open "GET", BASE_URL, False
        .setRequestHeader "Authorization", Token
        Do
            DoEvents
        Loop Until DateDiff("s", LastTime, Now()) > 3
        .send
        LastTime = Now()
    End With
    GET_CEPABERTO = xml.responseText
End Function