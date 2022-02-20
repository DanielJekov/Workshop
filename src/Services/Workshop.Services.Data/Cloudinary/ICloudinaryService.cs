namespace Workshop.Services.Cloudinary
{
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(byte[] data, string fileName, string folderName, int? width, int? height);
    }
}
