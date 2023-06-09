﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SummitPro.Application.DependencyInjection;
using SummitPro.Application.Interface;
using SummitPro.Application.Repository;
using SummitPro.Application.UseCase.CreateBarbecue;
using SummitPro.Application.UseCase.GetBarbecueById;
using SummitPro.Infrastructure.DependencyInjector;
using SummitPro.Infrastructure.RepositoryInMemory;

namespace SummitPro.Test.Integration
{
    [TestFixture]
    public class GetByIdBarbecueTest
    {
        private IMediator _mediator = null!;
        private ServiceProvider _serviceProvider = null!;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IBarbecueRepository, BarbecueRepositoryInMemory>();
            services.AddMediator();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddInfrastructureInMemory();
            services.AddUseCase();

            _serviceProvider = services.BuildServiceProvider();
            _mediator = _serviceProvider.GetRequiredService<IMediator>();
        }

        [Test]
        public async Task GetBarbecueById()
        {
            // Arrange
            var createBarbecue = _serviceProvider.GetRequiredService<ICreateBarbecueUseCase>();
            var barbecue = _serviceProvider.GetRequiredService<IGetBarbecueByIdUseCase>();

            var additional = new List<string>
            {
                "Description 001",
                "Description 002",
                "Description 003",
            };

            var barbacueInstance = new CreateBarbecueInputBoundary
            {
                Description = "Description 01",
                AdditionalObservations = additional,
                BeginDate = DateTime.Parse("26/05/2025 01:00:00 -3:00"),
                EndDate = DateTime.Parse("26/05/2025 05:42:00 -3:00"),
            };

            var insertedbarbecue = await createBarbecue.Execute(barbacueInstance);

            var input = new GetBarbecueByIdInputBoundary
            {
                BarbecueIdentifier = insertedbarbecue.BarbecueIdentifier
            };

            // Act
            GetBarbecueByIdOutputBoundary output = await barbecue.Execute(input);

            // Assert
            Assert.That(insertedbarbecue.BarbecueIdentifier, Is.EqualTo(output.BarbecueIdentifier));
        }
    }
}
