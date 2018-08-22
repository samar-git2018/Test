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
    /// Contains NUnit test cases for ProjectController
    /// </summary>
    [TestFixture]
    public class ProjectServiceTest
    {
        ProjectController ProjectController;
        ProjectManagerEntities ProjectManagerDB;
        List<Project> allProjects;
        List<Project> Projects;
        int count = 0;

        [SetUp]
        public void Setup()
        {
            //create an instance of contactRepository
            ProjectManagerDB = new ProjectManagerEntities();

            //Create an instance of controller
            ProjectController = new ProjectController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test, Order(1)]
        public void PostProject()
        {
            // Arrange
            ProjectController controller = new ProjectController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:51052/api/Project")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "Projects" } });
            ///
            var response = ProjectController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Projects = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            // Act
            Project Project = new Project() { ProjectName = "Project MIS", Start_Date = DateTime.Now, End_Date = DateTime.Now };
            response = controller.Post(Project);
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isSaved = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isSaved > 0);
            Assert.IsTrue(response.IsSuccessStatusCode);

            response = ProjectController.Get(Projects[Projects.Count - 1].Project_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            List<Project> ProjectData;
            //Assert.IsTrue(response.TryGetContentValue<Project>(out ProjectData));
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            ProjectData = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            // Assert
            Assert.IsNotNull(ProjectData[0].ProjectName);
            Assert.IsNotNull(ProjectData[0].Start_Date);
        }

        [Test, Order(2)]
        public void GetAllProject()
        {
            // Act on Test  
            var response = ProjectController.Get();
            // Assert the result  

            //Assert.IsTrue(response.TryGetContentValue<List<Project>>(out Projects));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            allProjects = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            count = allProjects.Count;
            Assert.True(count > 0);
        }

        [Test, Order(3)]
        public void GetProjectByProjectId()
        {
            // Act on Test 
            // Act on Test  
           var response = ProjectController.Get(allProjects[count - 1].Project_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<Project>(out Project));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Projects = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            Assert.IsNotNull(Projects[0].ProjectName);
            Assert.IsNotNull(Projects[0].End_Date);
        }

        [Test, Order(4)]
        public void PutProject()
        {
            
            var response = ProjectController.Get(allProjects[count - 1].Project_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<Project>(out Project));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Projects = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            Projects[0].ProjectName = "Project test";
            // Act  
            response = ProjectController.Put(count -1, Projects[0]);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [Test, Order(5)]
        public void DeleteProject()
        {
            var response = ProjectController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            Projects = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            response = ProjectController.Delete(allProjects[count - 1].Project_ID);
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isDeleted = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isDeleted > 0 );
        }


        
    }
}
