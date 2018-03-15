static void TestV3()
{
    // required data
    var cepTest = "70150900";
    var token = "YOUR_TOKEN";
    var param = new Tuple<String, String>("cep", cepTest);

    // load parameters to array
    var parameters = new Tuple<String, String>[] { param };

    // instantiate the request
    var newRequest = new CEPAbertoV3.ByCEP(token, parameters);
    var result = newRequest.Make();
    // test output
    Console.WriteLine("Logradouro --> {0}", result.logradouro); Console.ReadKey();
}