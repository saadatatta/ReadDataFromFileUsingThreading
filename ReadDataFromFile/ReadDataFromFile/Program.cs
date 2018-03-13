using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadDataFromFile
{
    class Program
    {
        private static StreamReader text;

        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            //Get to the root folder of text file.
            string filePath = currentDirectory.Remove(currentDirectory.Length - 26) + "Test File.txt";

            // If file not present return
            if (!File.Exists(filePath))
                return;

            text = new StreamReader(filePath);

            // Buffer of 64 Bytes
            char[] buffer1 = new char[64];
            char[] buffer2 = new char[64];

            int counterIndex = 0;

            Thread thread2;
            using (text)
            {
                int peekValue;
                while ((peekValue = text.Peek())>0)
                {
                    // When program first load fill the first buffer.
                    if (counterIndex == 0)
                    {
                        FillBuffer(ref buffer1, counterIndex);
                        counterIndex++;

                    }
                    else // Buffer 1 is filled
                    {
                        thread2 = new Thread(() => { });

                        if (counterIndex % 2 != 0)
                        {
                            FillBuffer(ref buffer2, 0);

                            if (thread2.ThreadState == ThreadState.Stopped || thread2.ThreadState == ThreadState.Unstarted)
                            {
                                thread2 = new Thread(() => PrintData(ref buffer1));
                                thread2.Start();
                            }
                            
                        }
                        else
                        {
                             //FillBuffer(ref buffer1, (counterIndex * buffer1.Length));
                             FillBuffer(ref buffer1, 0);

                            if (thread2.ThreadState == ThreadState.Stopped || thread2.ThreadState == ThreadState.Unstarted)
                            {
                                thread2 = new Thread(() => PrintData(ref buffer2));
                                thread2.Start();
                            }
                        }
                        counterIndex++;

                    }
                }
            }
        }

        public static void FillBuffer(ref char[] buffer,int index)
        {
            lock (buffer)
            {
                text.Read(buffer, index, buffer.Length);
            }
        }

        public static void PrintData(ref char[] buffer)
        {
            lock (buffer)
            {
                foreach (char character in buffer)
                {
                    Console.Write(character);
                }
            }
        }
    }
}
