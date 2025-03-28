using Football.Common.Domain;
using MediatR;

namespace Football.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
