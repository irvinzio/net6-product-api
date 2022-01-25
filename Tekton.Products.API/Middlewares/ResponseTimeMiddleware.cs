using System.Diagnostics;

namespace Tekton.API.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            var path = context.Request.Path;
            var method = context.Request.Method;
            context.Response.OnStarting(() => {
                watch.Stop();
                var logFilePath = string.Format("{0}/{1}", Directory.GetCurrentDirectory(), "TimeResponseLog.txt");

                using (var file = File.Exists(logFilePath) ? File.Open(logFilePath, FileMode.Append) : File.Open(logFilePath, FileMode.CreateNew))
                using (var stream = new StreamWriter(file))
                    stream.WriteLine($"{DateTime.Now} :: time elapsed for {method} {path} : {watch.ElapsedMilliseconds} miliseconds");

                return Task.CompletedTask;
            });
            return this._next(context);
        }
    }
}
