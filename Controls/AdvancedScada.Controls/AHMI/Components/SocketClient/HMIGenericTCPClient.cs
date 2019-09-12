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
using AdvancedScada;
 
 
using AdvancedScada.Controls.AHMI.Components.SocketClient;
using AdvancedScada;
using AdvancedScada.Controls;
using AdvancedScada.Controls.AHMI;
using AdvancedScada.Controls.AHMI.Components;

namespace AdvancedScada.Controls.AHMI.Components.SocketClient
{
    [System.ComponentModel.DefaultEvent("DataReceived")]
    public class HMIGenericTCPClient :AdvancedScada.Controls_Net45.GenericTcpClient
    {
        
    }

}