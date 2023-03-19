namespace ContactPro_MVC.Services.Interfaces
{
    public interface IImageService
    {
        public Task<byte[]> CovertFileToByteArrayAsync(IFormFile file);
        public string ConvertByteArrayToFile(byte[] fileData, string extension);
    }
}
