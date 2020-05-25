#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChannelController : Controller
    {
        public ViewResult ChannelList(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var getChannelSearchResults = SqlHelper.GetChannels(search);
                ViewData["channelSearchData"] = getChannelSearchResults;
            }

            var channelList = SqlHelper.GetChannelListForUser();
            return View(channelList);
        }

        public ViewResult Messages(string? channel)
        {
            var getMessages = SqlHelper.GetChannelMessages(channel);
            ViewData["ChannelMessages"] = getMessages;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            SqlHelper.InsertNewMessage(message);
            return RedirectToAction($"Messages", new { channel = DatabaseManager.Recipient});
        }

        public JsonResult GetChannels(string search)
        {
            var getChannels = SqlHelper.GetChannels(search);
            return Json(getChannels);
        }
    }
}