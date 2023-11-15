using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.Helpers
{
    internal static class LocalStorage
    {
        public static async Task Insert<T>(string key, T data)
        {
            await BlobCache.UserAccount.InsertObject(key, data);
        }

        public static async Task InsertSecure<T>(string key, T data)
        {
            await BlobCache.Secure.InsertObject(key, data);
        }

        public static async Task<T> GetSecure<T>(string key)
        {
            var data = default(T);
            try
            {
                var c = await BlobCache.Secure.GetAllKeys();
                if (c.Contains(key))
                {
                    return await BlobCache.Secure.GetObject<T>(key);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return data;
        }

        public static async Task<T> Get<T>(string key)
        {
            var data = default(T);
            try
            {
                var c = await BlobCache.UserAccount.GetAllKeys();
                if (c.Contains(key))
                {
                    return await BlobCache.UserAccount.GetObject<T>(key);
                }
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return data;
        }

        public static async Task Remove(string key)
        {
            try
            {
                await BlobCache.UserAccount.Invalidate(key);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public static async Task RemoveAll()
        {
            await BlobCache.UserAccount.InvalidateAll();
            await BlobCache.Secure.InvalidateAll();
        }

        public static void Shutdown()
        {
            BlobCache.Shutdown().Wait();
        }
    }
}
