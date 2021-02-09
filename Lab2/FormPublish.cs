using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab2
{
    public partial class FormPublish : Form
    {
        private FormMain parent;
        public FormPublish(FormMain p)
        {
            parent = p;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            Publisher.publisher.PublishMessage(textBox1.Text);
            foreach (var email in SendViaEmail.subscribedEmails)
            {
                textBox2.Text += email.EmailAddress;
                textBox2.Text += "\r\n";
            }
            foreach(var phone in SendViaMobile.subscribedMobiles)
            {
                string number=Regex.Replace(phone.Number, @"^(.{3})(.{3})(.{4})$", "$1-$2-$3");
                textBox2.Text += number;
                textBox2.Text += "\r\n";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
        }
    }
}
