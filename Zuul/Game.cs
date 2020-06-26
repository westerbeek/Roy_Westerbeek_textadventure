using System;
using System.Diagnostics;

namespace Zuul
{
    public class Game
    {
        private Parser parser;
        private Player user;

        public Game()
        {


            parser = new Parser();
            user = new Player();
            user.setstats();
            createRooms();

        }

        private void createRooms()
        {

            Room outside, theatre, pub, lab, office, roof, basement;

            // create the rooms
            outside = new Room("outside the main entrance of the university");
            theatre = new Room("in a lecture theatre");
            pub = new Room("in the campus pub");
            lab = new Room("in a computing lab");
            office = new Room("in the computing admin office");
            roof = new Room("on the roof of the complex.");
            basement = new Room("in the basement of the building");
            // initialise room exits
            outside.setExit("east", theatre);
            outside.setExit("south", lab);
            outside.setExit("west", pub);

            theatre.setExit("west", outside);

            pub.setExit("east", outside);
            pub.setExit("down", basement);


            lab.setExit("north", outside);
            lab.setExit("east", office);

            office.setExit("west", lab);
            office.setExit("up", roof);

            basement.setExit("up", pub);
            basement.roominv.slots[0].name = "potion";

            roof.setExit("down", office);

            user.currentRoom = outside;  // start game outside
        }


        /**
	     *  Main play routine.  Loops until end of play.
	     */
        public void play()
        {
            printWelcome();

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;
            while (!finished) {
                Command command = parser.getCommand();
                finished = processCommand(command);
            }
            Console.WriteLine("Thank you for playing.");
        }

        /**
	     * Print out the opening message for the player.
	     */
        private void printWelcome()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Zuul!");
            Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
            Console.WriteLine("Type 'help' if you need help.");
            Console.WriteLine();
            Console.WriteLine(user.currentRoom.getLongDescription());
        }

        /**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
        private bool processCommand(Command command)
        {
            bool wantToQuit = false;

            if (command.isUnknown()) {
                Console.WriteLine("I don't know what you mean...");
                return false;
            }

            string commandWord = command.getCommandWord();
            switch (commandWord) {
                case "help":
                    printHelp();
                    break;
                case "go":
                    goRoom(command);
                    break;

                case "look":
                    Look();
                    break;
                case "take":
                    Take(command);
                    break;

                case "quit":
                    wantToQuit = true;
                    break;

            }

            return wantToQuit;
        }

        // implementations of user commands:

        /**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
        private void printHelp()
        {
            Console.WriteLine("You are lost. You are alone.");
            Console.WriteLine("You wander around at the university.");
            Console.WriteLine();
            Console.WriteLine("Your command words are:");
            parser.showCommands();
        }

        /**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
        private void goRoom(Command command)
        {
            if (!command.hasSecondWord()) {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = user.currentRoom.getExit(direction);

            if (nextRoom == null) {
                Console.WriteLine("There is no door to " + direction + "!");
            } else {

                user.damage(5);
                if (user.dead == true)
                {
                    Console.WriteLine("You seem to have died.\nPlease type quit in order to quit the game.");

                }
                else
                {
                    if (nextRoom.roominv.slots[0].name == null)
                    {
                        Console.WriteLine("You're really bad at walking and trip,\nyour current health is " + user.health + ".");
                        user.currentRoom = nextRoom;
                        Console.WriteLine(user.currentRoom.getLongDescription());
                    }
                    else
                    {
                        Console.WriteLine("You're really bad at walking and trip,\nyour current health is " + user.health + ".");
                        Console.WriteLine("There seems to be a " + nextRoom.roominv.slots[0].name + " in this room, \nmaybe you want to pick it up!");
                        user.currentRoom = nextRoom;
                        Console.WriteLine(user.currentRoom.getLongDescription());
                    }
                }
            }
        }

        private void Look()
        {
            // Try to leave current room.

            Console.WriteLine(user.currentRoom.description);


        }
        private void Take(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Take what?");
                return;
            }
            string itemname = command.getSecondWord();

            for (int i = 0; i < user.currentRoom.roominv.slots.Length; i++)
            {
                if (itemname == user.currentRoom.roominv.slots[i].name)//if the given itemname is the same as one in the room inv
                {
                    for (int x = 0; x < user.inv.slots.Length; x++)
                    {
                        if (user.inv.slots[x] == null)
                        {

                            user.inv.slots[x] = user.currentRoom.roominv.slots[i];

                            user.currentRoom.roominv.slots[i].name = null;
                            Console.WriteLine("You picked up '" + itemname + "'.");

                            break;
                        }
                        break;
                    }
                    break;
                }
                else
                {

                    Console.WriteLine("No such item could be found.");
                }

            }
           
        }

    }


}
