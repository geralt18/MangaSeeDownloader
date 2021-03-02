using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace MangaSeeDownloader
{
   class Program
   {
      static void Main(string[] args) {
         Dictionary<string, string> urls = new Dictionary<string, string>();
         //urls.Add(@"https://s1.mangabeast01.com/manga/Naruto/", @"D:\Naruto\"); //Naruto Black-White
         //urls.Add(@"https://s4-2.mangabeast.com/manga/Naruto-Digital-Colored-Comics/", @"D:\Naruto Color\"); //Naruto Color
         //urls.Add(@"https://official-hot.eorzea.us/manga/Dragon-Ball/DragonBallZ/", @"D:\Dragon Ball Z\"); //Dragon Ball Z
         //urls.Add(@"https://official-hot.eorzea.us/manga/Dragon-Ball/DragonBall/", @"D:\Dragon Ball\"); //Dragon Ball 
         //urls.Add(@"https://official-complete.granpulse.us/manga/Dragon-Ball-Full-Color---Freeza-Arc/", @"D:\Dragon Ball Full Color - Freeza Arc\"); //Dragon Ball Full Color - Freeza Arc
         urls.Add(@"https://official-complete.granpulse.us/manga/Dragon-Ball-Full-Color-Saiyan-Arc/", @"D:\Dragon Ball Full Color - Saiyan Arc\"); //Dragon Ball Full Color - Saiyan Arc

         foreach (var u in urls) {
            string baseUrl = u.Key;
            string basePath = u.Value;
            int chapterCount = 400;
            using var httpClient = new HttpClient();

            //https://s1.mangabeast01.com/manga/Naruto/0001-001.png

            for (int chapter = 1; chapter <= chapterCount; chapter++) {
               Console.WriteLine($"Rozpoczynam pobieranie rodziału {chapter}");
               string chapterPath = $"{basePath}{chapter:d4}";
               if (!Directory.Exists(chapterPath))
                  Directory.CreateDirectory(chapterPath);

               int page = 0;
               while (1 == 1) {
                  page++;
                  string url = $"{baseUrl}{chapter:d4}-{page:d3}.png";
                  string filePath = $@"{basePath}{chapter:d4}\{page:d3}.png";

                  try {
                     if (!File.Exists(filePath)) {
                        Console.WriteLine($"\tPobieram plik {url}");
                        var imageBytes = httpClient.GetByteArrayAsync(url).Result;
                        File.WriteAllBytes(filePath, imageBytes);
                     }
                  } catch (Exception e) {
                     Console.WriteLine($"Błąd pobrania pliku {url}. {e.Message}");
                     if (!Directory.EnumerateFileSystemEntries(chapterPath).Any())
                        Directory.Delete(chapterPath);
                     break;
                  }
               }
            }
         }
      }
   }
}

