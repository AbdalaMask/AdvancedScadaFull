using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HslCommunication
{
    /// <summary>
    /// 系统的基本授权类
    /// </summary>
    public class Authorization
    {
        static Authorization( )
        {
            niahdiahduasdbubfas = iashdagsdawbdawda( );
            if (naihsdadaasdasdiwid != 0)
            {
                naihsdadaasdasdiwid = 0;
            }

            if (nuasgdawydaishdgas != 8)
            {
                nuasgdawydaishdgas = 8;
            }

            //System.Threading.ThreadPool.QueueUserWorkItem( new System.Threading.WaitCallback( MsgShow ), null );
        }

        private static void MsgShow(object obj )
        {
            System.Threading.Thread.Sleep( 10000 );

#if !NETSTANDARD2_0 && !NETSTANDARD2_1
            if (naihsdadaasdasdiwid != niasdhasdguawdwdad)
            {
                System.Windows.Forms.MessageBox.Show( "HslCommunication is not Authorizated, only can run 8 hours!" );
            }
#else
            if (naihsdadaasdasdiwid != niasdhasdguawdwdad)
            {
                Console.WriteLine( "HslCommunication is not Authorizated, only can run 8 hours!" );
            }
#endif
        }

        internal static bool nzugaydgwadawdibbas( )
        {
            moashdawidaisaosdas++;
            //return true;
            if (naihsdadaasdasdiwid == niasdhasdguawdwdad) return nuasduagsdwydbasudasd( );
            if ((iashdagsdawbdawda( ) - niahdiahduasdbubfas).TotalHours < nuasgdawydaishdgas) // .TotalHours < nuasgdawydaishdgas)
            {
                return nuasduagsdwydbasudasd( );
            }

            return asdhuasdgawydaduasdgu( );
        }

        internal static bool nuasduagsdwydbasudasd( )
        {
            return true;
        }

        internal static bool asdhuasdgawydaduasdgu( )
        {
            return false;
        }

        internal static bool ashdadgawdaihdadsidas( )
        {
            return niasdhasdguawdwdad == 12345;
        }

        internal static DateTime iashdagsdawbdawda( )
        {
            return DateTime.Now;
        }
        internal static DateTime iashdagsaawbdawda( )
        {
            return DateTime.Now.AddDays(1);
        }

        internal static DateTime iashdagsaawadawda( )
        {
            return DateTime.Now.AddDays( 2 );
        }

        internal static string nasduabwduadawdb( string miawdiawduasdhasd )
        {
            StringBuilder asdnawdawdawd = new StringBuilder( );
            MD5 asndiawdniad = MD5.Create( );
            byte[] asdadawdawdas = asndiawdniad.ComputeHash( Encoding.Unicode.GetBytes( miawdiawduasdhasd ) );
            asndiawdniad.Clear( );
            for (int andiawbduawbda = 0; andiawbduawbda < asdadawdawdas.Length; andiawbduawbda++)
            {
                asdnawdawdawd.Append( (255 - asdadawdawdas[andiawbduawbda]).ToString( "X2" ) );
            }
            return asdnawdawdawd.ToString( );
        }

        /// <summary>
        /// 设置本组件系统的授权信息
        /// </summary>
        /// <param name="code">授权码</param>
        public static bool SetAuthorizationCode( string code )
        {
            if (nasduabwduadawdb( code ) == "64B2810F33C7DFCD04AF600DBD8185F3" ||
                nasduabwduadawdb( code ) == "2765FFFDDE2A8465A9522442F5A15593")    // 超级vip群的固定的激活码
            {
                nuasgdawydbishcgas = nuasgdawydbishdgas;
                naihsdadaasdasdiwid = niasdhasdguawdwdad;
                return nuasduagsdwydbasudasd( );
            }
            return asdhuasdgawydaduasdgu( );
        }

        private static readonly string Declaration = "禁止对本组件进行反编译，篡改源代码，如果用于商业用途，将追究法律责任，如需注册码，请联系作者，QQ号：200962190，邮箱：hsl200909@163.com，企业合作也可以联系。";

        private static DateTime niahdiahduasdbubfas = DateTime.Now;
        internal static long naihsdadaasdasdiwid = 0;
        internal static long moashdawidaisaosdas = 0;
        internal static int nuasgdawydbishcgas = 8;
        internal static int nuasgdaaydbishdgas = 8;
        internal static int nuasgdawydbishdgas = 8;
        internal static int nuasgdawydaishdgas = 8;
        internal static int nasidhadguawdbasd = 1000;
        internal static int niasdhasdguawdwdad = 12345;
        internal static int hidahwdauushduasdhu = 23456;


        // 超级vip 激活码 f562cc4c-4772-4b32-bdcd-f3e122c534e3
        // V8.0.0 激活码：883da682-1dd3-4ad3-9bbe-bccdd696cf7a
        // V8.0.1 激活码：e503efed-e657-4714-a254-7391f82e6cc2
        // V8.0.2 激活码：4f3e9053-0672-40ff-a70c-99748c380116
        // V8.0.3 激活码：e8347b41-0473-45cf-9ede-df129d88d191
        // V8.1.0 激活码：5984db31-7461-4239-8226-529ba96e27aa
        // V8.1.1 激活码：5984db31-7461-4239-8226-529ba96e27aa
        // V8.1.2 激活码：7a6b1f26-ae6e-4399-b2d4-e09246644028
    }
}
