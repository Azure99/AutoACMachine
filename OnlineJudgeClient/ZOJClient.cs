using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace OnlineJudgeClient
{
    public class ZOJClient : IOnlineJudgeClient
    {
        public string OJName
        {
            get
            {
                return "zoj";
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        CookieContainer cookie = new CookieContainer();

        /// <param name="timeout">操作超时时间</param>
        public ZOJClient(int timeout = 10000)
        {
            Timeout = timeout;
        }

        /// <summary>
        /// 登录HDUOJ账号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>是否登录成功</returns>
        public bool Login(string username, string password)
        {
            Username = username;
            Password = password;
            try
            {
                string res = POST("http://acm.zju.edu.cn/onlinejudge/login.do", string.Format("handle={0}&password={1}", username, password));
                if (res.IndexOf(username) != -1 && res.IndexOf("invalid") == -1)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <returns>是否已登录</returns>
        public bool IsLogined()
        {
            try
            {
                string res = GET("http://acm.zju.edu.cn/onlinejudge/editProfile.do");
                if (res.IndexOf("Edit Profile") != -1)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 提交题目
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <param name="code">代码</param>
        /// <param name="language">语言</param>
        /// <returns>是否提交成功</returns>
        public bool Submit(int problemID, string code, int language = -1)
        {
            if(language == -1)
            {
                language = 2;
            }

            string data = string.Format("problemId={0}&languageId={1}&source={2}",
                GetReallyProblemID(problemID), language, System.Web.HttpUtility.UrlEncode(code));

            try
            {
                string res = POST("http://acm.zju.edu.cn/onlinejudge/submit.do", data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 取得评测状态
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <param name="author">作者</param>
        /// <param name="langudge">语言</param>
        /// <returns></returns>
        public JudgeStatus GetJudgeStatus(int problemID, string author, int language = -1)
        {
            if(language == -1)
            {
                language = 2;
            }

            try
            {
                string url = string.Format("http://acm.zju.edu.cn/onlinejudge/showRuns.do?contestId=1&search=true&problemCode={0}&handle={1}&languageIds={2}", problemID, author, language);
                string res = GET(url);

                //处理字符串的异常直接全判为Unknown，省事
                int startP = res.IndexOf("<td class=\"runJudgeStatus\">");
                startP = res.IndexOf("<td class=\"runJudgeStatus\">", startP + 10);

                int endP = res.IndexOf("</a>", startP);

                string status = res.Substring(startP, endP - startP);

                JudgeStatus judgeStatus;

                if(status.IndexOf("Accepted") != -1)
                {
                    judgeStatus = JudgeStatus.Accepted;
                }
                else if(status.IndexOf("Presentation Error") != -1)
                {
                    judgeStatus = JudgeStatus.PresentationError;
                }
                else if(status.IndexOf("Wrong Answer") != -1)
                {
                    judgeStatus = JudgeStatus.WrongAnswer;
                }
                else if(status.IndexOf("Time Limit Exceeded") != -1)
                {
                    judgeStatus = JudgeStatus.TimeLimitExceeded;
                }
                else if(status.IndexOf("Memory Limit Exceeded") != -1)
                {
                    judgeStatus = JudgeStatus.MemoryLimitExceeded;
                }
                else if(status.IndexOf("Segmentation Fault") != -1)
                {
                    judgeStatus = JudgeStatus.RuntimeError;
                }
                else if(status.IndexOf("Non-zero Exit Code") != -1)
                {
                    judgeStatus = JudgeStatus.RuntimeError;
                }
                else if(status.IndexOf("Floating Point Error") != -1)
                {
                    judgeStatus = JudgeStatus.RuntimeError;
                }
                else if(status.IndexOf("Compilation Error") != -1)
                {
                    judgeStatus = JudgeStatus.CompilationError;
                }
                else if(status.IndexOf("Output Limit Exceeded") != -1)
                {
                    judgeStatus = JudgeStatus.OutputLimitExceeded;
                }
                else if(status.IndexOf("Running") != -1)
                {
                    judgeStatus = JudgeStatus.Compiling;
                }
                else if(status.IndexOf("Compiling") != -1)
                {
                    judgeStatus = JudgeStatus.Running;
                }
                else
                {
                    judgeStatus = JudgeStatus.Unknown;
                }

                return judgeStatus;

            }
            catch
            {
                return JudgeStatus.Unknown;
            }
        }

        /// <summary>
        /// 检测题目可用状态
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <returns>是否可用</returns>
        public bool IsAvailable(int problemID)
        {
            try
            {
                string page = GET(string.Format("http://acm.zju.edu.cn/onlinejudge/showProblem.do?problemCode={0}", problemID));
                if (page.IndexOf("No such problem") == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 题目是否已AC
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool IsAccepted(int problemID, string username)
        {
            return GetJudgeStatus(problemID, username) == JudgeStatus.Accepted;
        }

        private int GetReallyProblemID(int id)//ZOJ显示的题号与提交时的题号并不是对应的
        {

            const string startStr = "<a href=\"/onlinejudge/submit.do?problemId=";
            const string endStr = "\">";
            try
            {
                string res = GET("http://acm.zju.edu.cn/onlinejudge/showProblem.do?problemCode=" + id.ToString());
                int startP = res.IndexOf(startStr);
                int endP = res.IndexOf(endStr, startP + startStr.Length);

                return int.Parse(res.Substring(startP + startStr.Length, endP - startP - startStr.Length));
            }
            catch
            {
                return 0;
            }
        }

        public string POST(string url, string data = "")
        {
            byte[] reqArgs = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.Timeout = Timeout;

            request.ContentLength = reqArgs.Length;
            request.CookieContainer = cookie;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(reqArgs, 0, reqArgs.Length);
            reqStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            string result = sr.ReadToEnd();

            sr.Close();
            return result;

        }

        public string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.Timeout = Timeout;

            request.CookieContainer = cookie;
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            string result = sr.ReadToEnd();

            sr.Close();
            return result;
        }
    }
}
