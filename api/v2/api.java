 import org.json.JSONObject;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.Response;

public class CEPRepositoryImpl implements CEPRepository {
    private static String URL_API = "http://www.cepaberto.com";
    private static String TOKEN = "Token token=YOUR_TOKEN";
    private static String AUTHORIZATION_HEADER = "Authorization";
    private static String PATH = "api/v2/ceps.json";
            
    @Override
    public Endereco obter(String cep) {
        try {
            Client clienteHttp = ClientBuilder.newClient();
            WebTarget cepAberto = clienteHttp.target(URL_API).path(PATH).queryParam("cep", cep);
            Response response = cepAberto.request().header(AUTHORIZATION_HEADER, TOKEN).get();
            return fromJson(response.readEntity(String.class));
        } catch (Exception e) {
            return null;
        }
    }
    private Endereco fromJson(String response) {
        JSONObject jsonObject = new JSONObject(response);
        if (jsonObject.has("cep")) {
            String cep = jsonObject.getString("cep");
            String logradouro = jsonObject.getString("logradouro");
            String cidade = jsonObject.getString("cidade");
            return new Endereco(cep, logradouro, cidade, estado);
        }
        return null;
    }
}