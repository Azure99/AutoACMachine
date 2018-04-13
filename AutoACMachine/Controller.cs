using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineJudgeClient;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoACMachine
{
    class Controller
    {
        public IOnlineJudgeClient client = null;
        public ICrawler crawler = null;
        public string Username { get; set; }
        public string Password { get; set; }

        public bool SaveCode { get; set; }

        public int MaxRetry { get; set; }

        /// <summary>
        /// 构造自动AC控制器
        /// </summary>
        /// <param name="client">OJ客户端实例</param>
        /// <param name="crawler">代码爬虫实例</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public Controller(IOnlineJudgeClient client, ICrawler crawler, string username, string password)
        {
            this.client = client;
            this.crawler = crawler;
            Username = username;
            Password = password;
            MaxRetry = 100;
        }

        public bool Login()
        {
            WriteMessage("正在登录账号:" + Username);
            bool isSuccess = client.Login(Username, Password);
            if(isSuccess)
            {
                WriteMessage("登录成功");
            }
            else
            {
                WriteMessage("登录失败");
            }
            return isSuccess;
        }

        public void AutoAC(int startID, int endID)
        {
            int retryCount = 0;
            while (!Login())
            {
                if (++retryCount >= 3)
                {
                    return;
                }
            }

            for (int i = startID; i <= endID; i++)
            {
                SolveStatus status = SolveProblem(i, MaxRetry);

                if(status.Solved)
                {
                    WriteMessage(string.Format("题目{0}AC成功!", i));

                    if (SaveCode && status.Detail == "Accepted") //保存代码
                    {
                        try
                        {
                            if(!Directory.Exists("Code"))
                            {
                                Directory.CreateDirectory("Code");
                            }
                            File.WriteAllText(string.Format("Code\\{0}-{1}.txt", client.OJName, i), status.Code);
                        }
                        catch
                        {
                            WriteMessage("保存代码失败!");
                        }
                    }

                }
                else
                {
                    WriteMessage(string.Format("题目{0}AC失败!详情{1}", i, status.Detail));
                }
                WriteMessage("----------");
            }
        }

        public SolveStatus SolveProblem(int problemID, int maxRetry)
        {
            WriteMessage(string.Format("正在自动AC{0}的{1}题目", client.OJName, problemID));
            SolveStatus solveStatus;
            solveStatus.Solved = false;
            solveStatus.Detail = "UnknownError";
            solveStatus.Code = "已AC，无需爬取，因此无代码";

            if (client.IsAccepted(problemID, Username))//题目已经AC
            {
                solveStatus.Solved = true;
                solveStatus.Detail = "AlreadyAccepted";
                return solveStatus;
            }

            int retryCount = 0;//尝试次数
            while (!client.IsAvailable(problemID))//问题是否可用
            {
                if (++retryCount >= 3)
                {
                    solveStatus.Detail = "ProblemNotFound";
                    return solveStatus;
                }
            }

            WriteMessage("正在爬取题目代码");
            List<string> articlesList = crawler.GetACArticleLinks(problemID, client.OJName);//抓取题解链接
            List<string> acCodeList = new List<string>();
            foreach (string artLink in articlesList)//抓取题解代码
            {
                string code = crawler.GetCodeByArticleLink(artLink, problemID, client.OJName);
                if (!string.IsNullOrEmpty(code))
                {
                    acCodeList.Add(code);
                }
            }

            if (articlesList.Count == 0 || acCodeList.Count == 0)//无代码
            {
                solveStatus.Detail = "AcCodeNotFound";
                return solveStatus;
            }
            WriteMessage("共爬取到" + acCodeList.Count.ToString() + "份代码");

            JudgeStatus status = JudgeStatus.Unknown;
            foreach (string code in acCodeList)//尝试所有代码，直到AC为止
            {
                if (maxRetry-- <= 0)//最大尝试次数
                {
                    solveStatus.Detail = "RetryLimitExceeded";
                    WriteMessage("超出最大尝试次数");
                    return solveStatus;
                }
                WriteMessage(maxRetry.ToString());

                WriteMessage(string.Format("正在提交题目{0}，代码长度{1}", problemID, code.Length));
                retryCount = 0;
                while (!client.Submit(problemID, code))
                {
                    if (++retryCount >= 3)
                    {
                        continue;
                    }
                }


                WriteMessage("正在取回判题结果");
                System.Threading.Thread.Sleep(6000);//提交后先等待
                status = client.GetJudgeStatus(problemID, Username);

                while (status == JudgeStatus.Queuing || status == JudgeStatus.Compiling || status == JudgeStatus.Running)
                {
                    status = client.GetJudgeStatus(problemID, Username);
                    System.Threading.Thread.Sleep(2000);
                }

                WriteMessage(string.Format("OJ:{0}\tProblemID:{1}\tResult:{2}", client.OJName, problemID, status));
                if (status == JudgeStatus.Accepted)
                {
                    solveStatus.Solved = true;
                    solveStatus.Detail = "Accepted";
                    solveStatus.Code = Regex.Replace(code, "\r\n|\r|\n", Environment.NewLine);
                    return solveStatus;
                }
                else
                {
                    solveStatus.Detail = status.ToString();
                }
            }

            return solveStatus;
        }

        public void WriteMessage(string message)
        {
            MainForm.mainForm.AppendMessage(message);
        }
    }
}
