using S7.Net;
using System;
namespace AdvancedScada.IODriverV2.XSiemens

{
    public  class PLCAddressStrings
    {
        private DataType dataType;
        private int dbNumber;
        private int startByte;
        private int bitNumber;
        private VarType varType;

        public DataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
            }

        public int DbNumber
        {
            get
            { return dbNumber; }
            set { dbNumber = value; }
            }

        public int StartByte
        {
            get
            { return startByte; }
            set { startByte = value; }
            }

        public int BitNumber
        {
            get
            {
                return bitNumber;
            }
                set { bitNumber = value;
            }
            }

        public VarType VarType
        {
            get
            { return varType; }
            set { varType = value; }
            }

        public PLCAddressStrings(string address)
        {
            Parse(address, out dataType, out dbNumber, out varType, out startByte, out bitNumber);
        }

        public static void Parse(string input, out DataType dataType, out int dbNumber, out VarType varType, out int address, out int bitNumber)
        {
            bitNumber = -1;
            dbNumber = 0;


            string[] strings = input.Split(new char[] { '.' });

            dataType = DataType.DataBlock;
            dbNumber = int.Parse(strings[0].Substring(2));
            address = int.Parse(strings[1].Substring(3));
            varType = VarType.String;


        }
    }
}
