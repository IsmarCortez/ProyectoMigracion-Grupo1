using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;
using Capa_Controlador_Migracion;

namespace Capa_Vista_Migracion
{
    public partial class Form1 : Form
    {

        Controlador cn = new Controlador();
        public Form1()
        {
            InitializeComponent();
        }
    }
}
