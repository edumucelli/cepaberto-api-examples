
$crl = curl_init("http://www.cepaberto.com/api/v3/address?estado=SP&cidade=Ubatuba");

// Token here
$token = '';

// Set header request
curl_setopt($crl, CURLOPT_HTTPHEADER, ['Authorization: Token token='.$token]);

// Get data in api
$rest = curl_exec($crl);

// Close connection
curl_close($crl);

print_r($rest);
