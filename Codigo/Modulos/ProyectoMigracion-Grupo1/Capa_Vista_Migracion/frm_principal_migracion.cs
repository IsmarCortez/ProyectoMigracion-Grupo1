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

    public partial class frm_principal_migracion : Form
    {
        string idUsuario;
        Controlador cn = new Controlador();
        public frm_principal_migracion(String idUsuario)
        {
            this.idUsuario = idUsuario;
            InitializeComponent();


            //this.WindowState = FormWindowState.Maximized; // Maximiza el formulario al iniciar
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            /* Btn_maximizar.Visible = false;
             Btn_restaurar.Visible = true;*/
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            ocultaSubMenu();

        }

        private void ocultaSubMenu()
        {
            if (Pnl_mantenimientos.Visible == true)
                Pnl_mantenimientos.Visible = false;
            if (Pnl_generacion.Visible == true)
                Pnl_generacion.Visible = false;
            if (Pnl_genproc.Visible == true)
                Pnl_genproc.Visible = false;
        }

        private void muestraSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultaSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        #region Funcionalidades del formulario
        //Metodo para Redimensionar el tamaño del forumulario en tiempo de ejecuciòn
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //Dibujar panel y/o excluir esquina del panel
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.Pnl_contenedor.Region = region;
            this.Invalidate();
        }
        //Color y Grip de rectangulo inferior
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }




        private void Btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int lx, ly;
        int sw, sh;


        private void Btn_Mantenimientos_Click_1(object sender, EventArgs e)
        {
            muestraSubMenu(Pnl_mantenimientos);
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_GeneracionMenor_Click(object sender, EventArgs e)
        {
            muestraSubMenu(Pnl_genproc);
        }

        private void Btn_Generacion_Click(object sender, EventArgs e)
        {
            muestraSubMenu(Pnl_generacion);
        }

        private void Btn_restaurar_Click(object sender, EventArgs e)
        {
            Btn_maximizar.Visible = true;
            Btn_restaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void Btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frm_citas>(); // Pasa el idUsuario
            Btn_Citas.BackColor = Color.FromArgb(12, 61, 92);
            ocultaSubMenu();
        }

        private void Btn_Usuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Frm_usuario>(); // Pasa el idUsuario
            Btn_Usuario.BackColor = Color.FromArgb(12, 61, 92);
            ocultaSubMenu();
        }

        private void Btn_Pago_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frm_pago>(); // Pasa el idUsuario
            Btn_Pago.BackColor = Color.FromArgb(12, 61, 92);
            ocultaSubMenu();
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = Pnl_Formulario.Controls.OfType<MiForm>().FirstOrDefault(); // Busca en la colección el formulario

            // Si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;

                // Ajusta el formulario hijo al panel contenedor
                formulario.Dock = DockStyle.Fill; // Hace que el formulario se ajuste al tamaño del panel

                Pnl_Formulario.Controls.Add(formulario);
                Pnl_Formulario.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            else
            {
                formulario.BringToFront();
            }
        }
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
                Btn_Usuario.BackColor = Color.FromArgb(4, 41, 68);
        }

    }
}
#endregion