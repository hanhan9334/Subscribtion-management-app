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
    public partial class FormSubscript : Form
    {

        private FormMain parent;

        public FormSubscript(FormMain p)
        {
            Publisher.publisher = new Publisher();
            parent = p;
            InitializeComponent();
        }

        //Validate email format
        public static Boolean checkEmailFormat(string email)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        //Validate phone number format
        public static Boolean checkPhoneNumberFormat(String number)
        {
            bool isPhoneNum = Regex.IsMatch(number, @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            return isPhoneNum;
        }

        //Navigate back to main
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
            if (Publisher.publisher.publishmsg != null)
            {
                parent.button2.Enabled = true;
            }
        }

        //Subscribe
        private void subscribeBtn_Click(object sender, EventArgs e)
        {
            //Validate email format and check if the checkbox is checked
            if (checkBox1.Checked&& checkEmailFormat(textBox1.Text))
            {
                //Check if email has been subscribed
                bool hasEmail = false;
                foreach (var mail in SendViaEmail.subscribedEmails)
                {
                    if (mail.EmailAddress == textBox1.Text)
                    {
                        hasEmail = true;
                    }
                }
                if (!hasEmail)
                {
                    //Create a SendViaEmail object and subscibe it 
                    label1.Text = "Successfully Subscribed";
                    SendViaEmail sendViaEmail = new SendViaEmail(textBox1.Text);
                    sendViaEmail.Subscribe(Publisher.publisher);
                }
                else
                {
                    label1.Text = "Email is already subscribed";
                }
            }

            //Validate phone number format and check if the checkbox is checked
            if (checkBox2.Checked && checkPhoneNumberFormat(textBox2.Text))
            {
                //Normalize number format
                String newNumber = textBox2.Text.Replace("-",string.Empty).Replace("(", string.Empty).Replace(")",string.Empty);

                //Check if number has been subscribed
                bool hasNumber = false;
                foreach (var mobile in SendViaMobile.subscribedMobiles)
                {
                    if (mobile.Number == newNumber)
                    {
                        hasNumber = true;
                    }
                }
                if (!hasNumber)
                {
                    //Create a SendViaText object and subscibe it
                    label2.Text = "Successfully Subscribed";
                    SendViaMobile sendViaMobile = new SendViaMobile(newNumber);
                    sendViaMobile.Subscribe(Publisher.publisher);
                }
                else
                {
                    label2.Text = "Phone number is already subscribed.";
                }
            }

        }
        //Unsubscribe
        private void unsubBtn_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked && checkEmailFormat(textBox1.Text))
            {
                //Search for the SendViaEmail object
                SendViaEmail sendViaEmail =null;
                foreach(var email in SendViaEmail.subscribedEmails)
                {
                    if (textBox1.Text == email.EmailAddress)
                    {
                        //Unsubscribe the SendViaEmail object and remove it from the subscribedEmail list
                        label1.Text = "Successfully Unsubscribed";
                        sendViaEmail = email;
                        sendViaEmail.UnSubscribe(Publisher.publisher);
                        SendViaEmail.subscribedEmails.Remove(email);
                        break;
                    }
                    else
                    {
                        label1.Text = "Email is not subscribed";
                    }
                }                
            }
            if (checkBox2.Checked && checkPhoneNumberFormat(textBox2.Text))
            {
                //Normalize number
                String oldNumber = textBox2.Text.Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

                //Search for the SendViaText object
                SendViaMobile sendViaMobile = null;
                foreach (var mobile in SendViaMobile.subscribedMobiles)
                {
                    if (oldNumber == mobile.Number)
                    {
                        //Unsubscribe the SendViaText object and remove it from the subscribedText list
                        label2.Text = "Successfully Unsubscribed";
                        sendViaMobile = mobile;
                        sendViaMobile.UnSubscribe(Publisher.publisher);
                        SendViaMobile.subscribedMobiles.Remove(mobile);
                        break;
                    }
                    else
                    {
                        label2.Text = "Number is not subscribed";
                    }
                }
            }
        }

        //Check email input format and show if it's valid
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked&&textBox1.Text!="")
            {
                if (!checkEmailFormat(textBox1.Text))
                {
                    label1.Text = "Email Invalid";
                }
            }
            else
            {
                label1.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && textBox1.Text != "")
            {
                if (!checkEmailFormat(textBox1.Text))
                {
                    label1.Text = "Email Invalid";
                }
                else
                {
                    label1.Text = "";
                }
            }
        }

        //Check phone input format and show if it's valid
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked&&textBox2.Text!="")
            {
                if (!checkPhoneNumberFormat(textBox2.Text))
                {
                    label2.Text = "Number Invalid";
                }
            }
            else
            {
                label2.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked && textBox2.Text != "")
            {
                if (!checkPhoneNumberFormat(textBox2.Text))
                {
                    label2.Text = "Number Invalid";
                }
                else
                {
                    label2.Text = "";
                }
            }
        }
    }
}
