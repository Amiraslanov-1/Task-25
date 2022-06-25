using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace _25._06._2022Task.Extensions
{
    public static class Extension
    {
        public static bool CheckSize(this IFormFile photo,int mb)
        {
            if(photo.Length/1024/1024>=mb)
            {
                return true;
            }        
        return false;
        }
        public static bool CheckType(this IFormFile photo ,string type)
        {
            if (photo.ContentType.Contains(type))
            {return true;

            }
            return false;
        }
        public async static Task<string> SaveFileAsync(this IFormFile photo ,string path)
        {
            string url=Guid.NewGuid().ToString()+photo.FileName;
            string imgUrl=Path.Combine(path,url);
            using (FileStream fs = new FileStream(imgUrl, FileMode.Create))
            {
                await photo.CopyToAsync(fs);
            }
            return url;
        }
        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
