using System.Collections;
using System.Collections.Generic;

public class Graph<T>
{

     protected class Node{
        List<Edge> connections;
        bool visited;

        T data;
        Node prevNode;
        double costToPrevNode;

        public Node(T t)
        {
            this.data = t;
            connections = new List<Edge>();
            visited = false;
            costToPrevNode = 0;
        }

        public T getData()
        {
            return data;
        }

        public bool connectNode (T data)
        {
            return connectNode(new Node(data));
        }

        public bool connectNode(Node destNode)
        {
            return connectNode(destNode, 0);
        }

        public bool connectNode(Node destNode, double weight)
        {
            if (this.Equals(destNode))
                return false;

            foreach(Edge edge in connections)
            {
                if (edge.getDestNode().getData().Equals(destNode.getData()))
                {
                    return false;
                }
            }

            connections.Add(new Edge(destNode, weight));

            return true;
        }

        bool equals (Node other)
        {
            return data.Equals(other.getData());
        }

        // breadth first traversal
        public void displayNeighboursBFT()
        {

        }

        // depth first traversal
        void displayNeighboursDFT()
        {

        }
    }

    protected class Edge
    {
        Node destNode;
        double weight;

        public Edge(Node targetNode, double weight)
        {
            this.destNode = targetNode;
            this.weight = weight;
        }

        public Node getDestNode()
        {
            return destNode;
        }

        public double getWeight()
        {
            return weight;
        }
    }

    List<T> graph;

    public Graph()
    {
        graph = new List<T>();

    }

    public void displayGraph()
    {

    }


}
