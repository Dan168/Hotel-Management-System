public class Transactions
{
    private string custID;
    private string custName;
    private bool card;
    private int cardNumber;
    private string nameOnCard;
    private decimal ammount;

    public Transactions(string custID, string custName, bool card, int cardNumber, decimal ammount)
    {
        this.custID = custID;
        this.custName = custName;
        this.card = card;
        this.cardNumber = cardNumber;
        this.nameOnCard = nameOnCard;
        this.ammount = ammount;
    }

    public void MarkAsPaid()
    {
        //add transaction into csv file and change the settled collumn to TRUE in the custmoers csv
        string[] csvLines = File.ReadAllLines("DATA//CUSTOMERS.csv");
        for (int i = 0; i < csvLines.Length; i++)
        {
            try
            {
                string[] fields = csvLines[i].Split(",");
                if (!fields.Contains(custID))
                {
                    string toWrite = fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] +
                                     "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8] + "," +
                                     fields[9] + "," + fields[10] + "\n";
                    File.AppendAllText("DATA//temp.csv", toWrite);
                }
                else
                {
                    string toWrite = fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," + fields[4] +
                                     "," + fields[5] + "," + fields[6] + "," + fields[7] + "," + fields[8] + "," +
                                     fields[9] + "," + "TRUE" + "\n";
                    File.AppendAllText("DATA//temp.csv", toWrite);
                }
            }
            catch
            {
                Console.WriteLine("Something has gone wrong.");
            }
        }

        //Save the changes to the customer CSV
        File.Delete("DATA//CUSTOMERS.csv");
        File.Move("DATA//temp.csv", "DATA//CUSTOMERS.csv");

        //Add this transaction into the Transaction CSV
        string toWrite1 = custID + "," + custName + "," + ammount + "," + card + "\n";
        File.AppendAllText("DATA//TRANSACTIONS.csv", toWrite1);

    }
    public void CreateCustomerRecipt()
    {
        string text = $" ***CUSTOMER RECIPT*** \nAmount paid: £{ammount} \nCard payment?: {card} \n\n Thank you for your custom {custName}.";
        File.WriteAllText($"CUSTOMER_RECIPTS//{custName.Trim()}.txt", text);
    }
}