﻿using MediatR;
using SummitPro.Application.UseCase.CreateComponent;

namespace SummitPro.Application.Query.Handler
{
    public class ListComponentHandler : IRequestHandler<ListComponenteQuery, IEnumerable<CreateComponentOutputBoundary>>
    {
        private readonly IGateway<string> _gateway;

        public ListComponentHandler(IGateway<string> gateway)
        {
            _gateway = gateway;
        }

        public Task<IEnumerable<CreateComponentOutputBoundary>> Handle(ListComponenteQuery request, CancellationToken cancellationToken)
        {
            var output = _gateway.GetAll();
            var tokens = new Dictionary<string, string>();

            foreach (var token in output)
            {
                var proprieties = token.Split(';');
                tokens.Add(proprieties[0], proprieties[1]);
            }

            var result = from item in tokens
                      select new CreateComponentOutputBoundary
                      {
                          Name = item.Key,
                          Description = item.Value
                      };

            return Task.FromResult(result);
        }
    }
}