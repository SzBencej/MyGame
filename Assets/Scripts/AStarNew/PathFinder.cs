using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SimpleAStarExample1
{
    public class PathFinder
    {
        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;
        private SearchParameters searchParameters;
        private int step = 0;
        private int size = 0;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public PathFinder(SearchParameters searchParameters)
        {
            this.searchParameters = searchParameters;
            InitializeNodes(searchParameters.Map);
            this.startNode = this.nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
            this.endNode = this.nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
        public List<Point> FindPath()
        {
            // The start node is the first entry in the 'open' list

            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            List<Point> result = new List<Point>();
            bool success = Search(cameFrom, startNode, result);
            //Debug.Log(success + " " + step);
            result.Reverse();
            return result;
        }

        public int GetStepCount()
        {
            return step;
        }

        public int GetSizeCount()
        {
            return this.size;
        }

        /// <summary>
        /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
        /// </summary>
        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
        private void InitializeNodes(bool[,] map)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
            this.nodes = new Node[this.width, this.height];
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    this.nodes[x, y] = new Node(x, y, map[x, y], this.searchParameters.EndLocation, this.searchParameters.StartLocation);
                }
            }
        }

        /// <summary>
        /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
        /// </summary>
        /// <param name="currentNode">The node from which to find a path</param>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search(Dictionary<Node, Node> cameFrom, Node currentNode, List<Point> result)
        {
            //Debug.Log("start");
            SimplePriorityQueue<Node> open = new SimplePriorityQueue<Node>();
            currentNode.F = Node.GetTraversalCost(currentNode.Location, endNode.Location);
            currentNode.G = 0;
            open.Enqueue(currentNode, currentNode.F);
            currentNode.inOpen = true;

            while (open.Count != 0)
            {
                if (open.Count > this.size)
                {
                    this.size = open.Count;
                }
                step++;
                //Debug.Log(fScore[open.ElementAt(0)] + " " + open.ElementAt(0).Location.X + " " + open.ElementAt(0).Location.Y) ;
                currentNode = open.Dequeue() ; // if it has 0
                // Debug.Log(currentNode.Location);
                ////List < Node > list = open.GetData(); // minheap is bad...
                //list.Sort((node1, node2) => node1.F.CompareTo(node2.F));
                //currentNode = list[0];

                //list.Remove(currentNode);
                currentNode.inOpen = false;
                currentNode.inClosed = true;
                if (currentNode.Equals(endNode))
                {
                   ReconstructPath(cameFrom, currentNode, result);
                    return true;
                }
                
                List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
                int size = nextNodes.Count;
                for(int i = 0; i < size; i++)
                {
                    Node n = nextNodes[i];
                    if (n.inClosed)
                    {
                        continue;
                    }
                    float tentative = currentNode.G + 1;
                    float newF = tentative + Node.GetTraversalCost(endNode.Location, n.Location) + GetSecondaryHeuristic(n.Location);
                    
                    if (!n.inOpen)
                    {

                        //cameFrom[n] = currentNode;
                        //n.G = tentative;
                        //n.F = n.G + newF;
                        open.Enqueue(n, newF);
                        n.inOpen = true;
                    } else if (tentative >= n.G)
                    {
                        continue;
                    }
                    cameFrom[n] = currentNode;
                    n.G = tentative;
                    n.F = newF;
                }
            }
            

            return false;
        }

        private void ReconstructPath(Dictionary<Node, Node> cameFrom, Node currentNode, List<Point> result)
        {
            result.Add(currentNode.Location);
            while(cameFrom.ContainsKey(currentNode))
            {
                currentNode = cameFrom[currentNode];
                result.Add(currentNode.Location);
            }
        }

        /// <summary>
        /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
        /// </summary>
        /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
        /// <returns>A list of next possible nodes in the path</returns>
        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                    continue;

                Node node = this.nodes[x, y];
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

               /* // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
               { 
                    float traversalCost = Node.GetTraversalCost(node.Location, fromNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    */
                   // node.ParentNode = fromNode;
                    walkableNodes.Add(node);
                //}
            }

            return walkableNodes;
        }

        /// <summary>
        /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        /// </summary>
        /// <param name="fromLocation">The location from which to return all adjacent points</param>
        /// <returns>The locations as an IEnumerable of Points</returns>
        private static IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X-1, fromLocation.Y-1),
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X-1, fromLocation.Y+1),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X+1, fromLocation.Y-1),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }
   

        private float GetSecondaryHeuristic(Point loc) { // needed?

                float dx1 = loc.X - endNode.Location.X;
            float dy1 = loc.Y - endNode.Location.Y;
            float dx2 = startNode.Location.X - endNode.Location.X;
                float dy2 = startNode.Location.Y - endNode.Location.Y;
                float cross = Math.Abs(dx1 * dy2 - dx2 * dy1); ;
            return (float)(cross * 0.01);
            }
    }
}
