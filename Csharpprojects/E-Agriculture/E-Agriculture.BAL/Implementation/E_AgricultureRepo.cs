using E_Agriculture.BAL.Interface;
using E_Agriculture.DAL.Data_Context;
using System.Net.Mail;
using System.Net;
using E_Agriculture.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Agriculture.BAL.Implementation
{
    public class E_AgricultureRepo : ControllerBase, IE_Agriculture
    {
        private readonly E_Agriculture_Context _context;
       

        public E_AgricultureRepo(E_Agriculture_Context context)
        {
            _context = context;
          
        }

        //UserDetails
        public void AddUserDetails(User adduserdetails)
        {
            var EmailCheck = _context.userdetails.FirstOrDefault(e => e.Email == adduserdetails.Email);

            if (EmailCheck == null)
            {
               
                var user = new User();
                user.User_Name = adduserdetails.User_Name;
                user.Address = adduserdetails.Address;
                user.Joining_Date = adduserdetails.Joining_Date;
                user.Contact_No = adduserdetails.Contact_No;
                user.Email = adduserdetails.Email;
                user.Password = adduserdetails.Password;
                user.UserAs = adduserdetails.UserAs;

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(adduserdetails.Password);
                user.Hashpassword = passwordHash;
                user.OTP = "";

                _context.userdetails.Add(user);
                _context.SaveChanges();

                var fullname = user.User_Name;
                string fromAddress = "learnifypoint@gmail.com";
                string Password = "lhuytefzzaunguuj";
                string toAddress = adduserdetails.Email;
                string emailHeader = "<html><body><h1>Congratulations</h1></body></html>";
                string emailFooter = $"<html><head><title>E-Agriculture</title></head><body><p>Hi {fullname}, <br> This is the confidential email. Don't share your password with anyone..!<br> Click here to change your credentials  <a href=\"http://localhost:3000/\"> : http://localhost:3000/ </a> </p></body></html>";
                string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your password is: {user.Password}</p></body></html>";
                string emailContent = emailHeader + emailBody + emailFooter;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromAddress);
                message.Subject = "Welcome To E-Agriculture Service System";
                message.To.Add(new MailAddress(toAddress));
                message.Body = emailContent;
                message.IsBodyHtml = true;
                var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, Password),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
            }
            else
            {
                throw new EmailException();
            }

        }

        public IQueryable<User> GetAllUserDetails()
        {
            return _context.userdetails.AsQueryable();
        }

        public IActionResult DeleteUserDetails(int Id)
        {
            var user = _context.userdetails.FirstOrDefault(e => e.User_Id == Id);

            if (user == null)
            {
                throw new DataNotFound();
            }
            else
            {
                _context.userdetails.Remove(user);
                _context.SaveChanges();
                throw new DeleteRecord();
            }

        }

        //AvailabilityCrop

        public void AddAvailabilityCrop(AvailabilityCrops crops)
        {
           
                var data = new AvailabilityCrops();
                data.ACrop_Name = crops.ACrop_Name;
                data.AQuantity = crops.AQuantity;
                data.Price = crops.Price;
                data.ALocation = crops.ALocation;
                data.Add_Date = DateTime.UtcNow.Date;
                data.Image = crops.Image;
                data.User_Id = crops.User_Id;
                _context.availabilityCrops.Add(data);
                _context.SaveChanges();
            
        }
        public IQueryable<AvailabilityCrops> GetAllAvailabilityCrops()
        {
            return _context.availabilityCrops;
        }
        public void UpdateAvailabilityCrops(AvailabilityCrops crop)
        {
            var obj = _context.availabilityCrops.Find(crop.ACrop_Id);
            if (obj != null)
            {
                obj.ACrop_Name = crop.ACrop_Name;
                obj.AQuantity = crop.AQuantity;
                obj.Price = crop.Price;
                obj.ALocation = crop.ALocation;
                obj.Add_Date = crop.Add_Date;
                _context.SaveChanges();
            }
        }
       
        public IActionResult DeleteAvailabilityCrops(int Id)
        {
            var user = _context.availabilityCrops.FirstOrDefault(e => e.ACrop_Id == Id);

            if (user == null)
            {
                throw new DataNotFound();
            }
            else
            {
                _context.availabilityCrops.Remove(user);
                _context.SaveChanges();
                throw new DeleteRecord();
            }

        }


        //RequiredCrops

        public void AddRequiredCrops(RequiredCrops crops)
        {

            var data = new RequiredCrops();
            data.RCrop_Name = crops.RCrop_Name;
            data.RQuantity = crops.RQuantity;
            data.RLocation = crops.RLocation;
            data.User_Id = crops.User_Id;
            data.RAdd_Date = DateTime.UtcNow.Date;
            _context.requiredCrops.Add(data);
            _context.SaveChanges();

        }
        public IQueryable<RequiredCrops> GetAllRequiredCrops()
        {
            return _context.requiredCrops;
        }
        public void UpdateRequiredCrops(RequiredCrops crop)
        {
            var obj = _context.requiredCrops.Find(crop.RCrop_Id);
            if (obj != null)
            {
                obj.RCrop_Name = crop.RCrop_Name;
                obj.RQuantity = crop.RQuantity;
                obj.RLocation = crop.RLocation;
                obj.RAdd_Date = crop.RAdd_Date;
                _context.SaveChanges();
            }
        }

        public IActionResult DeleteRequiredCrops(int Id)
        {
            var user = _context.requiredCrops.FirstOrDefault(e => e.RCrop_Id == Id);

            if (user == null)
            {
                throw new DataNotFound();
            }
            {
                _context.requiredCrops.Remove(user);
                _context.SaveChanges();
                throw new DeleteRecord();
            }

        }

        //MarketDetails

        public void AddMarketDetails(MarketDetails crops)
        {

            var data = new MarketDetails();
            data.MCrop_Name = crops.MCrop_Name;
            data.MQuantity = crops.MQuantity;
            data.MPrice= crops.MPrice;
            data.MLocation = crops.MLocation;
            data.MAdd_Date = DateTime.UtcNow.Date;
            _context.marketDetails.Add(data);
            _context.SaveChanges();

        }
        public IQueryable<MarketDetails> GetAllMarketDetails()
        {
            return _context.marketDetails;
        }
        public void UpdateMarketDetails(MarketDetails crop)
        {
            var obj = _context.marketDetails.Find(crop.MCrop_Id);
            if (obj != null)
            {
                obj.MCrop_Name = crop.MCrop_Name;
                obj.MQuantity = crop.MQuantity;
                obj.MPrice = crop.MPrice;
                obj.MLocation = crop.MLocation;
                obj.MAdd_Date = crop.MAdd_Date;
                _context.SaveChanges();
            }
        }

        public IActionResult DeleteMarketDetails(int Id)
        {
            var user = _context.marketDetails.FirstOrDefault(e => e.MCrop_Id == Id);

            if (user == null)
            {
                throw new DataNotFound();
            }
            {
                _context.marketDetails.Remove(user);
                _context.SaveChanges();
                throw new DeleteRecord();
            }

        }

        //GovernmentPrograms


        public void AddGovernmentPrograms(GovernmentPrograms prog)
        {

            var data = new GovernmentPrograms();
            data.Program_Name = prog.Program_Name;
            data.Program_Description = prog.Program_Description;
            data.ProgramStart_Date = DateTime.UtcNow.Date;
            data.ProgramEnd_Date = DateTime.UtcNow.Date;
            _context.governmentPrograms.Add(data);
            _context.SaveChanges();

        }
        public IQueryable<GovernmentPrograms> GetAllGovernmentPrograms()
        {
            return _context.governmentPrograms;
        }
        public void UpdateGovernmentPrograms(GovernmentPrograms prog)
        {
            var obj = _context.governmentPrograms.Find(prog.Program_Id);
            if (obj != null)
            {
                obj.Program_Name = prog.Program_Name;
                obj.Program_Description = prog.Program_Description;
                obj.ProgramStart_Date = prog.ProgramStart_Date;
                obj.ProgramEnd_Date = prog.ProgramEnd_Date;
                _context.SaveChanges();
            }
        }

        //Queries

        public void AddQueries(Queries que)
        {

            var data = new Queries();
          
            data.Question = que.Question;
            data.Question_Date = DateTime.UtcNow.Date;
            data.User_Id = que.User_Id;
            _context.queries.Add(data);
            _context.SaveChanges();

        }
        public IQueryable<Queries> GetAllQueries()
        {
            return _context.queries;
        }

        //Answer

        public void AddAnswer(Answer ans)
        {

            var data = new Answer();
            data.AnswerFor = ans.AnswerFor;
            data.User_Id=ans.User_Id;
            data.Question_Id = ans.Question_Id;
            data.Answer_Date = DateTime.UtcNow.Date;
            _context.answer.Add(data);
            _context.SaveChanges();

        }
        public IQueryable<Answer> GetAllAnswer()
        {
            return _context.answer;
        }

        //check requiredcrops from buyer
        public async Task<object> GetRequiredCropsForBuyer()
        {
            var result = await _context.userdetails
                .Where(user => user.UserAs == "buyer")
                .Join(
                    _context.requiredCrops,
                    user => user.User_Id,
                    crop => crop.User_Id,
                    (user, crop) => new
                    {
                        User_Name = user.User_Name,
                        Contact_No = user.Contact_No,
                        Address = user.Address,
                        RCrop_Name = crop.RCrop_Name,
                        RQuantity = crop.RQuantity,
                        RLocation = crop.RLocation,
                        RAdd_Date = crop.RAdd_Date
                    })
                .ToListAsync();

            return result;
        }


        //check farmer selling details

        public async Task<object> GetFarmerSellingDetails()
        {
            var result = await _context.userdetails
                .Where(user => user.UserAs == "farmer")
                .Join(
                    _context.availabilityCrops,
                    user => user.User_Id,
                    crop => crop.User_Id,
                    (user, crop) => new
                    {
                        User_Name = user.User_Name,
                        Contact_No = user.Contact_No,
                        Address = user.Address,
                        ACrop_Name = crop.ACrop_Name,
                        AQuantity = crop.AQuantity,
                        ALocation = crop.ALocation,
                        Price = crop.Price,
                        Add_Date = crop.Add_Date
                    })
                .ToListAsync();

            return result;

        }


        //Fetching students questions
        public async Task<object> GetStudentQuestions()
        {
            var result = await _context.userdetails
                .Where(user => user.UserAs == "student")
                .Join(
                    _context.queries,
                    user => user.User_Id,
                    que => que.User_Id,
                    (user, que) => new
                    {
                        User_Name = user.User_Name,
                        User_Id = user.User_Id,
                        Question_Id = que.Question_Id,
                        Question = que.Question,
                        Question_Date = que.Question_Date,
                       
                    })
                .ToListAsync();

            return result;
        }


       public async Task<object> GetAnswersForQuestions()
        {
            var result = await _context.queries
                .Join(
                    _context.answer,
                    que => que.Question_Id,
                    ans => ans.Question_Id,
                    (que, answers) => new
                    {
                        Question_Id = que.Question_Id,
                        AnswersFor = answers.AnswerFor,
                    })
                .ToListAsync();

            return result;
        }


        













    }
}
