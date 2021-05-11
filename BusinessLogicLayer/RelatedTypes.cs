using System;

namespace BusinessLogicLayer
{
    public class RelatedTypes
    {
        public Type _Interface;
        public Type _Class;

        public RelatedTypes(Type @interface, Type @class)
        {
            _Interface = @interface;
            _Class = @class;
        }
    }
}
