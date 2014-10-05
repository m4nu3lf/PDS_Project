using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Sockets;

namespace PDS_Project_Common
{
    public class MsgStream
    {

        public static void Send(object o, Socket s)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, o);
            byte[] ba = ms.ToArray();
            Int32 len = ba.Length;
            s.Send(BitConverter.GetBytes(len));
            s.Send(ba);
        }

        public static object Receive(Socket s)
        {
            byte[] res = ReceiveN(s, 4);
            if (res == null)
                return null;
            int len = BitConverter.ToInt32(res, 0);
            BinaryFormatter bf = new BinaryFormatter();
            res = ReceiveN(s, len);
            if (res == null)
                return null;
            Stream stream = new MemoryStream(res);
            return bf.Deserialize(stream);
        }

        private static byte[] ReceiveN(Socket s, int len)
        {
            byte[] buffer = new byte[len];
            int readBytes = 0;
            while (readBytes < len)
            {
                int res = s.Receive(buffer, readBytes, len - readBytes, SocketFlags.None);
                if (res == 0)
                    return null;
                readBytes += res;
            }
            return buffer;
        }
    }

}
