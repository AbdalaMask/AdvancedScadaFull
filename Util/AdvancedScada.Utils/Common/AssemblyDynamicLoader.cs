/*----------------------------------------------------------------
// Copyright (C) 2011 
// 版权所有。 
//
// 文件名：AssemblyDynamicLoader.cs
// 文件功能描述：该类用来创建新的AppDomain，并生成用来执行.net程序的RemoteLoader类
 * AssemblyDynamicLoader　loader　=　new　AssemblyDynamicLoader();
　 String output　=　loader.InvokeMethod("fileName",　"ymtcla",　"yjoinp",　"ymtpgm");
　 loader.Unload();
//
// 
// 创建标识：陈奕昆 2011-4-4
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Common
{
    public class AssemblyDynamicLoader
    {
        ///　 
        ///　The　new　appdomain. 
        ///　 
        private AppDomain appDomain;
        ///　 
        ///　The　remote　loader. 
        ///　 
        private RemoteLoader remoteLoader;
        ///　 
        ///　Initializes　a　new　instance　of　the　　class. 
        ///　 
        public AssemblyDynamicLoader()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "ApplicationLoader";
            setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            setup.PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private");
            setup.CachePath = setup.ApplicationBase;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = setup.ApplicationBase;
            this.appDomain = AppDomain.CreateDomain("ApplicationLoaderDomain", null, setup);
            String name = Assembly.GetExecutingAssembly().GetName().FullName;
            this.remoteLoader = (RemoteLoader)this.appDomain.CreateInstanceAndUnwrap(name, typeof(RemoteLoader).FullName);
        }
        ///　 
        ///　Invokes　the　method. 
        ///　 
        ///　The　full　name. 
        ///　Name　of　the　class. 
        ///　The　args　input. 
        ///　Name　of　the　program. 
        ///　The　output　of　excuting. 
        public object InvokeMethod(String fullName, String className, Object[] argsInput, String methodName)
        {
            this.remoteLoader.InvokeMethod(fullName, className, argsInput, methodName);
            return this.remoteLoader.Output;
        }
        ///　 
        ///　Unloads　this　instance. 
        ///　 
        public void Unload()
        {
            try
            {
                AppDomain.Unload(this.appDomain);
                this.appDomain = null;
            }
            catch (CannotUnloadAppDomainException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
