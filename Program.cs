using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenuShow {
        public void ShowOptions()
        {
            string message = "Suas opções são\n[1]-Mostra a contação de todas as moedas\n2-Escolher detalhes de uma moeda especifíca ";
            Console.WriteLine(message);
        }
        public static  void CurrencMenuStart()
        {
            string message = "Bem vindo ao nosso conversor de moedas e a cotação atual de diversas moedas no mundo"
            Console.WriteLine(message);
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