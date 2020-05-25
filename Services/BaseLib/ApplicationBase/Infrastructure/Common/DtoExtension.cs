using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationBase.Infrastructure.Common
{

    public static class DtoExtension
    {
        private static IGlobalTool _globalTool;
        public static void BuilderTool(IGlobalTool globalTool)
        {
            _globalTool = globalTool;
        }
        public static Tout SetDto<Tin, Tout>(this Tin model) where Tin : class where Tout : class
        {
            return _globalTool.Map<Tin, Tout>(model);
        }
        public static List<Tout> SetDto<Tin, Tout>(this IEnumerable<Tin> list) where Tin : class where Tout : class
        {
            return _globalTool.MapList<Tin, Tout>(list);
        }
    }
}
