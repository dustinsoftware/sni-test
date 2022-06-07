// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Reflection;

if (args.Length == 0 || args[0] == null) {
    throw new ArgumentException("Please specify sql server instance");
}

var server = args[0];

Type type = Assembly.LoadFrom("bin/Debug/net7.0/System.Data.SqlClient.dll").GetType("System.Data.SqlClient.SNI.SNITCPHandle");

Console.WriteLine(string.Join(',', (await Dns.GetHostAddressesAsync(server)).Select(x => x.ToString())));

var ctor = type.GetConstructor(new[] { typeof(string) , typeof(int) , typeof(long) , typeof(object) , typeof(bool) });
var instance = ctor.Invoke(new object[] { server, 1433, long.MaxValue, new object(), true } );

var status = instance.GetType().GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance);
var socket = instance.GetType().GetField("_socket", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance) as System.Net.Sockets.Socket;


Console.WriteLine($"Status: {status}");
Console.WriteLine($"Socket: {socket.RemoteEndPoint}");
