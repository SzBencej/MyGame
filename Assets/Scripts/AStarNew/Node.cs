using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAStarExample1
{
    /// <summary>
    /// Represents a single node on a grid that is being searched for a path between two points
    /// </summary>
    public class Node
    {

        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// True when the node may be traversed, otherwise false
        /// </summary>
        public bool IsWalkable { get; set; }


        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="y">The node's location along the Y axis</param>
        /// <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
        /// <param name="endLocation">The location of the destination node</param>
        public Node(int x, int y, bool isWalkable, Point endLocation, Point startLocation)
        {
            this.Location = new Point(x, y);
            this.IsWalkable = isWalkable;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Location.X, this.Location.Y);
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        internal static float GetTraversalCost(Point location, Point otherLocation)
        {
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y-location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
