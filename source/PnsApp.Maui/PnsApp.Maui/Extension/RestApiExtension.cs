using DotNet.RestApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnsApp.Maui.Extension
{
    
    public static class RestApiExtension
    {
        private static string _url = "https://localhost:7187/Pns/";


        public static async Task<T> ApiGetAsync<T>(this RestApiClient api, DotazGet dotaz)
        {
            var response = await api.SendJsonRequest(HttpMethod.Get, new Uri(_url + dotaz.ToString()), null);
            
            return await response.DeseriaseJsonResponseAsync<T>();
        }
       
        public static async Task<T> ApiGetAsync<T>(this RestApiClient api, DotazGet dotaz, int? id)
        {
            var response = await api.SendJsonRequest(HttpMethod.Get, new Uri(_url + dotaz.ToString() + "?id=" + id), null);
            return await response.DeseriaseJsonResponseAsync<T>();
        }

        public static async Task ApiPostAsync<T>(this RestApiClient api, DotazPost dotaz, T data)
        {
            await api.SendJsonRequest(HttpMethod.Post, new Uri(_url + dotaz.ToString()), data);
        }

        public static async Task ApiPutAsync<T>(this RestApiClient api, DotazPut dotaz, T data)
        {
            await api.SendJsonRequest(HttpMethod.Put, new Uri(_url + dotaz.ToString()), data);
        }

        public static async Task ApiDeleteAsync(this RestApiClient api, DotazDelete dotaz, int id)
        {
            await api.SendJsonRequest(HttpMethod.Delete, new Uri(_url + dotaz.ToString() + "?id=" + id), string.Empty);
        }







        public enum DotazGet
        {
            GetZakaznici,
            GetZakaznik

        }

        public enum DotazPost
        {
            PridatZakaznika
            
        }

        public enum DotazPut
        {
            UpravitZakaznika
        }

        public enum DotazDelete
        {
            SmazatZakaznika
        }


    }
}
