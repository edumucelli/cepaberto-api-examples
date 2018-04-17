import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

import org.json.JSONObject;


public class Api {
	public String sendGet(String url) {
		try {
			StringBuffer buffer = new StringBuffer();
			String inputLine;
			URL obj = new URL(url);
			HttpURLConnection conn = (HttpURLConnection)obj.openConnection();
			conn.setRequestMethod("GET");
			conn.setRequestProperty("User-Agent", "CepAberto");
			conn.setRequestProperty("Accept", "application/json");
			conn.setRequestProperty("Authorization", "Token token=<SEU-TOKEN>");
			int responseCode = conn.getResponseCode();
			System.out.println("Response Code: " + responseCode);
			BufferedReader in = new BufferedReader(
				new InputStreamReader(conn.getInputStream()));

			while ((inputLine = in.readLine()) != null) {
				buffer.append(inputLine);
			}

			in.close();
			return buffer.toString();
		} catch (MalformedURLException e) {
			throw new RuntimeException(e);
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
	
	public static void main(String[] args) {
		String json = new Api().sendGet("http://www.cepaberto.com/api/v3/cep?cep=<CEP>");
		JSONObject obj = new JSONObject(json);
		System.out.println("Altitude: "+obj.getDouble("altitude"));
		System.out.println("Cep: "+obj.getString("cep"));
		System.out.println("Latitude: "+obj.getString("latitude"));
		System.out.println("Longitude: "+obj.getString("longitude"));
		System.out.println("Logradouro: "+obj.getString("logradouro"));
		System.out.println("Bairro: "+obj.getString("bairro"));
		JSONObject obj2 = obj.getJSONObject("cidade");
		System.out.println("Cidade DDD: "+obj2.getInt("ddd"));
		System.out.println("Cidade IBGE: "+obj2.getString("ibge"));
		System.out.println("Nome da Cidade: "+obj2.getString("nome"));
		JSONObject obj3 = obj.getJSONObject("estado");
		System.out.println("Sigla Estado: "+obj3.getString("sigla"));
	}
}
