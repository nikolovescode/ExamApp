using System;
using System.Threading.Tasks;
using ExamApp.Models;
using ExamApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser("junior@email.com", "B12&Ccp");

            Assert.IsNotNull(response);
        }
        [TestMethod]
        public async Task Test_Add_WorkNote_TrueAsync()
        {

            ApiServices apiServices = new ApiServices();

            var workNote = new WorkNote()
            {
                TitleWorkNote = "Fyll tvättmaskin",
                TitleWorkTask = "Hushållssysslor",
                CalendarUserEmail = "junior@email.com"
            };
            bool response = await apiServices.RegisterWorkTask(workNote);

            Assert.IsNotNull(response);       

    }
        [TestMethod]
        public async Task Test_Add_PlannedWorkshift_TrueAsync()
        {

            ApiServices apiServices = new ApiServices();

            var plannedWorkshift = new PlannedWorkshift()
            {
                IdWorkTask = 9,
                TitleWorkTask = "Hushållssysslor",
                CalendarUserEmail = "junior@email.com",
                MinutesToWork = 20,
                Minute = 20,
                Hour = 10,
                Day = 10,
                Month = 12,
                Year = 2019,
                Priority = false,
                Done = false
            };
            bool response = await apiServices.RegisterPlannedWorkshift(plannedWorkshift);

            Assert.IsNotNull(response);

        }

        [TestMethod]
        public async Task Test_Add_Workshift_TrueAsync()
        {

            ApiServices apiServices = new ApiServices();

            var workshift = new Workshift()
            {
                IdWorkTask = 9,
                TitleWorkTask = "Hushållssysslor",
                PlannedWorkshiftId =1,
                CalendarUserEmail = "junior@email.com",
                WasEffective=true,
                MinutesWorking=20
            };
            bool response = await apiServices.RegisterWorkshift(workshift);
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public async Task Test_Add_SettingsPause_Async()
        {

            ApiServices apiServices = new ApiServices();

            var pause = new SettingsPause()    
            {
                MinPauseBeforeActivity = 10,
                CalendarUserEmail = "junior@email.com"
    };
            bool response = await apiServices.RegisterSettingsPause(pause);
            Assert.IsNotNull(response);

        }

        [TestMethod]
        public async Task Test_Get_WorknotesFromTaskAndEmail_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();

            var list = await apiServices.FindWorkNoteSubject("junior@email.com", "Hushållssysslor");
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_Worktasks_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();

            var list = await apiServices.FindWorkTasks();
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_SettingsPauseFromEmail_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();

            var list = await apiServices.FindSettingsPauseEmail("junior@email.com");
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_Undone_PlannedWorkshifts_Date_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();
            var list = await apiServices.FindDatesPlannedWorkshifts("junior@email.com", false, DateTime.Today);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_AVGWorkshiftOfUserAndTask_Date_NotNull()
        {
            ApiServices apiServices = new ApiServices();
            int list = await apiServices.FindAvgUserAndTasks("junior@email.com", 9);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_AVGWorkshiftOfTask_Date_NotNull()
        {
            ApiServices apiServices = new ApiServices();
            int list = await apiServices.FindAvgTasks(9);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public async Task Test_Get_Workshift_UserSubject_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();
            var shiftsSubject = await apiServices.FindWorkshiftsSubjectOfUser("ni@gmail.com", "svenska");
            Assert.IsNotNull(shiftsSubject);

        }

        [TestMethod]
        public async Task Test_Get_Workshift_Subject_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();
            var shiftsSubject = await apiServices.FindWorkshiftsSubject("svenska");
            Assert.IsNotNull(shiftsSubject);

        }
        
    }
}
