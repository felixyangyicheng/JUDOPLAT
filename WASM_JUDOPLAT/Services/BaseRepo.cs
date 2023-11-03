using Microsoft.AspNetCore.Http;
using System.Net;


namespace WASM_JUDOPLAT.Services
{
    public class BaseRepo
    {
        protected readonly HttpClient _client;
        protected readonly ILocalStorageService _localStorage;
        protected readonly IHttpContextAccessor _httpContextAccessor;
    
        protected readonly IConfiguration _config;
        protected readonly AuthenticationStateProvider _authenticationStateProvider;

        public BaseRepo(HttpClient client,
            ILocalStorageService localStorage,
         AuthenticationStateProvider authenticationStateProvider,

        IHttpContextAccessor httpContextAccessor,

        IConfiguration config
            )
        {
            _client = client;
            _client.DefaultRequestVersion = HttpVersion.Version20;
            _client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
            _localStorage = localStorage;
            _httpContextAccessor = httpContextAccessor;

            _authenticationStateProvider = authenticationStateProvider;
            _config = config;
        }

        protected async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
