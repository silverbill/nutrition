using System;
using System.Collections.Generic;

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