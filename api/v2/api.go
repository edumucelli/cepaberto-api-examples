package main

import (
    "encoding/json"
    "fmt"
    "log"
    "net/http"
    "net/url"
)

type CepResponse struct {
    CEP        string  `json:"cep"`
    Logradouro string  `json:"logradouro"`
    Bairro     string  `json:"bairro"`
    Cidade     string  `json:"cidade"`
    Estado     string  `json:"estado"`
    Latitude   string  `json:"latitude"`
    Longitude  string  `json:"longitude"`
    Altitude   float64 `json:"altitude"`
    DDD        int     `json:"ddd"`
    IBGE       string  `json:"ibge"`
}

func main() {
    cep := "74333240"
    cepSeguro := url.QueryEscape(cep)

    url := fmt.Sprintf("http://www.cepaberto.com/api/v2/ceps.json?cep=%s", cepSeguro)

    req, err := http.NewRequest("GET", url, nil)

    req.Header.Set("Authorization", `Token token="YOUR_TOKEN"`)
    if err != nil {
        log.Fatal("NewRequest: ", err)
        return
    }
    client := &http.Client{}

    resp, err := client.Do(req)
    if err != nil {
        log.Fatal("Do: ", err)
        return
    }

    defer resp.Body.Close()
    var resultado CepResponse

    if err := json.NewDecoder(resp.Body).Decode(&resultado); err != nil {
        log.Println(err)
    }

    fmt.Printf("Logradouro localizado: [%s]\n", resultado.Logradouro)

    fmt.Printf("%+v\n", resultado)

}