using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TF.Data.Business.WMS;

namespace TF.DAL.Test
{
    [TestClass]
    public class DbContextTest
    {
        [TestMethod]
        public void CreateDbTest()
        {
            var context = new NoodleDbContext("NoodleDb");
            context.Init();
        }
    }
}
