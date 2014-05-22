using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_Project_Client
{
    class ServerPanel : System.Windows.Forms.Panel
    {
        private Host _host;

        public ServerPanel(Host h)
        {
            _host = h; 
            
            this.Location = new System.Drawing.Point(12, 12);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(200, 421);
            this.TabIndex = 2;
        }


    }
}
