using System;
using System.Collections.Generic;
using System.Text;

namespace MapperLibrary.Exceptions
{
    public class MapperLibraryException:Exception
    {
        public MapperLibraryException()
        {
            
        }
        public MapperLibraryException(string message): base(message)
        {
            
        }
    }
}
