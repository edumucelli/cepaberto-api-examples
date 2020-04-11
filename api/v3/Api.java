package cepaberto;

import com.google.gson.Gson;
import lombok.NonNull;
import lombok.Value;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

import java.io.IOException;
import java.util.Optional;

public class Api {

    private final OkHttpClient httpClient = new OkHttpClient();
    private final Gson gson = new Gson();

    private Optional<Cep> searchByCep(@NonNull String cep) {
        return cepFromRequest(new Request.Builder()
                .url(String.format("http://www.cepaberto.com/api/v3/cep?cep=%s", cep))
                .addHeader("Authorization", "Token token=YOUR_TOKEN")
                .build()
        );
    }

    private Optional<Cep> searchByAddress(
            @NonNull String estado,
            @NonNull String cidade,
            String logradouro
    ) {
        return cepFromRequest(
                new Request.Builder()
                        .url(String.format("http://www.cepaberto.com/api/v3/address?estado=%s&cidade=%s&logradouro=%s", estado, cidade, logradouro))
                        .addHeader("Authorization", "Token token=YOUR_TOKEN")
                        .build()
        );
    }

    private Optional<Cep> searchCities(@NonNull String estado) {
        return cepFromRequest(new Request.Builder()
                .url(String.format("http://www.cepaberto.com/api/v3/cities?estado=%s", estado))
                .addHeader("Authorization", "Token token=YOUR_TOKEN")
                .build()
        );
    }

    private Optional<Cep> cepFromRequest(Request request) {
        try (Response response = httpClient.newCall(request).execute()) {
            if (response.isSuccessful()) {
                return Optional.of(gson.fromJson(response.body().string(), Cep.class));
            }
            return Optional.empty();
        } catch (IOException e) {
            return Optional.empty();
        }
    }

    @Value
    private static class Cep {
        private final String cep;
        private final double altitude;
        private final double latitude;
        private final double longitude;
        private final String bairro;
        private final String logradouro;
        private final Cidade cidade;
        private final Estado estado;
        
        @Value
        private static class Cidade {
            private final String nome;
            private final Integer ddd;
            private final String ibge;
        }

        @Value
        private static class Estado {
            private final String sigla;
        }
    }

    public static void main(String[] args) throws IOException {
        Api api = new Api();
        System.out.println(api.searchByCep("40010000"));
        System.out.println(api.searchByAddress("SP", "São Paulo", "Praça da Sé"));
        System.out.println(api.searchCities("AM"));
    }
}