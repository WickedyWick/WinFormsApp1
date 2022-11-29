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

            DataGridViewManager dgvM = new DataGridViewManager(dgvEvents, bindingSource1, nupNumOfTickets);
            ConfigureGridViewAndGenerateColumns();
            dgvM.LoadAllEvents();

        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            DataGridViewManager dgvM = new DataGridViewManager(dgvEvents,bindingSource1, nupNumOfTickets);

             dgvM.OrderEvent();
        }

        private void ConfigureGridViewAndGenerateColumns()
        {
            // Configuring gridview
            dgvEvents.AutoGenerateColumns = false;
            dgvEvents.AutoSize = true;
            dgvEvents.DataSource = bindingSource1;
            // dgv.Refresh();
            // Generating columns
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "EventId";
            column.Name = "Event Id";
            dgvEvents.Columns.Add(column);

            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "EventName";
            column1.Name = "Event Name";
            dgvEvents.Columns.Add(column1);

            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "StartTime";
            column2.Name = "Start Time";
            dgvEvents.Columns.Add(column2);

            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "EndTime";
            column3.Name = "End Time";
            dgvEvents.Columns.Add(column3);

            DataGridViewColumn column4 = new DataGridViewTextBoxColumn();
            column4.DataPropertyName = "SeatsAvailable";
            column4.Name = "Seats Available";
            dgvEvents.Columns.Add(column4);
        }
    }
}