package main

import (
	"encoding/json"
	"errors"
	"fmt"
	"io/ioutil"
	"net/http"
)

type API struct {
	Token  string
	Domain string
}

type Cidade struct {
	Ddd  int64  `json:"ddd"`
	Ibge string `json:"ibge"`
	Nome string `json:"nome"`
}
type Estado struct {
	Sigla string `json:"sigla"`
}
type ResponseJSON struct {
	Altitude   float64 `json:"altitude"`
	Bairro     string  `json:"bairro"`
	Cep        string  `json:"cep"`
	Cidade     *Cidade `json:"cidade"`
	Estado     *Estado `json:"estado"`
	Latitude   string  `json:"latitude"`
	Logradouro string  `json:"logradouro"`
	Longitude  string  `json:"longitude"`
}

func (api *API) GetInfoByCep(cep string) (*ResponseJSON, error) {
	var responseJSON ResponseJSON
	url := fmt.Sprintf("%s/cep?cep=%s", api.Domain, cep)

	req, errorRequest := http.NewRequest("GET", url, nil)
	if errorRequest != nil {
		return nil, errors.New("build request error")
	}

	req.Header.Add("Authorization", fmt.Sprintf("Token token=%s", api.Token))

	res, errorSendRequest := http.DefaultClient.Do(req)
	if errorSendRequest != nil {
		return nil, errors.New("error send request")
	}
	defer res.Body.Close()

	body, errReadBuffResponse := ioutil.ReadAll(res.Body)
	if errReadBuffResponse != nil {
		return nil, errors.New("error read buff response")
	}

	errParseJSON := json.Unmarshal(body, &responseJSON)

	if errParseJSON != nil {
		return nil, errors.New("error parse json")
	}

	return &responseJSON, nil
}

func main() {
	api := API{
		Domain: "https://www.cepaberto.com/api/v3",
		Token:  "PUT YOUR TOKEN",
	}
	data, err := api.GetInfoByCep("PUT YOUR CEP")
	if err != nil {
		panic(err)
	}

	fmt.Println(data.Cep, data.Bairro, data.Cidade.Nome)
}
