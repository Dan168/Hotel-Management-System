public class Rooms
{
    private string roomNum;

    public void DisplayRooms()
    {
        string[] rooms = File.ReadAllLines("DATA//ROOMS.csv");

        int occupied = 0;
        int available = 0;
        
        foreach (var item in rooms)
        {
            Console.WriteLine(item);
            if (item.Contains("TRUE"))
            {
                occupied++;
            }
            else
            {
                available++;
            }
        }
        Console.WriteLine($"Occupied Rooms: {occupied} \nAvailable Rooms: {available}");
    }
    
    public void ChangeRoomStatusTrue(string roomNum)
        /*HOW THIS FUNCTION WORKS
         For loop goes throuhg the rooms csv and inputs each room and its status to a temp file.
         it stops at the selected room, changes its status to true and then continues writing the rest of the file
         to the temp file. once it reaches the end, the temp file renames itself to DATA and the original data file is removed.
         */
    {
        string[] roomCsvLines = File.ReadAllLines("DATA//ROOMS.csv");

        for (int i = 1; i < roomCsvLines.Length; i++)
        {
            string[] fields = roomCsvLines[i].Split(",");
            if (!fields.Contains(roomNum))
            {
                if (fields[1].Contains("FALSE"))
                {
                    string toWrite1 = "\n" + fields[0] + "," + "FALSE";
                    File.AppendAllText("DATA//temp.csv", toWrite1);
                }
                else
                {
                    string toWrite1 = "\n" + fields[0] + "," + "TRUE";
                    File.AppendAllText("DATA//temp.csv", toWrite1);
                }
            }
            else
            {
                string toWrite = "\n" + roomNum + "," + "TRUE";
                File.AppendAllText("DATA//temp.csv", toWrite);
            }
        }
        File.Delete("DATA//ROOMS.csv");
        File.Move("DATA//temp.csv", "DATA//ROOMs.csv");
    }
    
    public void ChangeRoomStatusFalse(string roomNum)
        /*HOW THIS FUNCTION WORKS
         For loop goes throuhg the rooms csv and inputs each room and its status to a temp file.
         it stops at the selected room, changes its status to true and then continues writing the rest of the file
         the file he 
         */
    {
        string[] csvLines = File.ReadAllLines("DATA//ROOMS.csv");

        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] fields = csvLines[i].Split(",");
            if (!fields.Contains(roomNum))
            {
                if (fields[1].Contains("FALSE"))
                {
                    string toWrite1 = "\n" + fields[0] + "," + "FALSE";
                    File.AppendAllText("DATA//temp.csv", toWrite1);
                }
                else
                {
                    string toWrite1 = "\n" + fields[0] + "," + "TRUE";
                    File.AppendAllText("DATA//temp.csv", toWrite1);
                }
            }
            else
            {
                string toWrite = "\n" + roomNum + "," + "FALSE";
                File.AppendAllText("DATA//temp.csv", toWrite);
            }
        }
        File.Delete("DATA//ROOMS.csv");
        File.Move("DATA//temp.csv", "DATA//ROOMS.csv");
    }

    public string AutoAllocateRoom()
    {
        string[] csvLines = File.ReadAllLines("DATA//ROOMS.csv");

        for (int i = 1; i < csvLines.Length; i++)
        {
            string[] fields = csvLines[i].Split(",");
            if (!fields.Contains("TRUE"))
            {
                ChangeRoomStatusFalse(fields[0]);
                return fields[0];
            }
        }
        return ("No rooms available");
    }   
}