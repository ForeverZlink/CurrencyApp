using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyApp;
using System;
using System.Text.Json;
namespace CurrencyAppTests
{
    
    [TestClass]
    public class CurrencyApiConnectionTest
    {
        Currency MainClass = new Currency();
        public static CurrencyApiConection? CurrencyApiInstance;
        [TestMethod]
        public void TestGetApiResponse()
        {
            CurrencyApiInstance = new CurrencyApiConection(MainClass.PathUrlRequired);
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
            this.MainClass.PathUrlRequired = "USD-BRL";
            
            CurrencyApiInstance = new CurrencyApiConection(this.MainClass.PathUrlRequired);
            this.MainClass.ResponseApiResult = this.CurrencyApiInstance.GetApiResponse();
            JsonElement JsonRepresentation = this.MainClass.CreateJsonDocumentFromApiResult();
            Assert.AreEqual("USD", JsonRepresentation[0].GetProperty("code").ToString());
            
        
        



        }

    }
    [TestClass]
    public class CurrencyHandlerLogicTest
    {
        CurrencyMenuHandlerLogic CurrencyMenuLogicInstance = new CurrencyMenuHandlerLogic();

        [TestMethod]
        public void Test1IfChoiseIsAOptionValid()
        {
            //1�Case: True is waited, because the number are in options and not is a letter
            string NumberforTest="2";
            bool ResultOfTestTrue = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidHandler(NumberforTest);
            Assert.AreEqual(this.CurrencyMenuLogicInstance.ChoiseOfUserValid, 2);
            Assert.IsTrue(ResultOfTestTrue);

            //2�Case: False is waited, because this is a number and this method always return false in this codition
            NumberforTest = "word_testing";
            bool ResultOfTestFalse = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidHandler(NumberforTest);
            Assert.IsFalse(ResultOfTestFalse);

            //3�Case: False is waited, because the variable its a number
            //but not is a valid option due a not are available int options

            NumberforTest = "100000";
            bool ResultOfTestFalseBecauseOfNotAvailable = this.CurrencyMenuLogicInstance.CheckIfChoiseIsAOptionValidHandler(NumberforTest);
            Assert.IsFalse(ResultOfTestFalseBecauseOfNotAvailable);






        }
        
        [TestMethod]

        public void Test2IfChoiseAreInOptions2()
        {
            //test if the method can find out if the number are in options 
            this.CurrencyMenuLogicInstance.ChoiseOfUserValid = 2;
            bool response_true= this.CurrencyMenuLogicInstance.CheckIfChoiseAreInOptions();
            Assert.IsTrue(response_true);

            //this test has the responsability of force a error and verify if the method
            //can return a waited response that this context is false, because
            //doesn't exists option with value 1000
            this.CurrencyMenuLogicInstance.ChoiseOfUserValid = 1000;
            bool response_false = this.CurrencyMenuLogicInstance.CheckIfChoiseAreInOptions();
            Assert.IsFalse(response_false);
        }




    }
}