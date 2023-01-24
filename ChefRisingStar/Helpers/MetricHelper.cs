using ChefRisingStar.Models;
using Xamarin.Forms;

namespace ChefRisingStar.Helpers
{
    internal class MetricHelper
    {
        private const string Route = "api/UserMetrics/";
        public static void SendMetric(AppMetric metric)
        {
            RestHelper helper = DependencyService.Get<RestHelper>();
            helper.Post(metric, Route);
        }

        public static void SendMetric(MetricType metricType, int userId, string value)
        {
            AppMetric metric = new AppMetric(metricType, userId, value);
            SendMetric(metric);
        }
    }
}
