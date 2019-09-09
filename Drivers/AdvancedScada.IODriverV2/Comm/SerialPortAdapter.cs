using System;
using System.IO.Ports;
using System.Linq;

namespace AdvancedScada.IODriverV2.Comm
{
    public class SerialPortAdapter : IDisposable
    {
        private const int READ_BUFFER_SIZE = 1024; // .

        private string bufferMsgReceiver;

        private byte[] bufferReceiver;

        private SerialPort serialPort;

        public SerialPortAdapter(SerialPort serialPort)
        {
            bufferReceiver = new byte[READ_BUFFER_SIZE];
            this.serialPort = serialPort;
        }

        public bool Connect()
        {
            try
            {
                if (serialPort.IsOpen) serialPort.Close();
                serialPort.Open();


                return true;
            }
            catch (System.Exception)
            {


                return false;
            }

        }

        public void Close()
        {
            try
            {
                if (serialPort.IsOpen) serialPort.Close();

            }
            catch (System.Exception)
            {


            }

        }

        public byte[] Read()
        {
            if (serialPort.BytesToRead >= 5)
            {
                bufferReceiver = new byte[serialPort.BytesToRead];
                var result = serialPort.Read(bufferReceiver, 0, serialPort.BytesToRead);
                serialPort.DiscardInBuffer();
            }

            return bufferReceiver;
        }

        public string ReadExisting()
        {
            if (serialPort.BytesToRead >= 10)
            {
                bufferMsgReceiver = serialPort.ReadExisting();
                serialPort.DiscardInBuffer();
            }

            return bufferMsgReceiver;
        }

        public string ReadLine()
        {
            if (serialPort.BytesToRead >= 11)
            {
                bufferMsgReceiver = serialPort.ReadLine();
                serialPort.DiscardInBuffer();
            }

            return bufferMsgReceiver;
        }

        public void Write(byte[] data, int offset, int size)
        {
            serialPort.Write(data, offset, size);
            serialPort.DiscardOutBuffer();
        }

        public void WriteLine(string data)
        {
            serialPort.WriteLine(data);
            serialPort.DiscardOutBuffer();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {

                serialPort.Dispose();
            }
        }
        ~SerialPortAdapter()
        {
            Dispose(false);
        }
    }
}
