import requests

from pprint import pprint

url = 'http://www.cepaberto.com/api/v3/cep?cep={}'
token = '<TOKEN>'

headers = {
    'Authorization': 'Token token={}'.format(token),
    'Accept': 'application/json',
    'User-Agent': 'CepAberto'
}

class Api:

    def get(self, url):
        r = requests.get(url, headers=headers)
        return r

    def cep(self, nu_cep):
        r = self.get(url.format(nu_cep))
        if r.status_code == 200:
            pprint(r.json())


if __name__ == "__main__":
    api = Api()
    api.cep(40010000)
