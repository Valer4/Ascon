using System;

namespace BusinessLogicLayer
{
    public class RelatedTypes
    {
        public Type Interface;
        public Type Class;

        public RelatedTypes(Type @interface, Type @class)
        {
            Interface = @interface;
            Class = @class;
        }
    }
}
