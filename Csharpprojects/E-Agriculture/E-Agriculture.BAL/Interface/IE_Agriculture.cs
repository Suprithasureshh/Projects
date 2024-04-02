using E_Agriculture.DAL.Model;
using Microsoft.AspNetCore.Mvc;


namespace E_Agriculture.BAL.Interface
{
    public interface IE_Agriculture
    {
        //UserDetails
        void AddUserDetails(User adduserdetails);
        IQueryable<User> GetAllUserDetails();
        IActionResult DeleteUserDetails(int Id);

        //AvailabilityCrops
        void AddAvailabilityCrop(AvailabilityCrops crops);
        IQueryable<AvailabilityCrops> GetAllAvailabilityCrops();
        void UpdateAvailabilityCrops(AvailabilityCrops crop);
        IActionResult DeleteAvailabilityCrops(int Id);

        //RequiredCrops
        void AddRequiredCrops(RequiredCrops crops);
        IQueryable<RequiredCrops> GetAllRequiredCrops();
        void UpdateRequiredCrops(RequiredCrops crop);
        IActionResult DeleteRequiredCrops(int Id);

        //MarketDetails
        void AddMarketDetails(MarketDetails crops);
        IQueryable<MarketDetails> GetAllMarketDetails();
        void UpdateMarketDetails(MarketDetails crop);
        IActionResult DeleteMarketDetails(int Id);

        //GovernmentPrograms
        void AddGovernmentPrograms(GovernmentPrograms prog);
        IQueryable<GovernmentPrograms> GetAllGovernmentPrograms();
        void UpdateGovernmentPrograms(GovernmentPrograms prog);

        //Queries
        void AddQueries(Queries que);
        IQueryable<Queries> GetAllQueries();

        //Answer
        void AddAnswer(Answer ans);
        IQueryable<Answer> GetAllAnswer();

        //check requiredcrops from buyer
        Task<object> GetRequiredCropsForBuyer();

        //check farmer selling details
        Task<object> GetFarmerSellingDetails();

        //Fetching students questions
        Task<object> GetStudentQuestions();
        
        Task<object> GetAnswersForQuestions();

    }

}
