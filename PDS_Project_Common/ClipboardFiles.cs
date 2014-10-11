using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace PDS_Project_Common
{
    public class ClipboardFiles
    {

        public static void SendClipboardFiles(Socket socket)
        {
            FileAttributes attr;

            MsgStream.Send(new InitFileMsgCBP(Clipboard.GetFileDropList()), socket);

            foreach (string path in Clipboard.GetFileDropList())
            {
                 attr = File.GetAttributes(path);
                 if ((attr & FileAttributes.Directory) == FileAttributes.Directory) 
                 {
                     MsgStream.Send(new DirMsgCBP(path), socket);
                     SeekAndSend(socket, path);
                 }
                 else
                 {
                      MsgStream.Send(new FileMsgCBP(path, File.ReadAllBytes(path)), socket); 
                 }
            }

            
        }

        public static void SeekAndSend(Socket socket, string path) 
        {

            foreach (string dir in Directory.GetDirectories(path))
            {
                MsgStream.Send(new DirMsgCBP(dir), socket);
                SeekAndSend(socket, dir);
            }

            foreach (string file in Directory.EnumerateFiles(path))
            {
                MsgStream.Send(new FileMsgCBP(file, File.ReadAllBytes(file)), socket);
            }

        }

        public static void RecvClipboardFiles(Socket socket)
        {
            Message o;
            StringCollection sc; 

            string path = null;
            string tmpdir = Path.GetTempPath() + "PDS_project\\";

            
            if( Directory.Exists(tmpdir) ) Directory.Delete(tmpdir);
            Directory.CreateDirectory(tmpdir);


            o = (Message)MsgStream.Receive(socket);
            
            if (o is InitFileMsgCBP) sc = ((InitFileMsgCBP)o).sc;
            else return;    //error??


            while (!(o is StopMsgCBP))
            {
                o = (Message)MsgStream.Receive(socket);

                if (o is FileMsgCBP)
                {
                    path = ((FileMsgCBP)o).name;
                    File.WriteAllBytes(tmpdir + path, ((FileMsgCBP)o).content);
                }

                if (o is DirMsgCBP)
                {
                    path = ((DirMsgCBP)o).name;
                    Directory.CreateDirectory(tmpdir + path);
                }
            }

            //caricamento in clipboard di sc


        }

        public static void FreeTmpResources()
        {
            string tmpdir = Path.GetTempPath() + "PDS_project";
            if (Directory.Exists(tmpdir)) Directory.Delete(tmpdir);
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
