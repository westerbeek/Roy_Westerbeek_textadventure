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
            //items
            Potion potion = new Potion("potion", "\nA nice looking flask with liquid inside.\n", 2, "potion");
            Boulder boulder = new Boulder("boulder", "\nA massive rock,\nfor it's size it's quite light.\nYou suspect that it's a theatre prop.\n", 5, "useless");
            Broken_Glass broken_glass = new Broken_Glass("broken_glass", "\nA broken glass shard,\nbe careful it might be sharp.\n", 1, "broken glass");
            Key key = new Key("key", "\nIt's a Key, It can be used to open various things\n", 1, "key");

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
            outside.roominv.slots.Add(broken_glass);

            theatre.setExit("west", outside);
            theatre.roominv.slots.Add(boulder);
            pub.setExit("east", outside);
            pub.setExit("down", basement);


            lab.setExit("north", outside);
            lab.setExit("east", office);

            office.setExit("west", lab);
            office.setExit("up", roof);
            office.roomname = "office";
            office.roominv.slots.Add(key);

            basement.setExit("up", pub);
            basement.roominv.slots.Add(potion);

            roof.setExit("down", office);
            roof.roomname = "roof";
            roof.locked = true;

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
            while (!finished)
            {
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
            Console.WriteLine("Type 'help' if you need help.\n");
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

            if (command.isUnknown())
            {
                Console.WriteLine("\nI don't know what you mean...\n");
                return false;
            }

            string commandWord = command.getCommandWord();
            switch (commandWord)
            {
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
                case "drop":
                    Drop(command);
                    break;
                case "use":
                    Use(command);
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
            Console.WriteLine("You wander around at the university.\n");
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
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = user.currentRoom.getExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine("There is no door to " + direction + "!\n");
            }
            else
            {
                if (user.inv.overweight == false)
                {
                    if (nextRoom.locked != true)
                    {
                        user.damage(5);
                        if (user.dead == true)
                        {
                            Console.WriteLine("\nYou seem to have died.\n better luck next time\n");

                        }
                        else
                        {
                            if (nextRoom.roominv.slots == null)
                            {
                                Console.WriteLine("------------------\nYou're really bad at walking and trip,\nyour current health is " + user.health + ".\n");
                                user.currentRoom = nextRoom;
                                Console.WriteLine(user.currentRoom.getLongDescription());
                            }
                            else
                            {
                                Console.WriteLine("------------------\nYou're really bad at walking and trip,\nyour current health is " + user.health + ".\n");
                                for (int i = nextRoom.roominv.slots.Count - 1; i >= 0; i--)
                                {
                                    Console.WriteLine("There seems to be a " + nextRoom.roominv.slots[i].name + " in this room, \nmaybe you want 'take' it with you!\n");
                                }
                                user.currentRoom = nextRoom;
                                Console.WriteLine(user.currentRoom.getLongDescription());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The Door to the " + nextRoom.roomname + " Seems to be locked, you should find a Key and 'use' it here");

                    }
                }
                else
                {
                    Console.WriteLine("\nYou are carrying too much!\nYou should 'drop' something to continue.\n");
                    
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
                Console.WriteLine("------------------\nTake what?\n");
                return;
            }
            string itemname = command.getSecondWord();

            if (user.currentRoom.roominv.look4Item(itemname) != null)
            {

                if (user.currentRoom.roominv.look4Item(itemname).type == "broken glass")
                {
                    Console.WriteLine("------------------\nyou tried to grab " + itemname + ".\nbut you cut yourself failing to pick it up.\n\nYou take 10 damage.\nYour current health is " + user.health + ".\n");
                    user.damage(10);

                    Console.WriteLine(user.currentRoom.getLongDescription());
                }
                else
                {
                    user.currentRoom.roominv.Switchitems(user.inv, itemname);
                    Console.WriteLine("------------------\nyou grabbed " + itemname + ".\n");
                    Console.WriteLine(user.currentRoom.getLongDescription());
                }
            }
            else
            {

                Console.WriteLine("------------------\nYou don't have " + itemname + " in your inventory\n");
                Console.WriteLine(user.currentRoom.getLongDescription());

            }

        }

        private void Drop(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("------------------\ndrop what?\n");
                return;
            }
            string itemname = command.getSecondWord();


            if (user.inv.look4Item(itemname) != null)
            {


                user.inv.Switchitems(user.currentRoom.roominv, itemname);
                Console.WriteLine("------------------\nyou dropped " + itemname + ".\n");
                Console.WriteLine(user.currentRoom.getLongDescription());
            }
            else
            {

                Console.WriteLine("------------------\nYou don't have " + itemname + " in your inventory\n");
                Console.WriteLine(user.currentRoom.getLongDescription());
            }



        }
        private void Use(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("------------------\nUse what?\n");
                return;
            }
            string itemname = command.getSecondWord();


            if (user.inv.look4Item(itemname) != null)
            {
                if (user.inv.look4Item(itemname).type == "key")
                {
                    Console.WriteLine("------------------\nfound key");
                    if (user.currentRoom.roomname == "office")
                    {

                        Console.WriteLine("------------------\nis in office");
                        user.inv.look4Item(itemname).Use();
                        user.inv.slots.Remove(user.inv.look4Item(itemname));
                        user.inv.checkweight();

                        user.currentRoom.getExit("up").locked = false;
                    }
                }
                else if (user.inv.look4Item(itemname).type == "potion")
                {
                    user.inv.look4Item(itemname).Use();
                    user.inv.slots.Remove(user.inv.look4Item(itemname));
                    user.inv.checkweight();
                    user.heal(25);
                    Console.WriteLine("\nYour current health is " + user.health + ".\n");
                }else if (user.inv.look4Item(itemname).type == "useless")
                {
                    user.inv.look4Item(itemname).Use();
                    
                }
                else
                {

                    Console.WriteLine("------------------\nYou don't have " + itemname + " in your inventory\n");
                    Console.WriteLine(user.currentRoom.getLongDescription());
                }

            }
        }

    }
}
