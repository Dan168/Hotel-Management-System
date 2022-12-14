using System.Threading.Channels;
public class Customers
{
    private string custName;
    private string custTelNum;
    private string custEmail;
    private string custAssignedRoom;
    private int numOfNights;
    private string custBreakfast;
    private string custLateCheckout;
    private DateTime checkOut;

    public Customers(string custName, string custTelNum, string custEmail, string custAssignedRoom, int numOfNights, string custBreakfast, string custLateCheckout, DateTime checkOut)
    {
        this.custName = custName;
        this.custTelNum = custTelNum;
        this.custEmail = custEmail;
        this.custAssignedRoom = custAssignedRoom;
        this.numOfNights = numOfNights;
        this.custBreakfast = custBreakfast;
        this.custLateCheckout = custLateCheckout;
        this.checkOut = checkOut;
    }

    public void AddGuest()
    {
        string[] guestCsvLines = File.ReadAllLines("DATA//CUSTOMERS.csv");
        string clientDetails = "\n" + guestCsvLines.Length + ","+ custName + "," + custTelNum + "," + custEmail + "," + custAssignedRoom + "," + custBreakfast.ToUpper() + "," + custLateCheckout.ToUpper() + "," + numOfNights + "," + checkOut.ToShortDateString() + "," + CalculateTotalCost() + "," + "FALSE";
        File.AppendAllText("DATA//CUSTOMERS.csv", clientDetails);
    }
    public void CreateCustomerLetter()//Will create a personalised customer letter containing customer number and their room. 
    {
        string text = $"Hi, {custName}! \nWelcome to our hotel. We hope you have a relaxing stay. Please see the breakdown of costs and addons below: \nRoom Number: {custAssignedRoom} \nBreakfast? {custBreakfast} \n Late Check-out?: {custLateCheckout} \nCheck-out day: {checkOut} \nShould you need any assistance during your stay, please contact reception. \n\nThank you for your custom.";
        File.WriteAllText($"CUSTOMER_WELCOME//{custName.Trim()}.txt", text);
    }
    public double CalculateTotalCost()
    {
        double price = 0;
        string[] csv = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
        string[] tarifLines = csv[0].Split(",");
        double nightRate = Convert.ToDouble(tarifLines[0]);
        double breakfastRate = Convert.ToDouble(tarifLines[1]);
        double lateCheckOutRate = Convert.ToDouble(tarifLines[2]);
        
        price = nightRate * numOfNights;

        if (custBreakfast == "YES")
        {
            price = price + (breakfastRate * numOfNights);
        }
        if (custLateCheckout == "YES")
        {
            price = price + lateCheckOutRate;
        }
        
        return price;
    }
}