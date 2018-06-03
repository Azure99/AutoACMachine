using System;

namespace OnlineJudgeClient
{
    public interface IOnlineJudgeClient
    {
        string OJName { get; }
        string Username { get; set; }
        string Password { get; set; }
        int Timeout { get; set; }
        bool Login(string username, string password);
        bool IsLogined();
        bool Submit(int problemID, string code, int language = -1);
        JudgeStatus GetJudgeStatus(int problemID, string author, int language = -1);
        bool IsAvailable(int problemID);
        bool IsAccepted(int problemID, string username);
    }
}
