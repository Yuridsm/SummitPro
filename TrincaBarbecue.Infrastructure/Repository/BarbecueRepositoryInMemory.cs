﻿using TrincaBarbecue.Application.Repository;
using TrincaBarbecue.Core.Entity;

namespace TrincaBarbecue.Infrastructure.Repository
{
    public class BarbecueRepositoryInMemory : IBarbecueRepository<Barbecue>
    {
        private List<Barbecue> _barbecues = new List<Barbecue>();

        public void Add(Barbecue entity)
        {
            _barbecues.Add(entity);
        }

        public Barbecue? Get(Guid identifier)
        {
            return _barbecues.Find(o => o.Identifier == identifier);
        }
    }
}
