using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.MessageProviders;

namespace SensorCalibrationApp.Domain.Factories
{
    internal static class MessageProviderFactory
    {
        public static IMessageProvider Create(bool isNew)
        {
            if(isNew)
                return new MessageProvider();

            return new LegacyMessageProvider();
        }
    }
}
