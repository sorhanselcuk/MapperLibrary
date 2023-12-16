using MapperLibrary.Abstract;
using System;
using System.Collections.Generic;

namespace MapperLibrary
{
    public static class Mapper
    {
        public static IEnumerable<TTarget> ConvertDatas<TSource, TTarget>(MapAbstractConfigurator<TSource, TTarget> mapConfigurator, IEnumerable<TSource> datas)
            where TSource : class, new()
            where TTarget : class, new()
        {
            List<TTarget> convertedModels = new List<TTarget>();
            var propertiyMaps = mapConfigurator.MatchingProperties;
            foreach (var data in datas)
            {
                var convertedModel = (TTarget)Activator.CreateInstance(typeof(TTarget));
                foreach (var propertyMap in propertiyMaps)
                {
                    var convertingData = propertyMap.Key.GetValue(data);
                    if(convertingData == null)
                        propertyMap.Value.SetValue(convertedModel, default);
                    else
                        propertyMap.Value.SetValue(convertedModel, Convert.ChangeType(convertingData, propertyMap.Value.PropertyType));
                }
                convertedModels.Add(convertedModel);
            }
            return convertedModels;
        }
    }
}
