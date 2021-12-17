using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Lotto_test
{
    // GET RECENT WINNING NUMBERS AND PRIZE SIZE
    class LottoWinnerNumbers
    {
        public string WinNum;
        public string WinAmount;
        public LottoWinnerNumbers()
        {
            string winNum = "";
            HtmlWeb webPage = new HtmlWeb();
            HtmlDocument document = webPage.Load("https://www.lottostat.dk/all.lottotal");
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//tr").Where(x => x.InnerHtml.Contains("red_ball")).ToArray();

            string tmp = nodes[0].InnerText;
            tmp = tmp.Replace("\t", "");
            tmp = tmp.Replace("\n", "");

            List<string> tmpNumbers = new List<string>();
            tmpNumbers = tmp.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            List<string> winNumList = new List<string>();
            foreach (var rawNum in tmpNumbers)
            {
                string tmpNum = rawNum.Remove(0, 2);
                tmpNum = tmpNum.Remove(2);
                winNumList.Add(tmpNum);
            }

            foreach (var num in winNumList)
            {
                winNum += num + " ";
            }
            this.WinNum = winNum;

            HtmlWeb webPage2 = new HtmlWeb();
            HtmlDocument document2 = webPage2.Load("https://danskespil.dk/lotto");
            HtmlNode[] nodes2 = document2.DocumentNode.SelectNodes("//head").Where(x => x.InnerHtml.Contains("Spil")).ToArray();
            string amountTmp = nodes2[0].InnerText.Remove(0, 16).Substring(0, nodes2[0].InnerText.Remove(0, 16).IndexOf("kr"));
            this.WinAmount = amountTmp;
        }
    }
}
