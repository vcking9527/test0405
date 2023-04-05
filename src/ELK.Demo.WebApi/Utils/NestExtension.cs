using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace ELK.Demo.WebApi.Utils
{
    public static class NestExtension
    {
        public static AggregationRangeDescriptor AgeFrom(this AggregationRangeDescriptor descriptor, int from, int to)
        {
            return descriptor.Key($"{from}-{to}").From(from).To(to);
        }
    }
}