namespace WASM_JUDOPLAT.Provider
{
    public static class Endpoints
    {
        public static string ApiBase = $"{BaseAddress.DevSSL}/api";
        public static string Pdf = $"{ApiBase}/pdf/";
        public static string PdfAll = $"{ApiBase}/pdf/all";
        public static string PdfAllSimple = $"{ApiBase}/pdf/all/simple";
    }
}
