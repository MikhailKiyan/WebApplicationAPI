using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;

namespace WebApplicationAPI.IntegrationTests.ExtensionMethods {
  public static class HttpClientExtensions {
    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
        this HttpClient httpClient,
        string url,
        T data) {
      var dataAsString = JsonConvert.SerializeObject(data);
      var content = new StringContent(dataAsString);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      return httpClient.PostAsync(url, content);
    }

    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content) {
      var dataAsString = await content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<T>(dataAsString);
    }

    public static async Task<T> ReadAsAsync<T>(this HttpContent content) {
      var dataStream = await content.ReadAsStreamAsync();
      var serializer = new JsonSerializer();
      using var sr = new StreamReader(dataStream);
      using var jsonTextReader = new JsonTextReader(sr);
      return serializer.Deserialize<T>(jsonTextReader);
    }
  }
}
