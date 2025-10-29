using Microsoft.AspNetCore.Http;  
 
namespace App2.BLL.Helper
{
    public static class Upload
    {
        public static string UploadFile(string folderName, IFormFile file)  
        {
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);  
                string finalPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(finalPath, FileMode.Create))
                {
                    file.CopyTo(stream);  
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RemoveFile(string folderName, string fileName)
        {
            try
            {
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }

                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
