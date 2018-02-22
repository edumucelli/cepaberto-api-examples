from urllib2 import urlopen, Request
from urllib import urlencode

TOKEN = "YOUR_TOKEN"

headers = {'Authorization': 'Token token=%s' % TOKEN}

# Pesquisa de CEP pelo número
url = "http://www.cepaberto.com/api/v2/ceps.json?cep=40010000"
json = urlopen(Request(url, None, headers=headers)).read()
print json

# Pesquisa de CEP por estado, cidade e logradouro
params = {'estado': 'SP', 'cidade': 'São Paulo', 'logradouro': 'Praça da Sé'}
url = "http://www.cepaberto.com/api/v2/ceps.json?%s" % (urlencode(params))
json = urlopen(Request(url, None, headers=headers)).read()
print json

# Pesquisa pelos nomes das cidades em SP
url = "http://www.cepaberto.com/api/v2/cities.json?estado=SP"
json = urlopen(Request(url, None, headers=headers)).read()
print json
