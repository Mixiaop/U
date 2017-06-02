using System;
using System.Reflection;
using AutoMapper;

namespace U.AutoMapper
{
    internal static class AutoMapperHelper
    {
        public static void CreateMap(Type type)
        {
            CreateMap<AutoMapFromAttribute>(type);
            CreateMap<AutoMapToAttribute>(type);
            CreateMap<AutoMapAttribute>(type);
        }

        public static void CreateMap<TAttribute>(Type type)
            where TAttribute : AutoMapAttribute
        {
            if (!type.IsDefined(typeof(TAttribute)))
            {
                return;
            }

            Mapper.Initialize((mapper) =>
                        {
                            foreach (var autoMapToAttribute in type.GetCustomAttributes<TAttribute>())
                            {
                                if (autoMapToAttribute.TargetTypes.IsNullOrEmpty())
                                {
                                    continue;
                                }

                                foreach (var targetType in autoMapToAttribute.TargetTypes)
                                {
                                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.To))
                                    {
                                        mapper.CreateMap(type, targetType);
                                        //Mapper.CreateMap(type, targetType);
                                    }

                                    if (autoMapToAttribute.Direction.HasFlag(AutoMapDirection.From))
                                    {
                                        mapper.CreateMap(type, targetType);

                                        //Mapper.CreateMap(targetType, type);
                                    }
                                }
                            }
                        });
        }
    }
}
