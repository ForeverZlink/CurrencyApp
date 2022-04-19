using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenu {
        public static  void CurrencMenuStart()
        {

        }
    }

    public class  CurrencyApiConection
    {
        HttpClient client = new HttpClient();
        public string BaseUrlAndressOfApi = "https://economia.awesomeapi.com.br/last/";
        public CurrencyApiConection(string PathUrl)
        {
            this.BaseUrlAndressOfApi = this.BaseUrlAndressOfApi + PathUrl;
        }
        public string GetApiResponse()
        {
            string Response = client.GetStringAsync(this.BaseUrlAndressOfApi).Result;
            return Response;
        }
        
    }
    
    
    public class Currency
    {
        public string PathUrlRequired="USD-BRL";
        public string? ResponseApiResult;
        
        
       
        

        public static void Main(string[] args)
        {
            Currency CurrencyInstance= new Currency();
            CurrencyApiConection CurrencyConectionApiInstance= new CurrencyApiConection(CurrencyInstance.PathUrlRequired);
           
            CurrencyInstance.ResponseApiResult = CurrencyConectionApiInstance.GetApiResponse();
            Console.WriteLine(CurrencyInstance.ResponseApiResult);
            CurrencyInstance.CreateJsonDocumentFromApiResult();
            
            
        } 
        public JsonElement CreateJsonDocumentFromApiResult()
        {
           
            
            JsonDocument DocumentJson = JsonDocument.Parse(this.ResponseApiResult);
            
            JsonElement JsonRepresentation = DocumentJson.RootElement;

            return JsonRepresentation;
        }

    }
}