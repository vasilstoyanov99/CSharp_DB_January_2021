using AutoMapper;

using CarDealer.DataTransferObjects.ExportModels;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<Car, CarWithDistanceEM>();
        }
    }
}
