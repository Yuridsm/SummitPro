﻿using TrincaBarbecue.SharedKernel.Interfaces;

namespace TrincaBarbecue.Application.UseCase.GetParticipant
{
    public class GetParticipantsInputBoundary : IInputBoundary
    {
        public IEnumerable<Guid> ParticipantIdentifiers { get; set; }
    }
}