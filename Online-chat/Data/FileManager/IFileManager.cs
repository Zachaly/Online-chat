using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Data.FileManager
{
    /// <summary>
    /// Used for file management
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Stream for images in messages
        /// </summary>
        FileStream ImageStream(string image);

        /// <summary>
        /// Stream for profile pictures
        /// </summary>
        FileStream ProfilePictureStream(string image);
        Task<string> SaveImage(IFormFile image, bool profilePicture = false);
    }
}
