using System;

namespace BusinessLogicLayer
{
    internal class RelatedTypes
    {
        internal Type _Interface;
        internal Type _Class;

        internal RelatedTypes(Type @interface, Type @class)
        {
            _Interface = @interface;
            _Class = @class;
        }
    }
}
