using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AutoACMachine
{
    public class ACMSearchCrawler : ICrawler
    {
        /// <summary>
        /// 获取题解的ID
        /// </summary>
        /// <param name="problemID">题目ID</param>
        /// <param name="oj">OJ代号</param>
        /// <returns>ID列表</returns>
        public List<string> GetACArticleLinks(int problemID, string oj = "hdu")
        {
            oj = (oj == "hdu") ? "hdoj" : oj;

            string page = "";
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Proxy = null;
                    string url = string.Format("http://www.acmsearch.com/article/?ArticleListSearch%5BFoj%5D={0}&ArticleListSearch%5BFproblem_id%5D={1}", oj, problemID);
                    page = wc.DownloadString(url);
                }
            }
            catch { }

            List<string> answersList = new List<string>();

            try
            {
                const string startStr = "<a href=\"/article/show/";
                int startP = page.IndexOf(startStr);
                while (startP != -1)
                {
                    int endP = page.IndexOf("\" title=\"", startP);
                    string answerID = page.Substring(startP + startStr.Length, endP - (startP + startStr.Length));

                    if (int.TryParse(answerID, out int id))
                    {
                        answersList.Add("http://www.acmsearch.com/article/show/" + id.ToString());
                    }
                    startP = page.IndexOf(startStr, endP);
                }
            }
            catch { }

            return answersList;
        }

        /// <summary>
        /// 由题解链接获取代码
        /// </summary>
        /// <param name="articleLink">题解链接</param>
        /// <returns>代码</returns>
        public string GetCodeByArticleLink(string articleLink)
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
            return code;
        }

        public bool JudgeCode(string code)//判断代码是否合法
        {
            if (code.IndexOf("<span") != -1 || code.IndexOf("<font") != -1 || code.IndexOf("<pre") != -1 || code.IndexOf("code>") != -1)
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
