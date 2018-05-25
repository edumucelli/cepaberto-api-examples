uses
  Data.DBXJSON;
  
procedure TForm1.Button1Click(Sender:TObject);
var
  strm : TStringStream;
  strngRet : TStrings;
  IdHTTP1 : TIdHTTP;
  objCep : TJSONObject;
  CEP : String;
  FToString:STring;
begin
  IdHTTP1 := TIdHTTP.Create;
  IdHTTP1.Request.Clear;
  IdHTTP1.Request.CustomHeaders.Clear;
  IdHTTP1.Request.Accept := 'text/html, */*';
  IdHTTP1.Request.CustomHeaders.Add('Authorization: Token token="SEU_TOKEN"');
  strm := TStringStream.Create('');
  strngRet := TStringList.Create;
  //Não esqueça de limpar os "." e os "-"
  CEP := Edit1.Text;
  
  try
    IdHTTP1.Get('http://www.cepaberto.com/api/v3/cep?cep='+  CEP, strm);
    FToString := Utf8ToAnsi(strm.DataString);
    if FToString = '{}' then
    begin
      //Não encontrou nada
      exit;
    end;

    objCep := TJSONObject.ParseJSONValue( FToString ) as TJSONObject;

    {
      Trate os campos como preferir
    sCidade := objCep.GetValue('cidade').Value;
    sCep := CEP;
    sUF := objCep.GetValue('estado').Value;
    sBairro := objCep.GetValue('bairro').Value;
    sEndereco := objCep.GetValue('logradouro').Value;
    }

  finally
    strm.Free;
    strngRet.Free;
    IdHTTP1.Free;
  end;
end;
