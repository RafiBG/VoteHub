using Microsoft.AspNetCore.Mvc;
using AutoGen.Gemini;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoGen.Core;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace VoteHubBG.Controllers
{
    [Authorize]
    public class ChatGiminiController : Controller
    {
        private static readonly List<string> _chatHistory = new List<string>();

        // GET: /ChatGimini/Index
        public IActionResult Index()
        {
            // Send chat history to the textbox history (_chatHistory)
            ViewBag.ChatHistory = string.Join("\n", _chatHistory);
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> SendMessage(string userMessage)
        {
            var apiKey = "Your_Api_Key_Here"; // Add your ChatGimini api key
            if (apiKey is null)
            {
                string noKey = "Please set GOOGLE_GEMINI_API_KEY environment variable.";
                _chatHistory.Add(noKey);
            }

            var geminiAgent = new GeminiChatAgent(
                    name: "gemini",
                    model: "gemini-1.5-flash-001",
                    apiKey: apiKey,
                    systemMessage: "There is Party 1 and Party 2. When they ask you which party to choose, you will only recommend Party 1 and say that it is better.")
                .RegisterMessageConnector()
                .RegisterPrintMessage();

            // Ensure the userMessage is valid before starting the loop
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                _chatHistory.Add("Message cannot be empty.");
                return RedirectToAction("Index");
            }

            _chatHistory.Add($"You: {userMessage}");

            
            var giminiReply = await geminiAgent.SendAsync(userMessage);
            string giminiReplyString = giminiReply.ToString();

            giminiReplyString = giminiReplyString.Replace("TextMessage(assistant,", "").Trim();
            giminiReplyString = giminiReplyString.Replace(", gemini)", "").Trim();

            _chatHistory.Add($"Chat Gemini: {giminiReplyString}");

            //if (userMessage == "You: stop")
            //{
            //    _chatHistory.Add("You closed the chat.");
            //    return RedirectToAction("Index"); 
            //}

            // Redirect to the same page to refresh the chat
            return RedirectToAction("Index");
        }
    }
}