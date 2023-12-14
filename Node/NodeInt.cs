public static class NodeInt
{
    public static string Print(this Node<int> node)
    {
        string str = $"[{node}]";
        Console.WriteLine(str);
        return str;
    }

    public static Node<int> Create(int i, bool IsDescending)
    {
        Node<int> node;
        if (IsDescending)
        {
            if (i <= 0)
            {
                return null;
            }
            node = new Node<int>(i);
            node.Create(i);
            return node;
        }
        node = new Node<int>(0);
        node.Create(0, i);
        return node;
    }
    private static Node<int> Create(this Node<int> node, int i)
    {
        //Node<int> node = new Node<int>(i);
        if (i < 0)
        {
            return node;
        }
        node.SetNext(new Node<int>(--i));
        return Create(node.GetNext(), --i);
        for (Node<int> point = node; i >= 0; i--, point = point.GetNext())
        {
            point.SetNext(new Node<int>(--i));
        }
        return node;
    }
    private static Node<int> Create(this Node<int> node, int i, int length)
    {
        //Node<int> node = new Node<int>(i);
        if (i >= length)
        {
            return node;
        }
        node.SetNext(new Node<int>(++i));
        return Create(node.GetNext(), ++i, length);
        for (Node<int> point = node; i < length; i++, point = point.GetNext())
        {
            point.SetNext(new Node<int>(++i));
        }
        return node;
    }
   
    public static Node<int> BuildRandom(int length, int min, int max)
    {
        if (min > max)
        {
            return BuildRandom(length, max, min);
        }
        Random random = new Random();
        Node<int> node = new Node<int>(random.Next(min, max));
        return node.BuildRandom(1, length, min, max, random);
    }
    private static Node<int> BuildRandom(this Node<int> node, int i, int length, int min, int max, Random random)
    {
        if (i >= length)
        {
            return node;
        }
        node.SetNext(new Node<int>(random.Next(min, max)));
        return BuildRandom(node.GetNext(), ++i, length, min, max, random);
    }
    
    public static Node<int> Create()
    {
        Node<int> node;
        Console.WriteLine("Enter any interger you wish");
        string input = Console.ReadLine().ToLower();
        Console.Clear();
        if (!int.TryParse(input, out int value))
        {
            return Create();
        }
        node = new Node<int>(value);
        node.Create();
        return node;
    }
    private static Node<int> Create(this Node<int> node)
    {
        Console.WriteLine("Enter any interger you wish\nEnter \"Stop\" or 'S' to exit");
        string input = Console.ReadLine().ToLower();
        Console.Clear();
        if (!int.TryParse(input, out int value))
        {
            if (input.Contains("stop") || input.Contains('s'))
            {
                return node;
            }
            return Create(node.GetNext());
        }
        node.SetNext(new Node<int>(value));
        return Create(node);
    }
    
    public static int Count(this Node<int> node)
    {
        if (node is null)
        {
            return 0;
        }
        return Count(node, 0);
        int i = 0;
        for (Node<int> point = node; point.HasNext(); point = point.GetNext())
        {
            i++;
        }
        return i;
    }
    private static int Count(Node<int> node, int i)
    {
        if (node is null)
        {
            return i;
        }
        return Count(node.GetNext(), ++i);
    }
    
    public static int Sum(this Node<int> node)
    {
        if (node is null)
        {
            return 0;
        }
        return Sum(node, 0);
    }
    private static int Sum(Node<int> node, int sum)
    {
        if (node is null)
        {
            return sum;
        }
        return Sum(node.GetNext(), sum + node.GetValue());

    }
   
    public static double Average(this Node<int> node)
    {
        if (node is null)
        {
            return 0;
        }
        return node.Average(0, 0);
    }
    private static double Average(this Node<int> node, int sum, int i)
    {
        if (node is null)
        {
            return sum / i;
        }
        return Average(node, sum + node.GetValue(), ++i);
    }
    
    public static int Product(this Node<int> node)
    {
        if (node is null)
        {
            return 0;
        }
        return node.Product(1 * node.GetValue());
    }
    private static int Product(this Node<int> node, int product)
    {
        if (node is null)
        {
            return product;
        }
        return Product(node, product * node.GetValue());
    }

    public static int Max(this Node<int> node)
    {
        if (node is null)
        {
            return 0;
        }
        return node.Max(node.GetValue());
    }
    private static int Max(this Node<int> node, int max)
    {
        if (node is null)
        {
            return 0;
        }
        int value = node.GetValue();
        if (max < value)
        {
            return Max(node, value);
        }
        return Max(node, max);
    }

    public static int CountValue(this Node<int> node, int value)
    {
        if (node is null)
        {
            return 0;
        }
        int result = CountValue(node.GetNext(), value);
        if (value == node.GetValue())
        {
            return ++result;
        }
        return result;
    }

    public static bool IsUp(this Node<int> node)
    {
        if (node is null)
        {
            return false;
        }
        return node._IsUp();
    }
    private static bool _IsUp(this Node<int> node)
    {
        if (!node.HasNext())
        {
            return true;
        }
        
        Node<int> next = node.GetNext();
        if (node.GetValue() > next.GetValue())
        {
            return false;
        }
        return _IsUp(next);
    }

    public static bool Contains(this Node<int> node, int value)
    {
        if (node is null)
        {
            return false;
        }
        if (node.GetValue() == value)
        {
            return true;
        }
        return Contains(node, value);
    }

    public static bool IsArithmetic(this Node<int> node)
    {
        if (node is null)
        {
            return true;
        }
        if (!node.HasNext())
        {
            return true;
        }
        return Arithmetic(node);
    }
    private static bool Arithmetic(Node<int> node)
    {
        if (!node.HasNext())
        {
            return true;
        }
        Node<int> next = node.GetNext();
        if (!next.HasNext())
        {
            return true;
        }
        int d = node.GetValue() - next.GetValue();
        int diff = next.GetValue() - next.GetNext().GetValue();
        if (d == diff)
        {
            return Arithmetic(next);
        }
        return false;
    }
    
}
