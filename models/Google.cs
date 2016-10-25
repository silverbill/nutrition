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
public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
}

public class Result
{
    public Geometry geometry { get; set; }
    public string name { get; set; }
    public double rating { get; set; }
}

public class Google
{
    public List<Result> results { get; set; }
}

  public class googler {
      string name;
      string apiCalls;
      string LatLng;
     
      public googler(){

      }
     
    public static async Task promptGoogle(string hitGoogleWith){
        Console.WriteLine("Enter your zipcode");
        string input = Console.ReadLine();

        string Googlekey = "AIzaSyAfdlKioHIQ6X06IfAoNa22KtU1t35Xd_A";
        string result = await Prompter.getUrl("https://maps.googleapis.com/maps/api/geocode/json?address="+input+"="+Googlekey);
        Google g = JsonConvert.DeserializeObject<Google>(result);
        double lat = g.results.ElementAt(0).geometry.location.lat;
        double lng = g.results.ElementAt(0).geometry.location.lng;
        string LatLng = (lat.ToString()+","+lng.ToString());

        IJSONAPI googapi = new GooglePlacesAPI();  
        
        Google place = await googapi.GetData<Google>(hitGoogleWith, Googlekey, LatLng);
        Console.WriteLine($"Here's a location nearby that serves your favorite search term:");
        // Console.WriteLine(googapi.ToJSON(place));
        if(place.results.Count > 0){
            Console.WriteLine(place.results[0].name);
        }
    }      
  }