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
        public void Test1IfChoiseIsNumber()
        {
            string NumberforTest="3";
            bool ResultOfTest = this.CurrencyMenuLogicInstance.CheckIfChoiseIsNumber(NumberforTest);
            Assert.AreEqual(this.CurrencyMenuLogicInstance.ChoiseOfUserValid, 3);
            Assert.IsTrue(ResultOfTest);
            
           
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