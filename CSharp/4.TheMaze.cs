using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Itenium.Interview
{
    /// <summary>
    /// A maze always starts on a single Crossroad
    /// On a Crossroad, you can
    ///     - Go Left leading to another Crossroad (or null if it is a dead end)
    ///     - Go Right leading to another Crossroad (or null if it is a dead end)
    ///     - Pick up gold, if any
    /// </summary>
    public class Crossroad
    {
        public Crossroad Left { get; }
        public Crossroad Right { get; }
        public int GoldCoins { get; private set; }

        public Crossroad(Crossroad left = null, Crossroad right = null)
        {
            Left = left;
            Right = right;
        }

        public static Crossroad WithTreasure(int goldCoins)
        {
            return new Crossroad { GoldCoins = goldCoins };
        }
    }

    /// <summary>
    /// The Maze:
    /// Assume there is always only one way to reach a given road
    /// </summary>
    public class Maze
    {
        private readonly Crossroad _start;

        public Maze(Crossroad start)
        {
            _start = start;
        }

        /// <summary>
        /// Returns the amount of the gold in the maze
        /// </summary>
        public int GetAllGoldInMaze()
        {
            // TODO: Mario doesn't seem to crossing many roads
            // --> Your implementation here!!
            return 0;
        }
    }

    /// <summary>
    /// Create mazes for the UnitTests below
    /// </summary>
    internal class MazeFactory
    {
        /// <summary>
        /// Creates a complex maze
        /// </summary>
        public static Maze CreateFullMaze()
        {
            return new Maze(
                new Crossroad(new Crossroad(new Crossroad(new Crossroad(), new Crossroad(
                        new Crossroad(new Crossroad(new Crossroad(),
                            new Crossroad(Crossroad.WithTreasure(50))), new Crossroad()))), new Crossroad(Crossroad.WithTreasure(20)))
                )
            );
        }

        /// <summary>
        /// Creates a 3 road maze:
        /// - one starting road
        ///     - one road to the left with 20 gold
        ///     - one road to the right with 10 gold
        /// </summary>
        public static Maze CreateEasyMaze()
        {
            return new Maze(new Crossroad(Crossroad.WithTreasure(20), Crossroad.WithTreasure(10)));
        }
    }

    /// <summary>
    /// The UnitTests
    /// </summary>
    public class MarioRunner
    {
        [Fact]
        public void EasyMaze()
        {
            var maze = MazeFactory.CreateEasyMaze();
            var totalGold = maze.GetAllGoldInMaze();
            Assert.Equal(30, totalGold);
        }

        [Fact]
        public void FullMaze()
        {
            var maze = MazeFactory.CreateFullMaze();
            var totalGold = maze.GetAllGoldInMaze();
            Assert.Equal(70, totalGold);
        }
    }
}
