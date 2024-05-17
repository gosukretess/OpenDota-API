using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenDotaApi.Utilities.JsonConverters;

namespace OpenDotaApi.Utilities
{
    public sealed class JsonFormatter : IDisposable
    {
        private readonly RequestHandler _request;
        private readonly JsonSerializerOptions _options;

        public JsonFormatter(RequestHandler request)
        {
            _request = request;

            _options = new JsonSerializerOptions();
            _options.Converters.Add(new Int32Converter());
            _options.Converters.Add(new DateTimeConverter());
            _options.Converters.Add(new Int64Converter());
        }

        public async Task<T> DeserializeAsync<T>(string url, string parameters = null,
            CancellationToken? cancellationToken = null) where T : class
        {
            var response = await _request.GetResponseAsync(url, parameters, cancellationToken.GetValueOrDefault());

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"OpenDotaApi responded with unsuccessful code. " +
                                               $"\nStatusCode {(int)response.StatusCode}" +
                                               $"\nContent: {responseContent}" +
                                               $"\nRequestUri: {response.RequestMessage.RequestUri}" +
                                               $"\n");
            }

            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream, _options, cancellationToken.GetValueOrDefault());
        }

        public async Task<T> DeserializeAsync<T>(Stream json, CancellationToken? cancellationToken)
        {
            cancellationToken ??= CancellationToken.None;

            if (json == null)
                throw new NullReferenceException(nameof(json));


            return await JsonSerializer.DeserializeAsync<T>(json, _options, cancellationToken.Value);
        }


        public void Dispose()
        {
            _request?.Dispose();
        }
    }
}