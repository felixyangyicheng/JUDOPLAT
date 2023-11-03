using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static MudBlazor.CategoryTypes;
using System.Net;
using static System.Net.WebRequestMethods;

namespace WASM_JUDOPLAT.Services
{
    public class PdfRepo : BaseRepo, IPdfRepo 
    {
        public PdfRepo(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(client, localStorage, authenticationStateProvider, httpContextAccessor, config)
        {
        }

        public Task<bool> CreateAsync(PdfModel pdfModel)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PdfModel>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PdfModel?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<PdfModel> GetDataAsync()
        {
            var serializer = new JsonSerializer();
    
            using (var stream = await _client.GetStreamAsync(Endpoints.PdfAll))
            {
                using (var sr = new StreamReader(stream))
                using (var jr = new JsonTextReader(sr))
                {
                    while (await jr.ReadAsync())
                    {
                        if (jr.TokenType != JsonToken.StartArray && jr.TokenType != JsonToken.EndArray)
                        {
                            yield return serializer.Deserialize<PdfModel>(jr);
                        }
                    };
                }
            }
        }

        public async IAsyncEnumerable<PdfModelSimple> GetSimpleDataAsync()
        {
            var serializer = new JsonSerializer();

            using (var stream = await _client.GetStreamAsync(Endpoints.PdfAllSimple))
            {
                using (var sr = new StreamReader(stream))
                using (var jr = new JsonTextReader(sr))
                {
                    while (await jr.ReadAsync())
                    {
                        if (jr.TokenType != JsonToken.StartArray && jr.TokenType != JsonToken.EndArray)
                        {
                            yield return serializer.Deserialize<PdfModelSimple>(jr);
                        }
                    };
                }
            }
        }

        public Task RemoveAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string id, PdfModel pdfModel)
        {
            throw new NotImplementedException();
        }
    }
}
