using MediatR;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<Result<TResponse>>>
{

}