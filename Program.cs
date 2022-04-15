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
        public string BaseUrlAndressOfApi = "https://economia.awesomeapi.com.br/last/";
        public CurrencyApiConection(string PathUrl)
        {
            this.BaseUrlAndressOfApi = this.BaseUrlAndressOfApi + PathUrl;
        }
       

    }
   
    public class Currency
    {
        public  string PathUrlRequired="USD-BRL";
        static void Main(string[] args)
        {
            Currency CurrencyInstance= new Currency();
            CurrencyApiConection CurrencyApiInstance= new CurrencyApiConection(CurrencyInstance.PathUrlRequired);

            
            
            
        }
    }
}