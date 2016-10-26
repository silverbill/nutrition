using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

internal interface IJSONAPI {
    Task<string> GetJSON(string term, string key, string latlng);
    Task<T> GetData<T>(string term, string key, string latlng);
    string ToJSON(Object o);
}

internal class MashapeAPI : IJSONAPI {

    private List<string> fields = new List<string>() {
        "item_name",
        "item_id",
        "brand_name",
        "nf_calories",
        "nf_total_fat"
    };

    public string urlFormat(string term) =>
        $"https://nutritionix-api.p.mashape.com/v1_1/search/{term}?fields={String.Join(",", fields)}";

    public async Task<string> GetJSON(string term, string key, string latlng = ""){
        var http = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, urlFormat(term));
        request.Headers.Add("X-Mashape-Key", key);
        var reply = await http.SendAsync(request);//json file
        var result = await reply.Content.ReadAsStringAsync();
        return result;
    }

    public async Task<T> GetData<T>(string term, string key, string latlng = ""){
        string json = await GetJSON(term, key);
        T instance = JsonConvert.DeserializeObject<T>(json);
        return instance;
    }

    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }
}

internal class GooglePlacesAPI : IJSONAPI {
    public string urlFormat(string term, string key, string LatLng) =>
        $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={LatLng}&radius=5000&type=restaurant&keyword={term}&key={key}";

    public async Task<string> GetJSON(string term, string key, string LatLng = ""){
        var http = new HttpClient();
        var result = await http.GetStringAsync(urlFormat(term, key, LatLng));
        return result;
    }

    public async Task<T> GetData<T>(string term, string key, string LatLng = ""){
        string json = await GetJSON(term, key, LatLng);
        T instance = JsonConvert.DeserializeObject<T>(json);
        return instance;
    }

    public string ToJSON(Object o){
        return JsonConvert.SerializeObject(o);
    }   
}
