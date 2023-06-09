﻿using AutoMapper;
using SummitPro.Application.Repository;
using SummitPro.Core.Aggregate.Participant;
using SummitPro.Infrastructure.RepositoryInMemory.Models;

namespace SummitPro.Infrastructure.RepositoryInMemory
{
    public class ParticipantRepositoryInMemory : IParticipantRepository
    {
        private List<ParticipantModel> _participants = new();
        private Dictionary<Guid, List<string>> _items = new();
        private readonly IMapper _mapper;

        public ParticipantRepositoryInMemory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Add(Participant entity)
        {
            var model = _mapper.Map<ParticipantModel>(entity);

            if (model.Items.Any()) FillItems(model);

            _participants.Add(model);
        }

        public Participant? Find(Predicate<Participant> action)
        {
            ParticipantModel? model = _participants.FirstOrDefault(m => action(_mapper.Map<Participant>(m)));

            if (model == null) return null;

            PreAssemblyItems(model);

            return _mapper.Map<Participant>(model);
        }

        public Participant? Get(Guid identifier)
        {
            var model = _participants.Find(o => o.Identifier == identifier);

            if (model == null) return null;

            PreAssemblyItems(model);

            return _mapper.Map<Participant>(model);
        }

        public IEnumerable<Participant> GetByIdentifiers(IEnumerable<Guid> identifier)
        {
            List<ParticipantModel> models = _participants.FindAll(o => identifier.Contains(o.Identifier));
            var result = new List<Participant>();

            foreach (ParticipantModel model in models)
            {
                PreAssemblyItems(model);
                result.Add(_mapper.Map<Participant>(model));
            }

            return result.AsEnumerable();
        }

        public void Update(Participant entity)
        {
            var participant = _participants.FirstOrDefault(o => o.Identifier == entity.Identifier);


            if (participant != null)
            {
                PreAssemblyItems(participant);

                var identifier = participant.Identifier;

                participant = _mapper.Map<ParticipantModel>(entity);

                participant.Identifier = identifier;
            }
        }

        private void FillItems(ParticipantModel model)
        {
            if (model == null) return;

            if (_items.ContainsKey(model.Identifier))
            {
                var elements = _items[model.Identifier];

                elements
                    .ToList()
                    .AddRange(model.Items);

                _items[model.Identifier] = elements;
            }
            else if (model.Items.Any())
            {
                _items.Add(model.Identifier, model.Items);
            }
        }

        private void PreAssemblyItems(ParticipantModel model)
        {
            if (model == null) return;

            if (_items.ContainsKey(model.Identifier))
                model.Items = _items[model.Identifier];
        }

        public IEnumerable<Participant>? GetAll()
        {
            if (_participants == null) return Enumerable.Empty<Participant>();

            var participants = _participants.FindAll(o => o.Identifier != Guid.Empty);

            if (!participants.Any()) return Enumerable.Empty<Participant>();

            var output = new List<Participant>();

            foreach (var participant in participants)
            {
                PreAssemblyItems(participant);
                output.Add(_mapper.Map<Participant>(participant));
            }

            return output;
        }
    }
}
