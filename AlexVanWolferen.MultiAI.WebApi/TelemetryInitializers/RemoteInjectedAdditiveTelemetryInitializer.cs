using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexVanWolferen.MultiAI.WebApi.TelemetryInitializers
{
    public class RemoteInjectedAdditiveTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;        


        public RemoteInjectedAdditiveTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (_httpContextAccessor.HttpContext == null) return;

            string remoteAI = _httpContextAccessor.HttpContext.Request.Headers["ai-callback"];
            if (!string.IsNullOrWhiteSpace(remoteAI) && telemetry.Context.InstrumentationKey != remoteAI)
            {
                var telemetryClone = telemetry.DeepClone();
                telemetryClone.Context.InstrumentationKey = remoteAI;

                TelemetryConfiguration.CreateDefault().TelemetryChannel.Send(telemetryClone);
            }
        }
    }
}
