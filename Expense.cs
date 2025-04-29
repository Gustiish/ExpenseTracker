using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Expense
    {
       
        public int Id { get; set; }

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


        public Expense(int amount, string description)
        {
          
            CreatedAt = DateTime.Now;
            Amount = amount;
            if (Amount == 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
            Description = description;

        }

        public Expense()
        {
            
        }









    }
}
