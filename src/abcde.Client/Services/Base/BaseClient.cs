using abcde.Model.Exceptions;
using Serilog;
using System.Net.Http.Json;
using System.Text.Json;

namespace abcde.Client.Services.Base
{
    public class BaseClient : IBaseClient
    {
        protected HttpClient httpClient;

        public BaseClient(HttpClient client)
        {
            httpClient = client;
        }

        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string uri) where T : new()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(uri);
            }
            catch (HttpRequestException ex) // Non success
            {
                Log.Error(ex.Message, ex);
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Log.Error("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Log.Error("Invalid JSON.");
            }

            return default;
        }

        /// <summary>
        /// Get Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetIEnumerableAsync<T>(string uri)
        {
            try
            {
                //var check = await httpClient.GetStringAsync(uri);

                return await httpClient.GetFromJsonAsync<IEnumerable<T>>(uri);
            }
            catch (HttpRequestException ex) // Non success
            {
                Log.Error(ex.Message);
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");

                Log.Error(ex.Message);
            }
            catch (JsonException ex) // Invalid JSON
            {
                Log.Error(ex.Message);

                Console.WriteLine("Invalid JSON.");
            }

            return default;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string uri, T entity)
        {
            try
            {
                // Use to check what is being serialized and sent
                //string json = JsonSerializer.Serialize(entity);

                var response = await httpClient.PostAsJsonAsync(uri, entity);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TR> PostAsync<T, TR>(string url, T entity)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(url, entity);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                return await response.Content.ReadFromJsonAsync<TR>();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string uri, T entity)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(uri, entity);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    throw new ClientException(errorResponse);
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                throw;
            }
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string url)
        {
            try
            {
                var response = await httpClient.DeleteAsync(url);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);

                throw;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(string url, T entity)
        {
            var response = await httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> PostEnumerableAsync<T>(string url, IEnumerable<T> entity)
        {
            var response = await httpClient.PostAsJsonAsync(url, entity);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();

                throw new ClientException(errorResponse);
            }
            try
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> PutEnumerableAsync<T>(string url, IEnumerable<T> entity)
        {
            var response = await httpClient.PutAsJsonAsync(url, entity);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();

                throw new ClientException(errorResponse);
            }
            try
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
