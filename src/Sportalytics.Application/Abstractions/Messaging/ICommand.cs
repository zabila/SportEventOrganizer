using MediatR;
using Sportalytics.Domain.Shared;

namespace Sportalytics.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<Result<TResponse>>>
{

}