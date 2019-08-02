using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_MeetingMS.ViewModels;
using System.Web.Http.Cors;

namespace E_MeetingMS.API
{


    //// Allow CORS for all origins. (Caution!)
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

   
    public class MeetingsController : ApiController
    {
        
        [HttpGet]
        [Route("api/testing/{number}/{password}")]

        // GET api/<controller>/5
        public IHttpActionResult Get(string number, string password)
        {
            decimal balance = 20000;



            return Ok(new {
                CardNo = "***************" + number.Substring(14),
                Balance = balance.ToString("C"),
                  Message = "Success"

            });
        }

        [HttpGet]
        [Route("api/testing/GetAllComments")]
        public IHttpActionResult GetAllComments()
        {

            List<UserComment> UScomment = new List<UserComment>()
            {
                new UserComment() { username = "victorimoru@gmail.com",  content = "The Document is very Clear and Accurate",
                          datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt") },

                  new UserComment() { username = "emmanuel@gmail.com",  content = "The Document is very Clear and Accurate",
                          datetime = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt") }
            };


            return Ok(new { Comments = UScomment });
        }




        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    class UserComment
    {

        public string username { get; set; }
        public string content { get; set; }

        public string datetime { get; set; }

    }





}