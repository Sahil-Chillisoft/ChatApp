#nullable enable
using System;
using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class MessageController : Controller
    {
        public ViewResult MessageList(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var getSearchResults = SqlHelper.GetUsersListByName(search);
                ViewData["UserSearchResultData"] = getSearchResults;
            }

            var previousMessagedRecipients = SqlHelper.GetPreviouslyMessagedRecipientsForUser();
            return View(previousMessagedRecipients);
        }

        public ViewResult Messages(string? recipient)
        {
            var getMessages = SqlHelper.GetMessages(recipient);
            ViewData["Messages"] = getMessages;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            SqlHelper.InsertNewMessage(message);
            return RedirectToAction($"Messages", new {recipient = DatabaseManager.Recipient});
        }

        public JsonResult GetUsers(string username)
        {
            var getUserList = SqlHelper.GetUserListForAutoComplete(username);
            return Json(getUserList);
        }
    }
}