namespace WASM_JUDOPLAT.Provider
{
    /// <summary>
    /// Class de configuration pour les URL base d'api
    /// </summary>
    public static class BaseAddress
    {

#if DEBUG

        public static string Dev = "http://localhost:8081";
        public static string DevSSL = "https://localhost:8081";

#else
        public static string Protocole ="https://";
        public static string Host = "judo-univ-rennes.duckdns.org";
        public static string Port  = null;
#endif


    }
}
