namespace ELK.Demo.WebApi
{
    public static class ConfigureWebApp
    {
        public static void UseWebApp(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.InitSwagger();
            }

            // 使用靜態檔案
            app.UseStaticFiles();

            app.UseAuthorization();
            app.MapControllers();
            app.UseCors();

            // 當路由找不到路徑的時候，會跳轉到/wwww/index.html
            app.MapFallbackToFile("index.html"); ;
        }


        private static void InitSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            });
        }
    }
}