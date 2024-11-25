using Newtonsoft.Json.Linq;

namespace CSharpAutomationProject.Utilities
{
    public class JSONReader
    {
        public JSONReader()
        {

        }

        public string ExtractJSON(String tokenName)
        {

            String myJsonString = File.ReadAllText(@"C:\\Luxoft\\Learnings\\CSharpAutomationProject\\TestOutput\\ECommerceTestData.json");

            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] ExtractArrayJSON(string tokenName)
        {

            String myJsonString = File.ReadAllText(@"C:\\Luxoft\\Learnings\\CSharpAutomationProject\\TestOutput\\ECommerceTestData.json");

            var jsonObject = JToken.Parse(myJsonString);
            List<string> ProductList =  jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return ProductList.ToArray();
        }

    }
}

