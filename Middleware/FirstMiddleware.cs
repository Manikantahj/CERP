namespace CERP.Middleware
{
    public class FirstMiddleware
    {
        private readonly RequestDelegate _next;

        public FirstMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            /*Console.WriteLine("Hello from First - Before");

            await _next(context);

            Console.WriteLine("Hello from First - After");*/

            Console.WriteLine($"First - Before: {context.Request.Path}");

            await _next(context);

            Console.WriteLine($"First - After: {context.Request.Path}");
        }
    }
}
