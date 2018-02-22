using System;
using System.Net;
using System.Web.Script.Serialization;

namespace PetHelper.AddressProvider.Providers {
    public static class CEPAberto {
        public static CEPAbertoResponse GetCepInfo(string cep) {
            try {
                var accessToken = "Token token=YOUR_TOKEN";
                var url = "http://www.cepaberto.com/api/v2/ceps.json?cep={0}";
                var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                client.Headers.Add(HttpRequestHeader.Authorization, accessToken);
                var result = client.DownloadString(string.Format(url, cep));
                var jss = new JavaScriptSerializer();
                return jss.Deserialize(result);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
    public class CEPAbertoResponse {
        public int? altitude { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string cidade { get; set; }
        public short? ddd { get; set; }
        public int? ibge { get; set; }
        public string estado { get; set; }
        public string estado_sigla { get; set; }
        public string status { get; set; }
    }
}