using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenuShow {
        public static void ShowOptionsFirstMenu()
        {

            string message = "Suas opções são\n[All]-Mostra a contação de todas as moedas\n[SpecificCoin]-Escolher detalhes de uma moeda especifíca ";
            Console.WriteLine(message);
            Console.Write("Digite a opção escolhida ");

        }

        public static void ShowOptionsSecondMenuSpecificCoin(Dictionary<int, string> Coins)
        {
            string Message = "Escolha uma das opções abaixo!\n";

            foreach (var coin in Coins)
            {
                Message += $"[{coin.Key}]{coin.Value}\n";
            }
            Console.WriteLine(Message);

        }
        public static void ShowDetailsOfCoinFromJson(JsonElement JsonToUse)
        {

            string CodeOfTheCoinForConversion = JsonToUse[0].GetProperty("code").ToString();
            string CodeOfTheCoinConversed = JsonToUse[0].GetProperty("codein").ToString();
            string NameOfConversion = JsonToUse[0].GetProperty("name").ToString();
            string HighestPriceOfTheDay = JsonToUse[0].GetProperty("high").ToString();
            string LowestPriceOfTHeDay = JsonToUse[0].GetProperty("low").ToString();
            string TextWithInformations = $"Código da moeda que será convertida: {CodeOfTheCoinForConversion}\n" +
                $"Código da moeda para qual vai ser feita a conversão: {CodeOfTheCoinConversed}\n" +
                $"Nome da conversão: {NameOfConversion}\n" +
                $"Maior preço diário: R${HighestPriceOfTheDay}\n" +
                $"Menor preço diário: R${LowestPriceOfTHeDay}";
            Console.WriteLine("-=================================================================-");
            Console.WriteLine(TextWithInformations);
       
        }

    


    



        public static void CurrencMenuStart()
        {
            CurrencyMenuShow Menu = new CurrencyMenuShow();
            string message = "Bem vindo ao nosso conversor de moedas e a cotação atual de diversas moedas no mundo";
            Console.WriteLine(message);
            
            



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
        public Dictionary<string,string> OptionsFirstMenu= new Dictionary< string,string> {
            { "A","ALL"},{"Spec","SPECIFICCOIN"}};

        public  Dictionary<int, string> OptionsForChoiseJustACoins;
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
        public  string  ReadInputUserMenuHandler(){
            
            while (true){

                CurrencyMenuShow.ShowOptionsFirstMenu();
                string? Choise = Console.ReadLine().ToUpper();
                
                    bool Response = this.CheckIfChoiseIsAOptionValidFirstMenu(Choise);

                    if (Response)
                    {
                        
                        if (this.ChoiseOfUserValidFirstMenu == "ALL")
                        {
                        return this.ChoiseOfUserValidFirstMenu;
                        }
                        else {

                        if (this.ChoiseOfUserValidFirstMenu == "SPECIFICCOIN")
                            {
                                CurrencyMenuShow.ShowOptionsSecondMenuSpecificCoin(this.SecondMenu);
                                bool ResponseOfValidation = this.ReadInputValidation(this.SecondMenu);
                                
                                return this.OptionsForChoiseJustACoins.GetValueOrDefault(this.ChoiseOfUserValid);
                                
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
            if (ChoiseOfUser == this.OptionsFirstMenu["A"] || ChoiseOfUser == this.OptionsFirstMenu["Spec"])
            {
                this.ChoiseOfUserValidFirstMenu = ChoiseOfUser;
                return true;
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
            
            CurrencyMenuShow.CurrencMenuStart();
            CurrencyApiConection CurrencyConectionApiInstance = new CurrencyApiConection();
            CurrencyMenuHandlerLogic MenuHandler = new CurrencyMenuHandlerLogic();
            MenuHandler.SecondMenu = CurrencyConectionApiInstance.ValuesSupportedForCallApi;
            string KeyChoised = MenuHandler.ReadInputUserMenuHandler();
            CurrencyConectionApiInstance.ConfigArgsAndressOfApi = KeyChoised;
            Console.WriteLine(CurrencyConectionApiInstance.BaseUrlAndressOfApiWithArgs);
            MenuHandler.MenuOptionCallerApiFromChoise(CurrencyConectionApiInstance);
            
            
            
            CurrencyInstance.ResponseApiResult = CurrencyConectionApiInstance.GetApiResponse();
            
            JsonElement DataJson = CurrencyInstance.CreateJsonDocumentFromApiResult();
            CurrencyMenuShow.ShowDetailsOfCoinFromJson(DataJson);
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