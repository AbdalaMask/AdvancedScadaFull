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
            if (nasduabwduadawdb( code ) == "8FE65660C75D26FF5CF749B571F11764")
            {
                nuasgdawydbishcgas = nuasgdawydbishdgas;
                naihsdadaasdasdiwid = niasdhasdguawdwdad;
                return nuasduagsdwydbasudasd( );
            }
            return asdhuasdgawydaduasdgu( );
        }

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



        // V8.0.0 激活码：883da682-1dd3-4ad3-9bbe-bccdd696cf7a
    }
}
