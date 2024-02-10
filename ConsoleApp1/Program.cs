using System;
using System.Collections.Generic;

class DijkstraAlgorithm
{
    private int V;
    private List<Tuple<int, int>>[] graph;

    public DijkstraAlgorithm(int vertices)
    {
        V = vertices;
        graph = new List<Tuple<int, int>>[V];
        for (int i = 0; i < V; i++)
        {
            graph[i] = new List<Tuple<int, int>>();
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        graph[source].Add(new Tuple<int, int>(destination, weight));
        graph[destination].Add(new Tuple<int, int>(source, weight)); // For undirected graph
    }

    public void DijkstraShortestPath(int source)
    {
        int[] distance = new int[V];
        bool[] visited = new bool[V];

        for (int i = 0; i < V; i++)
        {
            distance[i] = int.MaxValue;
            visited[i] = false;
        }

        distance[source] = 0;

        for (int count = 0; count < V - 1; count++)
        {
            int u = MinDistance(distance, visited);
            visited[u] = true;

            foreach (var neighbor in graph[u])
            {
                int v = neighbor.Item1;
                int weight = neighbor.Item2;
                if (!visited[v] && distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                {
                    distance[v] = distance[u] + weight;
                }
            }
        }

        PrintSolution(distance);
    }

    private int MinDistance(int[] distance, bool[] visited)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < V; v++)
        {
            if (visited[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private void PrintSolution(int[] distance)
    {
        Console.WriteLine("Destination \t\t Distance from Source");
        for (int i = 0; i < V; i++)
        {
            Console.WriteLine($"{i}\t\t\t{distance[i]}");
        }
    }

    static void Main(string[] args)
    {
        int V = 6; // Number of vertices (cities)
        DijkstraAlgorithm graph = new DijkstraAlgorithm(V);

        // Adding edges between cities
        graph.AddEdge(0, 1, 450);  // Ankara - Istanbul
        graph.AddEdge(0, 2, 500);  // Ankara - Izmir
        graph.AddEdge(1, 2, 700);  // Istanbul - Izmir
        graph.AddEdge(1, 3, 800);  // Istanbul - Antalya
        graph.AddEdge(2, 4, 550);  // Izmir - Adana
        graph.AddEdge(3, 4, 650);  // Antalya - Adana
        graph.AddEdge(1, 5, 250);  // Istanbul - Bursa

        // Finding the shortest path from Ankara (0) to other cities
        graph.DijkstraShortestPath(0);
    }
}
