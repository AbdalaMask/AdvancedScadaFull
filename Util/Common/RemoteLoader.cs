using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Common
{
    public class RemoteLoader : MarshalByRefObject
    {
        ///　 
        ///　The　assembly　we　need. 
        ///　 
        private Assembly assembly = null;
        ///　 
        ///　The　output. 
        ///　 
        private object output = null;
        ///　 
        ///　Gets　the　output. 
        ///　 
        ///　The　output. 
        public object Output
        {
            get
            {
                return this.output;
            }
        }
        ///　 
        ///　Invokes　the　method. 
        ///　 
        ///　The　full　name. 
        ///　Name　of　the　class. 
        ///　The　args　input. 
        ///　Name　of　the　program. 
        public void InvokeMethod(String fullName, String className,
            object[] args, String methodName)
        {
            this.assembly = null;
            this.output = String.Empty;
            try
            {
                this.assembly = Assembly.LoadFrom(fullName);
                object eval = this.assembly.CreateInstance(className);
                System.Reflection.MethodInfo method = eval.GetType().GetMethod(methodName);
                this.output = method.Invoke(eval, args);
                //Type pgmType = null;
                //if (this.assembly != null)
                //{
                //    pgmType = this.assembly.GetType(className, true, true);
                //}
                //else
                //{
                //    pgmType = Type.GetType(className, true, true);
                //}
                //Object[] args = RunJob.GetArgs(argsInput);
                //BindingFlags defaultBinding = BindingFlags.DeclaredOnly
                //    | BindingFlags.Public
                //| BindingFlags.NonPublic | BindingFlags.Instance
                //| BindingFlags.IgnoreCase
                //| BindingFlags.InvokeMethod | BindingFlags.Static;
                //CultureInfo cultureInfo = new CultureInfo("es-ES", false);
                //try
                //{
                //    MethodInfo methisInfo = RunJob.GetItsMethodInfo(pgmType,
                //        defaultBinding, programName);
                //    if (methisInfo == null)
                //    {
                //        this.output = "EMethod　does　not　exist!";
                //    }
                //    if (methisInfo.IsStatic)
                //    {
                //        if (methisInfo.GetParameters().Length == 0)
                //        {
                //            if (methisInfo.ReturnType == typeof(void))
                //            {
                //                pgmType.InvokeMember(programName, defaultBinding,
                //                    null, null, null, cultureInfo);
                //                this.output = "STo　call　a　method　without　return　value　successful.";
                //            }
                //            else
                //            {
                //                this.output = (String)pgmType.InvokeMember(programName,
                //                    defaultBinding, null, null, null, cultureInfo);
                //            }
                //        }
                //        else
                //        {
                //            if (methisInfo.ReturnType == typeof(void))
                //            {
                //                pgmType.InvokeMember(programName, defaultBinding,
                //                    null, null, args, cultureInfo);
                //                this.output = "STo　call　a　method　without　return　value　successful.";
                //            }
                //            else
                //            {
                //                this.output = (String)pgmType.InvokeMember(programName,
                //                    defaultBinding, null, null, args, cultureInfo);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (methisInfo.GetParameters().Length == 0)
                //        {
                //            object pgmClass = Activator.CreateInstance(pgmType);
                //            if (methisInfo.ReturnType == typeof(void))
                //            {
                //                pgmType.InvokeMember(programName, defaultBinding,
                //                    null, pgmClass, null, cultureInfo);
                //                this.output = "STo　call　a　method　without　return　value　successful.";
                //            }
                //            else
                //            {
                //                this.output = (String)pgmType.InvokeMember(programName,
                //                    defaultBinding, null, pgmClass, null, cultureInfo);　　　//'ymtpgm'　is　program's　name　and　the　return　value　of　it　must　be　started　with　'O'. 
                //            }
                //        }
                //        else
                //        {
                //            object pgmClass = Activator.CreateInstance(pgmType);
                //            if (methisInfo.ReturnType == typeof(void))
                //            {
                //                pgmType.InvokeMember(programName, defaultBinding,
                //                    null, pgmClass, args, cultureInfo);
                //                this.output = "STo　call　a　method　without　return　value　successful.";
                //            }
                //            else
                //            {
                //                this.output = (String)pgmType.InvokeMember(programName,
                //                    defaultBinding, null, pgmClass, args, cultureInfo);　　　//'ymtpgm'　is　program's　name　and　the　return　value　of　it　must　be　started　with　'O'. 
                //            }
                //        }
                //    }
                //}
                //catch
                //{
                //    this.output = (String)pgmType.InvokeMember(programName,
                //        defaultBinding, null, null, null, cultureInfo);
                //}
            }
            catch (Exception e)
            {
                this.output = "E" + e.Message;
            }
        }
    }
}
