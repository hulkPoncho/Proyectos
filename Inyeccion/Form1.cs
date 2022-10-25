using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inyeccion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = textUsr.Text;
            var pwd = textPwd.Text;
            var validacion = new LoginValidateEntity();
            var resultado=validacion.ValidacionCredencialesCount(userName, pwd);
            if (resultado)
            {
                MessageBox.Show("Estas dentro", "Login exitoso",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Favor de validar", "Login inválido",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
            }
        }
    }
}
