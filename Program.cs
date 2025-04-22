namespace ExpenseTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Expense tracker

            //Classes: Expense, Output, Input, Controller/Menu
            while (true)
            {
                Controller controller = new Controller();
                string input = controller.Input.GetUserInput();
                string [] array = controller.ConvertStringInput(input);
                controller.CheckCommandInput(array);
            }
        }
    }
}
