import requests

token = "YOUR_TOKEN"
headers = {'Authorization': 'Token token=%s' % token}


def search_by_cep():
    url = "https://www.cepaberto.com/api/v3/cep?cep=40010000"
    response = requests.get(url, headers=headers)
    return response.json()


def search_by_address():
    url = "https://www.cepaberto.com/api/v3/address?estado=SP&cidade=São Paulo&logradouro=Praça da Sé"
    response = requests.get(url, headers=headers)
    return response.json()


def search_cities():
    url = "https://www.cepaberto.com/api/v3/cities?estado=AM"
    response = requests.get(url, headers=headers)
    return response.json()


def update_ceps():
    url = "https://www.cepaberto.com/api/v3/update"
    ceps_to_update = ['32371380', '32210180']
    data = {'ceps': ",".join(ceps_to_update)}
    response = requests.post(url, data=data, headers=headers)
    return response.json()


if __name__ == '__main__':
    print(search_by_cep())
    print(search_by_address())
    print(search_cities())
