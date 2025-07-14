using ConsoleApp.Models;
using System;

namespace ConsoleApp.Common
{
    public static class LinkedList
    {
        public static Node newHead;

        public static void Append(ref Node head, int data)
        {
            if (head != null)
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = new Node
                {
                    Data = data
                };
            }
            else
            {
                head = new Node
                {
                    Data = data
                };
            }
        }

        public static void Print(Node head)
        {
            if (head == null) return;

            Node current = head;
            do
            {
                Console.Write("{0} ", current.Data);
                current = current.Next;
            } while (current != null);
        }

        public static void PrintRecursive(Node head)
        {
            if (head == null)
            {
                Console.WriteLine();
                return;
            }

            Console.Write("{0} ", head.Data);
            PrintRecursive(head.Next);
        }

        public static void Reverse(ref Node head)
        {
            if (head == null) return;

            Node prev = null, current = head, next = null;

            while (current.Next != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            current.Next = prev;
            head = current;
        }

        public static void ReverseUsingRecursion(Node head)
        {
            if (head == null) return;

            if (head.Next == null)
            {
                newHead = head;
                return;
            }

            ReverseUsingRecursion(head.Next);
            head.Next.Next = head;
            head.Next = null;

        }

        public static void Test()
        {
            Node head = null;
            Append(ref head, 25);
            Append(ref head, 5);
            Append(ref head, 18);
            Append(ref head, 7);

            Console.WriteLine("Linked list:");
            Print(head);

            Reverse(ref head);

            Console.WriteLine();
            Console.WriteLine("Reversed Linked list:");
            Print(head);

            Console.WriteLine();
            Console.WriteLine("Reverse of Reversed Linked list:");

            ReverseUsingRecursion(head);
            head = newHead;
            PrintRecursive(head);
        }
    }
}
