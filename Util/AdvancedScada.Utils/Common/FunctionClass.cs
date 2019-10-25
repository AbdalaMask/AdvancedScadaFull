/*----------------------------------------------------------------
// Copyright (C) 2010 珠海亚太电效系统有限公司
// 版权所有。 
//
// 文件名：
// 文件功能描述：
//
// 
// 创建标识：陈奕昆20100126
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Common
{
    /// <summary>
    /// 函数类
    /// </summary>
    public class FunctionClass
    {

        #region 将DataTable的列和行对调
        /// <summary>
        /// 将DataTable的列和行对调
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable RevertDataTable(DataTable dt)
        {
            int rowCount = dt.Rows.Count + 1;
            int columnsCount = dt.Columns.Count;
            DataTable newDt = new DataTable();
            DataColumn dc = new DataColumn();
            for (int rowi = 0; rowi < rowCount; rowi++)
            {
                if (rowi == 0)
                {
                    newDt.Columns.Add(dt.Columns[0].ToString(), typeof(string));
                }
                else
                {
                    newDt.Columns.Add(dt.Rows[rowi - 1][0].ToString(), typeof(int));
                }
            }
            for (int columnsi = 0; columnsi < columnsCount - 1; columnsi++)
            {
                DataRow dr = newDt.NewRow();
                for (int rowj = 0; rowj < rowCount; rowj++)
                {
                    if (rowj == 0)
                    {
                        dr[rowj] = dt.Columns[columnsi + 1].ToString();
                    }
                    else
                    {
                        dr[rowj] = dt.Rows[rowj - 1][columnsi + 1];
                    }
                }
                newDt.Rows.Add(dr);
            }
            return newDt;
        }
        #endregion

        #region char数组转换string
        /// <summary>
        /// char数组转换string
        /// </summary>
        /// <param name="charArr"></param>
        /// <returns></returns>
        public static string charToString(char[] charArr)
        {
            string result = "";
            for (int i = 0; i < charArr.Length; i++)
            {
                result = charArr[i].ToString() + result;
            }
            return result;
        }
        #endregion

        #region 判断时间格式是否正确
        /// <summary>
        /// 判断时间格式是否正确
        /// </summary>
        /// <param name="date">08:34</param>
        /// <param name="msg">返回信息</param>
        /// <returns>true,false</returns>
        public static bool chkDate(string date, ref string msg)
        {
            string[] sDate = date.Split(':');
            int h = Convert.ToInt32(sDate[0]);
            if (h < 0 || h > 24)
            {
                msg = "小时设置不正确";
                return false;
            }
            int m = Convert.ToInt32(sDate[1]);
            if (m < 0 || m > 60)
            {
                msg = "分钟设置不正确";
                return false;
            }
            return true;
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string md5String(string s)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// MD5加密算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getMD5(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion

        #region 运行DOS命令
        /// <summary>
        /// 运行DOS命令
        /// DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string RunCmd(string command)
        {
            //實例一個Process類，啟動一個獨立進程
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：

            p.StartInfo.FileName = "cmd.exe";           //設定程序名
            p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數
            p.StartInfo.UseShellExecute = false;        //關閉Shell的使用
            p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入
            p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出
            p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出
            p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口

            p.Start();   //啟動

            //p.StandardInput.WriteLine(command);       //也可以用這種方式輸入要執行的命令
            //p.StandardInput.WriteLine("exit");        //不過要記得加上Exit要不然下一行程式執行的時候會當機

            return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果

        }
        #endregion

        #region 判断电脑是否能上网
        /// <summary>
        /// 判断电脑是否能上网
        /// </summary>
        /// <returns></returns>
        public static bool isConnectInternet()
        {
            try
            {
                Ping p = new Ping();//创建Ping对象p
                PingReply pr = p.Send("www.baidu.com");//向指定IP或者主机名的计算机发送ICMP协议的ping数据包

                if (pr.Status == IPStatus.Success)//如果ping成功
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 记录异常信息
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="log"></param>
        public static void saveExceptionLog(string log)
        {
            try
            {
                log = "【" + DateTime.Now.ToLongTimeString() + "】" + log;

                String rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                rootPath = rootPath.Replace(@"file:\", "");

                string logLocation = rootPath + "/Log/异常信息-" + DateTime.Now.ToLongDateString() + ".txt";

                if (Directory.Exists(rootPath + "/Log") == false)
                {
                    Directory.CreateDirectory(rootPath + "/Log");
                }

                //删除一个月前的历史记录
                if (File.Exists(rootPath + "/Log/异常信息-" + DateTime.Now.AddMonths(-1).ToLongDateString() + ".txt") == true)
                {
                    File.Delete(rootPath + "/Log/异常信息-" + DateTime.Now.AddMonths(-1).ToLongDateString() + ".txt");
                }

                //创建一个日志文件,权限为读写
                FileStream Mfile = new FileStream(logLocation, FileMode.Append, FileAccess.Write);
                //创建一个数据流写入器,和打开文件关联
                StreamWriter Mwriter = new StreamWriter(Mfile);
                Mwriter.WriteLine(log);
                Mwriter.Flush();
                Mwriter.Close();
                Mfile.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region 改变该控件在enable等于false下的字体颜色
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x8000000;

        public static void SetControlEnabled(Control c, bool enabled)
        {
            if (enabled)
            { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE)); }
        } 
        #endregion

        #region 返回周的起始日期和结束日期
        /// <summary>
        /// 返回周的起始日期和结束日期
        /// </summary>
        /// <param name="year">指定月份所属的年</param>
        /// <param name="month">指定月份</param>
        /// <param name="week">指定第几周</param>
        public static DateTime[] WeekBeDateAndEndDate(int year, int month, int week)
        {
            int[] _mDay = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            //判断指定月份所属年的2月是闰年还是平年
            //能被4整除、但不能被100整除，或者能被400整除 
            if (year % 4 == 0 && year % 100 != 0)
            {
                _mDay[1] = 29;
            }
            else if (year % 400 == 0)
            {
                _mDay[1] = 29;
            }
            //存储指定月份一共有几周
            int sum;
            DateTime[,] weekArray;//存储指定月份各周的起止时间
            DateTime dt = new DateTime(year, month, 1);//指定月份的1号
            //如果1号不是星期天，则当前日期到本月第一个星期天为第一周
            if (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                int _week = (int)dt.DayOfWeek;//当前日期的在一周的序号星期一为1，星期二为2，星期天为0
                int _day = 6 - _week;//本周还有几天
                int lastDay = _mDay[month - 1] - (_day + 1);//本月除开本周还有多少天
                //判断本月除开本周还有多少周
                if (lastDay / 7 == 0)
                {
                    sum = (lastDay / 7) + 1;
                }
                else
                {
                    sum = (lastDay / 7) + 2;
                }
                weekArray = new DateTime[sum, 2];//确定本月有多少周之后初始化存储本月各周的起止时间的数组
                weekArray[0, 0] = dt;//第一周的开始时间
                weekArray[0, 1] = dt.AddDays(_day);//第一周的结束时间
                //为余下各周的其止时间赋值
                for (int i = 1; i < sum; i++)
                {
                    weekArray[i, 0] = weekArray[i - 1, 1].AddDays(1);//上一周的最后一天加1天
                    weekArray[i, 1] = weekArray[i - 1, 1].AddDays(7);//上一周的最后一天加7天
                }
            }
            //如果1号是星期天则1号为第一周
            else
            {
                int lastDay = _mDay[month - 1];//本月共多少天
                //判断本月除开本周还有多少周
                if (lastDay / 7 == 0)
                {
                    sum = (lastDay / 7);
                }
                else
                {
                    sum = (lastDay / 7) + 1;
                }
                weekArray = new DateTime[sum, 2];//确定本月有多少周之后初始化存储本月各周的起止时间的数组
                weekArray[0, 0] = dt;//第一周的开始时间
                weekArray[0, 1] = dt.AddDays(6);//第一周的结束时间
                //为本月各周的起止时间赋值
                for (int i = 1; i < sum; i++)
                {
                    weekArray[i, 0] = weekArray[i - 1, 1].AddDays(1);//上一周的最后一天加1天
                    weekArray[i, 1] = weekArray[i - 1, 1].AddDays(7);//上一周的最后一天加7天
                }
            }
            //根据指定周来读取起止时间保存到结构
            DateTime[] dateArr = new DateTime[2];
            dateArr[0] = weekArray[week - 1, 0];
            dateArr[1] = weekArray[week - 1, 1];
            return dateArr;
        } 
        #endregion

        #region 某日期是当月的第几周
        /// <summary>
        /// 某日期是当月的第几周
        /// </summary>
        /// <param name="day">要判断的日期</param>
        /// <param name="WeekStart">1周一为一周的开始，2周日为一周的开始</param>
        /// <returns></returns>
        public static int WeekOfMonth(DateTime day, int WeekStart)
        {
            //WeekStart                                                                      
            //1表示 周一至周日 为一周                                                        
            //2表示 周日至周六 为一周                                                        
            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);
            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }
            if (WeekStart == 1)
            {
                return (day.Date.Day + i - 2) / 7 + 1;
            }
            if (WeekStart == 2)
            {
                return (day.Date.Day + i - 1) / 7;
            }
            return 0;
            //错误返回值0                                                                    
        }  
        #endregion    

        #region C#调用IE打开网址
        public static void OpenIE(string sUrl)
        {
            object url = sUrl;

            try
            {
                // 调用默认浏览器打开网址.
                ShellExecute(IntPtr.Zero, "open", sUrl, "", "", ShowCommands.SW_SHOWNOACTIVATE);
            }
            catch {}
        }


        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);

        #endregion

        #region 获取系统运行路径
        /// <summary>
        /// 获取系统运行路径
        /// </summary>
        /// <returns></returns>
        public static string getRunPath()
        {
            String rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            rootPath = rootPath.Replace(@"file:\", "");
            return rootPath;
        } 
        #endregion

        #region 动态编译
        //int i = (int)Calc("1+2*3");
        //Console.WriteLine(i.ToString());
        public static object Calc(string expression)
        {
            string className = "Calc";
            string methodName = "Run";
            expression = expression.Replace("/", "*1.0/");

            //  创建编译器实例。  
            System.CodeDom.Compiler.CodeDomProvider _CodeDomProvider = System.CodeDom.Compiler
                .CodeDomProvider.CreateProvider("CSharp"); 
            //  设置编译参数。  
            System.CodeDom.Compiler.CompilerParameters paras = new System.CodeDom.Compiler.CompilerParameters();
            paras.GenerateExecutable = false;   //编译成exe还是dll
            paras.GenerateInMemory = false;   //是否写入内存,不写入内存就写入磁盘
            paras.OutputAssembly = "c:\\test.dll";  //输出路径
            paras.ReferencedAssemblies.Add("System.dll");
            //  创建动态代码。  
            StringBuilder classSource = new StringBuilder();
            classSource.Append("public  class  " + className + "\n");
            classSource.Append("{\n");
            classSource.Append("        public  object  " + methodName + "()\n");
            classSource.Append("        {\n");
            classSource.Append("                return  " + expression + ";\n");
            classSource.Append("        }\n");
            classSource.Append("}");

            //   System.Diagnostics.Debug.WriteLine(classSource.ToString());  

            //  编译代码。  
            System.CodeDom.Compiler.CompilerResults result = _CodeDomProvider
                .CompileAssemblyFromSource(paras, classSource.ToString());

            //  获取编译后的程序集。  
            System.Reflection.Assembly assembly = result.CompiledAssembly;

            //  动态调用方法。  
            object eval = assembly.CreateInstance(className);
            System.Reflection.MethodInfo method = eval.GetType().GetMethod(methodName);
            object reobj = method.Invoke(eval, null);
            GC.Collect();
            return reobj;
        }

        public static bool makeCompiler(string sourcePath, string name, string[] referenceAssemblies)
        {
            //  创建编译器实例。  
            System.CodeDom.Compiler.CodeDomProvider _CodeDomProvider = System.CodeDom.Compiler
                .CodeDomProvider.CreateProvider("CSharp");
            //  设置编译参数。  
            System.CodeDom.Compiler.CompilerParameters paras = new System.CodeDom.Compiler.CompilerParameters();
            paras.GenerateExecutable = false;   //编译成exe还是dll
            paras.GenerateInMemory = false;   //是否写入内存,不写入内存就写入磁盘
            paras.OutputAssembly = Directory.GetCurrentDirectory() + "/Compiler/" + name + ".dll";  //输出路径
            foreach (string referenceAssemblie in referenceAssemblies)
            {
                paras.ReferencedAssemblies.Add(referenceAssemblie);
            }

            //  编译代码。  
            System.CodeDom.Compiler.CompilerResults result = _CodeDomProvider
                .CompileAssemblyFromSource(paras, File.ReadAllText(sourcePath, Encoding.Default));

            //  获取编译后的程序集。  
            System.Reflection.Assembly assembly = result.CompiledAssembly;
            return true;
        } 
        #endregion

        #region 点坐标相加

        /// <summary>
        /// 点坐标相加
        /// </summary>
        /// <param name="point"></param>
        /// <param name="addPoint"></param>
        public static void PointAdd(ref Point point, Point addPoint)
        {
            point.X += addPoint.X;
            point.Y += addPoint.Y;
        }

        public static void RectangleAdd(ref Rectangle rectangle, Point addPoint)
        {
            rectangle.X += addPoint.X;
            rectangle.Y += addPoint.Y;
        }
        #endregion

        #region 创建文件夹
        public static string creatDir(string path)
        {
            string[] paths = path.Split('/');
            string sPath = Common.FunctionClass.getRunPath();
            foreach (string cPath in paths)
            {
                sPath += '/' + cPath;
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
            }
            return sPath;
        } 
        #endregion

    }


        

}
