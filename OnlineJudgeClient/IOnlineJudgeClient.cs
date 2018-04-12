using System;

namespace OnlineJudgeClient
{
    interface IOnlineJudgeClient
    {
        string Username { get; set; }
        string Password { get; set; }
        int Timeout { get; set; }
        bool Login(string username, string password);
        bool IsLogined();
        bool Submit(int problemID, string code, int language = 0);
        JudgeStatus GetJudgeStatus(int problemID, string author, int language = 0);
        bool IsAvailable(int problemID);
        bool IsAccepted(int problemID, string username);
    }
}
