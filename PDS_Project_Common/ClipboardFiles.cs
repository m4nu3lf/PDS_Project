using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace PDS_Project_Common
{


    public class CBLoaderThread
    {
        public void run(object param)
        {
            StringCollection _sc= (StringCollection)param;
            Clipboard.SetFileDropList(_sc);
            //Console.WriteLine("Update CB Done!");
            return;
        }


    }




    public class ClipboardFiles
    {

        public const int MaxSize = 1024 * 1024 * 1024;

        static Thread CBloader;



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
                     name = path.Substring(di.FullName.Length - di.Name.Length);
                     //Console.WriteLine("Try to sending Dir: " + name);
                     MsgStream.Send(new DirMsgCBP(name, true), socket);
                     SeekAndSend(socket, path, di.FullName.Length - di.Name.Length);
                 }
                 else
                 {
                     fi = new FileInfo(path);
                     name = path.Substring(fi.DirectoryName.Length + 1);
                     //Console.WriteLine("Try to sending File: " + name);
                     MsgStream.Send(new FileMsgCBP(name, File.ReadAllBytes(path), true), socket); 
                 }
            }

            
        }

        private static void SeekAndSend(Socket socket, string path, int toCut) 
        {
            string name;

            foreach (string dir in Directory.EnumerateDirectories(path))
            {
                name = dir.Substring(toCut);
                //Console.WriteLine("Try to sending Dir: " + name);
                MsgStream.Send(new DirMsgCBP(name, false), socket);
                SeekAndSend(socket, dir, toCut);
            }

            foreach (string file in Directory.EnumerateFiles(path))
            {
                name = file.Substring(toCut);
                //Console.WriteLine("Try to sending File: " + name);
                MsgStream.Send(new FileMsgCBP(name, File.ReadAllBytes(file), false), socket);
            }

        }



        public static void RecvClipboardFiles(Socket socket)
        {
            Message o;
            StringCollection sc = new StringCollection(); 

            string path = null;
            string tmpdir = Path.GetTempPath() + "PDS_project\\";

            //Console.WriteLine("Files stored in: "  + tmpdir);
            
            if( Directory.Exists(tmpdir) )
                Directory.Delete(tmpdir, true);
            Directory.CreateDirectory(tmpdir);
            
            do
            {
                o = (Message)MsgStream.Receive(socket);

                if (o is FileMsgCBP)
                {
                    path = tmpdir + ((FileMsgCBP)o).name;
                    File.WriteAllBytes(path, ((FileMsgCBP)o).content);
                    if (((FileMsgCBP)o).root) sc.Add(path);
                    //Console.WriteLine("Craeted new File: " + path);
                }

                if (o is DirMsgCBP)
                {
                    path = tmpdir + ((DirMsgCBP)o).name;
                    Directory.CreateDirectory(path);
                    if (((DirMsgCBP)o).root) sc.Add(path);
                    //Console.WriteLine("Craeted new Dir: " + path);
                }
            }
            while (!(o is StopFileCBP));
            

            CBloader = new Thread((new CBLoaderThread()).run);
            CBloader.SetApartmentState(ApartmentState.STA);
            CBloader.Start(sc);

            CBloader.Join();

            MsgStream.Send(new ConfirmCBP(), socket);

        }

        public static void FreeTmpResources()
        {
            string tmpdir = Path.GetTempPath() + "PDS_project";
            if (Directory.Exists(tmpdir))
                Directory.Delete(tmpdir, true);
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
