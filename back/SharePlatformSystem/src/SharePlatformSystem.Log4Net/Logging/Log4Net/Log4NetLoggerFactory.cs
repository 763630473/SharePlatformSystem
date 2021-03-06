﻿using System;
using System.IO;
using System.Xml;
using Castle.Core.Logging;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using SharePlatformSystem.Core.Extensions;
using SharePlatformSystem.Core.Reflection.Extensions;

namespace SharePlatformSystem.Log4Net.Logging.Log4Net
{
    public class Log4NetLoggerFactory : AbstractLoggerFactory
    {
        internal const string DefaultConfigFileName = "log4net.config";
        private readonly ILoggerRepository _loggerRepository;

        public Log4NetLoggerFactory()
            : this(DefaultConfigFileName)
        {
        }

        public Log4NetLoggerFactory(string configFileName)
        {
            _loggerRepository = LogManager.CreateRepository(
                typeof(Log4NetLoggerFactory).GetAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
            );

            var log4NetConfig = new XmlDocument();
            var f = File.OpenRead(configFileName);
                log4NetConfig.Load(f);
            XmlConfigurator.Configure(_loggerRepository, log4NetConfig["log4net"]);
        }

        public Log4NetLoggerFactory(string configFileName, bool reloadOnChange)
        {
            _loggerRepository = LogManager.CreateRepository(
                typeof(Log4NetLoggerFactory).GetAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
            );

            if (reloadOnChange)
            {
                XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(configFileName));
            }
            else
            {
                var log4NetConfig = new XmlDocument();
                log4NetConfig.Load(File.OpenRead(configFileName));
                XmlConfigurator.Configure(_loggerRepository, log4NetConfig["log4net"]);
            }
        }

        public override Castle.Core.Logging.ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new Log4NetLogger(LogManager.GetLogger(_loggerRepository.Name, name), this);
        }

        public override Castle.Core.Logging.ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}