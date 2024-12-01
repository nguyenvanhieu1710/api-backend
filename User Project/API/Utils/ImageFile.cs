namespace API.Utils
{
    public class ImageFile
    {
        public static string? ConvertImageToBase64(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;

            var imageBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = GetContentType(filePath);

            if (string.IsNullOrEmpty(contentType))
                return null;

            var base64String = Convert.ToBase64String(imageBytes);
            return $"data:{contentType};base64,{base64String}";
        }
        public static string? GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                ".webp" => "image/webp",
                _ => null
            };
        }
    }
}
