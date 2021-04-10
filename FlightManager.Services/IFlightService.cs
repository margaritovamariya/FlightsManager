using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Services.Models.OutputModels;

namespace FlightManager.Services
{
    /// <summary>
    /// FlighteService interface
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Calls Create method from FlightServices
        /// </summary>
        /// <param name="model"></param>
        void Create(FlightViewModel model);

        /// <summary>
        /// Call GetExactFlight method from FlightServices
        /// </summary>
        /// <param name="uniquePlaneNumber"></param>
        /// <returns> Flight by given UniquePlanenumber </returns>
        IEnumerable<FlightViewModel> GetExactFlight(int uniquePlaneNumber);

        /// <summary>
        /// Call GetAllFlights method from FlightServices
        /// </summary>
        /// <returns>All Flights</returns>
        IEnumerable<FlightViewModel> GetAllFlights();
    }
}
