using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Numerics;
using MemoryLeaker.Models;

namespace MemoryLeaker.Controllers
{
    public class HomeController : Controller
    {
        private static List<byte[]> LeakRoot = new List<byte[]>();
        private static List<byte> Leak = new List<byte>();

        private enum Symbol
        {
            KB = 1,
            MB = 2,
            GB = 3
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemoryLeak()
        {
            return View(new MemoryDetails());
        }

        [HttpPost]
        public ActionResult CreateMemoryLeak(string leakSize)
        {
            var match = Regex.Match(leakSize, "(?i:(?<size>[0-9]+)(?<symbol>[k|m|g]b?))");
            if (!match.Success)
            {
                ViewBag.Message = $"Unable to understand leak amount {leakSize}. Use something like 50mb";
                return View("MemoryLeak");
            }

            int size = int.Parse(match.Groups["size"].Value);
            Symbol symbol = (Symbol)Enum.Parse(typeof(Symbol), match.Groups["symbol"].Value, true);
            BigInteger numBytes = size * (BigInteger.Pow(1024, (int)symbol));
            if (numBytes > int.MaxValue - 1)
            {
                ViewBag.Message = $"Leak amount must be less than 2gb";
                return View("MemoryLeak");
            }

            LeakMemory((int)numBytes);

            return RedirectToAction("MemoryLeak");
        }

        private void LeakMemory(int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
            {
                Leak.Add(1);
            }

            //var leak = new byte[0];
            //Array.Resize(ref leak, numBytes);
            //LeakRoot.Add(leak);
        }
    }
}