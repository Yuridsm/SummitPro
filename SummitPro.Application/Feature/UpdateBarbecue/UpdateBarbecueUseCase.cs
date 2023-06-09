﻿using MediatR;

using SummitPro.Application.Feature.UpdateBarbecue;
using SummitPro.Application.Interface;

namespace SummitPro.Application.UseCase.UpdateBarbecue
{
    public class UpdateBarbecueUseCase : IUpdateBarbecueUseCase
    {
        private readonly IMediator _mediator;

        public UpdateBarbecueUseCase(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task Execute(UpdateBarbecueInputBoundary inputBoundary)
        {
            var model = new UpdateBarbecueCommandModel
            {
                BarbecueIdentifier = inputBoundary.BarbecueIdentifier,
                AdditionalMarks = inputBoundary.AdditionalMarks,
                BeginDate = inputBoundary.BeginDate,
                EndDate = inputBoundary.EndDate,
                Description = inputBoundary.Description
            };

            var command = new UpdateBarbecueCommand(model);

            await _mediator.Send(command);
        }
    }
}
