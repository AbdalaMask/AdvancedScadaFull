using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;


namespace HslCommunication.Enthernet
{
    /// <summary>
    /// 与服务器文件引擎交互的客户端类，支持操作Advanced引擎和Ultimate引擎
    /// </summary>
    /// <remarks>
    /// 这里需要需要的是，本客户端支持Advanced引擎和Ultimate引擎文件服务器，服务的类型需要您根据自己的需求来选择。
    /// </remarks>
    /// <example>
    /// 此处只演示创建实例，具体的上传，下载，删除的例子请参照对应的方法
    /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Intergration File Client" title="IntegrationFileClient示例" />
    /// </example>
    public class IntegrationFileClient : FileClientBase
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的对象，需要额外指定服务器的远程地址
        /// </summary>
        public IntegrationFileClient( )
        {

        }

        /// <summary>
        /// 通过指定的Ip地址及端口号实例化一个对象
        /// </summary>
        /// <param name="ipAddress">服务器的ip地址</param>
        /// <param name="port">端口号信息</param>
        public IntegrationFileClient( string ipAddress, int port )
        {
            ServerIpEndPoint = new System.Net.IPEndPoint( System.Net.IPAddress.Parse( ipAddress ), port );
        }

        #endregion

        #region Delete File

        /// <summary>
        /// 删除服务器的文件操作
        /// </summary>
        /// <param name="fileName">文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <returns>是否成功的结果对象</returns>
        public OperateResult DeleteFile(
            string fileName,
            string factory,
            string group,
            string id )
        {
            return DeleteFileBase( fileName, factory, group, id );
        }

        /// <summary>
        /// 删除服务器的文件操作，此处的分类为空
        /// </summary>
        /// <param name="fileName">文件名称，带后缀</param>
        /// <returns>是否成功的结果对象</returns>
        public OperateResult DeleteFile( string fileName )
        {
            return DeleteFileBase( fileName, "", "", "" );
        }

        #endregion

        #region Download File


        /// <summary>
        /// 下载服务器的文件到本地的文件操作
        /// </summary>
        /// <param name="fileName">文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="processReport">下载的进度报告</param>
        /// <param name="fileSaveName">准备本地保存的名称</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是服务器不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Download File" title="DownloadFile示例" />
        /// </example>
        public OperateResult DownloadFile(
            string fileName,
            string factory,
            string group,
            string id,
            Action<long, long> processReport,
            string fileSaveName
            )
        {
            return DownloadFileBase( factory, group, id, fileName, processReport, fileSaveName );
        }

        /// <summary>
        /// 下载服务器的文件到本地的数据流中
        /// </summary>
        /// <param name="fileName">文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="processReport">下载的进度报告</param>
        /// <param name="stream">流数据</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是服务器不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Download File" title="DownloadFile示例" />
        /// </example>
        public OperateResult DownloadFile(
            string fileName,
            string factory,
            string group,
            string id,
            Action<long, long> processReport,
            Stream stream
            )
        {
            return DownloadFileBase( factory, group, id, fileName, processReport, stream );
        }

#if !NETSTANDARD2_0 && !NETSTANDARD2_1

        /// <summary>
        /// 下载服务器的文件到本地的数据流中
        /// </summary>
        /// <param name="fileName">文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="processReport">下载的进度报告</param>
        /// <param name="bitmap">内存文件</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是服务器不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Download File" title="DownloadFile示例" />
        /// </example>
        public OperateResult DownloadFile(
            string fileName,
            string factory,
            string group,
            string id,
            Action<long, long> processReport,
            out Bitmap bitmap
            )
        {
            MemoryStream stream = new MemoryStream( );
            OperateResult result = DownloadFileBase( factory, group, id, fileName, processReport, stream );
            if (result.IsSuccess)
            {
                bitmap = new Bitmap( stream );
            }
            else
            {
                bitmap = null;
                result.IsSuccess = false;
            }
            stream.Dispose( );
            return result;
        }

#endif

        #endregion

        #region Upload File

        /// <summary>
        /// 上传本地的文件到服务器操作
        /// </summary>
        /// <param name="fileName">本地的完整路径的文件名称</param>
        /// <param name="serverName">服务器存储的文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="fileTag">文件的额外描述</param>
        /// <param name="fileUpload">文件的上传人</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是客户端不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Upload File" title="UploadFile示例" />
        /// </example>
        public OperateResult UploadFile(
            string fileName,
            string serverName,
            string factory,
            string group,
            string id,
            string fileTag,
            string fileUpload,
            Action<long, long> processReport )
        {
            if (!File.Exists( fileName )) return new OperateResult( StringResources.Language.FileNotExist );

            return UploadFileBase( fileName, serverName, factory, group, id, fileTag, fileUpload, processReport );
        }

        /// <summary>
        /// 上传本地的文件到服务器操作，服务器存储的文件名就是当前文件默认的名称
        /// </summary>
        /// <param name="fileName">本地的完整路径的文件名称</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="fileTag">文件的额外描述</param>
        /// <param name="fileUpload">文件的上传人</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        public OperateResult UploadFile(
            string fileName,
            string factory,
            string group,
            string id,
            string fileTag,
            string fileUpload,
            Action<long, long> processReport )
        {
            if (!File.Exists( fileName )) return new OperateResult( StringResources.Language.FileNotExist );

            FileInfo fileInfo = new FileInfo( fileName );
            return UploadFileBase( fileName, fileInfo.Name, factory, group, id, fileTag, fileUpload, processReport );
        }

        /// <summary>
        /// 上传本地的文件到服务器操作，服务器存储的文件名就是当前文件默认的名称
        /// </summary>
        /// <param name="fileName">本地的完整路径的文件名称</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        public OperateResult UploadFile(
            string fileName,
            string factory,
            string group,
            string id,
            Action<long, long> processReport )
        {
            if (!File.Exists( fileName )) return new OperateResult( StringResources.Language.FileNotExist );

            FileInfo fileInfo = new FileInfo( fileName );
            return UploadFileBase( fileName, fileInfo.Name, factory, group, id, "", "", processReport );
        }

        /// <summary>
        /// 上传本地的文件到服务器操作，服务器存储的文件名就是当前文件默认的名称，其余参数默认为空
        /// </summary>
        /// <param name="fileName">本地的完整路径的文件名称</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        public OperateResult UploadFile( string fileName, Action<long, long> processReport )
        {
            if (!File.Exists( fileName )) return new OperateResult( StringResources.Language.FileNotExist );

            FileInfo fileInfo = new FileInfo( fileName );
            return UploadFileBase( fileName, fileInfo.Name, "", "", "", "", "", processReport );
        }

        /// <summary>
        /// 上传数据流到服务器操作
        /// </summary>
        /// <param name="stream">数据流内容</param>
        /// <param name="serverName">服务器存储的文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="fileTag">文件的额外描述</param>
        /// <param name="fileUpload">文件的上传人</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是客户端不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Upload File" title="UploadFile示例" />
        /// </example>
        public OperateResult UploadFile(
            Stream stream,
            string serverName,
            string factory,
            string group,
            string id,
            string fileTag,
            string fileUpload,
            Action<long, long> processReport )
        {
            return UploadFileBase( stream, serverName, factory, group, id, fileTag, fileUpload, processReport );
        }

#if !NETSTANDARD2_0 && !NETSTANDARD2_1

        /// <summary>
        /// 上传内存图片到服务器操作
        /// </summary>
        /// <param name="bitmap">内存图片，不能为空</param>
        /// <param name="serverName">服务器存储的文件名称，带后缀</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <param name="fileTag">文件的额外描述</param>
        /// <param name="fileUpload">文件的上传人</param>
        /// <param name="processReport">上传的进度报告</param>
        /// <returns>是否成功的结果对象</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常，或是客户端不存在文件。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="Upload File" title="UploadFile示例" />
        /// </example>
        public OperateResult UploadFile(
            Bitmap bitmap,
            string serverName,
            string factory,
            string group,
            string id,
            string fileTag,
            string fileUpload,
            Action<long, long> processReport )
        {
            MemoryStream stream = new MemoryStream( );
            if (bitmap.RawFormat != null) bitmap.Save( stream, bitmap.RawFormat );
            else bitmap.Save( stream, System.Drawing.Imaging.ImageFormat.Bmp );
            OperateResult result = UploadFileBase( stream, serverName, factory, group, id, fileTag, fileUpload, processReport );
            stream.Dispose( );
            return result;
        }

#endif

        #endregion

        #region Get FileNames

        /// <summary>
        /// 获取指定路径下的所有的文档
        /// </summary>
        /// <param name="fileNames">获取得到的文件合集</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="DownloadPathFileNames" title="DownloadPathFileNames示例" />
        /// </example>
        public OperateResult DownloadPathFileNames(
            out GroupFileItem[] fileNames,
            string factory,
            string group,
            string id
            )
        {
            return DownloadStringArrays(
                out fileNames,
                HslProtocol.ProtocolFileDirectoryFiles,
                factory,
                group,
                id
                );
        }


        #endregion

        #region Get FolderNames

        /// <summary>
        /// 获取指定路径下的所有的文档
        /// </summary>
        /// <param name="folders">输出结果</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <returns>是否成功的结果对象</returns>
        /// <remarks>
        /// 用于分类的参数<paramref name="factory"/>，<paramref name="group"/>，<paramref name="id"/>中间不需要的可以为空，对应的是服务器上的路径系统。
        /// <br /><br />
        /// <note type="warning">
        /// 失败的原因大多数来自于网络的接收异常。
        /// </note>
        /// </remarks>
        /// <example>
        /// <code lang="cs" source="TestProject\HslCommunicationDemo\Hsl\FormFileClient.cs" region="DownloadPathFolders" title="DownloadPathFolders示例" />
        /// </example>
        public OperateResult DownloadPathFolders(
            out string[] folders,
            string factory,
            string group,
            string id
            )
        {
            return DownloadStringArrays(
                out folders,
                HslProtocol.ProtocolFileDirectories,
                factory,
                group,
                id );
        }


        #endregion

        #region Private Method

        /// <summary>
        /// 获取指定路径下的所有的文档
        /// </summary>
        /// <param name="arrays">想要获取的队列</param>
        /// <param name="protocol">指令</param>
        /// <param name="factory">第一大类</param>
        /// <param name="group">第二大类</param>
        /// <param name="id">第三大类</param>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <returns>是否成功的结果对象</returns>
        private OperateResult DownloadStringArrays<T>(
            out T[] arrays,
            int protocol,
            string factory,
            string group,
            string id
            )
        {
            OperateResult result = new OperateResult( );
            // 连接服务器
            // connect server
            OperateResult<Socket> socketResult = CreateSocketAndConnect( ServerIpEndPoint, ConnectTimeOut );
            if (!socketResult.IsSuccess)
            {
                arrays = new T[0];
                return socketResult;
            }


            // 上传信息
            OperateResult send = SendStringAndCheckReceive( socketResult.Content, protocol, "nosense" );
            if (!send.IsSuccess)
            {
                arrays = new T[0];
                return send;
            }

            // 上传三级分类
            OperateResult sendClass = SendFactoryGroupId( socketResult.Content, factory, group, id );
            if (!sendClass.IsSuccess)
            {
                arrays = new T[0];
                return sendClass;
            }

            // 接收数据信息
            OperateResult<int, string> receive = ReceiveStringContentFromSocket( socketResult.Content );
            if (!receive.IsSuccess)
            {
                arrays = new T[0];
                return receive;
            }
            socketResult.Content?.Close( );

            // 数据转化
            try
            {
                arrays = Newtonsoft.Json.Linq.JArray.Parse( receive.Content2 ).ToObject<T[]>( );
                return OperateResult.CreateSuccessResult( );
            }
            catch (Exception ex)
            {
                arrays = new T[0];
                return new OperateResult( )
                {
                    Message = ex.Message
                };
            }

        }

        #endregion

    }
}
