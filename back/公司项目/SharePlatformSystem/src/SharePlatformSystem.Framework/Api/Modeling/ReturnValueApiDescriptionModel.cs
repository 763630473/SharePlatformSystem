using System;

namespace SharePlatformSystem.Framework.Api.Modeling
{
    [Serializable]
    public class ReturnValueApiDescriptionModel
    {
        public Type Type { get; }
        public string TypeAsString { get; }

        public ReturnValueApiDescriptionModel(Type type)
        {
            Type = type;
            TypeAsString = type.FullName;
        }
    }
}