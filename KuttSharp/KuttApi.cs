using KuttSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace KuttSharp
{
    /// <summary>
    /// Provides an interface for calling Kutt.it url shortening services
    /// </summary>
    public class KuttApi
    {
        private static readonly HttpClient client = new HttpClient();

        private const string SubmitUrl = "https://kutt.it/api/url/submit";
        private const string GetAllUrl = "https://kutt.it/api/url/geturls";
        private const string DeleteUrl = "https://kutt.it/api/url/deleteurl";
        private const string GetStatsUrl = "https://kutt.it/api/url/stats";

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            Converters = { new IsoDateTimeConverter() }
        };

        public string ApiKey { get; }

        public KuttApi(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        }

        /// <summary>
        /// Submit a link to be shortened
        /// </summary>
        /// <param name="target">Original long URL to be shortened</param>
        /// <param name="customUrl">Set a custom URL</param>
        /// <param name="password">Set a password</param>
        /// <param name="reuse">If a URL with the specified target exists returns it, otherwise will send a new shortened URL. Default is false.</param>
        /// <returns>Submitted url as a <see cref="KuttUrl"/></returns>
        public async Task<KuttUrl> SubmitAsync(string target,
            string customUrl = "",
            string password = "",
            bool reuse = false)
        {
            var values = new Dictionary<string, string>
            {
                ["target"] = target,
                ["reuse"] = reuse.ToString()
            };

            if (customUrl?.Length > 0)
                values.Add("customUrl", customUrl);

            if (password?.Length > 0)
                values.Add("password", password);

            var body = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(SubmitUrl, body).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<KuttUrl>(responseString, jsonSettings);
            }
            else
            {
                try
                {
                    var errMessage = JsonConvert.DeserializeObject<KuttError>(responseString).Error;
                    throw new KuttException(errMessage);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Get the shortened URLs list
        /// </summary>
        /// <returns>List of URL objects</returns>
        public async Task<List<KuttUrl>> GetUrlsAsync()
        {
            var response = await client.GetAsync(GetAllUrl).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<KuttUrlList>(responseString, jsonSettings).Urls;
            }
            else
            {
                try
                {
                    var errMessage = JsonConvert.DeserializeObject<KuttError>(responseString).Error;
                    throw new KuttException(errMessage);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Delete a shortened URL
        /// </summary>
        /// <param name="id">ID of the shortened URL</param>
        /// <param name="domain"> Required if a custom domain is used for short URL</param>
        public async Task DeleteAsync(string id, string domain = "")
        {
            var values = new Dictionary<string, string>
            {
                ["id"] = id
            };

            if (domain?.Length > 0)
                values.Add("domain", domain);

            var body = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(DeleteUrl, body).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var errMessage = JsonConvert.DeserializeObject<KuttError>(responseString).Error;
                    throw new KuttException(errMessage);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Get stats for a shortened URL
        /// </summary>
        /// <param name="id">ID of the shortened URL</param>
        /// <param name="domain"> Required if a custom domain is used for short URL</param>
        public async Task<UrlStats> GetStatsAsync(string id, string domain = "")
        {
            var requestUrl = $"{GetStatsUrl}?id={id}";
            requestUrl += (domain.Length > 0) ? $"&domain={domain}" : "";

            var response = await client.GetAsync(requestUrl).ConfigureAwait(false);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UrlStats>(responseString);
            }
            else
            {
                try
                {
                    var errMessage = JsonConvert.DeserializeObject<KuttError>(responseString).Error;
                    throw new KuttException(errMessage);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
