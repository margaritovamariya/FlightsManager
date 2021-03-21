using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        void Create(string firstName, string secondName, string familyName, long pin, string telephoneNumber, string nationality,string ticketType);

        void Read();

        void Update();

        void Delete();
    }
}
