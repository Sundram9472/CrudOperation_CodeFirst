using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudOperation_CodeFirst.Data;
using CrudOperation_CodeFirst.Services;
using CrudOperation_CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using CrudOperation_CodeFirst.Services;

namespace CrudOperation_CodeFirst.Controllers
{
    public class ErrorController : Controller
    {
        private readonly AppDbContext _context;

        public ErrorController(AppDbContext context)
        {
            _context = context;
        }
        readonly ErrorLog Data = new ErrorLog();
        public IActionResult Error()
        {
            var ExceptionHandlePathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = ExceptionHandlePathFeature.Path;
            ViewBag.ExceptionMessage = ExceptionHandlePathFeature.Error.Message;
            ViewBag.StackTrace = ExceptionHandlePathFeature.Error.StackTrace;
            return View("Error");
        }

        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var ExceptionHandlePathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exceptionMessage = ExceptionHandlePathFeature.Error.Message;
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry ! The Resource You Requested coudn't Be Found..!";
                    return View("NotFound");
                    break;

                case 500:
                    ViewBag.ErrorMessage = " Internally Server Error Occured ";
                    return View("NotFound");
                    break;

                default :
                    ViewBag.ErrorMessage = exceptionMessage;
                    Boolean check = ErrorLogDB();
                    if(check)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("NotFound");
                    }
                    break;
            }
        }

        public Boolean ErrorLogDB()
        {
            var ExceptionHandlePathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var currentDateAndTimeToLoggedError = DateTime.Now;
            var stackTrace = ExceptionHandlePathFeature.Error.StackTrace;
            var exceptionMessage = ExceptionHandlePathFeature.Error.Message;
            var errorId = Guid.NewGuid();
            Data.ErrorId = errorId;
            Data.LoggedOn = currentDateAndTimeToLoggedError;
            Data.Message = "Sundram_"+exceptionMessage;
            Data.StackTrace = stackTrace;
            _context.Add(Data);
            _context.SaveChanges();
            return true;
        }
    }
}
