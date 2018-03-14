using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandycrushProject
{
    public class DLinked
    {
        public Node head;
        public Node tail;
        public int size;


        public DLinked()
        {
            head = tail = null;
            size = 0;
        }

        public void add(Node newNode)
        {
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                Node temp = tail;
                tail = newNode;
                tail.left = temp;
                temp.right = tail;

            }
            size++;
        }

               
        public void Print()
        {
            Node temp = head;

            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    switch (temp.data.Score)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        //case 5:
                        //    Console.ForegroundColor = ConsoleColor.Blue;
                        //    break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                    }

                    Console.Write(temp.data.Name + " ");

                    temp = temp.right;

                }

                Console.WriteLine();

            }

            Console.ForegroundColor = ConsoleColor.White;
        }
       
    }
}

