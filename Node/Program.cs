/*
int i = 5;
Node<int> node = i.Create(true);
Node<int> idx3 = node.GetNext().GetNext();
Node<int> second = node.GetPreviousNode(idx3);
Console.WriteLine((second == node.GetNext()));
*/
Node<double> n3 = new Node<double>(10);
Node<double> n2 = new Node<double>(0.5, n3);
Node<double> n1 = new Node<double>(0.3, n2);

Console.WriteLine(n1.Min<double>());
