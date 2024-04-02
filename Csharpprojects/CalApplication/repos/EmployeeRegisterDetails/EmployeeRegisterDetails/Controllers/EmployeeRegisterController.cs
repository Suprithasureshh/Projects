using EmployeeRegisterDetails.Interface;
using EmployeeRegisterDetails.Migrations;
using EmployeeRegisterDetails.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EmployeeRegisterDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRegisterController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmployeeRegister _repository;
        public EmployeeRegisterController(IEmployeeRegister repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public ActionResult Post(EmployeeProperties emp)
        {
            return _repository.Post(emp);
        }
        [HttpGet]
        public IEnumerable<EmployeeProperties> Gets()
        {
            return _repository.Get();
        }
        [HttpPut("UpdateEmployee")]

        public IActionResult UpdateEmployee( EmployeeProperties emp)
        {

            _repository.UpdateEmployee(emp);
            return NoContent();


        }
        [HttpPut("ActivateUser")]

        public void updateAct(int[] Id)
        {
            _repository.ActivateEmployee(Id);
        }

        [HttpPut("InactivateUser")]

        public void updateInact(int[] Id)
        {
            _repository.DeactivateEmployee(Id);
        }
        [HttpGet("Active and InActive")]
        public IActionResult select(bool? isActive)
        {
            return _repository.select(isActive);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            _repository.Delete(Id);
            return Ok();
        }
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Image file is not selected");
            }
            var folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }



            // Return the image path
            var imagePath = Path.Combine("Images", fileName);
            return Ok(new { imagePath });
        }
        [HttpGet("GetImage")]
        public IActionResult GetImage(string imagePath)
        {
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, imagePath);
            var imageBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(imageBytes, "image/jpeg");
        }
    }
}
