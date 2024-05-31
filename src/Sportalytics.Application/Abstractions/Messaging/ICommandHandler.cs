﻿using MediatR;
using Sportalytics.Domain.Shared;

namespace Sportalytics.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{

}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<Result<TResponse>>>
    where TCommand : ICommand<TResponse>
{

}