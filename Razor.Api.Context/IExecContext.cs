using Razor.Api.Context.Models;

namespace Razor.Api.Context
{
    public interface IExecContext
    {
        ContextData Data { get; }
    }
}
