using backend.BaseModule.Interfaces.Repositories;
using backend.Entities;
using backend.Helpers;

namespace backend.BaseModule.Repositories;

public class MediaRepo(
        DataContext context
    ) : IMediaRepo
{
    public readonly DataContext _context = context;

    public async Task CreateMedia(Media media)
    {
        await _context.Medias.AddAsync(media);
        await _context.SaveChangesAsync();
    }
}