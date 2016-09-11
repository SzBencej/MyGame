using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAStarExample1;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    { 
         private TestContext testContextInstance;
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
        get
        {
            return testContextInstance;
        }
        set
        {
            testContextInstance = value;
        }
    }
    
        [TestMethod]
        public void TestMethod1()
        {
            int num = 2000;
            bool[,] grid = new bool[num,num];
            for(int i = 0; i<num; i++)
            {
                for(int j = 0; j < num; j++)
                {
                    grid[j, i] = true;
                }
            }
            Point start = new Point(0, 0);
            Point end = new Point(1000, 1000);

            SearchParameters searchParameters = new SearchParameters(start, end, grid);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            Debug.WriteLine(path.ToString());
            TestContext.WriteLine(path.ToString());
            Assert.AreEqual(1001, path.Count);
            //Assert.AreEqual(0, pathFinder.GetStepCount());
            Assert.AreEqual(0, pathFinder.GetSizeCount());
        }

        [TestMethod]
        public void MinHeap()
        {
            MinHeap<int> heap = new MinHeap<int>();
            heap.Insert(1);
            Assert.AreEqual(1, heap.Peek());
            Assert.AreEqual(1, heap.ExtractMin());
            Assert.AreEqual(0, heap.Count);
            heap.Insert(2);
            heap.Insert(6);
            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);

            Assert.AreEqual(2, heap.Peek());
            Assert.AreEqual(2, heap.ExtractMin());


            Assert.AreEqual(2, heap.Peek());
            Assert.AreEqual(2, heap.ExtractMin());

            heap.Insert(5);
            heap.Insert(1);
            heap.Insert(8);


            Assert.AreEqual(1, heap.Peek());
            Assert.AreEqual(1, heap.ExtractMin());
        }
    }
}
