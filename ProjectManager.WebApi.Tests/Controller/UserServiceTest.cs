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
using NBench;
using NBench.Util;

namespace ProjectManager.WebApi.Tests.Controller
{
    /// <summary>
    /// Contains NUnit test cases for UserController
    /// </summary>
    [TestFixture]    
    public class UserServiceTest
    {
        UserController UserController;
        ProjectManagerEntities ProjectManagerDB;
        List<User> allUsers;
        List<User> users;
        int count = 0;

        [SetUp]
        public void Setup()
        {
            //create an instance of contactRepository
            ProjectManagerDB = new ProjectManagerEntities();

            //Create an instance of controller
            UserController = new UserController
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test, Order(1)]
        public void PostUser()
        {
            // Arrange
            UserController controller = new UserController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:51052/api/User")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "Users" } });
            ///
            var response = UserController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            users = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            // Act
            User user = new User() { First_Name = "Samar", Last_Name = "Dutta", Employee_ID = "EMP" + users[users.Count - 1].User_ID.ToString() + "1" };
            response = controller.Post(user);
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isSaved = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isSaved > 0);
            Assert.IsTrue(response.IsSuccessStatusCode);

            response = UserController.Get(users[users.Count - 1].User_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            List<User> userData;
            //Assert.IsTrue(response.TryGetContentValue<User>(out userData));
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            userData = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            // Assert
            Assert.IsNotNull(userData[0].First_Name);
            Assert.IsNotNull(userData[0].Last_Name);
        }

        [Test, Order(2)]
        public void GetAllUser()
        {
            // Act on Test  
            var response = UserController.Get();
            // Assert the result  

            //Assert.IsTrue(response.TryGetContentValue<List<User>>(out users));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            allUsers = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            count = allUsers.Count;
            Assert.True(count > 0);
        }

        [Test, Order(3)]
        public void GetUserByUserId()
        {
            // Act on Test 
            // Act on Test  
           var response = UserController.Get(allUsers[count - 1].User_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<User>(out user));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            users = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            Assert.IsNotNull(users[0].First_Name);
            Assert.IsNotNull(users[0].Last_Name);
        }

        [Test, Order(4)]
        public void PutUser()
        {
            
            var response = UserController.Get(allUsers[count - 1].User_ID);
            Assert.IsNotNull(response);
            // Assert the result  
            //Assert.IsTrue(response.TryGetContentValue<User>(out user));
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            users = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            users[0].Employee_ID = "EMP008";
            // Act  
            response = UserController.Put(count -1, users[0]);            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [Test, Order(5)]
        public void DeleteUser()
        {
            var response = UserController.Get();
            Assert.IsNotNull(response);
            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            users = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            response = UserController.Delete(allUsers[count - 1].User_ID);
            jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            int isDeleted = Convert.ToInt16(jsonString.Result);
            Assert.IsTrue(isDeleted > 0 );
        }
    }
}
