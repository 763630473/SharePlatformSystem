using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Windsor;
using NUnit.Framework;
using SharePlatformSystem.Core.IO;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using SharePlatformSystem.Log4Net.Logging.Log4Net;

namespace SharePlatformSystem.Test.Log4Net
{
    public class Log4NetTest
    {
        [Test]
        public void Test()
        {
            //Arrange
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "CastleLog4NetTests-Logs.txt");
            FileHelper.DeleteIfExists(logFilePath);

            //Act
            var container = new WindsorContainer();
            container.AddFacility<LoggingFacility>(facility =>
            {
                facility.UseSharePlatformLog4Net().WithConfig("log4net.config");
            });

            var logger = container.Resolve<ILoggerFactory>().Create(typeof(Log4NetTest));
            logger.Info("Should_Write_Logs_To_Text_File works!");

            //Assert
            File.Exists(logFilePath).ShouldBeTrue();
        }
    }
}
