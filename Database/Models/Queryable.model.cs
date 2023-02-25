namespace dchv_api.Database.Models;

public interface IQueryableRequest
{
    int? Skip { get; set; }
    int? Limit { get; set; }

    // FIXME: This should be actually object { param: value }, not string
    // string? Like { get; set; }
}

public class QueryableRequest : IQueryableRequest
{
    public int? Skip { get; set; }
    public int? Limit { get; set; }
    // public string? Like { get; set; }
}
