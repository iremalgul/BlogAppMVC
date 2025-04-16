namespace BlogAppMVC.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Path.GetFileName(image.FileName);
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return fileName;
        }
    }


}
