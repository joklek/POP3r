using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using POP3r.Pop3.Interfaces;

namespace POP3r
{
    public partial class MainWindow : Form
    {
        private readonly string _ipAddress = ConfigurationManager.AppSettings["serverIp"];
        private readonly string _port = ConfigurationManager.AppSettings["port"];

        private MailHandler _handler;
        private List<IMessage> _listOfMail;

        public MainWindow()
        {
            InitializeComponent();
            _handler = new MailHandler(_ipAddress, _port);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Text))
            {
                throw new Exception("Both username and password have to be filled");
            }

            try
            {
                _handler.Login(UsernameBox.Text, PasswordBox.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            //UpadateMailList();
        }

        private void UpadateMailList()
        {
            try
            {
                _listOfMail = _handler.FetchMailList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            foreach (var email in _listOfMail)
            {
            }
        }
    }
}
