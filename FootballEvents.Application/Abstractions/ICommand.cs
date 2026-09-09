using MediatR;

namespace FootballEvents.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
