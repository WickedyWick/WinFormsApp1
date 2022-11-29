using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Classes;

namespace Client.Utils
{
    internal class DataGridViewManager
    {
        private DataGridView dgv;
        private BindingSource bs;
        public DataGridViewManager(DataGridView dgv, BindingSource bs) {
            this.dgv = dgv;
            this.bs = bs;
        }

        public async void LoadAllEvents()
        {
            ConfigureGridViewAndGenerateColumns();

            List<Event>? events = await APIConsumer.GetAllEvents();
            if (events == null)
                return;
            foreach (Event e in events)
            {
                bs.Add(e);
            }
        }

        public async void OrderEvent()
        {
            Tuple<bool, int?> checkResultTuple = CheckIfEventOrderable();

            if (checkResultTuple.Item1)
            {
                // simulate userId
                int userId = 1;
                int orderId = Convert.ToInt32(checkResultTuple.Item2);
                bool result = await APIConsumer.OrderEvent(userId, orderId);
                if (result)
                {
                    // update dgv
                }
            }
        }

        private Tuple<bool, int?> CheckIfEventOrderable()
        {
            Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount != 0)
            {
                MessageBoxWrapper.ErrorMessage(
                    "Its only possible to order one event at the time!",
                    "No event selected!"
                );
                return new Tuple<bool, int?>(false, null);
            }

            int id = -1;
            for (int i = 0; i < selectedRowCount; i++)
            {
                try
                {

                    id = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value);
                    int maxSeats = Convert.ToInt32(dgv.SelectedRows[i].Cells[4].Value);
                    if (maxSeats == 0)
                    {
                        MessageBoxWrapper.ErrorMessage(
                            $"Not possible to order even with 0 seats -- check event with id {id}",
                            "Order not allowed!"
                        );

                        return new Tuple<bool, int?>(false, null);
                    }

                } catch(NullReferenceException)
                {
                    // Possible same message as one above?
                    MessageBoxWrapper.ErrorMessage(
                        $"Not possible to order event with no seats -- check event with id {id}",
                        "Order not allowed!"
                    );
                    return new Tuple<bool, int?>(false, null);
                }
               
            }
            return new Tuple<bool, int?>(id > -1, id);

        }
        private void ConfigureGridViewAndGenerateColumns()
        {
            // Configuring gridview
            dgv.AutoGenerateColumns = false;
            dgv.AutoSize = true;
            dgv.DataSource = bs;

            // Generating columns
            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "EventId";
            column.Name = "Event Id";
            dgv.Columns.Add(column);

            DataGridViewColumn column1 = new DataGridViewTextBoxColumn();
            column1.DataPropertyName = "EventName";
            column1.Name = "Event Name";
            dgv.Columns.Add(column1);

            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "StartTime";
            column2.Name = "Start Time";
            dgv.Columns.Add(column2);

            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "EndTime";
            column3.Name = "End Time";
            dgv.Columns.Add(column3);

            DataGridViewColumn column4 = new DataGridViewTextBoxColumn();
            column4.DataPropertyName = "SeatsAvailable";
            column4.Name = "Seats Available";
            dgv.Columns.Add(column4);
        }


    }
}
