using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GVC
{
    internal class CustomTextBox : TextBox // Herda de TextBox para ter acesso aos eventos Enter e Leave
    {
        public CustomTextBox()
        {
            this.Enter += new EventHandler(CustomTextBox_Enter);
            this.Leave += new EventHandler(CustomTextBox_Leave);
        }

        private void CustomTextBox_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue; // Azul claro quando entra no foco
        }

        private void CustomTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White; // Branco quando perde o foco
        }
    }
}
