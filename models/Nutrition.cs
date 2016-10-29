using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
internal class Fields
{
    public string item_id;
    public string item_name;
    public string brand_name;
    public double? nf_calories;
    public double? nf_total_fat;
    public int? nf_serving_size_qty;
    public string nf_serving_size_unit;
}

internal class Hit
{
    public Fields fields;
    
}

internal class Nutrition
{
    public int total_hits;
    public List<Hit> hits;   
}

public class Search {
    public static string mostCommonSearch(List<string> searchTerms){        //the function below does the exact same thing
        string mode = searchTerms.GroupBy(v => v)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;
            return mode;
    }

    // public static string mostCommonSearch(List<string> searchTerms){
    //     var count = new Dictionary<string, int>();
    //     foreach (string term in searchTerms)
    //     {
    //         if (count.ContainsKey(term))
    //         {
    //             count[term]++;
    //         }
    //         else
    //         {
    //             count.Add(term, 1);
    //         }
    //     }
    //     string mostCommonString = String.Empty;
    //     int highestCount = 0;
    //     foreach (KeyValuePair<string, int> pair in count)
    //     {
    //         if (pair.Value > highestCount)
    //         {
    //             mostCommonString = pair.Key;
    //             highestCount = pair.Value;
    //         }
    //     }  
    //     return mostCommonString;
    // }
}   
public class Prompter {                                     //moved the whole prompter methode into new propmter class 
    string name;
    string apiCalls;
    // List <string> searchTerms;
    static List<string> searchTerms = new List<string>();
    public Prompter(){

    }
    public static async Task prompt()
    {   
        Console.WriteLine(@"
        Howdy, fatso!!  
        --------
        Enter a food or beverage to get caloric info:

        ");
        
        string term = Console.ReadLine();
        IJSONAPI mashapi = new MashapeAPI();
        var nutrixapiKey = "8H6stHhT25mshXH1okEaCywiRiCUp1DYIsxjsnyespJHYCy7ca";        
        Nutrition n = await mashapi.GetData<Nutrition>(term,nutrixapiKey, "");
        
        if(term == "" || term == "n") {
            string hitGoogleWith = Search.mostCommonSearch(searchTerms); 
            Console.WriteLine("most common search term:"+ hitGoogleWith);                //mostCommonSearch(searchTerms);
            googler.promptGoogle(hitGoogleWith).Wait();
            Environment.Exit(0);
        } else if (n.hits.Count() > 0){
            searchTerms.Add(term);
            string item = n.hits.ElementAt(0).fields.item_name;
            double? calories = n.hits.ElementAt(0).fields.nf_calories ?? 0;
            double? fatGrams = n.hits.ElementAt(0).fields.nf_total_fat ?? 0;            
            Console.WriteLine("Item: "+item+" Calories: "+calories+" Fat Grams: "+fatGrams+" grams");
            
            string stickem = (item + calories.ToString() + fatGrams.ToString());
            List<string> list1 = new List<string>();
            list1.Add(item);list1.Add(calories.ToString());list1.Add(fatGrams.ToString());
            
            if (!File.Exists(@"csv/nutrix.csv"))
                {
                    File.AppendAllLines(@"csv/nutrix.csv", list1); 
                                                        
                    // or : File.WriteAllText(@"csv/nutrix.csv", stickem); but writes all in 1 line
                }
                    File.AppendAllLines(@"csv/nutrix.csv", list1);   
                }           
            
            prompt().Wait();                     
    }
    public static async Task<string> getUrl(string url){
        var http = new HttpClient();
        string reply = await http.GetStringAsync(url);
        return reply;

    }
}
