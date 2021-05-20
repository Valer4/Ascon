using System;

namespace BusinessLogicLayer
{
    internal class RelatedTypes
    {
        internal Type Interface;
        internal Type Class;

        internal RelatedTypes(Type @interface, Type @class)
        {
            Interface = @interface;
            Class = @class;
        }
    }
}
