procedure TForm1.Button1Click(Sender: TObject);
var
    strm : TStringStream;
begin
    IdHTTP1.Request.Clear;
    IdHTTP1.Request.CustomHeaders.Clear;
    IdHTTP1.Request.Accept := 'text/html, */*';
    IdHTTP1.Request.CustomHeaders.Add('Authorization: Token token="YOUR_TOKEN"');
    strm := TStringStream.Create('');
    try
        IdHTTP1.Get('http://www.cepaberto.com/api/v2/ceps.json?cep=17290000', strm);
        Memo1.Text := Utf8ToAnsi( strm.DataString );
    finally
        strm.Free;
    end;
end;