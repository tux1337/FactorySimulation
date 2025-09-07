using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class Log
    {
        public static void write(String toLog, bool noDate)
        {
            write(toLog, null, noDate);
        }
        public static void write(String toLog, int? TeileID, bool noDate = false)
        {
            if (TeileID != null)
            {
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " PartID " + TeileID.ToString() + " " + toLog);
            }
            else if(TeileID == null && noDate)
            {
                Console.WriteLine(toLog);
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " " + toLog);
            }
        }

        public static void writeError(String toLog, int? TeileID)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            write(toLog, TeileID);
            Console.ResetColor();
        }

        public static void linebreak()
        {
            Console.WriteLine("");
        }


        
        public static void printHappyFace()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;
            //ASCII Art from https://emojicombos.com/face

            Console.WriteLine("⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣤⣴⠶⠶⠶⠶⠶⠶⣦⣤⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠀⢀⣠⡶⠟⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠻⢶⣄⡀⠀⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⢀⣴⠟⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣦⡀⠀⠀⠀");
            Console.WriteLine("⠀⠀⢠⡾⣋⣤⣤⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣤⣄⡀⠀⠈⠛⢷⡄⠀⠀");
            Console.WriteLine("⠀⣰⡟⣴⠟⠁⠀⣾⣿⣷⡄⠀⠀⠀⠀⢰⡿⠋⠁⠀⣸⣿⣿⣄⠀⠀⠈⢿⣆⠀");
            Console.WriteLine("⢀⣿⢁⡯⠀⠀⠀⠙⠛⢻⣧⠀⠀⠀⠀⣾⠃⠀⠀⠀⠘⠛⠛⣿⡄⠀⠀⠘⣿⡀");
            Console.WriteLine("⢸⡏⠀⣧⣠⣤⣤⣤⣤⣼⡟⠀⠀⠀⠀⢿⣤⣤⣤⣤⣤⣤⣤⣿⠃⠀⠀⠀⢸⡇");
            Console.WriteLine("⢸⡇⠀⠉⠁⠉⠈⠁⠉⠀⠁⠀⠀⠀⠀⠈⠁⠉⠈⠁⠉⠈⠁⠁⠀⠀⠀⠀⢸⡇");
            Console.WriteLine("⢸⡇⠀⠰⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣶⣆⠀⠀⠀⠀⢸⡇");
            Console.WriteLine("⠈⣿⡀⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢠⣿⠁");
            Console.WriteLine("⠀⠹⣧⠀⠀⠹⣿⣿⣽⣿⣯⣿⣿⣽⣷⣿⣯⣷⣿⣯⣿⣾⣿⣟⠀⠀⠠⣾⠏⠀");
            Console.WriteLine("⠀⠀⠘⢧⡄⠀⠙⣿⣿⣷⣿⢿⣾⠟⠋⠈⠀⠈⠉⠛⢿⣿⡿⠁⠀⢰⡿⠃⠀⠀");
            Console.WriteLine("⠀⠀⠀⠈⠻⣦⣀⠈⠛⢿⣿⣿⠃⠀⠀⠀⠀⢀⣀⣴⠿⠋⠀⣀⣶⠟⠁⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠀⠈⠙⠶⣤⣀⠉⠛⠛⠲⠶⠖⠛⠛⠋⢁⣀⣤⠾⠋⠁⠀⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠳⠶⠶⠶⠶⠶⠶⠞⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀");

            Console.ResetColor();
        }


        public static void printSadFace()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Red;
            //ASCII Art from https://emojicombos.com/face

            Console.WriteLine("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣤⣄⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠀⠀⢀⣠⣴⠞⠛⠉⠉⠉⠈⠈⠉⠉⠛⠷⣦⣄⡀⠀⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⢀⣴⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢻⣷⡄⠀⠀");
            Console.WriteLine("⠀⠀⢀⣴⡟⠁⠀⡀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⡀⠀⢀⠀⡀⠈⠻⣷⣤⠀");
            Console.WriteLine("⠀⢀⣾⠏⢀⠈⢄⣀⣀⡀⠐⠀⠀⠄⠀⠀⠠⠀⠀⣀⣀⣀⡀⠂⠌⢻⣧⠀");
            Console.WriteLine("⠀⣾⡏⡐⣦⠿⠛⣉⡉⠛⢷⣤⠈⠀⠠⢁⢀⣶⠟⠛⣉⡙⠻⢷⣌⠌⢷⠀"); 
            Console.WriteLine("⢰⣿⢂⣽⠋⣴⣿⣿⣿⣷⡄⢹⣧⠈⡐⢠⣿⢃⣴⣿⣿⣿⣷⡈⢿⡜⣸⢠");
            Console.WriteLine("⢸⣿⠤⣿⡘⣿⣿⣿⣿⣿⡟⢠⣿⠀⡐⢸⣧⠸⣿⣿⣿⣿⣿⡇⢸⡗⣼⡆");
            Console.WriteLine("⠘⣿⡆⡹⣧⡙⠻⠿⠿⠛⢁⣼⠃⢄⠡⠂⢻⣆⠙⠿⠿⠿⠋⣠⡿⢱⣸⡇");
            Console.WriteLine("⠀⢹⣷⢡⠎⡛⠷⠶⠶⠞⡛⠡⡈⠤⠑⡌⢠⠘⠻⠶⠶⠶⢞⢫⡐⢧⡿⠁");
            Console.WriteLine("⠀⠀⢻⣷⡜⡰⠣⠜⣠⠓⢄⠣⢘⠠⢃⠌⡂⢍⢢⠱⣈⠎⢆⠦⣹⣿⠃⠀");
            Console.WriteLine("⠀⠀⠀⠹⣿⣜⠥⣋⠴⣈⠆⣿⣷⣿⣶⣿⣾⡆⢎⠴⣡⢚⣬⣾⠿⠁⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠈⠙⠷⣎⡶⣡⠚⣤⢃⠖⡰⢢⠱⡘⣬⢒⣵⡾⠟⠁⠀⠀⠀⠀");
            Console.WriteLine("⠀⠀⠀⠀⠀⠀⠀⠈⠙⠓⠻⢦⣯⣞⣥⣧⡷⠿⠞⠛⠉⠀⠀⠀⠀⠀⠀⠀");

            Console.ResetColor();
        }



    }
}
