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
            Console.Write("Digite a opção escolhida ");
            
        }
        public static  void CurrencMenuStart()
        {
            CurrencyMenuShow Menu = new CurrencyMenuShow();
            string message = "Bem vindo ao nosso conversor de moedas e a cotação atual de diversas moedas no mundo";
            Console.WriteLine(message);
            Menu.ShowOptions();

            
        }
    }

    public class  CurrencyApiConection
    {
        HttpClient client = new HttpClient();
        public string BaseUrlAndressOfApi = "https://economia.awesomeapi.com.br/";
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
        public string PathUrlRequired="All";
        public string? ResponseApiResult;
        
        
       
        

        public static void Main(string[] args)
        {
            Currency CurrencyInstance= new Currency();
            CurrencyMenuShow Menu = new CurrencyMenuShow();
            CurrencyMenuShow.CurrencMenuStart();
            
           
            CurrencyApiConection CurrencyConectionApiInstance= new CurrencyApiConection(CurrencyInstance.PathUrlRequired);
           
            CurrencyInstance.ResponseApiResult = CurrencyConectionApiInstance.GetApiResponse();
            Console.WriteLine(CurrencyInstance.ResponseApiResult);
            CurrencyInstance.CreateJsonDocumentFromApiResult();
            
            
        } 
        public JsonElement CreateJsonDocumentFromApiResult()
        {
            JsonDocument DocumentJson;
            try
            {
                DocumentJson= JsonDocument.Parse(this.ResponseApiResult);
            }
            catch (JsonException error)
            {
                Console.WriteLine("Não é possível converter porque o argumento passado não é no formato json");
                throw error;
            }
            

            JsonElement JsonRepresentation = DocumentJson.RootElement;

            return JsonRepresentation;
        }

    }
}