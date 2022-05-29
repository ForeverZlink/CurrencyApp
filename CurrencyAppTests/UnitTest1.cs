using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyApp;
using System;
using System.Text.Json;
using System.Collections.Generic;

namespace CurrencyAppTests
{

    [TestClass]
    public class CurrencyApiConnectionTest
    {
        Currency MainClass = new Currency();
        public static CurrencyApiConection? CurrencyApiInstance;


        [TestMethod]
        public void TestGetterSetterVariable()
        {
            CurrencyApiInstance = new CurrencyApiConection();
            CurrencyApiInstance.ConfigArgsAndressOfApi = "All";
            Assert.AreEqual("https://economia.awesomeapi.com.br/last/All", CurrencyApiInstance.BaseUrlAndressOfApiWithArgs);
        }

        [TestMethod]
        public void TestGetApiResponse()
        {
            CurrencyApiInstance = new CurrencyApiConection();
            CurrencyApiInstance.ConfigArgsAndressOfApi = "USD-BRL";
            this.MainClass.ResponseApiResult = CurrencyApiInstance.GetApiResponse();
            string ErrorApiReturn = "404";
            Assert.IsFalse(this.MainClass.ResponseApiResult.Contains(ErrorApiReturn));
        }
    }

    [TestClass]
    public class CurrencyTest
    {
        Currency MainClass = new Currency();
        public CurrencyApiConection? CurrencyApiInstance;

        [TestMethod]
        public void TestCreateJsonDocument()
        {
            
           
            CurrencyApiInstance = new CurrencyApiConection(this.MainClass.PathUrlRequired);
            CurrencyApiInstance.ConfigArgsAndressOfApi= "USD-BRL";
            this.MainClass.ResponseApiResult = this.CurrencyApiInstance.GetApiResponse();
            JsonElement JsonRepresentation = this.MainClass.CreateJsonDocumentFromApiResult();
            Assert.AreEqual("USD", JsonRepresentation.GetProperty("USDBRL").GetProperty("code").ToString());






        }
        [TestMethod]
        public void TestDeserializeJsonDocumentFromApiResult()
        {
            CurrencyApiInstance = new CurrencyApiConection(this.MainClass.PathUrlRequired);
            CurrencyApiInstance.ConfigArgsAndressOfApi = "USD-BRL";
            this.MainClass.ResponseApiResult = this.CurrencyApiInstance.GetApiResponse();
            Dictionary<string, Dictionary<string, string>> Coin = this.MainClass.DeserializeJsonDocumentFromApiResult();
            Assert.AreEqual("USD", Coin.GetValueOrDefault("USDBRL").GetValueOrDefault("code").ToString());

        }

    }
    [TestClass]
    public class CurrencyMenuShowTest
    {
        Currency MainClass = new Currency();
        public CurrencyApiConection? CurrencyApiInstance;
        
        [TestMethod]
        public void TestShowDetailsOfCoinFromJson()
        {
            CurrencyApiInstance = new CurrencyApiConection(this.MainClass.PathUrlRequired);
            CurrencyApiInstance.ConfigArgsAndressOfApi = "USD-BRL";
            this.MainClass.ResponseApiResult = this.CurrencyApiInstance.GetApiResponse();
            Dictionary<string, Dictionary<string, string>> JsonRepresentation = this.MainClass.DeserializeJsonDocumentFromApiResult();
            CurrencyMenuShow.ShowDetailsFromJsonDeserialized(JsonRepresentation);
            

        }
    }
    [TestClass]
    public class CurrencyHandlerLogicTest
    {
        CurrencyMenuHandlerLogic CurrencyMenuLogicInstance = new CurrencyMenuHandlerLogic();
        CurrencyApiConection CurrencyApiInstance = new CurrencyApiConection();

        [TestMethod]
        public void TestReturnAllCoinsSupportedAsStringForApiCall()
        {
            this.CurrencyMenuLogicInstance.SecondMenu = new Dictionary<int, string> { { 1, "USD-BRL" }, { 2, "BTC-BRL" } };
            string AllCoins = this.CurrencyMenuLogicInstance.ReturnAllCoinsSupportedAsStringForApiCall();
            string MessageWanted = "USD-BRL,BTC-BRL";
            Assert.AreEqual(AllCoins, MessageWanted);
            this.CurrencyApiInstance.ConfigArgsAndressOfApi = AllCoins;

            string ResultOfCallApi = this.CurrencyApiInstance.GetApiResponse();
            Console.WriteLine(ResultOfCallApi);
        }
        [TestMethod]
        public void TestCheckIfChoiseIsAOptionValidHandlerMenu()
        {
           
            //TEst verify if the argument of first option , are a option and wanted for a true return, because its option valid 
            Dictionary<int,string> DataWithSpecificCoinOptions =new Dictionary<int, string> { {1,"USD-BRL"} };
            string Option = "1";
            bool FirstTrueResponseWanted  = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidHandlerMenu(Option,DataWithSpecificCoinOptions);
            Assert.IsTrue(FirstTrueResponseWanted);

            //TEst Verifiy if argument is valid and wanted for a False return, because its option invalid 
            string OptionNoneExists = "200000";
            bool SecondFalseResponseWanted = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidHandlerMenu(OptionNoneExists, DataWithSpecificCoinOptions);
            Assert.IsFalse(SecondFalseResponseWanted);



            






        }
    
        [TestMethod]
        public void Test1IfChoiseIsAOptionValidFirstMenu()
        {
            //1°Case: True is waited, because the are in options 
            string OptionForTest = "ALL";
            bool ResultOfTestTrue = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidFirstMenu(OptionForTest);
            Assert.AreEqual(this.CurrencyMenuLogicInstance.ChoiseOfUserValidFirstMenu, "ALL");
            Assert.IsTrue(ResultOfTestTrue);

            //2°Case: True is waited, because this is are a option valid~SpecificCoin Case 
            OptionForTest = "SPECIFICCOIN";
            bool ResultOfTestTrueSpecificCoin= this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidFirstMenu(OptionForTest);
            Assert.IsTrue(ResultOfTestTrueSpecificCoin);

            //3°Case: False is waited, because the variable not are in option still
           
            OptionForTest = "100000";
            bool ResultOfTestFalseBecauseOfNotAvailable = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidFirstMenu(OptionForTest);
            Assert.IsFalse(ResultOfTestFalseBecauseOfNotAvailable);






        }
        [TestMethod]
        public void TestTrasnformNumberInKey()
        {
            int NumberToSearch = 1;
            Dictionary<int, string> AllCoins = new Dictionary<int, string>
            {
                {1,"USD-BRL" },
                {2,"EUR-BRL" }
            };
            string USDCOIN = AllCoins.GetValueOrDefault(NumberToSearch);
            Assert.AreEqual(USDCOIN, AllCoins[1]);




        }
        [TestMethod]
        public void TestMenuOptionCalleThatsDependOfApi()
        {
            CurrencyApiConection  Api = new CurrencyApiConection();
            this.CurrencyMenuLogicInstance.ChoiseOfUserValid = 1;
            Api.ConfigArgsAndressOfApi = "USD-BRL";
            string ResponseFirstCall = this.CurrencyMenuLogicInstance.MenuOptionCallerApiFromChoise(Api);
            Assert.IsTrue(ResponseFirstCall.Contains("USD"));

            ///Passing a different argument for the call 
            ///
            string EurCoinChoise = "EUR-BRL";

            Api.ConfigArgsAndressOfApi = EurCoinChoise;
            string ReponseOfSecondCall = this.CurrencyMenuLogicInstance.MenuOptionCallerApiFromChoise(Api);
            Assert.IsTrue(ReponseOfSecondCall.Contains("EUR"));
            

        }

        public void Test2IfChoiseAreInOptions2()
        {
            //test if the method can find out if the number are in options 
            Dictionary<int, string> DataForCompare = this.CurrencyMenuLogicInstance.OptionsForChoiseJustACoins;
            this.CurrencyMenuLogicInstance.ChoiseOfUserValid = 2;
            bool response_true= this.CurrencyMenuLogicInstance.CheckIfChoiseAreInOptionsMenu(DataForCompare);
            Assert.IsTrue(response_true);

            //this test has the responsability of force a error and verify if the method
            //can return a waited response that this context is false, because
            //doesn't exists option with value 1000
            this.CurrencyMenuLogicInstance.ChoiseOfUserValid = 1000;
            bool response_false = this.CurrencyMenuLogicInstance.CheckIfChoiseAreInOptionsMenu(DataForCompare);
            Assert.IsFalse(response_false);

            
        }




    }
}