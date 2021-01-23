using System.Collections.Generic;
using Airtraffic.Domain.Models;
using Airtraffic.Domain.Repositories;
using Airtraffic.Domain.Services;
using Airtraffic.Services;
using NSubstitute;
using Xunit;

namespace Airtraffic.Tests
{
    public class AircraftServiceTest
    {
		private const int notValidId = 5678;
		private const int validId = 50;

		private IAircraftRepository<Airplane> _aircraftRepositoryMock;
		private IUnitOfWork _unitOfWorkMock;
		private IAirportRepository _airportRepositoryMock;

		private IAircraftService<Airplane> _aircraftService;

		private void SetUpMocksAndServices()
        {
			_aircraftRepositoryMock = Substitute.For<IAircraftRepository<Airplane>>();
			_unitOfWorkMock = Substitute.For<IUnitOfWork>();
			_airportRepositoryMock = Substitute.For<IAirportRepository>();
			_aircraftService = new AircraftService<Airplane>(_aircraftRepositoryMock, _unitOfWorkMock, _airportRepositoryMock);
		}

        [Fact]
		public async void GetAirplane_IdDoesNotExist_ShouldFail()
		{
			SetUpMocksAndServices();
			var result = await _aircraftService.FindById(notValidId);

			Assert.False(result.Success);
		}

		[Fact]
		public async void GetAirplane_IdDoesExist_ShouldSucceed()
		{
			SetUpMocksAndServices();
			_aircraftRepositoryMock.Read(validId).Returns(new Airplane());
			var result = await _aircraftService.FindById(validId);

			Assert.True(result.Success);
		}
	}
}
