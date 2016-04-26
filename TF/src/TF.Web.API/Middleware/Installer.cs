using Owin;
using System;
using TF.DAL;

namespace TF.Web.API.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class Installer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseInstaller(IAppBuilder app)
        {
            app.Run((ctx) =>
            {
                try
                {
                    new NoodleDbContext("NoodleDb").Init();

                    ctx.Response.Write("DB context created successfully");
                }
                catch (Exception ex)
                {
                    ctx.Response.Write(ex.Message);
                }

                return System.Threading.Tasks.Task.FromResult(0);
            });
        }
    }
}
