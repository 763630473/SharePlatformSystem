﻿using System;
using System.Linq;
using System.Collections.Generic;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Core.Collections.Extensions;

namespace SharePlatformSystem.Framework.Api.Modeling
{
    [Serializable]
    public class ModuleApiDescriptionModel
    {
        public string Name { get; set; }

        public IDictionary<string, ControllerApiDescriptionModel> Controllers { get; }

        private ModuleApiDescriptionModel()
        {
            
        }

        public ModuleApiDescriptionModel(string name)
        {
            Name = name;

            Controllers = new Dictionary<string, ControllerApiDescriptionModel>();
        }

        public ControllerApiDescriptionModel AddController(ControllerApiDescriptionModel controller)
        {
            if (Controllers.ContainsKey(controller.Name))
            {
                throw new SharePlatformException($"已经有一个名为的控制器: {controller.Name} 模块内: {Name}");
            }

            return Controllers[controller.Name] = controller;
        }

        public ControllerApiDescriptionModel GetOrAddController(string name)
        {
            return Controllers.GetOrAdd(name, () => new ControllerApiDescriptionModel(name));
        }
        
        public ModuleApiDescriptionModel CreateSubModel(string[] controllers, string[] actions)
        {
            var subModel = new ModuleApiDescriptionModel(Name);

            foreach (var controller in Controllers.Values)
            {
                if (controllers == null || controllers.Contains(controller.Name))
                {
                    subModel.AddController(controller.CreateSubModel(actions));
                }
            }

            return subModel;
        }
    }
}