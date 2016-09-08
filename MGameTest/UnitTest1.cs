using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        GameModel m;

        [TestMethod]
        public void TestMethod1()
        {
            m = new GameModel();
            Resource r = new Resource();
            r = m.GetResource();
            Assert.AreEqual(r, m.GetResource());
            m.NextRound();
            r.Add(new Resource(1, 1, 1, 1));
            Assert.AreEqual(r, m.GetResource());
        }

        [TestMethod]
        public void TestMethod3()
        {
            m = new GameModel();
            Resource r = new Resource(6, 5, 5, 5);
            Assert.IsFalse(m.Affordable(r));
            m.NextRound();
            Assert.IsTrue(m.Affordable(r));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Resource r1 = new Resource(1,1,1,1);
            Resource r2 = new Resource(1, 1, 1, 1);
            Resource r3 = new Resource(2, 2, 2, 2);
            r1 = r1 + r2;
            Assert.AreEqual(r1, r3);
            r1 = r1 - r2;
            Assert.AreEqual(r1, r2);
            r3.Decrease(r2);
            Assert.AreEqual(r1, r3);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "The node with id does not exists in IsAvailable call")]
        public void TestMethod4()
        {
            m = new GameModel();
            m.BuildingDepTree = new Graph();
            m.BuildingDepTree.IsAvailable(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "The node with id does not exists in SetAvailable call")]
        public void TestMethod5()
        {
            m = new GameModel();
            m.BuildingDepTree = new Graph();
            m.BuildingDepTree.SetAvailable(1, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "The node with parent does not exists in AddChild call")]
        public void TestMethod6()
        {
            m = new GameModel();
            m.BuildingDepTree = new Graph();
            m.BuildingDepTree.AddChild(2, 2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            m = new GameModel();
            m.BuildingDepTree = new Graph();
            m.BuildingDepTree.AddChild(0, 1);
            m.BuildingDepTree.AddChild(0, 2);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(1));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(2));
            m.BuildingDepTree.AddChild(1, 3);
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(3));
            m.BuildingDepTree.AddChild(2, 3);
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(3));
            m.BuildingDepTree.AddChild(2, 4);
            m.BuildingDepTree.AddChild(3, 5);
            m.BuildingDepTree.AddChild(4, 6);
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(4));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(5));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(6));

            m.BuildingDepTree.SetAvailable(1, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(1));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(3));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(4));
            m.BuildingDepTree.SetAvailable(2, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(2));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(3));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(4));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(5));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(6));
            m.BuildingDepTree.SetAvailable(3, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(3));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(5));
            m.BuildingDepTree.SetAvailable(4, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(4));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(6));

            m.BuildingDepTree.SetAvailable(3, false);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(3));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(5));
            m.BuildingDepTree.SetAvailable(4, false);
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(6));
            m.BuildingDepTree.SetAvailable(4, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(6));
            m.BuildingDepTree.SetAvailable(3, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(5));
            m.BuildingDepTree.SetAvailable(1, false);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(1));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(3));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(5));
            m.BuildingDepTree.SetAvailable(2, false);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(2));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(4));
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(6));

            m.BuildingDepTree.SetAvailable(2, true);
            Assert.IsFalse(m.BuildingDepTree.IsAvailable(3));
            m.BuildingDepTree.SetAvailable(1, true);
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(3));
            Assert.IsTrue(m.BuildingDepTree.IsAvailable(5));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Building count is less than 0")]
        public void TestMethod8()
        {
            m = new GameModel();
            m.BuildingDepTree = new Graph();
            m.BuildingDepTree.AddChild(0, 1);
            m.BuildingDepTree.SetAvailable(1, true);
            m.BuildingDepTree.SetAvailable(1, true);
            m.BuildingDepTree.SetAvailable(1, false);
            m.BuildingDepTree.SetAvailable(1, false);
            m.BuildingDepTree.SetAvailable(1, false);
        }
    }
}
