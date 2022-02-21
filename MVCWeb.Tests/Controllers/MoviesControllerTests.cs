using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCWeb.Controllers.Tests
{
    [TestClass()]
    public class MoviesControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            ViewResult result = controller.Index("Comdey", "Ghost", "電影名稱",1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}