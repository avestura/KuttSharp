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

        private const string KuttDefaultServer = "https://kutt.it";

        private const string SubmitUrlShape   = "{0}/api/url/submit";
        private const string GetAllUrlShape   = "{0}/api/url/geturls";
        private const string DeleteUrlShape   = "{0}/api/url/deleteurl";
        private const string GetStatsUrlShape = "{0}/api/url/stats";

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            Converters = { new IsoDateTimeConverter() }
        };

        private Uri SubmitUri   => new Uri(string.Format(SubmitUrlShape,   KuttServer.OriginalString));
        private Uri GetAllUri   => new Uri(string.Format(GetAllUrlShape,   KuttServer.OriginalString));
        private Uri DeleteUri   => new Uri(string.Format(DeleteUrlShape,   KuttServer.OriginalString));
        private Uri GetStatsUri => new Uri(string.Format(GetStatsUrlShape, KuttServer.OriginalString));

        /// <summary>
        /// Determines the server that the requests send to
        /// </summary>
        public Uri KuttServer { get; }

        public string ApiKey { get; }

        /// <summary>
        /// Creates an Api which communicates with the default Kutt server
        /// </summary>
        /// <param name="apiKey"></param>
        public KuttApi(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            KuttServer = new Uri(KuttDefaultServer);
            if (!client.DefaultRequestHeaders.Contains("X-API-Key"))
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        }

        /// <summary>
        /// Creates an Api which communicates with a self-hosted Kutt server
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="server">Determines the server that the requests send to</param>
        public KuttApi(string apiKey, Uri server)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            KuttServer = server ?? throw new ArgumentNullException(nameof(server));
            if (!client.DefaultRequestHeaders.Contains("X-API-Key"))
                client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        }

        /// <summary>
        /// Creates an Api which communicates with a self-hosted Kutt server
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="server">Determines the server that the requests send to</param>
        public KuttApi(string apiKey, string server)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            KuttServer = new Uri(server);
            if (!client.DefaultRequestHeaders.Contains("X-API-Key"))
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

            var response = await client.PostAsync(SubmitUri, body).ConfigureAwait(false);

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
            var response = await client.GetAsync(GetAllUri).ConfigureAwait(false);

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

            var response = await client.PostAsync(DeleteUri, body).ConfigureAwait(false);

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
            var requestUrl = $"{GetStatsUri}?id={id}";
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
