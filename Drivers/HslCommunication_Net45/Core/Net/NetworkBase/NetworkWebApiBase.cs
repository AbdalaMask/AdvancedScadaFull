using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
#if !NET35
using System.Net.Http;
#endif

namespace HslCommunication.Core.Net
{
    /// <summary>
    /// 基于webapi的数据访问的基类
    /// </summary>
    public class NetworkWebApiBase
    {

        #region Constrcutor

        /// <summary>
        /// 使用指定的ip地址来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        public NetworkWebApiBase( string ipAddress )
        {
            this.ipAddress = ipAddress;

#if !NET35
            this.httpClient = new HttpClient( );
#endif
        }

        /// <summary>
        /// 使用指定的ip地址来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        /// <param name="port">端口号信息</param>
        public NetworkWebApiBase( string ipAddress, int port )
        {
            this.ipAddress = ipAddress;
            this.port = port;
#if !NET35
            this.httpClient = new HttpClient( );
#endif
        }

        /// <summary>
        /// 使用指定的ip地址及端口号来初始化对象
        /// </summary>
        /// <param name="ipAddress">Ip地址信息</param>
        /// <param name="port">端口号信息</param>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        public NetworkWebApiBase( string ipAddress, int port, string name, string password )
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.name = name;
            this.password = password;

#if !NET35
            if (!string.IsNullOrEmpty( name ) && !string.IsNullOrEmpty( password ))
            {
                var handler = new HttpClientHandler { Credentials = new NetworkCredential( name, password ) };
                handler.Proxy = null;
                handler.UseProxy = false;

                this.httpClient = new HttpClient( handler );
            }
            else
            {
                this.httpClient = new HttpClient( );
            }

#endif
        }

        #endregion

        #region Protect Method

        /// <summary>
        /// 等待重写的额外的指令信息的支持。
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns>是否读取成功的内容</returns>
        protected virtual OperateResult<string> ReadByAddress(string address)
        {
            return new OperateResult<string>( StringResources.Language.NotSupportedFunction );
        }

        #endregion

        #region Read Write Support

        /// <summary>
        /// 读取对方信息的的数据信息，通常是针对GET的方法信息设计的。如果使用了url=开头，就表示是使用了原生的地址访问
        /// </summary>
        /// <param name="address">无效参数</param>
        /// <returns>带有成功标识的byte[]数组</returns>
        public virtual OperateResult<byte[]> Read( string address )
        {
            OperateResult<string> read = ReadString( address );
            if (!read.IsSuccess) return OperateResult.CreateFailedResult<byte[]>( read );

            return OperateResult.CreateSuccessResult( Encoding.UTF8.GetBytes( read.Content ) );
        }

        /// <summary>
        /// 读取对方信息的的数据信息，通常是针对GET的方法信息设计的。如果使用了url=开头，就表示是使用了原生的地址访问
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns>带有成功标识的字符串数据</returns>
        public virtual OperateResult<string> ReadString( string address )
        {
            if (!Authorization.nzugaydgwadawdibbas( )) return new OperateResult<string>( StringResources.Language.AuthorizationFailed );

            if (address.StartsWith( "url=" ) || address.StartsWith( "URL=" ))
            {
                address = address.Substring( 4 );
                string url = $"http://{ipAddress}:{port}/{ (address.StartsWith( "/" ) ? address.Substring( 1 ) : address) }";

                try
                {
#if !NET35
                    using (HttpResponseMessage response = httpClient.GetAsync( url ).Result)
                    using (HttpContent content = response.Content)
                    {
                        response.EnsureSuccessStatusCode( );
                        string result = content.ReadAsStringAsync( ).Result;

                        return OperateResult.CreateSuccessResult( result );
                    }
#else
                    WebClient webClient = new WebClient( );
                    if (!string.IsNullOrEmpty( name ) || !string.IsNullOrEmpty( password ))
                        webClient.Credentials = new NetworkCredential( name, password );

                    byte[] content = webClient.DownloadData( url );
                    webClient.Dispose( );
                    return OperateResult.CreateSuccessResult( Encoding.UTF8.GetString( content ) );
#endif
                }
                catch (Exception ex)
                {
                    return new OperateResult<string>( ex.Message );
                }
            }
            else
            {
                return ReadByAddress( address );
            }
        }

        /// <summary>
        /// 使用POST的方式来向对方进行请求数据信息
        /// </summary>
        /// <param name="address">指定的地址信息，有些设备可能不支持</param>
        /// <param name="value">原始的字节数据信息</param>
        /// <returns>是否成功的写入</returns>
        public virtual OperateResult Write( string address, byte[] value )
        {
            return Write( address, Encoding.Default.GetString( value ) );
        }

        /// <summary>
        /// 使用POST的方式来向对方进行请求数据信息
        /// </summary>
        /// <param name="address">指定的地址信息</param>
        /// <param name="value">字符串的数据信息</param>
        /// <returns>是否成功的写入</returns>
        public virtual OperateResult Write( string address, string value )
        {
            if (address.StartsWith( "url=" ) || address.StartsWith( "URL=" ))
            {
                address = address.Substring( 4 );
                string url = $"http://{ipAddress}:{port}/{ (address.StartsWith( "/" ) ? address.Substring( 1 ) : address) }";

                try
                {
#if !NET35
                    using (StringContent stringContent = new StringContent( value ))
                    using (HttpResponseMessage response = httpClient.PostAsync( url, stringContent ).Result)
                    using (HttpContent content = response.Content)
                    {
                        response.EnsureSuccessStatusCode( );
                        string result = content.ReadAsStringAsync( ).Result;

                        return OperateResult.CreateSuccessResult( result );
                    }
#else
                    WebClient webClient = new WebClient( );
                    webClient.Proxy = null;
                    if (!string.IsNullOrEmpty( name ) && !string.IsNullOrEmpty( password ))
                        webClient.Credentials = new NetworkCredential( name, password );

                    byte[] content = webClient.UploadData( url, Encoding.UTF8.GetBytes( value ) );
                    webClient.Dispose( );
                    return OperateResult.CreateSuccessResult( Encoding.UTF8.GetString( content ) );
#endif
                }
                catch (Exception ex)
                {
                    return new OperateResult<string>( ex.Message );
                }
            }
            else
            {
                return new OperateResult<string>( StringResources.Language.NotSupportedFunction );
            }
        }

        #endregion

        #region Private Member

        private string ipAddress = "127.0.0.1";
        private int port = 80;
        private string name = string.Empty;
        private string password = string.Empty;

#if !NET35
        private HttpClient httpClient;
#endif
        #endregion
    }
}
