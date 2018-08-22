using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using ProjectManager.Persistence;
using System.Data.Entity;

namespace ProjectManager.WebApi.Controllers
{
    public class TaskController : BaseAPIController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage Get()
        {
            return ToJson(ProjectManagerDB.Tasks.AsEnumerable());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(ProjectManagerDB.Tasks.Where(dr => dr.Task_ID == id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]Task value)
        {
            ProjectManagerDB.Tasks.Add(value);
            return ToJson(ProjectManagerDB.SaveChanges());
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, [FromBody]Task value)
        {
            if (ProjectManagerDB.Entry(value).State == EntityState.Added)
            {
                ProjectManagerDB.Entry(value).State = EntityState.Modified;
            }
            return ToJson(ProjectManagerDB.SaveChanges());
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Delete(int id)
        {
            ProjectManagerDB.Tasks.Remove(ProjectManagerDB.Tasks.FirstOrDefault(x => x.Task_ID == id));
            return ToJson(ProjectManagerDB.SaveChanges());
        }
    }
}
