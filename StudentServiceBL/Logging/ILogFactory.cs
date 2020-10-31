using log4net;
using System;

namespace StudentServiceBL.Logging
{
    public interface ILogFactory
    {
        ILog GetLogger(Type declaringType);
    }
}
