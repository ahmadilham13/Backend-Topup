
using backend.Entities;

namespace backend.BaseModule.Interfaces.Repositories;

public interface IMediaRepo
{
    Task CreateMedia(Media media);

}