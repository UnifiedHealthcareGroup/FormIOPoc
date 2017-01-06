using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace FormAPITest
{
    public class Credential
    {
        public string email { get; set; }
        public string Password { get; set; }
    }
    class Program
    {
        static readonly HttpClient Client = new HttpClient();
        private static string _formIOAdress = "https://formio.form.io/";
        static void Main(string[] args)
        {
            var token= GetTokenFromFormIO();
            GetSumissionData(token);
        }

        private static string GetTokenFromFormIO()
        {
            try
            {
                var client = new RestClient("https://formio.form.io/user/login");
                var request = new RestRequest();
                var body = "{\"data\":{\"email\":\"arpan.saxena@uhg.com.au\",\"password\":\"password\"}}";
                
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var response = client.Execute(request);
                var content = response.Content; // raw content as string  
                string token = response.Headers.ToList().Find(x => x.Name == "x-jwt-token").Value.ToString();
                return token;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void GetSumissionData(string token)
        {
            var client = new RestClient("https://uhg.form.io/emawizard/submission");
            var request = new RestRequest {Method = Method.GET};
            request.AddHeader("Accept", "application/json");
            request.AddHeader("x-jwt-token", token);
            var response = client.Execute(request);

        }

    }
}
