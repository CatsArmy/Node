public class Node<T>
{
    private T Value;
    
    public void SetValue(T value)
    {
        this.Value = value;
    }
    public T GetValue()
    {
        return this.Value;
    }
    private Node<T> Next;
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
    public bool Remove(Node<T> curr)
    {
        if (curr == this)
        {
            if (!curr.HasNext())
                return false;
            Node<T> next = curr.GetNext();
            this.Value = next.Value;
            this.Next = next.Next;
            return true;
        }
        Node<T> prev = GetPreviousNode(curr);
        if (prev == null)
            return false;
        prev.SetNext(curr.GetNext());
        return true;
    }
    
    internal int RemoveAll(Predicate<T> match)
    {
        if (match == null)
        {
            return 0;
        } 
        int i = 0; 
        Node<T> node = this;
        
        while (!match(node.GetValue()) && node.HasNext())
        {
            node = node.GetNext();
        }

        while (match(node.GetValue()) && node.HasNext())
        {
            if (Remove(node))
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

    private Node<T> GetPreviousNode(Node<T> current)
    {
        Node<T> first = this;
        for (Node<T> next = first.Next; first.HasNext(); first = next,
            next = first.Next)
            if (next == current)
            {
                return first;
            }
        return null;
    }

}