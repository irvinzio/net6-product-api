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
                Console.WriteLine("time elapsed");
                Console.WriteLine(watch.ElapsedMilliseconds.ToString());
                return Task.CompletedTask;
            });
            return this._next(context);
        }
    }
}
