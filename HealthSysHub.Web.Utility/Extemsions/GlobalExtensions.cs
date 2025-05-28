
namespace HealthSysHub.Web.Utility.Extemsions
{
    public static class GlobalExtensions
    {
        public static string GenerateOrderReference(this Guid requestId,string type)
        {
            // Remove hyphens and take first 8 characters of GUID
            var guidPart = requestId.ToString("N").Substring(0, 8).ToUpper();

            // Current timestamp in compact format (YYYYMMDDHHmmss)
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            // Combine into a readable format
            return $"{type}-{timestamp}-{guidPart}";
            // Example: "ORD-20230515143045-ABC123D4"
        }
    }
}
