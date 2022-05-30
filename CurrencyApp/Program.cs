using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenuShow {


        // Static constructor is called at most one time, before any
        // instance constructor is invoked or member is accessed.
        static CurrencyMenuShow()
        {
            string message = "Bem vindo ao nosso conversor de moedas e a cotação atual de diversas moedas no mundo";
            Console.WriteLine(message);
        }
        public static void ShowOptionsFirstMenu()
        {

            string message = "Suas opções são\n[All]-Mostra a contação de todas as moedas\n[SpecificCoin]-Escolher detalhes de uma moeda especifíca\n[Exit]Fecha o programa ";
            Console.WriteLine(message);
            Console.Write("Digite a opção escolhida: ");

        }

        public static void ShowOptionsSecondMenuSpecificCoin(Dictionary<int, string> Coins)
        {
            Console.WriteLine("---------------------------------------------------");
            string Message = "Escolha uma das opções abaixo!\n";

            foreach (var coin in Coins)
            {
                Message += $"[{coin.Key}]{coin.Value}\n";
            }
            Console.WriteLine(Message);
            Console.Write("Qual a opção desejada? ");

        }
        public static void ShowDetailsFromJsonDeserialized(JsonElement JsonDesiarilized,List<string> CoinsForShow)
        {

            foreach (var Coin in CoinsForShow)
            {

                string CodeOfTheCoinForConversion = JsonDesiarilized.GetProperty(Coin).GetProperty("code").ToString();
                string CodeOfTheCoinConversed = JsonDesiarilized.GetProperty(Coin).GetProperty("codein").ToString();
                string NameOfConversion = JsonDesiarilized.GetProperty(Coin).GetProperty("name").ToString();
                string HighestPriceOfTheDay = JsonDesiarilized.GetProperty(Coin).GetProperty("high").ToString();
                string LowestPriceOfTHeDay = JsonDesiarilized.GetProperty(Coin).GetProperty("low").ToString();
                string TextWithInformations = $"Código da moeda que será convertida: {CodeOfTheCoinForConversion}\n" +
                    $"Código da moeda para qual vai ser feita a conversão: {CodeOfTheCoinConversed}\n" +
                    $"Nome da conversão: {NameOfConversion}\n" +
                    $"Maior preço diário: R${HighestPriceOfTheDay}\n" +
                    $"Menor preço diário: R${LowestPriceOfTHeDay}";

                Console.WriteLine("-=================================================================-");
                Console.WriteLine(TextWithInformations);
                Console.WriteLine("===================================================================");
            }
        }
            
                
                
            
            
         
        }

    


    



       
    
    public interface ApiConnection
    {
        public Dictionary<int, string> ValuesSupportedForCallApi { get;  }
        
        public string GetApiResponse();
    }
    public class CurrencyMenuHandlerLogic{

        public int ChoiseOfUserValid;
        public string ChoiseOfUserValidFirstMenu;
        public string ChoiseOfUserKey;
        public string ChoiseOfUserWithTheName;
        public List<string> ListWithAllParametersChoised = new List<string>();
        public Dictionary<string,string> OptionsFirstMenu= new Dictionary<string, string> {
            { "A","ALL"},{"Spec","SPECIFICCOIN"}, {"E","EXIT"}};

        public  Dictionary<int, string> OptionsForChoiseJustACoins;

        public  Dictionary<int, string> AllCoins
        {
            get { return this.OptionsForChoiseJustACoins; }
        }
        public  Dictionary<int, string> SecondMenu
        {
            get { return this.OptionsForChoiseJustACoins; }
            set { this.OptionsForChoiseJustACoins = value; }
        }
           
        public bool ReadInputValidation (Dictionary<int,string> OptionWithNewsMenuForValidation)
        {
            while (true)
            {

                string Choise = Console.ReadLine();
                bool ResponseHandler = this.CheckIfChoiseIsAOptionValidHandlerMenu(Choise, OptionWithNewsMenuForValidation);
                if (ResponseHandler)
                {

                    return true;
                }
                else
                {
                    Console.WriteLine("Opção Errada! Digite apenas os valores aceitos ");
                    CurrencyMenuShow.ShowOptionsSecondMenuSpecificCoin(this.SecondMenu);
                }
               
            }
           
        }
        public string ReturnAllCoinsSupportedAsStringForApiCall() {

            string PathWithAllCoins = "";
            foreach(string coin in this.OptionsForChoiseJustACoins.Values)
            {
                 PathWithAllCoins += $"{coin},";
            }
            PathWithAllCoins = PathWithAllCoins.Remove(PathWithAllCoins.Length-1);
            return PathWithAllCoins;
        }
        public string RemoveTheTraceOfAOptionBecauseTheKeyInJsonDoNotHave(string raw)
        {
            string WithoutYrace = raw.Replace("-", "");
            return WithoutYrace;
        }
        public  string  ReadInputUserMenuHandler(){
            
            while (true){

                CurrencyMenuShow.ShowOptionsFirstMenu();
                string? Choise = Console.ReadLine().ToUpper();
                
                    bool Response = this.CheckIfChoiseIsAOptionValidFirstMenu(Choise);

                    if (Response)
                    {
                        
                        if (this.ChoiseOfUserValidFirstMenu == "ALL")
                        {
                            this.ListWithAllParametersChoised.Clear();
                            foreach(var CoinName in this.AllCoins.Values)
                            {
                            
                            
                            this.ListWithAllParametersChoised.Add(this.RemoveTheTraceOfAOptionBecauseTheKeyInJsonDoNotHave(CoinName));
                             
                            }
                            
                            return this.ReturnAllCoinsSupportedAsStringForApiCall();
                        }
                        else {

                            if (this.ChoiseOfUserValidFirstMenu == "SPECIFICCOIN")
                                {
                                    
                                    CurrencyMenuShow.ShowOptionsSecondMenuSpecificCoin(this.SecondMenu);
                                    bool ResponseOfValidation = this.ReadInputValidation(this.SecondMenu);
                                    this.ChoiseOfUserWithTheName = this.OptionsForChoiseJustACoins.GetValueOrDefault(this.ChoiseOfUserValid);
                                    this.ListWithAllParametersChoised.Clear();
                                    
                                    this.ListWithAllParametersChoised.Add(this.RemoveTheTraceOfAOptionBecauseTheKeyInJsonDoNotHave(this.ChoiseOfUserWithTheName));
                                    
                                    return this.ChoiseOfUserWithTheName;
                                
                                }


                        }

                    }
                    else
                    {
                        Console.WriteLine("Digite apenas um valor que esteja nas opções!");
                        CurrencyMenuShow.ShowOptionsFirstMenu();
                    }

                
                
                
                
                
            }
        

        }
        public bool CheckIfChoiseIsAOptionValidFirstMenu(string ChoiseOfUser)
        {
            foreach(var option in OptionsFirstMenu.Values)
            {
                if (option==ChoiseOfUser)
                {
                    if (option == "EXIT")
                    {
                        Environment.Exit(0);
                    }
                    this.ChoiseOfUserValidFirstMenu = ChoiseOfUser;
                    return true;
                }
            }
            
            return false;
        }
        public string MenuOptionCallerApiFromChoise(ApiConnection ApiForTheCallObject)
        {
                
            string ResponseOfApi = ApiForTheCallObject.GetApiResponse();
            return ResponseOfApi;
            
            
        }
        
        public bool CheckIfChoiseIsAOptionValidHandlerMenu(string ChoiseOfUser,Dictionary<int,string>DataForCompare)
        {
            
            
                bool IsNumber = this.CheckIfChoiseIsNumber(ChoiseOfUser);
                if (IsNumber)
                {
                    bool ItsOptionOrNot = this.CheckIfChoiseAreInOptionsMenu(DataForCompare);
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
        public bool CheckIfChoiseAreInOptionsMenu( Dictionary<int,string>DataForCompare)
        {
 
            
            foreach (int option in DataForCompare.Keys)
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

        public static Dictionary<int, string> Coins = new Dictionary<int, string>
        {
            {1,"USD-BRL" },
            {2,"EUR-BRL" },
            {3,"BTC-BRL" }


        };
        public Dictionary<int,string> ValuesSupportedForCallApi
        {
            get { return Coins; }
        }


        HttpClient client = new HttpClient();
        public string BaseUrlAndressOfApi = "https://economia.awesomeapi.com.br/last/";


        public string BaseUrlAndressOfApiWithArgs;

        public string ConfigArgsAndressOfApi { 
            set {
                
                this.BaseUrlAndressOfApiWithArgs= this.BaseUrlAndressOfApi +value;
            }
        
        }

        

        public CurrencyApiConection(string PathUrl = "All")
        {
            this.BaseUrlAndressOfApi = this.BaseUrlAndressOfApi;
        }
        public string GetApiResponse()
        {
            string Response = client.GetStringAsync(this.BaseUrlAndressOfApiWithArgs).Result;
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
            CurrencyApiConection CoinsSupportedForApi = new CurrencyApiConection();
            
            while (true)
            {
                
                CurrencyApiConection CurrencyConectionApiInstance = new CurrencyApiConection();
                CurrencyMenuHandlerLogic MenuHandler = new CurrencyMenuHandlerLogic();
                MenuHandler.SecondMenu = CurrencyConectionApiInstance.ValuesSupportedForCallApi;
                string KeyChoised = MenuHandler.ReadInputUserMenuHandler();
                CurrencyConectionApiInstance.ConfigArgsAndressOfApi = KeyChoised;
                
                MenuHandler.MenuOptionCallerApiFromChoise(CurrencyConectionApiInstance);
            
            
            
                CurrencyInstance.ResponseApiResult = CurrencyConectionApiInstance.GetApiResponse();
                
                JsonElement Coin =   CurrencyInstance.DeserializeJsonDocumentFromApiResult();

                CurrencyMenuShow.ShowDetailsFromJsonDeserialized(Coin, MenuHandler.ListWithAllParametersChoised);
                
            

            }
            
            
        }

        public JsonElement DeserializeJsonDocumentFromApiResult()
        {
            JsonElement Coin = JsonSerializer.Deserialize<JsonElement>(this.ResponseApiResult);
            return Coin;
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