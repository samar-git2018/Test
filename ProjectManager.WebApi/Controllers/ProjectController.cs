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
    public class ProjectController : BaseAPIController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public HttpResponseMessage Get()
        {
            return ToJson(ProjectManagerDB.Projects.AsEnumerable());
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Get(int id)
        {
            return ToJson(ProjectManagerDB.Projects.Where(dr=> dr.Project_ID == id));
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public HttpResponseMessage Post([FromBody]Project value)
        {
            ProjectManagerDB.Projects.Add(value);
            return ToJson(ProjectManagerDB.SaveChanges());
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public HttpResponseMessage Put(int id, [FromBody]Project value)
        {
            if(ProjectManagerDB.Entry(value).State == EntityState.Added)
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
            ProjectManagerDB.Projects.Remove(ProjectManagerDB.Projects.FirstOrDefault(x => x.Project_ID == id));
            return ToJson(ProjectManagerDB.SaveChanges());
        }
    }
}
