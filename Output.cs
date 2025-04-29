using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Output
    {
        public readonly string WelcomeMessage = "Welcome to the expensetracker\nType 'help' in order to recieve the commands.";

        public readonly string ErrorMessageCommand = "Error, wrong command input";
        public readonly string ErrorMessageDescription = "Error, wrong description input";
        public readonly string ErrorMessageAmount = "Error, wrong amount input";

        public StringBuilder ShowHelp = new StringBuilder();

        public Output()
        {
            BuildSb();
        }

        public void BuildSb()
        {
            ShowHelp.AppendLine("Available Commands");
            ShowHelp.AppendLine("------------------------------------------------------");
            ShowHelp.AppendLine("""add ["Description"] [Integer(Cost)] - add a new expense""");
            ShowHelp.AppendLine("delete [--id 'id']- delete the expense using id");
            ShowHelp.AppendLine("list - list all current expenses");
            ShowHelp.AppendLine("summary - show the total expenditure");
            ShowHelp.AppendLine("export - export the expenses to a csv file");
            ShowHelp.AppendLine("------------------------------------------------------");
        }




    }
}
