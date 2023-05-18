using Newtonsoft.Json;
using ToyStoreClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;

namespace ToyStoreClient.Helpers
{
    public class Utilities
    {
        public static T SendDataRequest<T>(string APIUrl, object? input = null)
        {
            HttpClient client = new();
            client.BaseAddress = new System.Uri("https://localhost:44350");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(APIUrl, input).Result;
            T result = default!;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var returnData = JsonConvert.DeserializeObject<T>(jsonString);
                if (returnData != null)
                {
                    return returnData;
                }
            }
            return result;
        }

        //public static T SendDataRequest<T>(string apiUrl, object? input = null, string token = null!)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44322/");
        //    client.DefaultRequestHeaders.Accept.Clear();

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    }

        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = client.PostAsJsonAsync(apiUrl, input).Result;
        //    T result = default(T)!;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = response.Content.ReadFromJsonAsync<T>().Result!;
        //    }

        //    return result;
        //}


        //public static DataTable GetTable<TEntity>(List<TEntity> table, string name) where TEntity : class
        //{
        //    var offset = 78;
        //    DataTable result = new DataTable(name);
        //    PropertyInfo[] infos = typeof(TEntity).GetProperties();
        //    foreach (PropertyInfo info in infos)
        //    {
        //        if (info.PropertyType.IsGenericType
        //        && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        //        {
        //            result.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));
        //        }
        //        else
        //        {
        //            result.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        //        }
        //    }
        //    foreach (var el in table)
        //    {
        //        DataRow row = result.NewRow();
        //        foreach (PropertyInfo info in infos)
        //        {
        //            if (info.PropertyType.IsGenericType &&
        //                info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        //            {
        //                object t = info.GetValue(el);
        //                if (t == null)
        //                {
        //                    t = Activator.CreateInstance(Nullable.GetUnderlyingType(info.PropertyType));
        //                }

        //                row[info.Name] = t;
        //            }
        //            else
        //            {
        //                if (info.PropertyType == typeof(byte[]))
        //                {
        //                    //Fix for Image issue.
        //                    var imageData = (byte[])info.GetValue(el);
        //                    var bytes = new byte[imageData.Length - offset];
        //                    Array.Copy(imageData, offset, bytes, 0, bytes.Length);
        //                    row[info.Name] = bytes;
        //                }
        //                else
        //                {
        //                    row[info.Name] = info.GetValue(el);
        //                }
        //            }
        //        }
        //        result.Rows.Add(row);
        //    }

        //    return result;
        //}
    }
}
