using Microsoft.AspNetCore.SignalR;

namespace Early_warning.Hubs
{
    public class SensorHub : Hub
    {
        public async Task SendSensorData(SensorData data)
        {
            await Clients.All.SendAsync("ReceiveSensorData", data);
        }

        public async Task SendAlert(string alertMessage)
        {
            await Clients.All.SendAsync("ReceiveAlert", alertMessage);
        }
    }
}