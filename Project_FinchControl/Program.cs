using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 1/22/2020
    // Last Modified: 1/25/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Play Song");
                Console.WriteLine("\tc) Movement");
                Console.WriteLine("\td) All Of The Above");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayPlaySong(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMovement(finchRobot);
                        break;

                    case "d":
                        TalentShowDisplayAll(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }


        static void TalentShowDisplayPlaySong(Finch finchRobot)
        {
            string userResponse;
            int frequency;

            DisplayScreenHeader("Play a Song");

            Console.WriteLine("Enter Frequency: ");
            userResponse = Console.ReadLine();
            frequency = int.Parse(userResponse);

            finchRobot.noteOn(frequency);
            finchRobot.wait(1000);
            finchRobot.noteOff();

            Console.WriteLine();
            Console.WriteLine("Press any button to hear the birthday song for good luck.");
            Console.ReadLine();

            //
            //play happy birthday song
            //

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(440);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(700);
            finchRobot.noteOff();
            
            finchRobot.noteOn(450);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(493);
            finchRobot.wait(800);
            finchRobot.noteOff();

            finchRobot.wait(700);

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(440);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(450);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(493);
            finchRobot.wait(800);
            finchRobot.noteOff();

            finchRobot.wait(700);

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(350);
            finchRobot.noteOff();

            finchRobot.noteOn(480);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(392);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(349);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(400);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(440);
            finchRobot.wait(800);
            finchRobot.noteOff();

            finchRobot.wait(700);

            finchRobot.noteOn(349);
            finchRobot.wait(400);
            finchRobot.noteOff();

            finchRobot.noteOn(349);
            finchRobot.wait(400);
            finchRobot.noteOff();

            finchRobot.noteOn(329);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(311);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(261);
            finchRobot.wait(700);
            finchRobot.noteOff();

            finchRobot.noteOn(140);
            finchRobot.wait(800);
            finchRobot.noteOff();


            DisplayMenuPrompt("Talent Show");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 55; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 20);
                finchRobot.noteOff();
            }

            Console.WriteLine();
            Console.WriteLine("Press anything to continue with more lights");
            Console.ReadLine();

            //
            // turn on red LED
            //

            finchRobot.setLED(250, 0, 0);
            finchRobot.wait(800);
            finchRobot.setLED(0, 0, 0);

            finchRobot.wait(400);

            //
            // turn on green LED
            //

            finchRobot.setLED(0, 250, 0);
            finchRobot.wait(800);
            finchRobot.setLED(0, 0, 0);

            finchRobot.wait(400);

            //
            // turn on blue LED
            //

            finchRobot.setLED(0, 0, 250);
            finchRobot.wait(800);
            finchRobot.setLED(0, 0, 0);

            //
            //flash user
            //
            
            for (int numberofFlashes = 0; numberofFlashes < 7; numberofFlashes++)
            {
                finchRobot.setLED(250, 0, 0);
                finchRobot.wait(300);
                finchRobot.setLED(0, 0, 250);
                finchRobot.wait(500);
            }
            for (int numberofFlashes = 0; numberofFlashes < 10; numberofFlashes++)
            {
                finchRobot.setLED(0, 250, 0);
                finchRobot.wait(175);
                finchRobot.setLED(0, 0, 250);
                finchRobot.wait(175);
                finchRobot.setLED(250, 0, 0);
            }

           //
           // increase green light
           //

            for (int greenLightLevel = 0; greenLightLevel <= 255; greenLightLevel++)
            {
                finchRobot.setLED(0, greenLightLevel, 0);
            }

            for (int redLightLevel = 0; redLightLevel <= 255; redLightLevel++)
            {
                finchRobot.setLED(redLightLevel, 0, 0);
            }

            for (int blueLightLevel = 0; blueLightLevel <= 255; blueLightLevel++)
            {
                finchRobot.setLED(0, 0, blueLightLevel);
            }

            //
            //decrease blue LED
            //

            for (int blueLightLevel = 255; blueLightLevel >= 0; blueLightLevel--)
            {
                finchRobot.setLED(0, 0, blueLightLevel);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 105; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 20);
                finchRobot.noteOff();
            }

            DisplayMenuPrompt("Talent Show Menu");         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="finchRobot"></param>
       
        static void TalentShowDisplayMovement(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Movement");

            Console.WriteLine("\tThe Finch robot will now show off some unique movements!");
            Console.WriteLine("Press anything to continue");
            DisplayContinuePrompt();

            //
            // move robot forward
            //

            finchRobot.setMotors(250, 250);
            finchRobot.wait(2000);
            finchRobot.setMotors(0, 0);

            //
            // turn wide right
            //

            finchRobot.setMotors(250, 180);
            finchRobot.wait(1300);
            finchRobot.setMotors(0, 0);

            //
            // continue straight
            //

            finchRobot.setMotors(250, 250);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            //
            // turn left sharp
            //

            finchRobot.setMotors(100, 230);
            finchRobot.wait(1300);
            finchRobot.setMotors(0, 0);

            //
            // reverse right 
            //

            finchRobot.setMotors(100, -180);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);

            //
            // go farward and stop
            //

            finchRobot.setMotors(230, 250);
            finchRobot.wait(2000);
            finchRobot.setMotors(0, 0);

            Console.WriteLine();
            Console.WriteLine("Thank you for watching.");


            DisplayMenuPrompt("Talent Show Menu");

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="finchRobot"></param>

        static void TalentShowDisplayAll(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("All steps");

            Console.WriteLine("\t Now, the Finch robot will be showing off everything all together!");
            Console.WriteLine("Press anything to continue");
            DisplayContinuePrompt();

            //
            //flash user while going in cirlces and displaying sound
            //

            for (int lightSoundLevel = 0; lightSoundLevel < 165; lightSoundLevel++)
            {
                finchRobot.setMotors(150, -150);
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 30);
                finchRobot.noteOff();
                finchRobot.setMotors(0, 0);
            }

            for (int numberofFlashes = 0; numberofFlashes < 7; numberofFlashes++)
            {
                finchRobot.setLED(250, 0, 0);
                finchRobot.setMotors(-200, 200);
                finchRobot.wait(300);
                finchRobot.setLED(0, 0, 250);
                finchRobot.wait(500);
                finchRobot.setMotors(0, 0);
            }
            for (int numberofFlashes = 0; numberofFlashes < 10; numberofFlashes++)
            {
                finchRobot.setLED(0, 250, 0);
                finchRobot.setMotors(-250, 250);
                finchRobot.wait(175);
                finchRobot.setMotors(150, -150);
                finchRobot.setLED(0, 0, 250);
                finchRobot.wait(175);
                finchRobot.setLED(250, 0, 0);
                finchRobot.setMotors(0, 0);
            }

            DisplayMenuPrompt("Talent Show Menu");
            
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
