using System;
using log4net;


namespace StudentServiceBL.Logging
{
    public class LogSystem : ILogFactory
    {
        public LogSystem()
        {
        }

        public ILog GetLogger(Type declaringType)
        {
            return LogManager.GetLogger(declaringType);
        }
    }
}
