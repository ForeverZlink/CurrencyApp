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
        public Dictionary<int, string> ValuesSupportedForCallApi { get;  }
        public string GetApiResponse();
    }
    public class CurrencyMenuHandlerLogic{
        public int ChoiseOfUserValid;
        public string ChoiseOfUserKey;
        public Dictionary<string, int> OptionsFirstMenu= new Dictionary<string, int> {
            { "All",1 },{"SpecificCoin",2 }};

        public Dictionary<string, int> OptionsForChoiseJustACoins = new Dictionary<string, int> {
            { "USDBRL",1 },{"EURBRL",2 },
            {"BTCBRL",3 }
        
        };

        public string  ReadInputUserMenu(){
            
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
        public string MenuOptionCallerApiThatDependOfChoise(ApiConnection ApiForTheCallObject)
        {


            if (this.ChoiseOfUserValid == 1)
            {
                string ResponseOfApi = ApiForTheCallObject.GetApiResponse();
                return ResponseOfApi;
            }
            else
            {
                string KeyForCall = this.TransformOptionNumberInKeyOfDictionary(ApiForTheCallObject.ValuesSupportedForCallApi);
                
            }
            return "TEsting";
            
        }
        public string TransformOptionNumberInKeyOfDictionary(Dictionary<int,string> DataForCompare)
        {
            string KeyForApiCall = DataForCompare.GetValueOrDefault(this.ChoiseOfUserValid);
            return KeyForApiCall;
        }
        public bool CheckIfChoiseIsAOptionValidHandler(string ChoiseOfUser)
        {

            bool IsNumber = this.CheckIfChoiseIsNumber(ChoiseOfUser);
            if (IsNumber)
            {
                bool ItsOptionOrNot = this.CheckIfChoiseAreInOptionsMenu();
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
        public bool CheckIfChoiseAreInOptionsMenu(int ItsTheMenuForChoiseACoin=1)
        {
            Dictionary<string, int> Options;
            if (ItsTheMenuForChoiseACoin == 1)
            {
                //REceive the option of first menu 
                 Options = this.OptionsFirstMenu;

            }
            else
            {
                 Options =this.OptionsForChoiseJustACoins;
            }

            foreach (int option in Options.Values)
            {
                if (this.ChoiseOfUserValid == option)
                {
                    return true;
                }
            }

            return false;

        }


    }

    public class CurrencyApiConection : ApiConnection
    {
        public string AllCoins = "All";

        static Dictionary<int, string> Coins = new Dictionary<int, string>
        {
            {1,"USD-BRL" },
            {2,"EUR-BRL" },


        };
        public Dictionary<int,string> ValuesSupportedForCallApi
        {
            get { return Coins; }
        }


        HttpClient client = new HttpClient();
        public string BaseUrlAndressOfApi = "https://economia.awesomeapi.com.br/";
        

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
            CurrencyApiConection CurrencyConectionApiInstance = new CurrencyApiConection();
            CurrencyMenuHandlerLogic MenuHandler = new CurrencyMenuHandlerLogic();
            MenuHandler.ReadInputUserMenu();
            MenuHandler.MenuOptionCallerApiThatDependOfChoise(CurrencyConectionApiInstance);
            
           
            
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