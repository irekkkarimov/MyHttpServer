using System.Net;
using System.Reflection;
using MyHttpServer.Handlers.controllers;

namespace MyHttpServer.Handlers;

public class ControllerHandler : Handler
{
    public override void HandleRequest(HttpListenerContext context)
    {
        var strParams = context.Request.Url.LocalPath
            .Split('/')
            .Skip(1)
            .ToArray();
        
        var assembly = Assembly.GetExecutingAssembly();
        var controller = assembly.GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(ControllerAttribute)))
            .FirstOrDefault(c => string.Equals(c.Name, strParams[0], StringComparison.CurrentCultureIgnoreCase));

        if (controller != null)
        {
            var method = controller.GetMethods()[0];
            method.Invoke(null, new [] {context});

        }
        else if (Successor != null)
        {
            Successor.HandleRequest(context);
        }
    }
}