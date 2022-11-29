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
        private NumericUpDown nup;
        private BindingSource bs;
        public DataGridViewManager(DataGridView dgv, BindingSource bs, NumericUpDown nup) {
            this.dgv = dgv;
            this.nup = nup;
            this.bs = bs;
        }

        public async void LoadAllEvents()
        {
            // Lazy way of doing it?
           // BindingSource bs = new BindingSource();
            
            List<Event>? events = await APIConsumer.GetAllEvents();
            if (events == null)
                return;
            bs.DataSource= events;

            
        }

        public async void OrderEvent()
        {
            Tuple<bool, int?> checkResultTuple = CheckIfEventOrderable();

            if (checkResultTuple.Item1)
            {
                // simulate userId
                int userId = 1;
                int orderId = Convert.ToInt32(checkResultTuple.Item2);
                int ticketAmount = Convert.ToInt32(nup.Value);
                await APIConsumer.OrderEvent(userId, orderId, ticketAmount);
                LoadAllEvents();
                
            }
        }

        private Tuple<bool, int?> CheckIfEventOrderable()
        {
            Int32 selectedRowCount = dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount != 1)
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
                    } else if (maxSeats < Convert.ToInt32(nup.Value))
                    {
                        MessageBoxWrapper.ErrorMessage(
                            $"Amount of tickets you want to order is higher than number of tickets available",
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
        


    }
}
