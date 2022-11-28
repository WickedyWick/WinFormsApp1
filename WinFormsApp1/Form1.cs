using Client.Classes;
using Client.Utils;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string jsonString = @"[{ 
                ""eventId"": 0 ,
                ""eventName"": ""event name 0"",
                ""startTime"": ""2022-02-01 10:00"",
                ""endTime"": ""2022-02-01 12:00"",
                ""maxSeats"": 10
            },
            { 
                ""eventId"": 1 ,
                ""eventName"": ""event name 2"",
                ""startTime"": ""2022-02-01 10:00"",
                ""endTime"": ""2022-02-01 12:00"",
                ""maxSeats"": 10
            }]
            ";

            // MessageBox.Show("BOOOOOOOOOOOOOOOO");

            DataGridViewManager dgvM = new DataGridViewManager(dgvEvents, bindingSource);

            dgvM.LoadAllEvents();

        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            DataGridViewManager dgvM = new DataGridViewManager(dgvEvents, bindingSource);

            dgvM.OrderEvent();
        }
    }
}