using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HslCommunication.LogNet;

namespace HslCommunication.Enthernet
{
    /// <summary>
    /// 一个支持完全自定义的Http服务器，支持返回任意的数据信息，方便调试信息
    /// </summary>
    public class HttpServer
    {

        #region Constrcutor

        /// <summary>
        /// 实例化一个默认的对象，当前的运行，需要使用管理员的模式运行
        /// </summary>
        public HttpServer( )
        {

        }

        #endregion

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="port">端口号信息</param>
        /// <exception cref="HttpListenerException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public void Start( int port )
        {
            this.port = port;
            this.listener = new HttpListener( );
            this.listener.Prefixes.Add( $"http://+:{port}/" );
            this.listener.Start( );
            this.listener.BeginGetContext( GetConnectCallBack, this.listener );
            this.logNet?.WriteDebug( $"{ToString( )} Server Started, wait for connections" );
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        public void Close( )
        {
            this.listener?.Close( );
        }

        private void GetConnectCallBack( IAsyncResult ar )
        {
            if (ar.AsyncState is HttpListener listener)
            {
                HttpListenerContext context = null;
                try
                {
                    context = listener.EndGetContext( ar );
                }
                catch (Exception ex)
                {
                    logNet?.WriteException( ToString( ), ex );
                }

                int restartcount = 0;
                while (true)
                {
                    try
                    {
                        listener.BeginGetContext( GetConnectCallBack, listener );
                        break;
                    }
                    catch (Exception ex)
                    {
                        logNet?.WriteException( ToString( ), ex );
                        restartcount++;
                        if(restartcount >= 3)
                        {
                            logNet?.WriteError( ToString( ) + " ReGet Content Failed!" );
                            return;
                        }
                        System.Threading.Thread.Sleep( 1000 );
                    }
                }

                if (context == null) return;
                var request = context.Request;
                var response = context.Response;

                if (IsCrossDomain)
                {
                    // 如果是js的ajax请求，还可以设置跨域的ip地址与参数
                    context.Response.AppendHeader( "Access-Control-Allow-Origin", request.Headers["Origin"] );           //后台跨域请求，通常设置为配置文件
                    context.Response.AppendHeader( "Access-Control-Allow-Headers", "*" );                                 //后台跨域参数设置，通常设置为配置文件
                    context.Response.AppendHeader( "Access-Control-Allow-Method", "POST,GET,PUT,OPTIONS,DELETE" );       //后台跨域请求设置，通常设置为配置文件
                    context.Response.AppendHeader( "Access-Control-Allow-Credentials", "true" );
                    context.Response.AppendHeader( "Access-Control-Max-Age", "3600" );
                }
                context.Response.AddHeader( "Content-type", "Content-Type: text/html; charset=utf-8" ); // 添加响应头信息
                //context.Response.ContentType = "Content-Type: text/html; charset=utf-8";
                //context.Response.ContentEncoding = encoding;

                string data = GetDataFromRequest( request );
                response.StatusCode = 200;
                try
                {
                    string ret = HandleRequest( request, response, data );
                    using (var stream = response.OutputStream)
                    {
                        // 把处理信息返回到客户端
                        if (string.IsNullOrEmpty( ret ))
                        {
                            stream.Write( new byte[0], 0, 0 );
                        }
                        else
                        {
                            byte[] buffer = encoding.GetBytes( ret );
                            stream.Write( buffer, 0, buffer.Length );
                        }
                    }

                    this.logNet?.WriteDebug( $"{ToString( )} New Request, {request.RawUrl}" );
                }
                catch (Exception ex)
                {
                    logNet?.WriteException( $"{ToString( )} HandleRequest", ex );
                }
            }
        }

        private string GetDataFromRequest( HttpListenerRequest request )
        {
            try
            {
                var byteList = new List<byte>( );
                var byteArr = new byte[2048];
                int readLen = 0;
                int len = 0;
                // 接收客户端传过来的数据并转成字符串类型
                do
                {
                    readLen = request.InputStream.Read( byteArr, 0, byteArr.Length );
                    len += readLen;
                    byteList.AddRange( byteArr );
                } while (readLen != 0);
                return encoding.GetString( byteList.ToArray( ), 0, len );
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 支持重写的请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="response">回应</param>
        /// <param name="data">Body数据</param>
        /// <returns>返回的内容</returns>
        protected virtual string HandleRequest( HttpListenerRequest request, HttpListenerResponse response, string data )
        {
            if (HandleRequestFunc != null) return HandleRequestFunc.Invoke( request, response, data );
            return "This is HslWebServer, Thank you for use!";
        }


        #region Public Properties

        /// <summary>
        /// 系统的日志信息
        /// </summary>
        public ILogNet LogNet
        {
            get => logNet;
            set => logNet = value;
        }

        /// <summary>
        /// 当前服务器的编码信息
        /// </summary>
        public Encoding ServerEncoding
        {
            get => encoding;
            set => encoding = value;
        }

        /// <summary>
        /// 获取或设置是否支持跨域操作。
        /// </summary>
        public bool IsCrossDomain
        {
            get;
            set;
        }

        /// <summary>
        /// 当前的自定义的处理信息
        /// </summary>
        public Func<HttpListenerRequest, HttpListenerResponse, string, string> HandleRequestFunc
        {
            get => handleRequestFunc;
            set => handleRequestFunc = value;
        }

        #endregion

        #region Private Member

        private int port = 80;                                               // 当前服务器的端口号
        private HttpListener listener;                                       // 侦听的服务器信息
        private ILogNet logNet;                                              // 日志信息
        private Encoding encoding = Encoding.UTF8;                           // 当前系统的编码
        private Func<HttpListenerRequest, HttpListenerResponse, string, string> handleRequestFunc;

        #endregion

        #region Object Override

        /// <summary>
        /// 返回表示当前对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString( )
        {
            return $"HttpServer[{port}]";
        }

        #endregion
    }
}
