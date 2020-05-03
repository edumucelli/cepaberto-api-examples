using System;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Helpers.AddressProvider.Providers
{
    class CEPAbertoV3
    {
        public class CEPAbertoResponse
        {
            public Cidade cidade { get; set; }
            public Estado estado { get; set; }
            public string bairro { get; set; }
            public string cep { get; set; }
            public string logradouro { get; set; }
            public decimal? altitude { get; set; }
            public decimal? latitude { get; set; }
            public decimal? longitude { get; set; }

            public class Cidade
            {
                public short? ddd { get; set; }
                public int? ibge { get; set; }
                public String nome { get; set; }
            }

            public class Estado
            {
                public String sigla { get; set; }
            }
        }

        public CEPAbertoResponse Make(String cep)
        {
            var token = "Token token=YOUR_TOKEN";
            var url = "https://www.cepaberto.com/api/v3/cep?cep={0}";

            var client = new WebClient { Encoding = Encoding.UTF8 };
            client.Headers.Add(HttpRequestHeader.Authorization, token);

            var requestResult = client.DownloadString(string.Format(url, cep));

            var jss = new JavaScriptSerializer();
            var response = jss.Deserialize<CEPAbertoResponse>(requestResult);

            return response;
        }

        public void Test()
        {
            var cep = "70150900";
            
            var result = new CEPAbertoV3().Make(cep);

            Console.WriteLine("Logradouro --> {0}", result.logradouro); Console.ReadKey();
        }
    }
}