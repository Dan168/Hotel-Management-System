using System;
try
{
    string userInput = "0";

    while (userInput != "7")
    {
        Console.WriteLine("\nPlease enter a command: \n1) Check guest in \n2) See available rooms \n3) Check guest out \n4) Take payment \n5) Guest lookup \n6) Admin controls \n7) Quit system");
        userInput = Console.ReadLine();
        
        switch (userInput)
        {
            //Check guest in
            case ("1"):
                try
                {
                    Console.Write("Customer Full Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Customer Contact Number: ");
                    string num = Console.ReadLine();

                    Console.Write("Customer Email: ");
                    string mail = Console.ReadLine();
                    
                    Console.Write("Check out date (dd/mm/yyyy): ");
                    DateTime inputtedDate = DateTime.Parse(Console.ReadLine());
                    var diffOfDates = inputtedDate - DateTime.Now;
                    int nights = diffOfDates.Days + 1;

                    Console.WriteLine("Allocated room (room number/ auto): ");
                    string userInptRoom = Console.ReadLine().ToLower();
                    Rooms roomCheckIn = new Rooms();
                    if (userInptRoom == "auto")
                    {
                        userInptRoom = roomCheckIn.AutoAllocateRoom();
                    }
                    roomCheckIn.ChangeRoomStatusTrue(userInptRoom);

                    Console.WriteLine("Add Breakfast? (yes/ no): ");
                    string breakfast = Console.ReadLine().ToUpper();
                    
                    Console.WriteLine("Add Late Checkout? (yes/ no): ");
                    string lateCheckout = Console.ReadLine().ToUpper();
                    
                    Customers customer = new(name, num, mail, userInptRoom, nights, breakfast, lateCheckout, inputtedDate);
                    
                    customer.AddGuest();
                    
                    //Calculate total cost
                    Console.WriteLine($"Total Cost: £{customer.CalculateTotalCost()}");
                    Console.WriteLine($"Guest has been successfully checked into room {userInptRoom}.");
                    customer.CreateCustomerLetter();
                    Console.WriteLine("A personalised welcome document has been generated to be sent to the customer. This can be found in the 'GUEST WELCOME' folder.");
                    break;
                }
                catch
                {
                    Console.WriteLine("Something has gone wrong. Please make sure responses are formatted correctly. \neg. DD/MM/YYYY");
                    break;
                }
            
            //See rooms 
            case ("2"):
                Rooms SeeRooms = new();
                Console.WriteLine("Room, Occupied?");
                SeeRooms.DisplayRooms();
                break;
           
            //check guests out
            case ("3"):
                //ask for the guest ID and name and mark them as checked out on the database
                
                Console.Write("Guest Name: ");
                string name1 = Console.ReadLine();

                Console.Write("Guest ID number: ");
                string IDnum = Console.ReadLine();
                
                Console.Write("Room Number: ");
                string room1 = Console.ReadLine();
                
                CheckOut checkOut = new(name1, IDnum);
                checkOut.CheckGuestOut();
                
                Rooms roomsCheckOut = new Rooms();
                roomsCheckOut.ChangeRoomStatusFalse(room1);
                break;
            
            //take payment
            case ("4"):
                Console.WriteLine("Customer ID: ");
                string userID = Console.ReadLine();
                
                Console.WriteLine("Customer name (Should match name on card if paying by card): ");
                string custName = Console.ReadLine();
                
                Console.WriteLine("Is the customer paying by card? (true/false)");
                bool card = Convert.ToBoolean(Console.ReadLine());
                int cardNum;
                if (card)
                {
                    Console.WriteLine("Card number: ");
                    cardNum = Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                   cardNum = 0;
                }

                Console.WriteLine("Amount due: ");
                decimal ammount = Convert.ToDecimal(Console.ReadLine());

                Transactions transaction = new Transactions(userID, custName, card, cardNum, ammount);
                transaction.MarkAsPaid();
                Console.WriteLine("Payment processed and saved.");
                transaction.CreateCustomerRecipt();
                Console.WriteLine("A customer recipt has been created to send to the customer.");
                
                break;
            
            //Guest lookup 
            case ("5"):
                Console.WriteLine("Guest ID");
                string ID = Console.ReadLine();
                Reservations custLookup = new(ID);
                custLookup.customerLookup();
                break;
            
            //admin controls 
            case ("6"):
                Console.WriteLine("Please enter Admin Controls password: ");
                string adminPass = Console.ReadLine();

                Admin admin = new();
                if (admin.AdminAuthenticate(adminPass))
                {
                    int userInputAdmin = 0;
                    while (userInputAdmin != 4)
                    {
                        Console.WriteLine("\n•••ADMIN PANEL•••");
                        Console.WriteLine("1) See rates \n2) Change rates \n3) Change admin password \n4) Exit admin panel");
                        userInputAdmin = Convert.ToInt32(Console.ReadLine());

                        switch (userInputAdmin)
                        {
                            case(1):
                                Console.WriteLine($"Night rate £{admin.GetNightRate()} \nBreakfast rate (per morning): £{admin.GetBreakfastRate()} \nLate checkout rate: £{admin.GetLateCheckoutRate()}");
                                break;
                            
                            case(2):
                                Console.WriteLine($"NEW NIGHT RATE (Current: £{admin.GetNightRate()}): ");
                                double inputNewNightRate = Convert.ToDouble(Console.ReadLine());
                                admin.NewNightRate(inputNewNightRate);
                                
                                Console.WriteLine($"NEW BREAKFAST RATE (Current: £{admin.GetBreakfastRate()}): ");
                                double inputNewBreakfastRate = Convert.ToDouble(Console.ReadLine());
                                admin.NewBreakfastRate(inputNewBreakfastRate);
                                
                                Console.WriteLine($"NEW LATE CHECKOUT RATE (Current: £{admin.GetLateCheckoutRate()}): ");
                                double inputLateCheckoutRate = Convert.ToDouble(Console.ReadLine());
                                admin.MewLateCheckoutRate(inputLateCheckoutRate);
                                break;
                            
                            case(3):
                                Console.WriteLine("Old password: ");
                                string passInput = Console.ReadLine();

                                if (admin.AdminAuthenticate(passInput))
                                {
                                    Console.WriteLine("Choose a new password: ");
                                    string newPass1 = Console.ReadLine();
                                    Console.WriteLine("Re-type the password:  ");
                                    string newPass2 = Console.ReadLine();

                                    if (newPass1 == newPass2)
                                    {
                                        try
                                        {
                                            admin.NewPassword(newPass1);
                                            Console.WriteLine("Password has been successfully changed.");
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Password failed to change.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Passwords did not match. \nPassword has not been changed.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect current password.");
                                }
                                break;
                            
                            case(4):
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                }
                break;

            //exit system
            case ("7"):
                Console.WriteLine("Exiting system");
                Console.WriteLine("3");
                Thread.Sleep(1000);
                Console.WriteLine("2");
                Thread.Sleep(1000);
                Console.WriteLine("1");
                Thread.Sleep(1000);
                break;
            
            default:
                Console.WriteLine("Invalid entry. Did you enter a number from the list above?");
                break;
        }
    }
}
catch(Exception ex)
{
    Console.WriteLine("Fatal error. \nWe don't know whats happened here. Please copy this error and send it to the developer along with what you were doing at the time when the error ocoured.");
    Console.WriteLine(ex.ToString());
}


//When tranfering to new file, make sure the view room comma issue is fixed
//ensure the white space issue is sorted in the CSV - decide wether to use headers within the databases and keep it consistent 