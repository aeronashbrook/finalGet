using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace finalGet
{
    public class Item
    {
        public string id;
        public string name;
        public double price;
        public double market_cap;

    }
    public class Function
    {

        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private static string tableName = "crypto";
        public async Task<Item> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string id = "";
            Dictionary<string, string> dict = (Dictionary<string, string>)input.QueryStringParameters;
            dict.TryGetValue("id", out id);
            GetItemResponse res = await client.GetItemAsync(tableName, new Dictionary<string, AttributeValue>
            {
                {"id", new AttributeValue { S = id } }
            });


            Document doc = Document.FromAttributeMap(res.Item);
            Item myItem = JsonConvert.DeserializeObject<Item>(doc.ToJson());
            Console.WriteLine(myItem.id);
            Console.WriteLine(myItem.name);
            Console.WriteLine(myItem.price + "\n");
            Console.WriteLine(myItem.market_cap);
            Console.WriteLine(myItem.ToString());
            return myItem;

            //APIGatewayProxyResponse response = new APIGatewayProxyResponse();
            //response.StatusCode = 200;
            //response.Body = res.IsItemSet.ToString();
            //return JsonConvert.SerializeObject(res.Item);
        }
    }
}
