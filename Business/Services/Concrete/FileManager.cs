using Business.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Concrete
{
    public class FileManager: IFileService
    {
        private readonly string _rootPath;
        private readonly string[] _allowedExtensions ={ ".jpg", ".jpeg", ".png", ".gif" };
        private const long _maxFileSize = 2 * 1024 * 1024; 
        public FileManager(IWebHostEnvironment env)
        {
            _rootPath = Path.Combine(env.WebRootPath,"uploads");
        }
        public async Task<string> UploadAsync(Microsoft.AspNetCore.Http.IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty");
            }
            if (file.Length > _maxFileSize)
            {
                throw new ArgumentException("File size exceeds the limit");
            }
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(_allowedExtensions, extension) < 0)
            {
                throw new ArgumentException("Invalid file type");
            }
            var folderPath = Path.Combine(_rootPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Path.Combine("Images", folderName, uniqueFileName).Replace("\\", "/");
        }
        public void Delete(string filePath)
        {
            var fullPath = Path.Combine(_rootPath, filePath.Replace("Images/", ""));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
