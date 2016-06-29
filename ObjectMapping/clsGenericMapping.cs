using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace ObjectMapping
{

        public class clsGenericMapping<TSource, TDestination>
        {
            public static IMappingExpression<TSource, TDestination> CreateMapping()
            {
                return Mapper.CreateMap<TSource, TDestination>();
            }
            public static void CreateMappingSingle()
            {
                Mapper.CreateMap<TSource, TDestination>();
            }
            /// <summary>
            /// Only Map
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public static TDestination Map(TSource source)
            {
                var res = Mapper.Map<TSource, TDestination>(source);
                return res;
            }
            /// <summary>
            /// Map Whit CreateMap
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public static TDestination MapToDestination(TSource source)
            {
                Mapper.CreateMap<TSource, TDestination>();
                var res = Mapper.Map<TSource, TDestination>(source);
                return res;
            }


        }
    }
