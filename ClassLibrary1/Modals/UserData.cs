namespace ClassLibrary1;

public class RootObject
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int TotalPages { get; set; }
    public List<Data> data { get; set; } = new List<Data>();
    public Support support { get; set; }
}

public class Data
{
    public int id { get; set; }
    public string email { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string avatar { get; set; }
}

public class Support
{
    public string url { get; set; }
    public string text { get; set; }
}




