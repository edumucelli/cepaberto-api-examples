

/*
�����������������������������������������������������������������������������
�����������������������������������������������������������������������������
�������������������������������������������������������������������������ͻ��
���Programa  �CEP      �Autor �Thiago Ramos Cavalcanti� Data �  30/05/2018���
�������������������������������������������������������������������������͹��
���Desc.     � Fun��o utilizada para consultar API de CEPs                ���
�������������������������������������������������������������������������͹��
���Uso       � Gen�rico                                                   ���
�������������������������������������������������������������������������ͼ��
�����������������������������������������������������������������������������
�����������������������������������������������������������������������������
*/

User Function xfnCEP()

Local cUrl := "http://www.cepaberto.com/api/v3/cep?cep=40010000"
Local cGetParams := ""
Local nTimeOut := 200
Local aHeadOut := {"Authorization: Token [informe aqui seu token]", "Content-Type: application/json"}
Local cheaderGet := ""
Local cRetorno := ""  
Local oObjJson := Nil 
Local nRet := .T.    

cRetorno := HttpGet(cUrl, cGetParams, nTimeOut, aHeadOut, @cheaderGet )  

If !FWJsonDeserialize(cRetorno,@oObjJson) 
	MsgInfo("CEP Invalido, Preencha o CEP correto.","Aten��o!")  
	nRet := .F.
Else 
	MsgInfo(oObjJson:logradouro)
Endif
	
Return(nRet)  