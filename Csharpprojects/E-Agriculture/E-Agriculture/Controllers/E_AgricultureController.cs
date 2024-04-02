using E_Agriculture.BAL.Implementation;
using E_Agriculture.BAL.Interface;
using E_Agriculture.DAL.Model;
using Microsoft.AspNetCore.Mvc;

namespace E_Agriculture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class E_AgricultureController : ControllerBase
    {
        private readonly IE_Agriculture _admin;

        public E_AgricultureController(IE_Agriculture admin)
        {

            _admin = admin;

        }
        //UserDetails

        [HttpPost("AddUserDetails")]
        public void AddUserDetails(User model)
        {
            _admin.AddUserDetails(model);
        }
        
        [HttpGet("GetAllUserDetails")]
        public IQueryable<User> GetAllUserDetails()
        {
            return _admin.GetAllUserDetails();
        }
        [HttpDelete("DeleteUserDetailsById")]
        public IActionResult DeleteUserDetails(int Id)
        {

            return _admin.DeleteUserDetails(Id);
        }

        //AvailabilityCrops

        [HttpPost("AddAvailabilityCrops")]
        public void AddAvailabilityCrop(AvailabilityCrops model)
        {
            _admin.AddAvailabilityCrop(model);
        }

        [HttpPut("EditAvailabilityCrops")]
        public void UpdateAvailabilityCrop(AvailabilityCrops model)
        {
            _admin.UpdateAvailabilityCrops(model);
        }

        [HttpGet("GetAllAvailabilityCrops")]
        public IQueryable<AvailabilityCrops> GetAllAvailabilityCrops()
        {
            return _admin.GetAllAvailabilityCrops();
        }
        [HttpDelete("DeleteAvailabilityCropsById")]
        public IActionResult DeleteAvailabilityCrops(int Id)
        {

            return _admin.DeleteAvailabilityCrops(Id);
        }

        //RequiredCrops

        [HttpPost("AddRequiredCrops")]
        public void AddRequiredCrops(RequiredCrops model)
        {
            _admin.AddRequiredCrops(model);
        }

        [HttpPut("UpdateRequiredCrops")]
        public void UpdateRequiredCrops(RequiredCrops model)
        {
            _admin.UpdateRequiredCrops(model);
        }

        [HttpGet("GetAllRequiredCrops")]
        public IQueryable<RequiredCrops> GetAllRequiredCrops()
        {
            return _admin.GetAllRequiredCrops();
        }
        [HttpDelete("DeleteRequiredCropsById")]
        public IActionResult DeleteRequiredCrops(int Id)
        {

            return _admin.DeleteRequiredCrops(Id);
        }

        //MarketDetails


        [HttpPost("AddMarketDeatils")]
        public void AddMarketDetails(MarketDetails model)
        {
            _admin.AddMarketDetails(model);
        }

        [HttpPut("UpdateMarketDeatils")]
        public void UpdateMarketDetails(MarketDetails model)
        {
            _admin.UpdateMarketDetails(model);
        }

        [HttpGet("GetAllMarketDetails")]
        public IQueryable<MarketDetails> GetAllMarketDetails()
        {
            return _admin.GetAllMarketDetails();
        }
        [HttpDelete("DeleteMarketDetailsById")]
        public IActionResult DeleteMarketDetails(int Id)
        {

            return _admin.DeleteMarketDetails(Id);
        }

        //GovernmentPrograms


        [HttpPost("AddGovernmentPrograms")]
        public void AddGovernmentPrograms(GovernmentPrograms model)
        {
            _admin.AddGovernmentPrograms(model);
        }

        [HttpPut("UpdateGovernmentPrograms")]
        public void UpdateGovernmentPrograms(GovernmentPrograms model)
        {
            _admin.UpdateGovernmentPrograms(model);
        }

        [HttpGet("GetAllGovernmentPrograms")]
        public IQueryable<GovernmentPrograms> GetAllGovernmentPrograms()
        {
            return _admin.GetAllGovernmentPrograms();
        }


        //Queries

        [HttpPost("AddQueries")]
        public void AddQueries(Queries model)
        {
            _admin.AddQueries(model);
        }

        [HttpGet("GetAllQueries")]
        public IQueryable<Queries> GetQueries()
        {
            return _admin.GetAllQueries();
        }

        //Answer

        [HttpPost("AddAnswer")]
        public void AddAnswer(Answer ans )
        {
            _admin.AddAnswer(ans);
        }

        [HttpGet("GetAllAnswer")]
        public IQueryable<Answer> GetAnswer()
        {
            return _admin.GetAllAnswer();
        }

        //check requiredcrops from buyer
        [HttpGet("RequiredcropsForBuyer")]
        public async Task<object> GetRequiredCropsForBuyer()
        {
            var result = await _admin.GetRequiredCropsForBuyer();
            return result;
        }

        //check farmer selling details

        [HttpGet("FarmeSellingDetails")]
        public async Task<object> GetFarmeSellingDetails()
        {
            var result = await _admin.GetFarmerSellingDetails();
            return result;
        }
        //Fetching students questions

        [HttpGet("StudentQuestions")]
        public async Task<object> GetStudentQuestions()
        {
            var result = await _admin.GetStudentQuestions();
            return result;
        }

        

        [HttpGet("GetAnswersForQuestions")]
        public async Task<object> GetAnswersForQuestions()
        {
            var result = await _admin.GetAnswersForQuestions();
            return result;
        }

    }
}
