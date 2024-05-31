using MediatR;
using Sportalytics.Domain.Shared;

namespace Sportalytics.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}