using Lab.Pages.AWSupload;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Users
{
    public class FileUploadModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> files { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        public int userID { get; set; }

        public void OnGet(int userid)
        {
            HttpContext.Session.SetInt32("userID", userid);
        }
        public IActionResult OnPost()
        {
            var filePaths = new List<string>();
            foreach (var formFile in files)
            {                
                if (formFile.Length > 0)
                {
                    AmazonS3Uploader uploader = new AmazonS3Uploader();
                    var result = uploader.UploadFileAsync(formFile);
                    //    // full path to file in temp location
                    //    var filePath = Directory.GetCurrentDirectory() +
                    //    @"\wwwroot\images\" + formFile.FileName;
                    //    filePaths.Add(filePath);
                    //    using (var stream = new FileStream(filePath,
                    //    FileMode.Create))
                    //    {
                    //        formFile.CopyTo(stream);
                    //    }
                    //    fileName = formFile.FileName;
                    fileName = formFile.FileName;
                }
                string sqlQuery = "Update UserProfilePic SET ";
                sqlQuery += "[fileName] ='" + fileName + "' WHERE userID =" + HttpContext.Session.GetInt32("userID");

                DBClass.InsertQuery(sqlQuery);
            }
            return RedirectToPage("ViewProfiles");
        }
    }
}
