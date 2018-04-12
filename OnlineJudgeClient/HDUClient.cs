using System.IO;
using System.Net;
using System.Text;

namespace OnlineJudgeClient
{
    public class HDUClient : IOnlineJudgeClient
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        CookieContainer cookie = new CookieContainer();

        /// <param name="timeout">操作超时时间</param>
        public HDUClient(int timeout = 10000)
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
                string res = POST("http://acm.hdu.edu.cn/userloginex.php?action=login", string.Format("username={0}&userpass={1}&login=Sign+In", username, password));
                if (res.IndexOf(username) != -1 && res.IndexOf("wrong password") == -1)
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
                string res = GET("http://acm.hdu.edu.cn/control_panel.php");
                if (res.IndexOf("My Control Panel") != -1)
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
        public bool Submit(int problemID, string code, int language = 0)
        {
            string data = string.Format("check=0&problemid={0}&language={1}&usercode={2}",
                problemID, language, System.Web.HttpUtility.UrlEncode(code));

            try
            {
                POST("http://acm.hdu.edu.cn/submit.php?action=submit", data);
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
        public JudgeStatus GetJudgeStatus(int problemID, string author, int language = 0)
        {
            try
            {
                string url = string.Format("http://acm.hdu.edu.cn/status.php?first=&pid={0}&user={1}&lang={2}&status=0", problemID, author, language);
                string res = GET(url);

                //处理字符串的异常直接全判为Unknown，省事
                int startP = res.IndexOf("<tr align=center ><td height=22px>");

                int statusStartP = res.IndexOf("<font color=", startP);
                statusStartP = res.IndexOf(">", statusStartP) + 1;

                int statusEndP = res.IndexOf("</font>", statusStartP);

                string status = res.Substring(statusStartP, statusEndP - statusStartP);
                JudgeStatus judgeStatus;

                switch (status)
                {
                    case "Queuing":
                        judgeStatus = JudgeStatus.Queuing;
                        break;
                    case "Compiling":
                        judgeStatus = JudgeStatus.Compiling;
                        break;
                    case "Running":
                        judgeStatus = JudgeStatus.Running;
                        break;
                    case "Accepted":
                        judgeStatus = JudgeStatus.Accepted;
                        break;
                    case "Wrong Answer":
                        judgeStatus = JudgeStatus.WrongAnswer;
                        break;
                    case "Presentation Error":
                        judgeStatus = JudgeStatus.PresentationError;
                        break;
                    case "Compilation Error":
                        judgeStatus = JudgeStatus.CompilationError;
                        break;
                    case "Time Limit Exceeded":
                        judgeStatus = JudgeStatus.TimeLimitExceeded;
                        break;
                    case "Memory Limit Exceeded":
                        judgeStatus = JudgeStatus.MemoryLimitExceeded;
                        break;
                    case "Output Limit Exceeded":
                        judgeStatus = JudgeStatus.OutputLimitExceeded;
                        break;

                    default:
                        judgeStatus = JudgeStatus.Unknown;
                        break;
                }

                if (status.IndexOf("Runtime Error") != -1) //RE要特判
                {
                    judgeStatus = JudgeStatus.RuntimeError;
                }

                System.Diagnostics.Debug.WriteLine(status);
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
                string page = GET(string.Format("http://acm.hdu.edu.cn/showproblem.php?pid={0}", problemID));
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
