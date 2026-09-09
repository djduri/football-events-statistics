using MediatR;

namespace FootballEvents.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
