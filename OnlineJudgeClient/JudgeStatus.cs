using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineJudgeClient
{
    public enum JudgeStatus
    {
        Unknown = -1,
        Accepted = 0,
        WrongAnswer = 1,
        PresentationError = 2,
        CompilationError = 3,
        RuntimeError = 4,
        TimeLimitExceeded = 5,
        MemoryLimitExceeded = 6,
        OutputLimitExceeded = 7,
        Queuing = 10,
        Compiling = 11,
        Running = 12
    }
}
