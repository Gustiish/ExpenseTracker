using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml;

namespace ExpenseTracker
{
    internal class Controller
    {
        public Input Input { get; set; } = new Input();
        public Output Output { get; set; } = new Output();


        public List<Expense> Expenses { get; set; }

        public bool IsOn { get; set; } = true;
        private readonly string filePathJson = @"C:\Users\Isak Bäckström\JsonDokument\ExpenseTracker\Expenses.json";
        private readonly string filePathCSV = @"C:\Users\Isak Bäckström\CSVDokument\ExpenseTracker\Expenses.csv";
        public Controller()
        {
            Expenses = GetJson();
        }

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
                    SaveToJson();
                    IsOn = false;
                    break;
                case "export":
                    ExportToCSV();
                    break;
                case "help":
                    Console.WriteLine(Output.ShowHelp);
                    break;
                default:
                    break;
            }
        }

        public void WelcomeMessage()
        {
            Console.WriteLine(Output.WelcomeMessage); 
        }

        public string[] ConvertStringInput(string input)
        {
            string[] inputs = new string[3];
            try
            {


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

            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid input");
            }
            return inputs;
        }

        public void AddExpense(string description, string amount)
        {
            int amountInt = Convert.ToInt32(amount);
            try
            {
                Expense expense = new Expense(amountInt, description);
                expense.Id = CheckId();
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
                foreach (Expense expense in Expenses)
                {
                    if (expense.Id > foundExpense.Id)
                    {
                        expense.Id--;
                    }
                }

            }
            else
            {
                Console.WriteLine("Invalid Id");
            }
        }




        public void SaveToJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(Expenses, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePathJson, json);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save to json");
                Console.WriteLine(ex.ToString());
            }

        }

        public int CheckId()
        {
            try
            {

                int id = Expenses.Max(e => e.Id);
                id++;
                return id;
            }
            catch (Exception ex)
            {
                return 1;
            }



        }

        public List<Expense> GetJson()
        {

            try
            {
                if (File.Exists(filePathJson))
                {
                    string readFileName = File.ReadAllText(filePathJson);
                    var expenses = JsonSerializer.Deserialize<List<Expense>>(readFileName);
                    return expenses;
                }
                else
                {
                    File.WriteAllText(filePathJson, "[]");
                    return new List<Expense>();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Expense>();
            }


        }

        public void ExportToCSV()
        {
            var sb = new StringBuilder();


            //Header
            sb.AppendLine("Id,Description,Amount,CreatedAt");

            foreach (var expense in Expenses)
            {
                string line = $"{expense.Id},{expense.Description},{expense.Amount},{expense.CreatedAt:dd-MM-y}";
                sb.AppendLine(line);
            }

            string csv = sb.ToString();
            try
            {
                File.WriteAllText(filePathCSV, csv);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to export");
                Console.WriteLine($"{ex.ToString}");
            }
          



        }


    }
}
