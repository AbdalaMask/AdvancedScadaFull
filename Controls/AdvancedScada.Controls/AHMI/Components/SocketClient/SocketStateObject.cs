using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

using System.Xml.Linq;


namespace AdvancedScada.Controls
{
    namespace Common
    {
        public class SocketStateObject
        {

            #region Properties
            private Socket m_workSocket;
            public Socket WorkSocket
            {
                get
                {
                    return m_workSocket;
                }
                set
                {
                    m_workSocket = value;
                }
            }

            //*********************************
            //* The received data byte stream
            //*********************************
            internal byte[] data = new byte[4096];

            //**********************************
            //* Current Index within data array
            //**********************************
            private int m_CurrentIndex;
            public int CurrentIndex
            {
                get
                {
                    return m_CurrentIndex;
                }
                set
                {
                    if (value >= data.Length)
                    {
                        throw new ArgumentException("TCP State object can only hold up to 4096 bytes");
                        return;
                    }
                    m_CurrentIndex = value;
                }
            }
            #endregion

            #region Constructors
            public SocketStateObject()
            {
            }

            public SocketStateObject(Socket workSocket)
            {
                m_workSocket = workSocket;
            }
            #endregion

        }
    }


}