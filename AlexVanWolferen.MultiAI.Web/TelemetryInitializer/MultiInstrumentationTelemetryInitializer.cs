using Microsoft.ApplicationInsights.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexVanWolferen.MultiAI.Web.TelemetryInitializer
{
    public class MultiInstrumentationTelemetryInitializer : Microsoft.ApplicationInsights.Extensibility.ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (HttpContext.Current != null && HttpContext.Current.Items.Contains("ai_preferred"))
            {
                var preferredAI = HttpContext.Current.Items["ai_preferred"];
                if (preferredAI.GetType() == typeof(int))
                {
                    telemetry.Context.InstrumentationKey = System.Configuration.ConfigurationManager.AppSettings[(int)preferredAI == 1 ? "ai_primary" : "ai_secondary"];
                }
            }
        }
    }
}