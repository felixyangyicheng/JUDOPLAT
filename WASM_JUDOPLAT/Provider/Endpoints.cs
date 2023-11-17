namespace WASM_JUDOPLAT.Provider
{
    public static class Endpoints
    {
#if DEBUG

        public static string ApiBase = $"{BaseAddress.DevSSL}/api";
#else
        public static string ApiBase = $"{BaseAddress.Protocole}{BaseAddress.Host}/api";

#endif

        public static string Pdf = $"{ApiBase}/pdf/";
        public static string PdfAll = $"{ApiBase}/pdf/all";
        public static string PdfAllSimple = $"{ApiBase}/pdf/all/simple";
    }
}
