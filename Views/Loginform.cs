﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_empresa.Views
{
    public partial class Loginform : Form
    {
        public Loginform()
        {
            InitializeComponent();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginFuncionario login = new LoginFuncionario();
            login.Show();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginFilial login = new LoginFilial();
            login.Show();
            


        }
    }
}
