using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtentions = new() { ".png", ".jpg",".jpeg" };
        private const int _allowedMaxSize = 2_097_152;

        public string? Upload(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtentions.Contains(extension))
                return null;

            if (file.Length > _allowedMaxSize)
                return null;

            //var FolderPath = $"D:{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";

            //Base Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files\\",folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //fileName + Extention
            var fileName = $"{Guid.NewGuid()}{extension}";

            //FolderPath + FileName
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }

        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;

        }

       
    }
}
