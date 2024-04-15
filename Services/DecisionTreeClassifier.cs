namespace iMate.Services;

public class Node
{
    public int Label;
    public string Data;
    public List<Node> Children;
    
    public Node(int value, string content)
    {
        Label = value;
        Data = content;
        Children = new List<Node>();
    }
}

public class DecisionTreeClassifier
{
    private readonly Node root;

    public DecisionTreeClassifier(int data, string label)
    {
        root = new Node(data, label);
    }

    public DecisionTreeClassifier WithChild(Node child)
    {
        root.Children.Add(child);
        return this;
    }

    public Node Build()
    {
        return root;
    }
}