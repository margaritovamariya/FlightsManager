using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    public interface IFlightService
    {
        void Create(string from, string to, DateTime dateTimeTakeOff, 
                    DateTime dateTimeLanding, string planeType, 
                    int uniquePlaneNumber, string pilotName,
                    int passengersCapacity, int businessClassCapacity);

        IEnumerable<FlightViewModel> GetExactFlight(string from, string to, DateTime dateTimeTakeOff, 
                                                    DateTime dateTimeLanding, string planeType, int uniquePlaneNumber);

        IEnumerable<FlightViewModel> GetAllFlights();

        void UpdateFlight(string from, string to, DateTime dateTimeTakeOff, 
                          DateTime dateTimeLanding, string planeType, int uniquePlaneNumber, 
                          string pilotName, int passengersCapacity, int businessClassCapacity);

        void DeleteFlight(string from, string to, DateTime dateTimeTakeOff,
                        DateTime dateTimeLanding, string planeType, int uniquePlaneNumber);
    }
}
