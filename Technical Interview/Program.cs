using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Technical_Interview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();


            HttpResponseMessage Res = await client.GetAsync("http://swapi.dev/api/people");
            var apiResponse = Res.Content.ReadAsStringAsync().Result;
            var people = JsonConvert.DeserializeObject<Person>(apiResponse);

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            List<List<string>> buddies = new List<List<string>>();
            int n = people.results.Length;

            int rowCounter = 0;
            for (int i = 0; i < n - 1; i++)
            {
                int filmCount1 = people.results[i].films.Length;
                for (int k = i + 1; k < n - 1; k++)
                {
                    int filmCount2 = people.results[k].films.Length;

                    if (filmCount1 == filmCount2)
                    {
                        bool isBuddy = false;
                        for (int j = 0; j < filmCount1 - 1; j++)
                        {

                            if (people.results[i].films[j] == people.results[k].films[j])
                            {
                                isBuddy = true;
                            }
                            else
                            {
                                isBuddy = false;
                            }
                        }

                        if (isBuddy)
                        {
                            string name1 = people.results[i].name;
                            string name2 = people.results[k].name;
                            if (!pairs.ContainsKey(name2))
                            {
                                pairs.Add(name2, name1);
                            }
                        }
                    }
                }
            }

            foreach (var val in pairs.Values.Distinct())
            {
                List<string> keys = (from kvp in pairs where kvp.Value == val select kvp.Key).ToList();
                buddies.Add(new List<string>());
                
                for (int j = 0; j < keys.Count; j++)
                {
                    buddies[rowCounter].Add(keys[j]);
                }
                buddies[rowCounter].Add(val);
                rowCounter++;
            }

            
        }
    }
}
