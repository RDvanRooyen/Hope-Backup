using BlazorTable.Components.ServerSide;
using System.Threading.Tasks;

namespace BlazorTable.Interfaces
{
    public interface IDataLoader<T>
    {
        public Task<PaginationResult<T>> LoadDataAsync(FilterData parameters);
    }
}
