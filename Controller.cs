using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Controller
    {
        public Input Input { get; set; } = new Input();
        public Output Output { get; set; } = new Output();
        private readonly string[] CommandPattern = { "add", "list", "delete", "summary" };
        public List<Expense> Expenses { get; set; } = new List<Expense>();


        public void CheckCommandInput(string[] args)
        {
            string command = args[0];
            string description = args[1];
            string amount = args[2];

            string commandLower = command.ToLower();
            switch (commandLower)
            {
                case "add":
                    AddExpense(description, amount);
                    break;
                case "list":
                   // ListExpenses();
                    break;
                case "summary":
                   // ShowTotalExpenses();
                    break;
                case "delete":
                   // DeleteExpenses();
                    break;
                default:
                    break;
            }
        }

     

        public string[] ConvertStringInput(string input)
        {

            string[] inputs = new string[3];

            string commandPattern = @"--([^\s]+)";
            Regex regexCommand = new Regex(commandPattern);
            Match matchCommand = regexCommand.Match(input);

            if (matchCommand.Success)
            {
                inputs[0] = regexCommand.Match(input).Groups[1].Value;
            }
           
            string descriptionPattern = @"""([^""]+)""";
            Regex regexDescription = new Regex(descriptionPattern);
            Match matchDescription = regexDescription.Match(input);
            if (matchDescription.Success)
            {
                inputs[1] = regexDescription.Match(input).Groups[1].Value;
            }

            string amountPattern = @"\d+";
            Regex regexAmount = new Regex(amountPattern);
            Match matchAmount = regexAmount.Match(input);
            if (matchAmount.Success)
            {
                string amountString = regexAmount.Match(input).Groups[1].Value.ToString();
                inputs[2] = amountString;
            }
            return inputs;
        }

        public void AddExpense(string description, string amount)
        {
            int amountInt = Convert.ToInt32(amount);
            Expense expense = new Expense()
            {
                Description = description,
                Amount = Convert.ToInt32(amountInt)
            };

            Expenses.Add(expense);
            Console.WriteLine($"Expense added succesfully! (Id: {expense.Id})");


        }


    }
}
