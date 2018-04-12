using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoACMachine.Crawler
{
    interface ICrawler
    {
        List<string> GetACArticleLinks(int problemID, string oj = "hdu");
        string GetCodeByArticleLink(string articleLink);
    }
}
