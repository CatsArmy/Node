using System.Numerics;

public class Node<T>
{
    private T Value;
    private Node<T> Next;
    public void SetValue(T value)
    {
        this.Value = value;
    }
    public T GetValue()
    {
        return this.Value;
    }
    public void SetNext(Node<T> node)
    {
        this.Next = node;
    }
    public Node<T> GetNext()
    {
        return this.Next;
    }

    public bool HasNext() => this.Next is not null;
    public override string ToString() => $"{this.Value} --> {this.Next}";
    public Node(T value, Node<T> next = null)
    {
        this.Value = value;
        this.Next = next;
    }
    public Node(Node<T> node)
    {
        this.Value = node.Value;
        this.Next = node.Next;
    }
        
    public Node<T>Remove(int i)
    {
        return Remove(Goto(i));
    }
    public Node<T>Remove(Node<T> curr)
    {
        Node<T> first = this;
        if (first != curr)
        {
            Node<T> prev = GetPreviousNode(curr);
            if (prev is null)
            {
                return prev;
            }
            prev.SetNext(curr.Next);
            return this;
        }
        if (!curr.HasNext())
        {
            return null;
        }
        Node<T> next = curr.GetNext();
        this.Value = next.Value;
        this.Next = next.Next;
        return this;
    }

    internal int RemoveAll(Predicate<T> match)
    {
        int i = 0;
        if (match == null)
        {
            return i;
        }
        Node<T> node = this;

        while (!match(node.GetValue()) && node.HasNext())
        {
            node = node.GetNext();
        }

        while (match(node.GetValue()) && node.HasNext())
        {
            if (Remove(node) is null)
            {
                i++;
            }
            else
            {
                node = node.GetNext();
            }
        }
        if (i == 0 || !HasNext())
        {
            return i;
        }
        return i + RemoveAll(match);
    }

    public Node<T> GetPreviousNode(Node<T> current)
    {
        Node<T> first = this;
        for (Node<T> next = first.Next; first is not null; first = next, next = first.Next)
        {
            if (next == current)
            {
                return first;
            }
        }
        return first;
    }

    public Node<char> RemoveAllMatching(Node<char> node, Node<char> compare)
    {
        for (Node<char> i = node; i is not null; i = i.GetNext())
        {
            if (!compare.Contains(i.Value))
            {
                node.RemoveAll(p => p == i.Value);
            }
        }
        return node;
    }
    public Node<T> Goto(int i)
    {
        if (i <= 0 || !this.HasNext())
        {
            return this;
        }
        return this.Next.Goto(--i);
    }
    public Node<T> Goto(Node<T> i)
    {
        if (this == i || !this.HasNext())
        {
            return this;
        }
        return this.Next.Goto(i);
    }
    public Node<T> Goto()
    {
        if (!this.HasNext())
        {
            return this;
        }
        return this.Next.Goto();
    }
    public Node<T> Insert(Node<T> i, T value) 
    {
        Node<T> node = Goto(i);
        node.SetNext(new Node<T>(value));
        return this;
    }
    public Node<T> Insert(int i, T value) 
    {
        Node<T> node = Goto(i);
        node.SetNext(new Node<T>(value));
        return this;
    }
    public Node<T> FirstOrDefualt(T value)
    {
        Node<T> node = this;
        return FirstOrDefualt(predicate: new Predicate<T>(p => new Node<T>(p) == node));
    }
    public Node<T> FirstOrDefualt(Predicate<T> predicate)
    {
        Node<T> n = this;
        if (predicate(n.Value))
        {
            return this;
        }
        if (!this.HasNext())
        {
            return null;
        }
        return this.Next.FirstOrDefualt(predicate);
    }
    enum State
    {
        Null = 'n',
        TypeError= 'e',
        Type = 's'
    }
    public Node<T> Min<T>() where T : IComparisonOperators<T, T, bool>
    {
        State state = State.Type;
        if (this is null)
        {
            state = State.Null;
            Console.WriteLine("Cant have a null Minimum. [Node is null]");
            return null;
        }
        Node<T> node = this as Node<T>;
        if (node is null)
        {
            state = State.TypeError;  
            Console.WriteLine($"Error {this.Value} does not have a comperision operator [Invalid Type]");
            return null;
        }
        return Min<T>(node);
    }
    
    
    private Node<T> Min<T>(Node<T> minNode) where T : IComparisonOperators<T, T, bool>
    {
        Node<T> node = this as Node<T>;
        if (!node.HasNext())
        {
            return minNode;
        }

        if (minNode.Value > node.Value)
        {
            minNode = node;
        }
        return node.Next.Min<T>(minNode);
    }
}


