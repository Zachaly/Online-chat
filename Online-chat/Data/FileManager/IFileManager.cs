using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    public interface IFileManager
    {
        FileStream ImageStream(string image);
        FileStream ProfilePictureStream(string image);
        Task<string> SaveImage(IFormFile image, bool profilePicture = false);
        bool RemoveImage(string image);
    }
}
