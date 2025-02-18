using Capa_Vista_Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Capa_Vista_Migracion;


namespace Interfac_V3
{
    public partial class FormModulos : Form
    {
        string idUsuario;
        public FormModulos(string idUsuario)
        {

            InitializeComponent();
            this.idUsuario = idUsuario;
            UsuarioSesion.SetIdUsuario(idUsuario);
        }

        private void FormModulos_Load(object sender, EventArgs e)
        {
            // Configuración inicial si es necesaria
            // Asignar los eventos MouseEnter y MouseLeave explícitamente al botón
            btnSeguridad.MouseEnter += btnSeguridad_MouseEnter;
            btnSeguridad.MouseLeave += btnSeguridad_MouseLeave;

            Btn_Migracion.MouseEnter += Btn_Nominas_MouseEnter;
            Btn_Migracion.MouseLeave += Btn_Nominas_MouseLeave;

            //btnSalir.MouseEnter += btnSalir_MouseEnter;
            //btnSalir.MouseLeave += btnSalir_MouseLeave;

        }

        // Métodos para mover la ventana sin bordes
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelBarraSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            var usuario = new Capa_Vista_Seguridad.frm_login();
            string idUsuario = usuario.Txt_usuario.ToString();

            frm_login login = new frm_login();
            login.ShowDialog();

            MDI_Seguridad formMDI = new MDI_Seguridad(idUsuario);
            formMDI.Show();
            this.Hide();
        }

        private void btnSeguridad_MouseEnter(object sender, EventArgs e)
        {
            btnSeguridad.BackColor = Color.FromArgb(30, 90, 223);  // Cambia el color de fondo al pasar el cursor
        }
        private void btnSeguridad_MouseLeave(object sender, EventArgs e)
        {
            btnSeguridad.BackColor = Color.FromArgb(240, 240, 240);  // Restaura el color original al quitar el cursor
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Nominas_Click(object sender, EventArgs e)
        {

            /*
            Aqui debe de agregarse la referencia a nominas
            */

            //frm_principal_migracion nominas = new frm_principal_migracion(UsuarioSesion.GetIdUsuario());
            //nominas.Show();


        }

        private void Btn_Nominas_MouseEnter(object sender, EventArgs e)
        {
            Btn_Migracion.BackColor = Color.FromArgb(130, 165, 248);  // Cambia el color de fondo al pasar el cursor
        }
        private void Btn_Nominas_MouseLeave(object sender, EventArgs e)
        {
            Btn_Migracion.BackColor = Color.FromArgb(240, 240, 240);  // Restaura el color original al quitar el cursor
        }

      
    }
}