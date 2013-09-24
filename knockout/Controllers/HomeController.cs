using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using knockout.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace knockout.Controllers
{
    public class HomeController : Controller
    {

        //private List<UserDetail> userdetail = new List<UserDetail>();

        UserDetail userdetail = new UserDetail();

        public string con = ConfigurationManager.AppSettings["KnockoutConnection"].ToString();
        private string gridDataResult;

        [HttpGet]
        public ActionResult Index()
        {

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDetail ud)
        {
            return View();
        }

        public JsonResult UserDetail()
        {
            return Json(0);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Grid()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Save(UserDetail item)
        {
            using (var sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();
                using (var sqlcmd = sqlcon.CreateCommand())
                {
                    sqlcmd.CommandText = "[dbo].[usp_InsertUser]";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add(new SqlParameter("@firsname", item.FirstName));
                    sqlcmd.Parameters.Add(new SqlParameter("@lastname", item.LastName));
                    sqlcmd.Parameters.Add(new SqlParameter("@birthdate", item.Bdate));
                    sqlcmd.ExecuteNonQuery();
                }
            }         
            return Json(item,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(UserDetail item)
        {
         
                using (var sqlcon = new SqlConnection(con))
                {
                    sqlcon.Open();
                    using (var sqlcmd = sqlcon.CreateCommand())
                    {
                        sqlcmd.CommandText = "[dbo].[usp_student_update]";
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.Add(new SqlParameter("@userid", item.UserId));
                        sqlcmd.Parameters.Add(new SqlParameter("@FirstName", item.FirstName));
                        sqlcmd.Parameters.Add(new SqlParameter("@LastName", item.LastName));
                        sqlcmd.Parameters.Add(new SqlParameter("@birthdate", item.Bdate));
                        sqlcmd.ExecuteNonQuery();
                    }
                }
              
            
            return Json(item, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Delete(UserDetail id)
        {
            using (var sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();
                using (var sqlcmd = sqlcon.CreateCommand())
                {
                    sqlcmd.CommandText = "usp_userdelete";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add(new SqlParameter("@Userid", id.UserId));
                    sqlcmd.ExecuteNonQuery();
                }
            }
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Detail()
        {
            var userdetaillist = new List<UserDetail>();
            using (var sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();
                using (var sqlcmd = sqlcon.CreateCommand())
                {
                    sqlcmd.CommandText = "dbo.usp_getuserdetails";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr=sqlcmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            int userIdKOrdinal = dr.GetOrdinal("Userid");
                            int firstnameordinal = dr.GetOrdinal("firstname");
                            int lastnameordinal = dr.GetOrdinal("lastname");
                            int birthnameordinal = dr.GetOrdinal("birthdate");
                            while (dr.Read())
	                        {
                                userdetail = new UserDetail
                                {
                                    UserId=dr.GetInt32(userIdKOrdinal),
                                    FirstName=dr.GetString(firstnameordinal),
                                    LastName=dr.GetString(lastnameordinal),
                                    Bdate=dr.GetInt32(birthnameordinal)
                                };
                                userdetaillist.Add(userdetail);
	                        }
                        }
                    }
                }
            }
            return Json(userdetaillist.ToList(),JsonRequestBehavior.AllowGet);
        }

        public JsonResult SampleGrid(int rows, int page)
        {
            var userdetaillist = new List<UserDetail>();
            using (var sqlcon = new SqlConnection(con))
            {
                sqlcon.Open();
                using (var sqlcmd = sqlcon.CreateCommand())
                {
                    sqlcmd.CommandText = "dbo.usp_getuserdetails";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = sqlcmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            int userIdKOrdinal = dr.GetOrdinal("Userid");
                            int firstnameordinal = dr.GetOrdinal("firstname");
                            int lastnameordinal = dr.GetOrdinal("lastname");
                            int birthnameordinal = dr.GetOrdinal("birthdate");
                            while (dr.Read())
                            {
                                userdetail = new UserDetail
                                {
                                    UserId = dr.GetInt32(userIdKOrdinal),
                                    FirstName = dr.GetString(firstnameordinal),
                                    LastName = dr.GetString(lastnameordinal),
                                    Bdate = dr.GetInt32(birthnameordinal)
                                };
                                userdetaillist.Add(userdetail);
                            }
                        }
                    }
                    UserDetail gridDataResult = new UserDetail();
                    gridDataResult.TotalRowCount = userdetaillist.Count;

                    int totalPages = 2;
                    if (rows > 0)
                    {
                        totalPages = gridDataResult.TotalRowCount / rows;
                        if (gridDataResult.TotalRowCount % rows != 0)
                            totalPages += 1;
                    }

                    gridDataResult.TotalPageCount = totalPages;
                    //gridDataResult.GridData = people.Skip((page - 1) * rows).Take(rows);
                }
            }
            return Json(userdetaillist.ToList(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult SampleData()
        {
            return View();
        }
        
       
    }
}


 