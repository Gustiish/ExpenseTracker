using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;

namespace ExpenseTracker
{
    internal class Controller
    {
        public Input Input { get; set; } = new Input();
        public Output Output { get; set; } = new Output();
        
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public bool IsOn { get; set; } = true;
        private readonly string Path = @"C:\Users\isakb\source\repos\ExpenseTracker\JsonFiles";


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
                    ListExpenses();
                    break;
                case "summary":
                    ShowTotalExpenses();
                    break;
                case "delete":
                    DeleteExpense(description);
                    break;
                case "off":
                    IsOn = false;
                    break;
                case "save":
                    SaveToJson();
                    break;
                default:
                    break;
            }
        }



        public string[] ConvertStringInput(string input)
        {

            string[] inputs = new string[3];

            string commandPattern = @"^\w+";
            Regex regexCommand = new Regex(commandPattern);
            Match matchCommand = regexCommand.Match(input);

            if (matchCommand.Success)
            {
                inputs[0] = matchCommand.Value;
            }

            string deleteIdPattern = @"--id\s+(\d+)";
            Regex regexId = new Regex(deleteIdPattern);
            Match matchId = regexId.Match(input);
            if (matchId.Success)
            {
                inputs[1] = matchId.Groups[1].Value;
            }


            string descriptionPattern = @"""([^""]+)""";
            Regex regexDescription = new Regex(descriptionPattern);
            Match matchDescription = regexDescription.Match(input);
            if (matchDescription.Success)
            {
                inputs[1] = regexDescription.Match(input).Groups[1].Value;
            }

            string amountPattern = @"-?\d+";
            Regex regexAmount = new Regex(amountPattern);
            Match matchAmount = regexAmount.Match(input);
            if (matchAmount.Success)
            {
                string amountString = regexAmount.Match(input).Groups[0].Value.ToString();
                inputs[2] = amountString;
            }
            return inputs;
        }

        public void AddExpense(string description, string amount)
        {
            int amountInt = Convert.ToInt32(amount);

            try
            {
                Expense expense = new Expense()
                {
                    Description = description,
                    Amount = amountInt
                };

                Expenses.Add(expense);
                Console.WriteLine($"Expense added succesfully! (Id: {expense.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expense not created {ex.ToString()}");
            }
            
        }

        public void ListExpenses()
        {
            Console.WriteLine("# ID Date Description Amount");
            foreach (Expense expense in Expenses)
            {
                Console.WriteLine($"# {expense.Id} {expense.CreatedAt.Date.ToString("dd/MM/yy")} {expense.Description} {expense.Amount}");
            }
        }

        public void ShowTotalExpenses()
        {
            if (Expenses.Count == 0)
            {
                Console.WriteLine("There are no expenses yet");
            }
            else
            {
                Console.WriteLine($"Total expenses: {Expenses.Sum(e => e.Amount)}");
            }

        }

        public void DeleteExpense(string userId)
        {
            int id = Convert.ToInt32(userId);
            Expense foundExpense = Expenses.Find(e => e.Id == id);
            if (foundExpense != null)
            {
                Expenses.Remove(foundExpense);


            }
            else
            {
                Console.WriteLine("Invalid Id");
            }
        }




        public void SaveToJson()
        {
            string json = JsonSerializer.Serialize(Expenses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path, json);
        }



    }
}
