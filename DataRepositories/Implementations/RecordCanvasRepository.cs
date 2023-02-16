using AutoMapper;
using dchv_api.Database;
using dchv_api.DTOs;

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
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(_context.RecordGroup.Where((x) => x.ID == RecordGroupID).OrderByDescending((x) => x.Created_at).ToList());
        res.Records = _mapper.Map<List<RecordDTO>>(_context.Record.Where((x) => x.RecordGroupID == RecordGroupID).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public Task<RecordCanvasDTO>? GetDirectoryContentAsync(uint RecordGroupID)
    {
        throw new NotImplementedException();
    }

    public RecordCanvasDTO? GetRootDirectoryContent()
    {
        RecordCanvasDTO res = new RecordCanvasDTO();
        res.Groups = _mapper.Map<List<RecordGroupDTO>>(_context.RecordGroup.Where((x) => x.RecordGroupID == null).OrderByDescending((x) => x.Created_at).ToList());
        res.Records = _mapper.Map<List<RecordDTO>>(_context.Record.Where((x) => x.RecordGroupID == null).OrderByDescending((x) => x.Created_at).ToList());
        return res;
    }

    public Task<RecordCanvasDTO>? GetRootDirectoryContentAsync()
    {
        throw new NotImplementedException();
    }
}