﻿using MyContacts_org.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts_org
{
    public partial class Form1 : Form
    {
        IContacts repository;
        
        public Form1()
        {
            repository = new Contacts();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            dgContacts.AutoGenerateColumns = false;
            dgContacts.Columns[0].Visible=false;
            dgContacts.DataSource = repository.SelectAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            AddEdit formm = new AddEdit();
            formm.ShowDialog();
            if (formm.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                AddEdit formm = new AddEdit();
                formm.contactId = contactId;
                if (formm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            
            
            
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
            string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
            string fullname = name + family;
            if (dgContacts.CurrentRow!=null)
            {
                if(MessageBox.Show($"  آیا از حذف {fullname} اطمینان دارید؟ ", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    repository.delete(contactId);
                    BindGrid();
                }
               

            }
            else
            {
                MessageBox.Show("حداقل یک مورد را انتخاب کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgContacts.DataSource = repository.search(txtSearch.Text);

        }
    }
}
