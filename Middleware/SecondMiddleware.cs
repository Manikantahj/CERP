namespace CERP.Middleware
{
    public class SecondMiddleware
    {
        private readonly RequestDelegate _next;

        public SecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            /* Console.WriteLine("Hello from Second - Before");

             await _next(context);

             Console.WriteLine("Hello from Second - After");*/

            Console.WriteLine($"Second - Before: {context.Request.Path}");

            await _next(context);

            Console.WriteLine($"Second - After: {context.Request.Path}");
        }
    }
}
