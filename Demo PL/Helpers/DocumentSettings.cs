using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo_PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName) 
        {
            //1. Get Located Folder Path

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //2. Get File Name amd Make Unique

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. Get File path

            string filePath = Path.Combine(folderPath, fileName);

            //4. save file as streams (Stream : Data per Time)

            using var fs = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fs);

            return fileName;
        }


        public static void DeleteFile(string fileName ,string folderName) 
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
            
        }
    }
}
