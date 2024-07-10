using backend.BaseModule.Models.Base;

namespace backend.Beritas.Models.Request;

public class BeritaFilter : BaseFilter
{
    public BeritaOrder order { get; set; }
}