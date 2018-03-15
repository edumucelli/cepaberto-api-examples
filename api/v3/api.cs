using System;
using System.Linq;
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

        public abstract class CEPAbertoRequest
        {
            private String BaseUrl = "http://www.cepaberto.com/api/v3/";
            public abstract String Resource { get; }
            public abstract String[] AvaiableParameters { get; }

            private String _token;
            public String Token
            {
                get { return this._token; }
                set
                {
                    this._token = "Token token=" + value;
                }
            }

            private Tuple<String, String>[] _parameters;
            public Tuple<String, String>[] Parameters
            {
                get
                {
                    return this._parameters;
                }
                set
                {
                    var isAvaiable = false;
                    foreach(var v in value)
                    {
                        isAvaiable = AvaiableParameters.Contains(v.Item1);
                        if(!isAvaiable)
                        {
                            throw new Exception("Parameter not avaiable for this resource.");
                        }
                    }

                    this._parameters = value;
                }
            }

            public String DownloadString
            {
                get
                {
                    var url = BaseUrl + Resource + "?";
                    for(int x = 0; x < Parameters.Length; x++)
                    {
                        url += Parameters[x].Item1 + "=" + Parameters[x].Item2;
                        if (x < Parameters.Length - 1)
                            url += "&";
                    }
                    return url;
                }
            }

            public CEPAbertoResponse Make(CEPAbertoRequest request)
            {
                if (String.IsNullOrEmpty(request.Token))
                {
                    throw new Exception("Token can't be null or empty.");
                }

                var client = new WebClient { Encoding = Encoding.UTF8 };
                client.Headers.Add(HttpRequestHeader.Authorization, request.Token);
                var requestResult = client.DownloadString(request.DownloadString);

                var jss = new JavaScriptSerializer();
                var response = jss.Deserialize<CEPAbertoResponse>(requestResult);

                return response;
            }
            public CEPAbertoResponse Make()
            {
                return Make(this);
            }

            public CEPAbertoRequest(String token, Tuple<String,String>[] parameters)
            {
                Token = token;
                Parameters = parameters;
            }
        }

        public class ByCEP : CEPAbertoRequest
        {
            // call base constructor
            public ByCEP(String token, Tuple<String, String>[] parameters) : base(token, parameters) { }
            public override String Resource
            {
                get { return "cep"; }
            }
            public override String[] AvaiableParameters
            {
                get { return new String[] { "cep" }; }
            }
        }

        public class ByNearest : CEPAbertoRequest
        {
            public ByNearest(String token, Tuple<String,String>[] parameters) : base(token, parameters) { }
            public override String Resource
            {
                get { return "nearest"; }
            }
            public override String[] AvaiableParameters
            {
                get { return new String[] { "lat", "lng" }; }
            }
        }
    }
}