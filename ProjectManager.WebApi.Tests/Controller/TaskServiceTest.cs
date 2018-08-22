using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectManager.WebApi.Controllers;
using ProjectManager.Persistence;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Routing;
using System.Net;
using System.Data.Entity;

namespace ProjectManager.WebApi.Tests.Controller
{
    /// <summary>
    /// Contains NUnit test cases for TaskController
    /// </summary>
    [TestFixture]
    public class TaskServiceTest
    {
        TaskController TaskController;
        ProjectManagerEntities ProjectManagerDB;
        List<Task> allTasks;
        List<Task> Tasks;
        int count = 0;

        [SetUp]
        public void Setup()
        {
            //create an instance of contactRepository
            ProjectManagerDB = new ProjectManagerEntities();

            //Create an instance of controller
            TaskController = new TaskController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test, Order(1)]
        public void PostTask()
        {
            // Arrange
            TaskController controller = new TaskController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:51052/api/Task")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "Tasks" } });
            ///
            var response = TaskController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Tasks = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            // Act
            Task Task = new Task() { TaskName = "Developement", Project_ID = 1, Parent_ID = 1, Status = "C" ,  Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(1) };
            response = controller.Post(Task); 
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isSaved = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isSaved > 0);
            Assert.IsTrue(response.IsSuccessStatusCode);

            response = TaskController.Get(Tasks[Tasks.Count - 1].Task_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            List<Task> TaskData;
            //Assert.IsTrue(response.TryGetContentValue<Task>(out TaskData));
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            TaskData = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            // Assert
            Assert.IsNotNull(TaskData[0].TaskName);
            Assert.IsNotNull(TaskData[0].Start_Date);
        }

        [Test, Order(2)]
        public void GetAllTask()
        {
            // Act on Test  
            var response = TaskController.Get();
            // Assert the result  

            //Assert.IsTrue(response.TryGetContentValue<List<Task>>(out Tasks));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            allTasks = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            count = allTasks.Count;
            Assert.True(count > 0);
        }

        [Test, Order(3)]
        public void GetTaskByTaskId()
        {
            // Act on Test 
            // Act on Test  
           var response = TaskController.Get(allTasks[count - 1].Task_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<Task>(out Task));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Tasks = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            Assert.IsNotNull(Tasks[0].TaskName);
            Assert.IsNotNull(Tasks[0].Start_Date);
        }

        [Test, Order(4)]
        public void PutTask()
        {
            
            var response = TaskController.Get(allTasks[count - 1].Task_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<Task>(out Task));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Tasks = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            Tasks[0].TaskName = "Testing";
            // Act  
            response = TaskController.Put(count -1, Tasks[0]);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [Test, Order(5)]
        public void DeleteTask()
        {
            var response = TaskController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Tasks = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            response = TaskController.Delete(allTasks[count - 1].Task_ID);
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isDeleted = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isDeleted > 0 );
        }


        
    }
}
