using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;

namespace EmployeeServices.Controllers
{
    [EnableCorsAttribute("*", "*","*")]   
    public class EmployeeController : ApiController
    {
        //[BasicAuthorization]
        //public HttpResponseMessage Get(string gender = "all")
        //{
        //    string username = Thread.CurrentPrincipal.Identity.Name;
        //    using (AIDummyDBEntities entities = new AIDummyDBEntities())
        //    {
        //        switch (username.ToLower())
        //        {
        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                entities.Employee.Where(e => e.gender.ToLower() == "male").ToList());
        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                entities.Employee.Where(e => e.gender.ToLower() == "female").ToList());
        //            default:
        //                return Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }
        //    }

        //}

        public IEnumerable<Employee> Get()
        {
            using (AIDummyDBEntities entities = new AIDummyDBEntities())
            {
                return entities.Employee.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (AIDummyDBEntities entities = new AIDummyDBEntities())
            {
                var entity = entities.Employee.FirstOrDefault(e => e.id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found");
                }
            }
        }

        public HttpResponseMessage Delete(int id){
            try{
                using (AIDummyDBEntities entity = new AIDummyDBEntities()){
                    entity.Employee.Remove(entity.Employee.FirstOrDefault(e => e.id==id));
                    entity.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Employee deleted successfully.");
                }
            } catch(Exception ex){
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee id is not valid", ex);
            }
        }

        public HttpResponseMessage Post(Employee employee){

            string path = @"C:\Users\Leads\Desktop\temp.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(employee.name+"\t"+employee.age+"\t"+employee.gender);
            }

            try
            {
                using (AIDummyDBEntities entities = new AIDummyDBEntities())
                {
                    entities.Employee.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.id.ToString());
                    return message;
                }
            }catch(Exception ex){
               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employee employee){
            using(AIDummyDBEntities entities = new AIDummyDBEntities()){
                var entity = entities.Employee.FirstOrDefault(e => e.id == id);

                if(entity!=null){
                    entity.name = employee.name;
                    entity.age = employee.age;
                    entity.gender = employee.gender;

                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "OKKKK");
                }
                else{
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found");
                }
            }
        }
    }
}
