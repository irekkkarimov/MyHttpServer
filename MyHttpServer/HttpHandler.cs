// using System.Net;
//
// namespace MyHttpServer;
//
// public class HttpHandler
// {
//     private ServerHandler _serverHandler;
//
//     public HttpHandler(_h)
//     
//     public static async void Handler(HttpListener httpListener, ServerHandler serverHandler)
//     {
//         var context = await httpListener.GetContextAsync();
//         var request = context.Request;
//         var response = context.Response;
//         byte[] buffer = null;
//         buffer = Router(request.Url);
//         var contentType = DetermineContentType(request.Url);
//         context.Response.ContentType = $"{contentType}; charset=utf-8";
//         response.ContentLength64 = buffer.Length;
//         await using Stream output = response.OutputStream;
//
//         await output.WriteAsync(buffer);
//         await output.FlushAsync();
//     }
//     
//     private bool CheckIfStaticFolderExists(string staticFolderPath)
//     {
//         return Directory.Exists(staticFolderPath);
//     }
//     
//     private bool CheckIfFileExists(string url)
//     {
//         return File.Exists(url);
//     }
//
//     private byte[] NotFoundHtml(ServerHandler serverHandler)
//     {
//         return File.ReadAllBytes(serverHandler._notFoundHtml);
//     }
//
//     private byte[] Router(Uri url)
//     {
//         var localPath = url.LocalPath;
//         var pathSeparated = localPath.Split("/");
//         switch (pathSeparated[1])
//         {
//             case "":
//             {
//                 return CheckIfFileExists(staticFolder + "/" + "index.html") 
//                     ? File.ReadAllBytes(staticFolder + "/" + "index.html") 
//                     : NotFoundHtml();
//             }
//             case "static":
//             {
//                 if (pathSeparated.Length < 3)
//                     return NotFoundHtml();
//                 return CheckIfFileExists(staticFolder + "/" + pathSeparated[2])
//                     ? File.ReadAllBytes(staticFolder + "/" + pathSeparated[2])
//                     : NotFoundHtml();
//             }
//             default:
//                 return CheckIfFileExists(staticFolder + localPath)
//                     ? File.ReadAllBytes(staticFolder + localPath)
//                     : NotFoundHtml();
//         }
//
//         return Array.Empty<byte>();
//     }
//
//     private string DetermineContentType(Uri url)
//     {
//         var stringUrl = url.ToString();
//         var extension = "";
//
//         try
//         {
//             extension = stringUrl.Substring(stringUrl.LastIndexOf('.'));
//         }
//         catch (Exception e)
//         {
//             extension = "text/html";
//             return extension;
//         }
//         
//         var contentType = "";
//         
//         switch (extension)
//         {
//             case ".htm":
//             case ".html":
//                 contentType = "text/html";
//                 break;
//             case ".css":
//                 contentType = "text/stylesheet";
//                 break;
//             case ".js":
//                 contentType = "text/javascript";
//                 break;
//             case ".jpg":
//                 contentType = "image/jpeg";
//                 break;
//             case ".jpeg":
//             case ".png":
//             case ".gif":
//                 contentType = "image/" + extension.Substring(1);
//                 break;
//             default:
//                 if (extension.Length > 1)
//                 {
//                     contentType = "application/" + extension.Substring(1);
//                 }
//                 else
//                 {
//                     contentType = "application/unknown";
//                 }
//                 break;
//         }
//
//         return contentType;
//     }
// }