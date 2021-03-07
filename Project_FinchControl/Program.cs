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
    // Author: McKillip, Zavier
    // Dated Created: 2/16/2021
    // Last Modified: 2/27/2020
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
                        DataRecoderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);
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

        #region ALARM SYSTEM

        /// <summary>
        /// *****************************************************************
        /// *                     Alarm System Menu                          *
        /// *****************************************************************
        /// </summary>
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType= "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;


            do
            {
                DisplayScreenHeader("Alarm System Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Thresholdvalue");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //

                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmSystemDisplaySetSensors();
                        break;

                    case "b":
                        rangeType = AlarmSystemDisplayRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = AlarmSystemDisplayThresholdValue(sensorsToMonitor, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = AlarmSystemDisplayTimeToMonitor();
                        break;

                    case "e":
                        AlarmSystemDisplaySetAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
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

        /// <summary>
        /// Set Alarm
        /// </summary>
        /// <param name="finchRobot"></param>
        /// <param name="sensorsToMonitor"></param>
        /// <param name="rangeType"></param>
        /// <param name="minMaxThresholdValue"></param>
        /// <param name="timeToMonitor"></param>
        static void AlarmSystemDisplaySetAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            bool thresholdExceeded = false;
            int secondsElapsed = 1;
            int leftLightSensorValue;
            int rightLightSensorValue;


            DisplayScreenHeader("Set Alarm");

            //
            //Echo valuse to user
            //
            Console.WriteLine("\tStart");

            //prompt user to start
            Console.ReadKey();

            //
            // test for alarm
            //

            do
            {
                //
                // get and display current light levels
                // 

                leftLightSensorValue = finchRobot.getLeftLightSensor();
                rightLightSensorValue = finchRobot.getRightLightSensor();

                //
                //display current light levels
                //
                switch (sensorsToMonitor)
                {
                    case "left":
                        Console.WriteLine($"\tCurrent Left Light Sensor: {leftLightSensorValue}");
                        break;

                    case "right":
                        Console.WriteLine($"\tCurrent Right Light Sensor: {rightLightSensorValue}");
                        break;

                    case "both":
                        Console.WriteLine($"\tCurrent Left Light Sensor: {leftLightSensorValue}");
                        Console.WriteLine($"\tCurrent Right Light Sensor: {rightLightSensorValue}");
                        break;

                    default:
                        Console.WriteLine("\tUnknown Sensor Reference");
                        break;
                }

                //
                // have robot wait 1 second and increment seconds
                //

                finchRobot.wait(1000);
                secondsElapsed++;

                //
                //test for threshold exceeded
                //

                switch (sensorsToMonitor)
                {
                    case "left":
                        if (rangeType == "minimum")
                        {
                           thresholdExceeded = (leftLightSensorValue < minMaxThresholdValue);
                        }
                        else // maximum
                        {
                            thresholdExceeded = (leftLightSensorValue > minMaxThresholdValue);
                        }
                        break;

                    case "right":
                        if (rangeType == "minimum")
                        {
                            if (rightLightSensorValue < minMaxThresholdValue)
                            {
                                thresholdExceeded = true;
                            }
                        }
                        else //maximum
                        {
                            if (rightLightSensorValue > minMaxThresholdValue)
                            {
                                thresholdExceeded = true;
                            }
                        }
                        break;

                    case "both":
                        if (rangeType == "minimum")
                        {
                            if ((leftLightSensorValue < minMaxThresholdValue) || (rightLightSensorValue < minMaxThresholdValue))
                            {
                                thresholdExceeded = true;
                            }
                        }
                        else
                        {
                            if ((leftLightSensorValue > minMaxThresholdValue) || (rightLightSensorValue > minMaxThresholdValue))
                            {
                                thresholdExceeded = true;
                            }
                        }
                        
                        break;

                    default:
                        Console.WriteLine("\tUnknown Sensor Reference");
                        break;
                }

            } while (!thresholdExceeded && (secondsElapsed <= timeToMonitor));

            //
            //display result of alarm
            //

            if (thresholdExceeded)
            {
                Console.WriteLine("Threshold Exceeded");
                finchRobot.noteOn(860);
                finchRobot.wait(500);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("Threshold Not Exceeded - Time Limit Exceeded");
                finchRobot.noteOn(460);
                finchRobot.wait(1000);
                finchRobot.noteOff();
            }


            DisplayMenuPrompt("Alarm System");
        }

        /// <summary>
        /// Get Overall Time For Readings
        /// </summary>
        /// <returns>TimeToMonitor</returns>
        static int AlarmSystemDisplayTimeToMonitor()
        {
            int timeToMonitor = 0;
            bool validResponse;
            string userResponse;
            
            DisplayScreenHeader("Time to Monitor");

            //
            //loop and validate them
            //

            do
            {            
                Console.Write("\tEnter Time to Monitor:");
                userResponse = Console.ReadLine();

                //timeToMonitor = int.Parse(Console.ReadLine());
                validResponse = int.TryParse(userResponse, out timeToMonitor);
                if (!validResponse)
                {
                    Console.WriteLine("Please enter a number, [1,4,74]");
                }

            } while (!validResponse);

            Console.WriteLine();
            Console.WriteLine($"\tYou chose {timeToMonitor} as the time to moniter.");

            DisplayMenuPrompt("Alarm System");

            return timeToMonitor;
        }

        /// <summary>
        /// Gets Threshold Value
        /// </summary>
        /// <param name="sensorsToMonitor"></param>
        /// <param name="finchRobot"></param>
        /// <returns>ThresholdValue</returns>
        static int AlarmSystemDisplayThresholdValue(string sensorsToMonitor, Finch finchRobot)
        {
            int thresholdValue = 0;

            int currentLeftSensorValue = finchRobot.getLeftLightSensor();
            int currentRightSensorValue = finchRobot.getRightLightSensor();

            DisplayScreenHeader("Threshold Value");

            //
            //display ambient values
            //
            switch (sensorsToMonitor)
            {
                case "left":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    break;

                case "right":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");

                    break;

                case "both":
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentLeftSensorValue}");
                    Console.WriteLine($"Current {sensorsToMonitor} Sensor Value: {currentRightSensorValue}");

                    break;

                default:
                    Console.WriteLine("\tUnknown Sensor Reference");
                    break;
            }

            //
            // get threshold from user
            //
            string userResponse;
            bool validResponse;
            //
            //Validate dont just do int.Parse
            //

            do
            {
                Console.Write("Enter Threshold Value:");
                userResponse = Console.ReadLine();

                validResponse = int.TryParse(userResponse, out thresholdValue);
                //thresholdValue = int.Parse(Console.ReadLine());
                if (!validResponse)
                {
                    Console.WriteLine("Please enter a number [1,6,32].");
                }
            } while (!validResponse);

            DisplayMenuPrompt("Alarm System");
            return thresholdValue;

        }

        /// <summary>
        /// Decides For Max or Min range type
        /// </summary>
        /// <returns>rangeType</returns>
        static string AlarmSystemDisplayRangeType()
        {
            string rangeType = "";
            bool validResponse = false;

            DisplayScreenHeader("Range Type");

            //
            //Validate Number
            //
            do
            {

                Console.Write("Enter Range Type [minimum, maximum]:");
                rangeType = Console.ReadLine().ToLower();

                if (rangeType != "minimum" && rangeType != "maximum")
                {
                    Console.WriteLine("It appears you didn't give a valid response.");
                }
                else 
                {
                    validResponse = true;
                }
            } while (!validResponse);

            DisplayMenuPrompt("Alarm System");

            return rangeType;
        }

        /// <summary>
        /// Get Desired Sensor
        /// </summary>
        /// <returns>sensorsToMonitor</returns>
        static string AlarmSystemDisplaySetSensors()
        {
            string sensorsToMonitor;
            bool validResposne = false;
            

            DisplayScreenHeader("Sensors to Monitor");

            //
            //Validate number
            //
            do
            {

                Console.Write("Enter Sensors to Monitor [left, right, both]:");
                sensorsToMonitor = Console.ReadLine();
                if (sensorsToMonitor != "left" && sensorsToMonitor != "right" && sensorsToMonitor != "both")
                {
                    Console.WriteLine("It appears you didn't give a valid response.");
                }
                else if (sensorsToMonitor == "left" || sensorsToMonitor == "right" || sensorsToMonitor == "both")
                {
                    validResposne = true;

                }
            } while (!validResposne);

            DisplayMenuPrompt("Alarm System");

            return sensorsToMonitor;

        }


        #endregion
        #region DATA Recorder

        /// <summary>
        /// *****************************************************************
        /// *                     Data Recorder Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DataRecoderDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            int numberofDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //

                switch (menuChoice)
                {
                    case "a":
                        numberofDataPoints = DataRecoderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberofDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
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

        /// <summary>
        /// Display Data Table
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayDataTable(double[] temperatures)
        {
            DisplayScreenHeader("Temperatures in F*");


            //
            // display table of temperatures
            //

            Console.WriteLine();
            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Temperature".PadLeft(15)
                );
            Console.WriteLine(
               "--------".PadLeft(20) +
               "-----------".PadLeft(15)
               );

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                (index + 1).ToString().PadLeft(20) +
                (temperatures[index]).ToString("n1").PadLeft(15)
                );
            }


        }
        /// <summary>
        /// Display temp table
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayGetData(double[] temperatures)
        {
            DisplayScreenHeader("Tempatures");

            DataRecorderDisplayDataTable(temperatures);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get temperatures from robot
        /// </summary>
        /// <param name="numberofDataPoints">number of data points</param>
        /// <param name="dataPointFrequency">data point frequency</param>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>temperatures</returns>
        static double[] DataRecorderDisplayGetData(int numberofDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberofDataPoints];
            int dataPointFrequencyMs;

            //
            // convert the frequency in seconds to ms
            //

            dataPointFrequencyMs = (int)(dataPointFrequency * 1000);

            //
            //dataPointFrequencyMs = Convert.ToInt32(dataPointFrequency * 1000)
            //

            DisplayScreenHeader("Tempatures");

            //
            // echo values
            //

            Console.WriteLine($"\tThe Finch robot will now record {numberofDataPoints} temperatures {dataPointFrequency} seconds apart.");
            Console.WriteLine("\tPress any key to begin.");

            DisplayContinuePrompt();

            for (int index = 0; index < numberofDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature() * (9 / 5) + 32;

                //
                //converted to F*
                //

                //
                //echo new temp
                //
                Console.WriteLine($"\tTemperature{index + 1}: {temperatures[index]:n1}");

                finchRobot.wait(dataPointFrequencyMs);
            }

            //
            // display table of temperatures
            //
            Console.WriteLine();
            Console.WriteLine(
                "Reading #".PadLeft(20) +
                "Temperature F*".PadLeft(15)
                );
            Console.WriteLine(
               "---------".PadLeft(20) +
               "-----------".PadLeft(15)
               );

            for (int index = 0; index < numberofDataPoints; index++)
            {
                Console.WriteLine(
                (index + 1).ToString().PadLeft(20) +
                (temperatures[index]).ToString("n1").PadLeft(15)
                );
            }

            DisplayMenuPrompt("Data Recorder");

            return temperatures;
        }

        /// <summary>
        /// get data points frequency from user
        /// </summary>
        /// <returns>data point frequency</returns>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            bool vaildResponse;
            string userResponse;
            
            DisplayScreenHeader("Data Point Frequency For Readings:");

            //
            //validate the number
            //

            do
            {

                Console.Write("\tData Point Frequency:");
                userResponse = Console.ReadLine();

                vaildResponse = double.TryParse(userResponse, out dataPointFrequency);
                if (!vaildResponse)
                {
                    Console.WriteLine("Please enter a number, [1,4,74]");
                }

            } while (!vaildResponse);

            //
            // echo response
            //

            Console.WriteLine();
            Console.WriteLine($"\tYou chose {dataPointFrequency} as the data point frequency");

            DisplayMenuPrompt("Data Recorder");

            return dataPointFrequency;
        }

        /// <summary>
        /// get number of data points from user
        /// </summary>
        /// <returns>number of data points</returns>
        static int DataRecoderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            bool validResponse;
            string userResponse;

            DisplayScreenHeader("Number of Data Points For Temps Ran");

            //
            //validate the number
            //
            do
            {
                Console.Write("Number of Data Points:");
                userResponse = Console.ReadLine();          

                validResponse = int.TryParse(userResponse, out numberOfDataPoints);
                if (!validResponse)
                {
                    Console.WriteLine("Enter a number, [1,4,68].");
                }

            } while (!validResponse);

            Console.WriteLine();
            Console.WriteLine($"\tYou chose {numberOfDataPoints} as the number of data points");

            DisplayMenuPrompt("Data Recorder");

            return numberOfDataPoints;
        }

        #endregion
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

        /// <summary>
        /// ****************************************************
        /// *                 Talent Show > Sing A Song        *
        /// **************************************************** 
        /// </summary>
        /// <param name="finchRobot"></param>
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
        /// ****************************************************
        /// *                 Talent Show > Movement           *
        /// ****************************************************
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
        /// ****************************************************
        /// *                 Talent Show > All Actions           *
        /// ****************************************************
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

            if (robotConnected)
            {
                Console.WriteLine();
                Console.WriteLine("\t\tRobot now connect.");
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(12000);
                finchRobot.wait(1000);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unable to connect");
            }

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
