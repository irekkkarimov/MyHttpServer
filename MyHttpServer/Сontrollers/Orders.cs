using System.Net;
using System.Text;
using HtmlAgilityPack;
using MyHttpServer.Attributes;

namespace MyHttpServer.Handlers.controllers;

[Controller("Orders")]
public class Orders
{
    [Get("List")]
    public static async void List(HttpListenerContext context)
    {
        var html1 = "https://steamcommunity.com/market/";
        var html2 = "https://steamcommunity.com/market/search?q=#p1_popular_desc";
        var web1 = new HtmlWeb();
        var web2 = new HtmlWeb();

        var htmlDoc1 = web1.Load(html1);
        var htmlDoc2 = web2.Load(html2);
        var node1 = htmlDoc1.GetElementbyId("popularItemsRows");
        var node2 = htmlDoc2.GetElementbyId("searchResultsRows");
        var node1ATag = node1.SelectNodes(".//a");
        var node2ATag = node2.SelectNodes(".//a");
        HtmlNode[] childNodes = new HtmlNode[15];

        for (var i = 0; i < 15; i++)
        {
            if (i < 10)
            {
                childNodes[i] = node1ATag[i];
            }
            else
            {
                childNodes[i] = node2ATag[i - 10];
            }
        }
        var response = context.Response;

        var toExport1 = childNodes[0].InnerHtml + childNodes[1].InnerHtml + childNodes[2].InnerHtml;
        var toExport2 =childNodes[3].InnerHtml + childNodes[4].InnerHtml + childNodes[5].InnerHtml;
        var toExport3 = childNodes[6].InnerHtml + childNodes[7].InnerHtml + childNodes[8].InnerHtml;
        var toExport4 = childNodes[9].InnerHtml + childNodes[10].InnerHtml + childNodes[11].InnerHtml;
        var toExport5 = childNodes[12].InnerHtml + childNodes[13].InnerHtml + childNodes[14].InnerHtml;
        
        var resultHtml = "<html>" +
                         "<body>" +
                         "<div style=\"display: flex; flex-direction: column;\">" +
                         "<div style=\"display:flex; width: 400px; margin: 100px;\">" + toExport1 +  "</div>" +
                         "<div style=\"display:flex; width: 400px; margin: 100px;\">" + toExport2 +  "</div>" +
                         "<div style=\"display:flex; width: 400px; margin: 100px;\">" + toExport3 +  "</div>" +
                         "<div style=\"display:flex; width: 400px; margin: 100px;\">" + toExport4 +  "</div>" +
                         "<div style=\"display:flex; width: 400px; margin: 100px;\">" + toExport5 +  "</div>" +
                         "</div>" +
                         "</body>" +
                         "</html>";
        
        
        
        var buffer = Encoding.ASCII.GetBytes(resultHtml);
        response.ContentType = "text/html; charset=utf-8";
        response.ContentLength64 = buffer.Length;
        await using Stream output = response.OutputStream;
            
        await output.WriteAsync(buffer);
        await output.FlushAsync();

    }
}