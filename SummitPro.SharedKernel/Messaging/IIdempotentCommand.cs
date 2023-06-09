﻿namespace SummitPro.SharedKernel.Messaging;

public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
{
	Guid RequestId { get; set; }
}
