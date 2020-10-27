using System;
using System.IO;
using System.Net.Http;

namespace MangaSeeDownloader
{
   class Program
   {
      static void Main(string[] args) {
         //string baseUrl = @"https://s1.mangabeast01.com/manga/Naruto/";
         string baseUrl = @"https://s4-2.mangabeast.com/manga/Naruto-Digital-Colored-Comics/";
         string basePath = @"D:\Naruto Color - MangaSee\";
         int chapterCount = 700;
         using var httpClient = new HttpClient();

         //https://s1.mangabeast01.com/manga/Naruto/0001-001.png

         for (int chapter = 1; chapter <= chapterCount; chapter++) {
            Console.WriteLine($"Rozpoczynam pobieranie rodziału {chapter}");
            if (!Directory.Exists($"{basePath}{chapter:d4}"))
               Directory.CreateDirectory($"{basePath}{chapter:d4}");

            int page = 0;
            while (1 == 1) {
               page++;
               string url = $"{baseUrl}{chapter:d4}-{page:d3}.png";
               string filePath = $@"{basePath}{chapter:d4}\{page:d3}.png";

               try {
                  Console.WriteLine($"\tPobieram plik {url}");
                  var imageBytes = httpClient.GetByteArrayAsync(url).Result;
                  if (!File.Exists(filePath))
                     File.WriteAllBytes(filePath, imageBytes);
               } catch (Exception e) {
                  Console.WriteLine($"Błąd pobrania pliku {url}. {e.Message}");
                  break;
               }
            }
         }
      }
   }
}

