namespace ConsumirAPI.Models;

public class Manga
{
    public Datum[]? data { get; set; }
    public Meta? meta { get; set; }
}

public class Meta
{
    public int count { get; set; }
}

public class Datum
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public Attributes? Attributes { get; set; }
}

public class Attributes
{
    // TITULO DO MANGA
    public string? slug { get; set; }
    public string? description { get; set; }
    public Titles? titles { get; set; }
    public string? canonicalTitle { get; set; }
    public string? startDate { get; set; }
    public string? endDate { get; set; }
    public string? subtype { get; set; }
    public string? status { get; set; }
    public Posterimage? posterImage { get; set; }
    public Coverimage? coverImage { get; set; }
    public int? chapterCount { get; set; }
    public int? volumeCount { get; set; }
    public string? serialization { get; set; }
    public string? mangaType { get; set; }
}

public class Titles
{
    public string? en_jp { get; set; }
    public string? en_kr { get; set; }
    public string? en_us { get; set; }
}

public class Posterimage
{
    public string? original { get; set; }
}

public class Coverimage
{
    public string? original { get; set; }
}