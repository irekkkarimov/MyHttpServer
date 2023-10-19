using System.Diagnostics;
using System.Net;
using System.Reflection;
using MyHttpServer.Attributes;

namespace MyHttpServer.Handlers;

public class ControllerHandler : Handler
{
    public override void HandleRequest(HttpListenerContext context)
    {
        var request = context.Request;
        string[] formData = null;
        var strParams = context.Request.Url.LocalPath
            .Split('/')
            .Skip(1)
            .ToArray();

        var requestMethod = new GetAttribute("");
        switch (request.HttpMethod)
        {
            case "POST":
            {
                requestMethod = new GetAttribute("POST");
                break;
            }
        }

        var assembly = Assembly.GetExecutingAssembly();
        var controller = assembly.GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(ControllerAttribute)))
            .FirstOrDefault(c => string.Equals(c.Name, strParams[0], StringComparison.CurrentCultureIgnoreCase));

        if (controller != null && strParams.Length > 1)
        {
            try
            {
                using (var sr = new StreamReader(context.Request.InputStream))
                {
                    var tempData = sr.ReadToEnd();
                    if (formData != null)
                        formData = WebUtility
                            .UrlDecode(tempData)
                            .Split('&')
                            .Select(param => param.Split('=')[1])
                            .ToArray();
                }

                var methods = controller
                    .GetMethods();
                switch (request.HttpMethod)
                {
                    case "post":
                    case "Post":    
                    case "POST":
                    {
                        methods = methods.Where(m => Attribute.IsDefined(m, typeof(PostAttribute))).ToArray();
                        break;
                    }
                    case "get":
                    case "Get":    
                    case "GET":
                    {
                        methods = methods.Where(m => Attribute.IsDefined(m, typeof(GetAttribute))).ToArray();
                        break;
                    }
                }

                var method = methods
                    .FirstOrDefault(c => string.Equals(c.Name, strParams[1], StringComparison.CurrentCultureIgnoreCase));
                if (formData != null)
                {
                    var queryParams = method.GetParameters()
                        .Select((p, i) => Convert.ChangeType(formData[i], p.ParameterType))
                        .ToArray();
                    
                    Console.WriteLine("param method passed");
                    Array.ForEach(queryParams, Console.WriteLine);
                    method.Invoke(null, queryParams);
                }
                else if (method.Name == "List")
                {
                    method.Invoke(null, new object?[]{context});
                }
                else
                {
                    method.Invoke(null, Array.Empty<object>());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        else if (Successor != null)
        {
            Successor.HandleRequest(context);
        }
    }
}