using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaUIHelp.Messages
{
    public class TypeMessage
    {
        public object Obj;

        public Type Type;

        public TypeMessage(object obj)
        {
            Obj = obj;
            Type = obj.GetType();
        }
    }

    public class TypeMessage<T>
    {
        public T TValue;

        public Type Type;

        public TypeMessage(T t)
        {
            TValue = t;
            Type = t.GetType();
        }

        public static implicit operator TypeMessage<T>(T message)
        {
            return new TypeMessage<T>(message);
        }
    }
}
