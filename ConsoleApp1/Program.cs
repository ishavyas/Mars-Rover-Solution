using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MarsRover
{
    class Program
    {


        /// <summary>
        /// Assumptions:
        /// 1. Rover does not go beyond max X and Y values in the platue. The North-East corner.
        /// 2. Rover does not go below 0 0 as X Y values in the platue. The South-West corner.
        /// 3. Inputs are not faultly.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string boundary_string = string.Empty;
            string rover_start_string = string.Empty;
            string rover_commands = string.Empty;

            Console.WriteLine("Enter the boundary coordinates 'x y' seperated by space.");
            boundary_string = Console.ReadLine();

            List<string> tlist = boundary_string.Split(' ').ToList<string>();
            int max_x = 0, max_y = 0;


            if ( int.TryParse(tlist[0], out max_x) == false || int.TryParse(tlist[1], out max_y) == false )
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }
            if(max_x < 0 || max_y < 0)
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }

            while (true)
            {
                try
                {

                    Console.WriteLine("Enter coordinates and direction the rover is pointing to 'x y d' seperated by space.");
                    rover_start_string = Console.ReadLine();

                    Console.WriteLine("Enter the rover command in single line using L or R or M. Must not have any space in this input.");
                    rover_commands = Console.ReadLine();

                    tlist.Clear();

                    tlist = rover_start_string.Split(' ').ToList<string>();
                    int rover_x = 0, rover_y = 0;
                    char rover_d = tlist[2].ToUpper()[0];
                    rover_x = int.Parse(tlist[0]);
                    rover_y = int.Parse(tlist[1]);

                    string possibleDirections = "NSEW";


                    // Making sure that start position of rover is not invalid
                    if (rover_x > max_x || rover_y > max_y || rover_x < 0 || rover_y < 0 || !possibleDirections.Contains(rover_d)  ) {
                        throw new Exception("Invalid input");
                    }

                    Console.WriteLine("Here are the new coordinates of the rover " + getNewRoverPosition(max_x, max_y, rover_x, rover_y, rover_d, rover_commands) + "\n\n");

                    Console.WriteLine("Wish to continue with other rover co-ordiantes? Y/N");

                    if (Console.ReadLine().ToLower() != "y")
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    // In case, the user enters invalid input the program will not terminate.
                    Console.WriteLine("Oops!! Either one of the inputs was invalid or some error occured. Please try again.\n\n");
                    continue;
                }
            }
        }


        static string getNewRoverPosition(int max_x, int max_y, int rover_x, int rover_y, char rover_d, string commands)
        {

            for (int i = 0; i < commands.Length; i++)
            {

                if (commands[i] == 'M')
                {
                    if (rover_d == 'N')
                    {
                        rover_y++;
                    }
                    else if (rover_d == 'S')
                    {
                        rover_y--;
                    }
                    else if (rover_d == 'E')
                    {
                        rover_x++;
                    }
                    else if (rover_d == 'W')
                    {
                        rover_x--;
                    }
                }

                else if (commands[i] == 'L')
                {
                    if (rover_d == 'N')
                    {
                        rover_d = 'W';
                    }
                    else if (rover_d == 'S')
                    {
                        rover_d = 'E';
                    }
                    else if (rover_d == 'E')
                    {
                        rover_d = 'N';
                    }
                    else if (rover_d == 'W')
                    {
                        rover_d = 'S';
                    }
                }

                else if (commands[i] == 'R')
                {
                    if (rover_d == 'N')
                    {
                        rover_d = 'E';
                    }
                    else if (rover_d == 'S')
                    {
                        rover_d = 'W';
                    }
                    else if (rover_d == 'E')
                    {
                        rover_d = 'S';
                    }
                    else if (rover_d == 'W')
                    {
                        rover_d = 'N';
                    }
                }


                if (rover_x < 0)
                {
                    rover_x = 0;
                }
                if (rover_y < 0)
                {
                    rover_y = 0;
                }

                if (rover_x > max_x)
                {
                    rover_x = max_x;
                }
                if (rover_y > max_y)
                {
                    rover_y = max_y;
                }

            }
            return rover_x + " " + rover_y + " " + rover_d;
        }

    }
}
