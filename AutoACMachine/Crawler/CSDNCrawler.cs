using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AutoACMachine
{
    public class CSDNCrawler : ICrawler
    {
        /// <summary>
        /// 获取题解的ID
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <param name="oj">OJ代号</param>
        /// <returns>ID列表</returns>
        public List<string> GetACArticleLinks(int problemID, string oj = "hdu")
        {
            string page = "";
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;
                    string url = string.Format("http://www.baidu.com/s?wd={0}%20{1}site%3Ablog.csdn.net", oj, problemID);
                    page = wc.DownloadString(url);
                }
            }
            catch { }
            HashSet<string> answersList = new HashSet<string>();

            try
            {
                const string startStr = "<div class=\"f13\"><a target=\"_blank\" href=\"";
                int startP = page.IndexOf(startStr);
                while (startP != -1)
                {
                    int endP = page.IndexOf("\"", startP + startStr.Length);
                    string answerLink = page.Substring(startP + startStr.Length, endP - (startP + startStr.Length));

                    if (answerLink.IndexOf("baidu") != -1)
                    {
                        answersList.Add(answerLink);
                    }
                    startP = page.IndexOf(startStr, endP);
                }
            }
            catch { }

            return answersList.ToList();
        }

        /// <summary>
        /// 由题解链接获取代码
        /// </summary>
        /// <param name="articleLink">题解链接</param>
        /// <returns>代码</returns>
        public string GetCodeByArticleLink(string articleLink, int problemID = 1000, string oj = "hdu")
        {
            string page = "";

            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;
                    wc.Encoding = Encoding.UTF8;
                    page = wc.DownloadString(articleLink);
                }
            }
            catch { }

            try
            {
                string startStr = "<pre ";
                int startP = page.IndexOf(startStr);
                if (startP == -1)
                {
                    return "";
                }


                if (!JudgeTittle(page, problemID, oj))
                {
                    return "";
                }

                startP = page.IndexOf("#include", startP);
                int endP = page.IndexOf("</pre>", startP);

                string code = page.Substring(startP, endP - startP);
                code = System.Web.HttpUtility.HtmlDecode(code);
                code = Pertreat(code);

                if (!JudgeCode(code))
                {
                    return "";
                }

                return code;
            }
            catch
            {
                return "";
            }
        }

        private string Pertreat(string code)//预处理
        {
            code = code.Replace("</span>", "");
            code = code.Replace("</code>", "");

            while (code.IndexOf("<span class=") != -1)
            {
                int startP = code.IndexOf("<span class=");
                int endP = code.IndexOf(">", startP);
                string tag = code.Substring(startP, endP - startP + 1);
                code = code.Replace(tag, "");
            }

            if (code.IndexOf("memset") != -1 && code.IndexOf("string") == -1)
            {
                code = "#include <cstring>\n" + code;
            }
            return code;
        }

        public bool JudgeTittle(string page, int problemID, string ojName)
        {
            int startP = page.IndexOf("<title>");
            int endP = page.IndexOf("</title>", startP);
            string title = page.Substring(startP + "<title>".Length, endP - (startP + "<title>".Length));
            Console.WriteLine(title);
            if (title.IndexOf("CSDN博客") == -1)
            {
                return false;
            }

            if (title.IndexOf(problemID.ToString()) == -1)
            {
                return false;
            }

            if (title.ToLower().IndexOf(ojName.ToLower()) == -1)
            {
                return false;
            }

            if (title.IndexOf("赛") != -1 && problemID <= 1015)
            {
                return false;
            }
            return true;
        }

        public bool JudgeCode(string code)//判断代码是否合法
        {
            if (code.IndexOf("<span") != -1 || code.IndexOf("<font") != -1 || code.IndexOf("<pre") != -1 || code.IndexOf("code>") != -1 || code.IndexOf("<br />") != -1)
            {
                return false;
            }

            if (code.IndexOf("include") == -1)
            {
                return false;
            }

            return true;
        }
    }
}
