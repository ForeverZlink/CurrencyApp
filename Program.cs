using System;

namespace CurrencyApp
{
    public class CurrencyMenu {
        public static  void CurrencMenuStart()
        {

        }
    }

    class CurrencyApiConection
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
        public  string PathUrlRequired="USD-BRL";
        static void Main(string[] args)
        {
            Currency CurrencyInstance= new Currency();
            CurrencyApiConection CurrencyApiInstance= new CurrencyApiConection(CurrencyInstance.PathUrlRequired);
           
            string ResponseApiResult= CurrencyApiInstance.GetApiResponse();

            Console.WriteLine(ResponseApiResult);
            
            
        }
    }
}