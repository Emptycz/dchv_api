using AutoMapper;
using dchv_api.Database;
using dchv_api.DTOs;
using dchv_api.Models;
using dchv_api.RequestModels;

namespace dchv_api.DataRepositories.Implementations;

public class RecordCanvasRepository : IRecordCanvasRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogger<RecordCanvasRepository> _logger;
    private readonly IMapper _mapper;

    public RecordCanvasRepository(
        ILogger<RecordCanvasRepository> logger,
        BaseDbContext dbContext,
        IMapper mapper
    )
    {
        _logger = logger;
        _context = dbContext;
        _mapper = mapper;
    }

    public RecordCanvasDTO? GetDirectoryContent(uint RecordGroupID)
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(_context.RecordGroup.Where((x) => x.ID == RecordGroupID && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        res.Records = _mapper.Map<List<RecordDTO>>(_context.Record.Where((x) => x.RecordGroupID == RecordGroupID && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public RecordCanvasDTO? GetRootDirectoryContent()
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(_context.RecordGroup.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        res.Records = _mapper.Map<List<RecordDTO>>(_context.Record.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public RecordCanvasDTO? GetSharedRootDirectoryContent()
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(_context.Record.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        res.Records = _mapper.Map<List<RecordDTO>>(_context.Record.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public RecordCanvasDTO? GetRootDirectoryContent(RecordCanvasRequest? filter)
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        var Rctx = _context.Record.AsQueryable();
        Rctx = _prepareRecordFilteredQuery(Rctx, filter);
        res.Records = _mapper.Map<List<RecordDTO>>(Rctx.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());

        var RGctx = _context.RecordGroup.AsQueryable();
        RGctx = _prepareRecordGroupFilteredQuery(RGctx, filter);
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(RGctx.Where((x) => x.RecordGroupID == null && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public RecordCanvasDTO? GetDirectoryContent(uint RecordGroupID, RecordCanvasRequest? filter)
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        var Rctx = _context.Record.AsQueryable();
        Rctx = _prepareRecordFilteredQuery(Rctx, filter);
        res.Records = _mapper.Map<List<RecordDTO>>(Rctx.Where((x) => x.ID == RecordGroupID && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());

        var RGctx = _context.RecordGroup.AsQueryable();
        RGctx = _prepareRecordGroupFilteredQuery(RGctx, filter);
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(RGctx.Where((x) => x.RecordGroupID == RecordGroupID && x.Deleted_at == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    private IQueryable<Record> _prepareRecordFilteredQuery(IQueryable<Record> ctx, RecordCanvasRequest? filter)
    {
      if (filter?.PersonID is not null)
      {
        ctx = ctx.Where((x) => x.PersonID == filter.PersonID);
      }

      if (filter?.Shared == true)
      {
        // ctx = ctx.Where((x) => x.SharedPersonGroups!.Where((y) => y. == x.RecordGroupID).Count() > 0);
      }

      return ctx;
    }

    private IQueryable<RecordGroup> _prepareRecordGroupFilteredQuery(IQueryable<RecordGroup> ctx, RecordCanvasRequest? filter)
    {
      if (filter?.PersonID is not null)
      {
        ctx = ctx.Where((x) => x.PersonID == filter.PersonID);
      }

      if (filter?.Shared == true)
      {
        // ctx = ctx.Where((x) => x.SharedPersonGroups!.Where((y) => y.RecordGroupID == x.RecordGroupID).Count() > 0);
      }

      return ctx;
    }
}