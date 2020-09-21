using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Connection;

namespace Services.ModelMapping
{
    public class ConnectionMapper
    {
        public IEnumerable<ConnectionResponse> MapResponseFromConnections(IEnumerable<Connection> connections)
        {
            var result = new List<ConnectionResponse>();

            foreach (var c in connections)
            {
                result.Add(this.MapResponseFromConnection(c));
            }

            return result;
        }

        public ConnectionResponse MapResponseFromConnection(Connection connection)
        {
            return new ConnectionResponse()
            {
                ConnectionId = connection.Id,
                DeviceId = connection.DeviceId.Value,
                PersonId = connection.PersonId.Value
            };
        }
    }
}
