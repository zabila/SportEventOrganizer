using MediatR;
using Sportalytics.Feed.Domain.Shared;

namespace Sportalytics.Feed.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>, IRequest<Result<TResponse>>
{

}