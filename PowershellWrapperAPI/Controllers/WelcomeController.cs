using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PowershellWrapperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WelcomeController : ControllerBase
    {
        [HttpGet(Name = "GetMessage")]
        public IActionResult GetMessage(string name)
        {
            string pathToScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "myscript.ps1");

            using var process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-File {pathToScript} '{name}'"; //name is the i/p param to be passed to script
                process.StartInfo.RedirectStandardOutput = true;
                
                process.Start();
                string outputFromPS = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return Ok(outputFromPS);
        }
    }
}