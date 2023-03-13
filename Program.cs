using System;
using System.Collections.Generic;
using System.IO;

namespace RandList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ListRand();

            list.AddToHead("1");
            list.AddToHead("Hello");
            list.AddToTail("3");
            list.AddToTail("4444");
            list.AddToHead("5");

            list.GenerateRandom();

        start:

            Console.Write("Save (S), load (L), get all nods (A), get all randoms (R): ");
            string Input = Console.ReadLine();

            switch (Input)
            {
                case "S": list.Save("list.dat"); break;
                case "L": list.Load("list.dat"); break;
                case "A": Console.WriteLine(list.GetAllNodes()); break;
                case "R": Console.WriteLine(list.GetAllRandoms()); break;
                default: break;
            }

            Console.WriteLine();
            goto start;
        }
    }



    // Класс ноды
    public class ListNode
    {
        public string Value { get; set; }

        public ListNode Back { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; }

        public ListNode(string value)
        {
            Value = value;
        }
    }

    // Класс листа
    public class ListRand
    {
        public ListNode Tail { get; private set; }
        public ListNode Head { get; private set; }
        public int Count { get; private set; }


        public void AddToHead(string value)
        {
            ListNode NewNode = new ListNode(value);

            if (Count == 0)
            {
                Tail = NewNode;
                Head = NewNode;
                Count = 1;
                return;
            }

            Head.Back = NewNode;
            NewNode.Next = Head;

            Head = NewNode;

            Count++;
        }
        public void AddToTail(string value)
        {
            ListNode NewNode = new ListNode(value);

            if (Count == 0)
            {
                Tail = NewNode;
                Head = NewNode;
                Count = 1;
                return;
            }

            Tail.Next = NewNode;
            NewNode.Back = Tail;

            Tail = NewNode;

            Count++;
        }

        public ListNode GetNode(int index)
        {
            if (Count == 0 || index >= Count)
                return null;

            ListNode Node = Head;

            for (int n = 0; n < index; n++)
                Node = Node.Next;

            return Node;
        }
        public int GetIndex(ListNode node)
        {
            int Index = 0;

            for (ListNode n = Head; n != null; n = n.Next, Index++)
            {
                if (n == node) return Index;
            }

            return -1;
        }

        public void GenerateRandom()
        {
            Random R = new Random();
            ListNode Node = Head;

            while (Node != null)
            {
                Node.Rand = GetNode(R.Next(Count));
                Node = Node.Next;
            }
        }

        public string GetAllNodes()
        {
            if (Count == 0)
                return null;

            ListNode Node = Head;
            string All = Head.Value;

            while (Node.Next != null)
            {
                Node = Node.Next;
                All += ", " + Node.Value;
            }

            return All;
        }
        public string GetAllRandoms()
        {
            if (Count == 0)
                return null;

            ListNode Node = Head;
            string All = Head.Rand.Value;

            while (Node.Next != null)
            {
                Node = Node.Next;
                All += ", " + Node.Rand.Value;
            }

            return All;
        }


        public void Save(string way)
        {
            if (File.Exists(way))
                File.Delete(way);

            using (BinaryWriter BW = new BinaryWriter(File.Open(way, FileMode.Create)))
            {
                for (ListNode Node = Head; Node != null; Node = Node.Next)
                {
                    BW.Write(Node.Value);
                    BW.Write(GetIndex(Node.Rand));
                }
            }

            Console.WriteLine("Saved as " + way);
        }

        public void Load(string way)
        {
            if (!File.Exists(way))
            {
                Console.WriteLine("File " + way + " not found");
                return;
            }

            Tail = null;
            Head = null;
            Count = 0;

            var ListOfRandoms = new List<int>();

            using (BinaryReader BR = new BinaryReader(File.Open(way, FileMode.Open)))
            {
                while (BR.PeekChar() > -1)
                {
                    AddToTail(BR.ReadString());
                    ListOfRandoms.Add(BR.ReadInt32());
                }
            }

            int n = 0;
            for (ListNode Node = Head; Node != null; Node = Node.Next, n++)
                Node.Rand = GetNode(ListOfRandoms[n]);

            Console.WriteLine("Loaded from " + way);
        }
    }
}