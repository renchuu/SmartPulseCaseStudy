using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartPulseCaseStudy
{
    class TGTService
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        private static readonly string _cacheKey = "TGT";
        private static IConfiguration _configuration;

        public TGTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetTGT()
        {
            if (_cache.Contains(_cacheKey))
            {
                Console.WriteLine("TGT cache'den çekildi.");
                return _cache.Get(_cacheKey) as string;
            }

            Console.WriteLine("TGT için istek gönderiliyor.");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

            var content = new FormUrlEncodedContent(
                new Dictionary<string, string> 
                {
                   { "username" , _configuration["username"] },
                   { "password" , _configuration["password"] }
                }                    
            );

            var responseContent = await client.PostAsync("https://giris.epias.com.tr/cas/v1/tickets", content);
            var TGT = await responseContent.Content.ReadAsStringAsync();

            Console.WriteLine("TGT alındı, cache'e kaydediliyor.");

            StoreTGT(TGT.ToString().Trim(), DateTime.UtcNow.AddHours(2));

            return TGT.ToString();
        }

        public static void StoreTGT(string tgt, DateTime expirationTime)
        {
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = expirationTime
            };
            _cache.Set(_cacheKey, tgt, cacheItemPolicy);
        }
    }
}
