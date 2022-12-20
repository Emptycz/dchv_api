using dchv_api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace dchv_api.DataHelpers;

public static class QueryableHelper<T>
{
  public static IQueryable<T> ApplyQuery(IQueryable<T> ctx, IQueryableRequest? filter)
  {
      if (!(filter?.Skip is null) && filter.Skip > 0)
      {
        ctx = ctx.Skip((int) filter.Skip);
      }

      if (!(filter?.Limit is null) && filter.Limit > 0)
      {
        ctx = ctx.Take((int) filter.Limit);
      }
      return ctx;
  }
}