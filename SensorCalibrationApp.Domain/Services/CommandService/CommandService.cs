using System;
using System.Threading.Tasks;

namespace SensorCalibrationApp.Domain.Services.CommandService
{
    public class CommandService : ICommandService
    {
        public async Task ReadById()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFrameId(byte newFrameId)
        {
            throw new NotImplementedException();
        }

        public async Task SendDeviceSpecificFrame()
        {
            throw new NotImplementedException();
        }
    }
}
