using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using Solicitacao.Modelos;
using System.Reflection;

namespace Solicitacao {
    public class Impressao {
        public static List<string> Impressoras(){
            List<string> list = new List<string>();
            foreach ( string impressor in PrinterSettings.InstalledPrinters) {
                list.Add(impressor);
            }
            return list;
        }
    }
}
