using MediatR;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}