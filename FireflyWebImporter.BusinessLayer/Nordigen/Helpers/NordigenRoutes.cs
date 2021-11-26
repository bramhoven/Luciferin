namespace FireflyWebImporter.BusinessLayer.Nordigen.Stores
{
    public static class NordigenRoutes
    {
        public static string NewToken(string baseUrl) => $"{baseUrl}/api/v2/token/new/";
    }
}