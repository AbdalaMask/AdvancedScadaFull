using AdvancedScada.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using static AdvancedScada.Common.XCollection;

namespace AdvancedScada.IBaseService
{
    public class GetIODriver
    {
        private static readonly object mutex = new object();
        private static GetIODriver _instance;

        public static GetIODriver GetFunctions()
        {
            lock (mutex)
            {
                if (_instance == null)
                {
                    _instance = new GetIODriver();
                }
            }

            return _instance;
        }

        public IODriver GetAssemblyDrivers(string Path, string NameSpaceAndClass)
        {
            IODriver iODriver = null;
            try
            {

                iODriver = GetAssembly($@"\AdvancedScada.{Path}.Core.dll",
                    string.Format("AdvancedScada.{0}.Core.{1}Service", NameSpaceAndClass));

            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }

            return iODriver;
        }

        public IODriver GetAssembly(string Path, string NameSpaceAndClass)
        {
            IODriver iODriver = null;
            try
            {
                // قراءة مصفوفة  البايت الخاصة بالمشروع الثاني
                string buffer = Application.StartupPath + Path;

                // تحميل الملف
                Assembly dll = Assembly.LoadFile(buffer);

                // تعريف متغير يعبر عن اسم الكلاس في المشروع الثاني شاملا الاسم الخاص بفضاء الأسماء الموجود بها الكلاس
                string dllName = NameSpaceAndClass;
                Type t = dll.GetType(dllName);
                // أخيرا نحصل علي الواجهة كالتالي
                iODriver = (IODriver)Activator.CreateInstance(t);


            }
            catch (Exception ex)
            {
                EventscadaException?.Invoke(GetType().Name, ex.Message);
            }


            return iODriver;
        }

        #region CreateInstance
        public static T CreateInstance<T>(params object[] Params) where T : class // params keyword for array
        {
            List<Type> argTypes = new List<Type>();

            //used .GetType() method to get the appropriate type
            //Param can be null so handle accordingly
            foreach (object Param in Params)
            {
                argTypes.Add((Param ?? new object()).GetType());
            }

            ConstructorInfo[] Types = typeof(T).GetConstructors();
            foreach (ConstructorInfo node in Types)
            {
                ParameterInfo[] Args = node.GetParameters();
                if (Params.Length == Args.Length)
                {
                    bool[] cond = new bool[Params.Length];
                    //handle derived types
                    for (int i = 0; i < Params.Length; i++)
                    {
                        if (Args[i].ParameterType.IsAssignableFrom(argTypes[i]))
                        {
                            cond[i] = true;
                        }
                    }

                    if (cond[0] && cond[1])
                    {
                        return (T)node.Invoke(Params);
                    }
                }
            }
            return default(T);
        }
        public object ParseNamespace(string Path, string classname) //Looks up class in System.dll
        {
            string DotNetPath = Application.StartupPath + Path;
            Assembly Asm = Assembly.LoadFile(DotNetPath);
            Type[] Types = Asm.GetExportedTypes();
            foreach (Type Node in Types)
            {
                if (Node.Name == classname)
                {
                    return Node;
                }
            }

            return null;
        }
        public object CreateInstance(Type pContext, object[] Params)
        {
            List<Type> argTypes = new List<Type>();

            //used .GetType() method to get the appropriate type
            //Param can be null so handle accordingly
            if (Params != null)
            {
                foreach (object Param in Params)
                {
                    if (Param != null)
                    {
                        argTypes.Add(Param.GetType());
                    }
                    else
                    {
                        argTypes.Add(null);
                    }
                }
            }

            ConstructorInfo[] Types = pContext.GetConstructors();
            foreach (ConstructorInfo node in Types)
            {
                ParameterInfo[] Args = node.GetParameters();
                // Params can be null for default constructors so use argTypes
                if (argTypes.Count == Args.Length)
                {
                    bool areTypesCompatible = true;
                    for (int i = 0; i < Params.Length; i++)
                    {
                        if (argTypes[i] == null)
                        {
                            if (Args[i].ParameterType.IsValueType)
                            {
                                //fill the defaults for value type if not supplied
                                Params[i] = CreateInstance(Args[i].ParameterType, null);
                                argTypes[i] = Params[i].GetType();
                            }
                            else
                            {
                                argTypes[i] = Args[i].ParameterType;
                            }
                        }
                        if (!Args[i].ParameterType.IsAssignableFrom(argTypes[i]))
                        {
                            areTypesCompatible = false;
                            break;
                        }
                    }
                    if (areTypesCompatible)
                    {
                        return node.Invoke(Params);
                    }
                }
            }

            //delegate type to Activator.CreateInstance if unable to find a suitable constructor
            return Activator.CreateInstance(pContext);
        }
        #endregion
    }
}
