using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenuShow {
        public static void ShowOptions()
        {
            string message = "Suas opções são\n[1]-Mostra a contação de todas as moedas\n[2]-Escolher detalhes de uma moeda especifíca ";
            Console.WriteLine(message);
            Console.Write("Digite a opção escolhida ");

        }
        public static void CurrencMenuStart()
        {
            CurrencyMenuShow Menu = new CurrencyMenuShow();
            string message = "Bem vindo ao nosso conversor de moedas e a cotação atual de diversas moedas no mundo";
            Console.WriteLine(message);
            ShowOptions();
            



        }
    }
    public interface ApiConnection
    {
        public string GetApiResponse();
    }
    public class CurrencyMenuHandlerLogic{
        public int ChoiseOfUserValid;
        public string ChoiseOfUserKey;
        Dictionary<string, int> Options = new Dictionary<string, int> {
            { "All",1 },{"SpecificCoin",2 }};
        
        public string  ReadInputUser(){
            
            while (true){   
                
                string? Choise = Console.ReadLine();
                bool Response = this.CheckIfChoiseIsAOptionValidHandler(Choise);
                if (Response)
                {
                    Console.WriteLine($"O numero é {this.ChoiseOfUserValid}");
                    return "pimba";
                }
                else
                {
                    Console.WriteLine("Digite apenas um valor que esteja nas opções!");
                    CurrencyMenuShow.ShowOptions();
                }
                
            }
            



        }
        public string MenuOptionCallerApiThatDependOfChoise(ApiConnection ApiForTheCall)
        {
            switch (this.ChoiseOfUserValid)
            {
                case 1:
                    ApiForTheCall.
                    

                
            }
            
        }
        public string TransformOptionNumberInKeyOfDictionary()
        {

        }
        public bool CheckIfChoiseIsAOptionValidHandler(string ChoiseOfUser)
        {

            bool IsNumber = this.CheckIfChoiseIsNumber(ChoiseOfUser);
            if (IsNumber)
            {
                bool ItsOptionOrNot = this.CheckIfChoiseAreInOptions();
                return (ItsOptionOrNot ? true : false);

            }
            return false;
            
            
            


        }
        public bool CheckIfChoiseIsNumber(string ChoiseOfUser) {

            try
            {
                this.ChoiseOfUserValid = int.Parse(ChoiseOfUser);
            }
            catch (Exception e)
            {
                Console.WriteLine("Escolha Invalida");
                return false;
            }
            return true;




        }
        public bool CheckIfChoiseAreInOptions()
        {
            foreach(int option in this.Options.Values)
            {
                if (this.ChoiseOfUserValid == option)
                {
                    return true;
                }
            }
            
            return false;
        }

    }

    public class  CurrencyApiConection: ApiConnection
    {
        public string AllCoins = "All";
        public string DolarToReal = "USD-BRL";

        HttpClient client = new HttpClient();
        public string BaseUrlAndressOfApi
        {
            get {return "https://economia.awesomeapi.com.br/"; }
            set
            {
                this.BaseUrlAndressOfApi+= value;
            }
        } 

        public CurrencyApiConection(string PathUrl = "All")
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
            
            CurrencyMenuShow.CurrencMenuStart();
            CurrencyMenuHandlerLogic MenuHandler = new CurrencyMenuHandlerLogic();
            MenuHandler.ReadInputUser();
            
            
           
            CurrencyApiConection CurrencyConectionApiInstance= new CurrencyApiConection();
            MenuHandler.MenuOptionCallerApiThatDependOfChoise(CurrencyConectionApiInstance);
            CurrencyInstance.ResponseApiResult = CurrencyConectionApiInstance.GetApiResponse();
            
            CurrencyInstance.CreateJsonDocumentFromApiResult();
            Console.ReadLine();
            
            
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
            Console.WriteLine(JsonRepresentation);

            return JsonRepresentation;
        }

    }
}