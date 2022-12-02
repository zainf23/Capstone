using Lab.Pages.AWSupload;
using Lab.Pages.DataClasses;
using Lab.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab.Pages.Projects
{
    public class FileUploadModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> files { get; set; }

        [BindProperty]
        public string fileName { get; set; }

        public int projectID { get; set; }
        public void OnGet(int projectid)
        {
            HttpContext.Session.SetInt32("projectID", projectid);
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
                    // full path to file in temp location
                    //var filePath = Directory.GetCurrentDirectory() +
                    //@"\wwwroot\images\" + formFile.FileName;
                    //filePaths.Add(filePath);
                    //using (var stream = new FileStream(filePath,
                    //FileMode.Create))
                    //{
                    //    formFile.CopyTo(stream);
                    //}
                    fileName = formFile.FileName;
                }
                projectID = (int)HttpContext.Session.GetInt32("projectID");
                //string sqlQuery = "Insert into Project (fileName) VALUES ("

                DBClass.UpdateProjectPic(fileName, projectID);
            }
            return RedirectToPage("ViewProjects");
        }
    }
}
