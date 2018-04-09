using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Message = POP3r.Pop3.Message;

namespace POP3r
{
    public partial class MainWindow : Form
    {
        private readonly string _ipAddress = ConfigurationManager.AppSettings["serverIp"];
        private readonly string _port = ConfigurationManager.AppSettings["port"];

        private MailHandler _handler;
        private List<Message> _listOfMail; // ideally this should be in the mail handler, but got no time for that

        public int AmountOfMail { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            _handler = new MailHandler(_ipAddress, _port);
        }

        // Ugly code written because lack of time
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("Both username and password have to be filled");
                return;
            }

            try
            {
                switch (_handler.State)
                {
                    case SessionStates.AUTHORIZATION:
                        _handler.Login(UsernameBox.Text, PasswordBox.Text);
                        UpadateMailList();
                        break;
                    case SessionStates.TRANSACTION:
                        _handler.Logout();
                        EmailListView.Rows.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex);
                return;
            }
            finally
            {
                LoginButton.Text = _handler.State == SessionStates.AUTHORIZATION ? "Login" : "Logout";
            }
        }

        private void FetchMail()
        {
            try
            {
                _listOfMail = _handler.FetchMailList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e);
                return;
            }
        }

        private void UpadateMailList()
        {
            AmountOfMail = _handler.GetAmountOfMail();
            FetchMail();

            foreach (var email in _listOfMail)
            {
                EmailListView.Rows.Add(email.Sender, email.Subject);
            }
        }

        private void EmailListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MailWindow.DocumentText = _listOfMail[e.RowIndex].Body;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MailWindow.DocumentText) && EmailListView.SelectedRows.Count != 0)
            {
                var index = EmailListView.SelectedRows[0].Index;
                try
                {
                    _handler.DeleteMessage(_listOfMail[index].IndexInMailList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Console.WriteLine(ex);
                    return;
                }
                
                EmailListView.Rows.Remove(EmailListView.SelectedRows[0]);
                MailWindow.DocumentText = "";
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            FetchMail();
        }
    }
}
