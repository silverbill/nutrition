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

public class Program
{
    
    public static void Main(string[] args)
    
    {
        prompt().Wait();
    }
    
    public static async Task prompt()
    {
        Console.WriteLine(@"
        Hey! Listen!
        
        --------
        
        Enter a food or beverage to get caloric info:


        ");
        string term = Console.ReadLine();
        IJSONAPI mashapi = new MashapeAPI();
        var apiKey = "8H6stHhT25mshXH1okEaCywiRiCUp1DYIsxjsnyespJHYCy7ca";        
        Nutrition n = await mashapi.GetData<Nutrition>(term,apiKey);
        
        //Directory.CreateDirectory("csv");
        
        if(term == "" || term == "n") {
            Environment.Exit(0);
        } else if (n.hits.Count() > 0){
            string item = n.hits.ElementAt(0).fields.item_name;
            double? calories = n.hits.ElementAt(0).fields.nf_calories ?? 0;
            double? fatGrams = n.hits.ElementAt(0).fields.nf_total_fat ?? 0;            
            Console.WriteLine("Item: "+item+" Calories: "+calories+" Fat Grams: "+fatGrams+" grams");
            
            string stickem = (item + calories.ToString() + fatGrams.ToString());
            List<string> list1 = new List<string>();
            list1.Add(item);list1.Add(calories.ToString());list1.Add(fatGrams.ToString());
            
            
            List<string> hitGoogleWith = mostCommonSearch(list1);
            
            if (!File.Exists(@"csv/nutrix.csv"))
                {
                    File.AppendAllLines(@"csv/nutrix.csv", list1); 
                                                          
                    // or : File.WriteAllText(@"csv/nutrix.csv", stickem); but writes all in 1 line
                }
                    File.AppendAllLines(@"csv/nutrix.csv", list1);   
                }           
            
            prompt().Wait();                     
    } 
    public string mostCommonSearch(List<string> list1){
        List<string>listSearches = new List <string>(); 
        listSearches.Add(list1.ElementAt(0));
        return list1.ElementAt(0);
    }
}

        
         
        

    



        //Console.WriteLine(mashapi.ToJSON(n));  

        // IJSONAPI googapi = new GoogleAPI();   
        // Google g = await googapi.GetData<Google>("tacos", "APIKEY"); 
        // Console.WriteLine(googapi.ToJSON(g));

