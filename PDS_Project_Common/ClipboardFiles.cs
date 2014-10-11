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

        static int MaxSize = 1024 * 1024 * 1024;

        public static void SendClipboardFiles(Socket socket)
        {
            FileAttributes attr;
            FileInfo fi;
            DirectoryInfo di;

            string name;

            foreach (string path in Clipboard.GetFileDropList())
            {
                 attr = File.GetAttributes(path);
                 if ((attr & FileAttributes.Directory) == FileAttributes.Directory) 
                 {
                     di = new DirectoryInfo(path);
                     name = path.Substring(di.Parent.Name.Length + 1);
                     Console.WriteLine("Try to sending: " + name);
                     //MsgStream.Send(new DirMsgCBP(name, true), socket);
                     SeekAndSend(socket, path);
                 }
                 else
                 {
                     fi = new FileInfo(path);
                     name = path.Substring(fi.DirectoryName.Length + 1);
                     Console.WriteLine("Try to sending: " + name);
                     //MsgStream.Send(new FileMsgCBP(name, File.ReadAllBytes(path), true), socket); 
                 }
            }

            //MsgStream.Send(new StopFileCBP(), socket);
            
        }

        public static void SeekAndSend(Socket socket, string path) 
        {
            FileInfo fi;
            DirectoryInfo di;
            string name;

            foreach (string dir in Directory.GetDirectories(path))
            {
                di = new DirectoryInfo(path);
                name = path.Substring(di.Parent.Name.Length + 1);
                Console.WriteLine("Try to sending Dir: " + name);
                //MsgStream.Send(new DirMsgCBP(dir, false), socket);
                SeekAndSend(socket, dir);
            }

            foreach (string file in Directory.EnumerateFiles(path))
            {
                fi = new FileInfo(path);
                name = path.Substring(fi.DirectoryName.Length + 1);
                Console.WriteLine("Try to sending File: " + name);
                //MsgStream.Send(new FileMsgCBP(file, File.ReadAllBytes(file), false), socket);
            }

        }



        public static void RecvClipboardFiles(Socket socket)
        {
            Message o;
            StringCollection sc = new StringCollection(); 

            string path = null;
            string tmpdir = Path.GetTempPath() + "PDS_project";

            Console.WriteLine("Files stored in: "  + tmpdir);
            
            if( Directory.Exists(tmpdir) ) Directory.Delete(tmpdir);
            Directory.CreateDirectory(tmpdir);


            o = (Message)MsgStream.Receive(socket);
            
            while (!(o is StopFileCBP))
            {
                o = (Message)MsgStream.Receive(socket);

                if (o is FileMsgCBP)
                {
                    path = tmpdir + ((FileMsgCBP)o).name;
                    File.WriteAllBytes(path, ((FileMsgCBP)o).content);
                    if (((FileMsgCBP)o).root) sc.Add(path);
                    Console.WriteLine("Craeted new File: " + path);
                }

                if (o is DirMsgCBP)
                {
                    path = tmpdir + ((DirMsgCBP)o).name;
                    Directory.CreateDirectory(path);
                    if (((DirMsgCBP)o).root) sc.Add(path);
                    Console.WriteLine("Craeted new Dir: " + path);
                }
            }

            Clipboard.SetFileDropList(sc);


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
