using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Expense
    {
        private static int id { get; set; }
        public int Id
        {
            get
            {
                return id;
            }
            private set
            {

                Id = id;
            }
        }

        private int amount { get; set; }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (amount + value < 0)
                {
                    Console.WriteLine("Amount cannot be negative");
                }
                else
                {
                    amount = value;
                }
            }
        }
        public string Description { get; set; }



        public DateTime CreatedAt { get; set; }


        public Expense()
        {
            IncrementId();
            CreatedAt = DateTime.Now;
            if (amount == 0)
            {
                throw new ArgumentException("Expense not created", nameof(amount));
            }
        }

        private void IncrementId()
        {
            id++;
        }




    }
}
