using Xunit;

public class SimpleTests {

    // public void TestingAdd() => 
    //     Assert.Equal(10, Calculator.Add(5,5));

    // public void TestingAdd2() => 
    //     Assert.Equal(16, Calculator.Add(5,11));

    /*
        other Assert methods:
        Equal(a,b)
        True(a)
        False(a)
    */

    public void CreateNutrition() {
        Nutrition n = new Nutrition();
        Assert.Equal(n.total_hits, 0);
        Assert.Equal(n.total_hits, 1);
    }
    
    public void getAPIUrl(){
        MashapeAPI m = new MashapeAPI();
        Assert.True(m.urlFormat("TACOS") != "");
    }

    public void checkJSON(){
        MashapeAPI m1 = new MashapeAPI();
        Assert.True(m1.GetJSON
        ("https://nutritionix-api.p.mashape.com/v1_1/search/{term}?fields",
        "8H6stHhT25mshXH1okEaCywiRiCUp1DYIsxjsnyespJHYCy7ca") != null);
        Assert.True(m1.urlFormat("") != "");
        
    }

    public void HasProperty()
    {
        Nutrition n1 = new Nutrition();
        Assert.True(n1.total_hits >= 0);
        Assert.True(n1.hits.Count >= 0);
        Assert.False(n1.total_hits >= 1);
        Assert.True(n1.hits != null);
        
    }
    
    
            

}