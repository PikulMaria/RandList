Реализуйте функции сериализации и десериализации двусвязного списка, заданного следующим образом:
    
    class ListNode
        {
     public ListNode Prev;
            public ListNode Next;
            public ListNode Rand; // произвольный элемент внутри списка
            public string Data;
        }


    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
        }

        public void Deserialize(FileStream s)
        {
        }
    }


![image](https://user-images.githubusercontent.com/79498918/224913032-7516c61d-aaf6-4f84-a2e1-99f04b9ca1b1.png)

пример ListRand, желтые стрелки — ссылки на rand элементы

Алгоритмическая сложность решения должна быть меньше квадратичной.

Нельзя добавлять новые поля в исходные классы ListNode, ListRand
