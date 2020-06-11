using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexVanWolferen.MultiAI.Web.TelemetryInitializer
{
    public class MultiInstrumentationTelemetryInitializer : ITelemetryInitializer
    {
        private TelemetryClient _telemetryClient = new TelemetryClient();

        public void Initialize(ITelemetry telemetry)
        {
            if (HttpContext.Current != null && HttpContext.Current.Items.Contains("ai_preferred"))
            {
                var preferredAI = HttpContext.Current.Items["ai_preferred"];
                if (preferredAI.GetType() == typeof(int))
                {
                    int aiOptions = (int)preferredAI;

                    if (aiOptions > 0)
                    {
                        telemetry.Context.InstrumentationKey = System.Configuration.ConfigurationManager.AppSettings[aiOptions == 1 ? "ai_primary" : "ai_secondary"];
                    }
                    else if (telemetry.Context.InstrumentationKey == System.Configuration.ConfigurationManager.AppSettings["ai_primary"])
                    {
                        var telemetryClone = telemetry.DeepClone();
                        telemetryClone.Context.InstrumentationKey = System.Configuration.ConfigurationManager.AppSettings["ai_secondary"];
                        _telemetryClient.Track(telemetryClone);
                    }
                }
            }
        }
    }
}