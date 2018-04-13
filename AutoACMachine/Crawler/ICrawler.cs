using System;
using System.Collections.Generic;

namespace AutoACMachine
{
    interface ICrawler
    {
        List<string> GetACArticleLinks(int problemID, string oj = "hdu");
        string GetCodeByArticleLink(string articleLink, int problemID = 1000, string oj = "hdu");
    }
}
