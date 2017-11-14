using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RabbitMQLab2
{
    class Consumer
    {
        public Consumer()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<Message>("id", HandleMessage);

                Console.WriteLine("Waiting for numbers.");
                Console.ReadLine();
            }
        }
        static async void HandleMessage(Message message)
        {
            var functionURL = "https://dectobin.azurewebsites.net/api/HttpTriggerCSharp1?code=GZVOgrWt5f5hXetGwXnyASjhx84sXYwCp9WspHskR12BoGkuIQ5PCA==  ";

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(functionURL));

            var requestString = "{\"name\": " + message.Number + "}";
            request.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);

            var responseString = response.Content.ReadAsStringAsync();

            Console.WriteLine("Binary number of " + message.Number + " is " + responseString.Result);
        }
    }
}
