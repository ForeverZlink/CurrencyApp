﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace CurrencyApp
{
    public class CurrencyMenuShow {
        public static void ShowOptionsFirstMenu()
        {
            string message = "Suas opções são\n[1]-Mostra a contação de todas as moedas\n[2]-Escolher detalhes de uma moeda especifíca ";
            Console.WriteLine(message);
            Console.Write("Digite a opção escolhida ");

        }
        public static void ShowOptionsSecondMenuSpecificCoin()
        {
            string message = "Escolha qual moeda você deseja ver a conversão atual\n[1]USD\n~[2]BTC\n[3]EUR";
            Console.WriteLine(message);
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
            { "A","All"},{"Spec","SpecificCoin"}};

        public Dictionary<int,string> OptionsForChoiseJustACoins = new Dictionary<int,string> {
            { 1,"USDBRL" },{2,"EURBRL" },
            {3,"BTCBRL" }
        
        };

        public string  ReadInputUserMenu(){
            
            while (true){   
                
                string? Choise = Console.ReadLine();
                
                    bool Response = this.CheckIfChoiseIsAOptionValidFirstMenu(Choise);

                    if (Response)
                    {
                        if (this.ChoiseOfUserValidFirstMenu == "SpecificCoin")
                        {
                            CurrencyMenuShow.ShowOptionsSecondMenuSpecificCoin();
                            string MenuChoise = Console.ReadLine();
                            this.CheckIfChoiseIsAOptionValidHandlerMenu(MenuChoise,this.OptionsForChoiseJustACoins);

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