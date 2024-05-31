using MediatR;
using Sportalytics.Domain.Shared;

namespace Sportalytics.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>, IRequest<Result<TResponse>>
{

}