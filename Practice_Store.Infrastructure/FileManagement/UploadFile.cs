using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Practice_Store.Common
{
    public static class UploadFile
    {
        public static UploadImageDto UploadImageFile(RequestUploadImageFile Request)
        {
            if (Request.File != null)
            {
                string Folder = Request.FolderPath;
                var UploadsRootFolder = Path.Combine(Request._hostingEnvironment.WebRootPath, Folder);
                if (!Directory.Exists(UploadsRootFolder))
                {
                    Directory.CreateDirectory(UploadsRootFolder);
                }


                if (Request.File == null || Request.File.Length == 0)
                {
                    return new UploadImageDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string FileExtension = Path.GetExtension(Request.File.FileName);
                string FileName = DateTime.Now.Ticks.ToString() + Request.Name + FileExtension;
                var FilePath = Path.Combine(UploadsRootFolder, FileName);
                using (var FileStream = new FileStream(FilePath, FileMode.Create))
                {
                    Request.File.CopyTo(FileStream);
                }

                return new UploadImageDto()
                {
                    FileNameAddress = Folder + FileName,
                    Status = true,
                };
            }
            return null;
        }

        public class UploadImageDto
        {
            public long Id { get; set; }
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
        }

        public class RequestUploadImageFile
        {
            public IFormFile File { get; set; }
            public string Name { get; set; }
            public string FolderPath { get; set; }
            public IHostingEnvironment _hostingEnvironment { get; set; }
        }
    }
}
