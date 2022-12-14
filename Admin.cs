public class Admin
{
    private string nightRate;
    private string breakfastRate;
    private string lateCheckOutRate;
    private string password;

    public Admin()
    {
        string[] adminCsvLines = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
        string[] adminFields = adminCsvLines[0].Split(",");
        
        this.nightRate = adminFields[0];
        this.breakfastRate = adminFields[1];
        this.lateCheckOutRate = adminFields[2];
        this.password = adminFields[3];
    }

    public bool AdminAuthenticate(string inputPassword)
    {
        if (inputPassword == password)
        {
            return true;
        }
        return false;
    }

    public string GetNightRate()
    {
        return nightRate;
    }

    public string GetBreakfastRate()
    {
        return breakfastRate;
    }

    public string GetLateCheckoutRate()
    {
        return lateCheckOutRate;
    }

    public void NewNightRate(double newRate)
    {
        try
        {
            string[] editCsvLines = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
            string[] adminFields = editCsvLines[0].Split(",");

            string toWrite1 = newRate + "," + adminFields[1] + "," + adminFields[2] + "," + adminFields[3];
            File.AppendAllText("DATA//ADMIN//temp.csv", toWrite1);

            File.Delete("DATA//ADMIN//TARIF.csv");
            File.Move("DATA//ADMIN//temp.csv", "DATA//ADMIN//TARIF.csv");

            nightRate = Convert.ToString(newRate);
        }
        catch
        {
            Console.WriteLine("Error has occoured setting new night rate. Please make sure the new price is in the right format and there are no spaces.");
        }
    }

    public void NewBreakfastRate(double newRate)
    {
        try
        {
            string[] editCsvLines = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
            string[] adminFields = editCsvLines[0].Split(",");

            string toWrite1 = adminFields[0] + "," + newRate + "," + adminFields[2] + "," + adminFields[3];
            File.AppendAllText("DATA//ADMIN//temp.csv", toWrite1);

            File.Delete("DATA//ADMIN//TARIF.csv");
            File.Move("DATA//ADMIN//temp.csv", "DATA//ADMIN//TARIF.csv");
            
            breakfastRate = Convert.ToString(newRate);
        }
        catch
        {
            Console.WriteLine("Error has occoured setting new breakfast rate. Please make sure the new price is in the right format and there are no spaces.");
        }
    }

    public void MewLateCheckoutRate(double newRate)
    {
        try
        {
            string[] editCsvLines = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
            string[] adminFields = editCsvLines[0].Split(",");

            string toWrite1 = adminFields[0] + "," + adminFields[1] + "," + newRate + "," + adminFields[3];
            File.AppendAllText("DATA//ADMIN//temp.csv", toWrite1);

            File.Delete("DATA//ADMIN//TARIF.csv");
            File.Move("DATA//ADMIN//temp.csv", "DATA//ADMIN//TARIF.csv");
            
            lateCheckOutRate = Convert.ToString(newRate);
        }
        catch
        {
            Console.WriteLine("Error has occoured setting new late checkout rate. Please make sure the new price is in the right format and there are no spaces.");
        }
    }
    
    public void NewPassword(string newPass)
    {
        try
        {
            string[] editCsvLines = File.ReadAllLines("DATA//ADMIN//TARIF.csv");
            string[] adminFields = editCsvLines[0].Split(",");

            string toWrite1 = adminFields[0] + "," + adminFields[1] + "," + adminFields[2] + "," + newPass;
            File.AppendAllText("DATA//ADMIN//temp.csv", toWrite1);

            File.Delete("DATA//ADMIN//TARIF.csv");
            File.Move("DATA//ADMIN//temp.csv", "DATA//ADMIN//TARIF.csv");
            
            password = newPass;
        }
        catch
        {
            Console.WriteLine("Error has occoured setting new password. Please try again. if that dosent work, take a copy of the customer databses and try re-installing the software.");
        }
    }
}