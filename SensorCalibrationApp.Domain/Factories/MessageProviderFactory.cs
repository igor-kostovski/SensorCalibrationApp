using SensorCalibrationApp.Domain.Interfaces;
using SensorCalibrationApp.Domain.LinVersions.MessageProviders;

namespace SensorCalibrationApp.Domain.Factories
{
    internal static class MessageProviderFactory
    {
        public static IMessageProvider Create(bool notLegacy)
        {
            if(notLegacy)
                return new MessageProvider();

            return new LegacyMessageProvider();
        }
    }
}
