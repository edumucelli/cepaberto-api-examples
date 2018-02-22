require "net/http"
require "uri"

TOKEN = "YOUR_TOKEN"

domain = "http://www.cepaberto.com/api/v2/ceps.json"
uri = URI(domain)
params = {'cep' => '40010000'}
headers = { "Authorization" => "Token token=#{TOKEN}" }

http = Net::HTTP.new(uri.host, uri.port)
request = Net::HTTP::Get.new(uri.path)
request.set_form_data(params)
request = Net::HTTP::Get.new(uri.path + '?' + request.body, headers)
response = http.request(request)
puts response