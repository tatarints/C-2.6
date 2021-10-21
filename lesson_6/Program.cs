//Обход графа в ширину
namespace Graph
{
    class Vertex
    {
        public int Number { get; set; }

        public Vertex(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
    class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Weight { get; set; }

        public Edge(Vertex from, Vertex to, int weight = 1)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"({From}; {To})";
        }
    }
    class Graph
    {
        List<Vertex> Vertexes = new List<Vertex>();
        List<Edge> Edges = new List<Edge>();

        public int VertexCount => Vertexes.Count;
        public int EdgeCount => Edges.Count;

        public void AddVertex(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }

        public void AddEdge(Vertex from, Vertex to)
        {
            var edge = new Edge(from, to);
            Edges.Add(edge);
        }

        public int[,] GetMatrix()
        {
            var matrix = new int[Vertexes.Count, Vertexes.Count];

            foreach (var edge in Edges)
            {
                var row = edge.From.Number - 1;
                var column = edge.To.Number - 1;

                matrix[row, column] = edge.Weight;
            }

            return matrix;
        }

        public List<Vertex> GetVetexLists(Vertex vertex)
        {
            var result = new List<Vertex>();

            foreach (var edge in Edges)
            {
                if (edge.From == vertex)
                {
                    result.Add(edge.To);
                }
            }

            return result;
        }

        public bool Wave(Vertex start, Vertex finish)
        {
            var list = new List<Vertex>
            {
                start
            };

            for (int i = 0; i < list.Count; i++)
            {
                var vertex = list[i];
                foreach (var v in GetVetexLists(vertex))
                {
                    if (!list.Contains(v))
                    {
                        list.Add(v);
                    }
                }
            }

            return list.Contains(finish);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var pr = new Program();

            var graph = new Graph();

            var v1 = new Vertex(1);
            var v2 = new Vertex(2);
            var v3 = new Vertex(3);
            var v4 = new Vertex(4);
            var v5 = new Vertex(5);
           
            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
           
            graph.AddEdge(v1, v2);
            graph.AddEdge(v1, v3);
            graph.AddEdge(v2, v4);
            graph.AddEdge(v2, v5);
            graph.AddEdge(v3, v5);
            graph.AddEdge(v4, v5);
            graph.AddEdge(v5, v1);

            pr.GetMatrix(graph);
            Console.WriteLine();

            pr.GetVertex(graph, v1);
            pr.GetVertex(graph, v2);
            pr.GetVertex(graph, v3);
            pr.GetVertex(graph, v4);
            pr.GetVertex(graph, v5);
            
            Console.WriteLine();
        }

        public void GetVertex(Graph graph, Vertex vertex)
        {
            Console.Write(vertex.Number + ": ");
            foreach (var v in graph.GetVetexLists(vertex))
            {
                Console.Write(v.Number + ", ");
            }
            Console.WriteLine();
        }

        public void GetMatrix(Graph graph)
        {
            int[,] matrix = graph.GetMatrix();
            for (int i = 0; i < graph.VertexCount; i++)
            {
                Console.Write(i + 1);
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    Console.Write(" | " + matrix[i, j] + " | ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(new string('*', 100));
        }
    }
}