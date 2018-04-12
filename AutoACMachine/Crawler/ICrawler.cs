using System;
using System.Collections.Generic;

namespace AutoACMachine
{
    interface ICrawler
    {
        List<string> GetACArticleLinks(int problemID, string oj = "hdu");
        string GetCodeByArticleLink(string articleLink);
    }
}
