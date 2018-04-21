require(pacman)
p_load(jsonlite)
p_load(magrittr)
p_load(RCurl)

# Funcao para converter uma named list em parametros URL ja com encoding correto
listToParams <- function(paramsList_) {
	paramsList <- lapply(paramsList_, utils::URLencode)
	return(paste(names(paramsList), paramsList, sep = "=", collapse = "&"))
}

TOKEN <- "YOUR_TOKEN"
header <- paste("Authorization: Token", TOKEN)

# Busca do CEP pelo numero
request <- RCurl::getURL(url = "http://www.cepaberto.com/api/v3/cep?cep=13087000",
						 httpheader = header)
jsonlite::fromJSON(request)

# Busca do CEP por estado, cidade e logradouro
paramsList <- list(estado = "SP", cidade = "São Paulo", logradouro = "Rio Tietê")
request <- RCurl::getURL(url = paste0("http://www.cepaberto.com/api/v3/address?", listToParams(paramsList)),
								 httpheader = header)
jsonlite::fromJSON(request)