// See https://aka.ms/new-console-template for more information
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Newtonsoft;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Serializers;
using System.Collections.Generic;
using Newtonsoft.Json;

//RestSharpPost();


// START // 

BirthdayChallenge();






Console.ReadLine();

    // END // 

public static partial class Program
{
    class Person
    {
        public Person(int id, string name, string notes)
        {
            this.Id = id;
            this.Name = name;
            this.Notes = notes;
        }
        public Person() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public List<string> Tags { get; set; }
    }


    public static void BirthdayChallenge()
    {
        DateTime testTime = new DateTime(1989, 07, 15);
        TimeSpan diff = DateTime.Now - testTime;
        //string days = Math.Truncate(diff.TotalDays / 365).ToString();
        //Console.WriteLine((Math.Truncate(diff.TotalDays/365)).ToString());

        // how long until next bday?
        testTime = testTime.AddYears(1);
        int diffMonths = DateTime.Now.Month - testTime.Month;
        int diffDays = DateTime.Now.Day - testTime.Day;


        Console.WriteLine($"Next bday: {diffMonths} months and {diffDays} days");

        // number of weekends until b-day
        


    }

    public static void CopyObjects()
    {
        // create two people
        Person personA = new Person
        {
            Id = 1,
            Name = "Test One",
            Notes = "This is the first one",
            Tags = new List<string> { "one", "two", "three", "four", "five" }
        };
        Person personB = new Person
        {
            Id = 2,
            Name = "Two Test",
            Notes = "Second Notes",
            Tags = new List<string> { "red", "bat", "cat", "top", "mat" }
        };

        Person copyPerson = new Person();
        // Option 1 - Reflection - doesn't quite work
         foreach (var prop in personA.GetType().GetProperties())
             copyPerson.GetType().GetProperty(prop.Name).SetValue(copyPerson, prop.GetValue(personA, null), null);


        // Method 3 - Covert to JSON, then convert back
        //string tempJSON = JsonConvert.SerializeObject(personA);
        //Person jsonPerson = JsonConvert.DeserializeObject<Person>(tempJSON);
        // can also one line it - copyPerson = JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(personA));

        copyPerson.Notes = "Also Changed";
        copyPerson.Tags[0] = "totally different";

        // Person copyPerson = new Person(personA.Id, personA.Name, personA.Notes);

        //copyPerson.Notes = "Changed";
        Console.WriteLine($"Copy Person: {copyPerson.Notes}");
        foreach (string tag in copyPerson.Tags)
        {
            Console.Write(tag);
        }
        Console.WriteLine($"Original Person: {personA.Notes}");
        foreach (string tag in personA.Tags)
        {
            Console.Write(tag);
        }
        

    }

    public static void GetTestSync()
    {
        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            // Define the URL you want to make a GET request to
            string url = "https://restcountries.com/v3.1/all";

            try
            {
                // Send a GET request to the URL synchronously
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseBody);
                }
                else
                {
                    // Handle the error if the request was not successful
                    Console.WriteLine($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // Handle any exception that may occur during the request
                Console.WriteLine($"Request failed: {e.Message}");
            }
        }
    }

    public static async void GetTestAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            // Define the URL you want to make a GET request to
            string url = "https://restcountries.com/v3.1/all";

            try
            {
                // Send a GET request to the URL
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    // Handle the error if the request was not successful
                    Console.WriteLine($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // Handle any exception that may occur during the request
                Console.WriteLine($"Request failed: {e.Message}");
            }
        }

        // Add this line to keep the console application running until Enter is pressed
        Console.ReadLine();
    }

    public static async void PostTest()
    {
        using (HttpClient client = new HttpClient())
        {
            // Define the URL you want to make a POST request to
            string url = "https://httpbin.org/post";

            try
            {
                // Define the data to be sent in the request body (as JSON in this example)
                string jsonContent = "{\"key1\":\"value1\",\"key2\":\"value2\"}";

                jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(new { foo = "bar" });

                // Create a StringContent object with the JSON content
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send a POST request to the URL with the data in the request body
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    // Handle the error if the request was not successful
                    Console.WriteLine($"Request failed with status code {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // Handle any exception that may occur during the request
                Console.WriteLine($"Request failed: {e.Message}");
            }
        }
    }

    public static void RestSharpGet()
    {

        // Define the base URL of the REST API
        string baseUrl = "https://restcountries.com/v3.1/all";

        // Create a RestClient instance
        RestClient client = new RestClient(baseUrl);

        // Define the resource path for your GET request
        string resourcePath = "https://restcountries.com/v3.1/all";

        // Create a RestRequest with the resource path and HTTP method (GET)
        RestRequest request = new RestRequest(resourcePath, Method.Get);

        try
        {
            // Execute the GET request and get the response
            RestResponse response = client.Execute(request);

            // Check if the request was successful (status code 200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Print the response content
                Console.WriteLine(response.Content);
            }
            else
            {
                // Handle the error if the request was not successful
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the request
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }

    public static void RestSharpPost()
    {
        // Define the base URL of the REST API
        string baseUrl = "https://httpbin.org/post";

        // Create a RestClient instance
        RestClient client = new RestClient(baseUrl);

        // Define the resource path for your POST request
        string resourcePath = baseUrl;

        // Create a RestRequest with the resource path and HTTP method (POST)
        RestRequest request = new RestRequest(resourcePath, Method.Post);

        // Define the data to be sent in the request body (e.g., as JSON)
        string jsonContent = "{\"key1\":\"value1\",\"key2\":\"value2\"}";
        request.AddParameter("application/json", jsonContent, ParameterType.RequestBody);

        try
        {
            // Execute the POST request and get the response
            RestResponse response = client.Execute(request);

            // Check if the request was successful (status code 200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Print the response content
                Console.WriteLine(response.Content);
            }
            else
            {
                // Handle the error if the request was not successful
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the request
            Console.WriteLine($"Request failed: {ex.Message}");
        }
    }

}
