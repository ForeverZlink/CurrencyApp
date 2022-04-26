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
        public void TestIfChoiseIsNumber()
        {
            string NumberforTest="1";
            bool ResultOfTest = this.CurrencyMenuLogicInstance.CheckIfChoiseIsNumber(NumberforTest);
            Assert.AreEqual(this.CurrencyMenuLogicInstance.ChoiseOfUserValid, 1);
            Assert.IsTrue(ResultOfTest);

           
        }
    }
}