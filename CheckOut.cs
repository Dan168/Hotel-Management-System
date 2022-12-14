public class CheckOut
{
    private string custID;

    public CheckOut(string custName, string custId)
    {
        this.custID = custId;
    }
    public void CheckGuestOut()
    {
        string[] custCsvLines = File.ReadAllLines("DATA//CUSTOMERS.csv");
        
        for (int i = 0; i < custCsvLines.Length; i++)
        {
            string[] fields = custCsvLines[i].Split(",");
            if (!fields.Contains(custID))
            {
                string custDetailsToWrite = "\n" + fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3] + "," +
                                            fields[4] + "," + fields[5] + "," + fields[6] + "," + fields[7] + "," +
                                            fields[8] + "," + fields[9] + "," + fields[10];
                File.AppendAllText("DATA//temp.csv", custDetailsToWrite);
            }
            else
            {
                continue;
            }
        }
        File.Delete("DATA//CUSTOMERS.csv");
        File.Move("DATA//temp.csv", "DATA//CUSTOMERS.csv");
    }
}