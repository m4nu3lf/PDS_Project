using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDS_Project_Common
{
    public class ClipboardFiles
    {
        public static void SendClipboardFiles(Socket socket)
        {
            

            
        }

        public static void RecvClipboardFiles(Socket socket)
        {

        }

        public static long GetCBFilesSize()
        {
            long size = 0;
            foreach (string path in Clipboard.GetFileDropList())
                size += PathSize(path);
            return size;
        }

        private static long PathSize(String path)
        {
            long size = 0;
            FileAttributes attr = File.GetAttributes(path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach (String subPath in Directory.EnumerateDirectories(path))
                    size += PathSize(subPath);
                foreach (String file in Directory.EnumerateFiles(path))
                    size += (new FileInfo(file)).Length;
                return size;
            }
            else
                return (new FileInfo(path)).Length;
        }

    }
}
