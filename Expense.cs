using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Expense
    {
        private int id { get; set; } = 0;
        public int Id
        {
            get
            {
                return Id;
            }
            private set
            {
                id++;
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
                if (amount + value > 0)
                {
                    amount = value;
                }
                else
                {
                    Console.WriteLine("Amount cannot be negative");
                }
            }
        }
        public string Description { get; set; }


        private DateTime createdAt { get; set; }
        public DateTime CreatedAt
        {
            get
            {
                return createdAt;
            }
            private set
            {
                createdAt = DateTime.Now;
            }
        }
    }
}
