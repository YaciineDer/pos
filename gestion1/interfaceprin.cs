using System;
using System.Windows.Forms;

namespace gestion1
{
    public partial class interfaceprin : Form
    {
        gestiondesproduits produits;
        ManageOrders ordr;
        gestiondecategoriedesproduits ctrg;
        gestiondespersonnes prsn;
        Usermanager usr;

        public interfaceprin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (produits == null || produits.IsDisposed)
            {
                produits = new gestiondesproduits();
                produits.FormClosed += ChildFormClosed;
            }
            produits.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ordr == null || ordr.IsDisposed)
            {
                ordr = new ManageOrders();
                ordr.FormClosed += ChildFormClosed;
            }
            ordr.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (ctrg == null || ctrg.IsDisposed)
            {
                ctrg = new gestiondecategoriedesproduits();
                ctrg.FormClosed += ChildFormClosed;
            }
            ctrg.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (prsn == null || prsn.IsDisposed)
            {
                prsn = new gestiondespersonnes();
                prsn.FormClosed += ChildFormClosed;
            }
            prsn.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (usr == null || usr.IsDisposed)
            {
                usr = new Usermanager();
                usr.FormClosed += ChildFormClosed;
            }
            usr.Show();
            this.Hide();
        }

        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
