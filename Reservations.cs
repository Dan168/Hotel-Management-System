//reservations class used to look up customers - possibly consider renaming??
public class Reservations
{
    private string custID;

    public Reservations(string ID)
    {
        this.custID = ID;
    }
    public void customerLookup()
    {
        string[] guestCsvLines = File.ReadAllLines("DATA//CUSTOMERS.csv");
        string[] headers = {"Guest ID", "Customer name", "Tel", "Email", "Room", "Breakfast?", "Late checkout", "Number of nights", "Check out date", "Amount due", "Settled?"};

        for (int i = 0; i < guestCsvLines.Length; i++)
        {
            string[] fields = guestCsvLines[i].Split(",");
            if (fields[0].Contains(custID))
            {
                int headerCount = 0;
                foreach (string item in fields)
                {
                    Console.WriteLine($"\n{headers[headerCount]}");
                    Console.WriteLine(item);
                    headerCount++;
                }
            }
        }
    }
}