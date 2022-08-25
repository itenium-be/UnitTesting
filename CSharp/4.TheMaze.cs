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
    /// The UnitTests
    /// </summary>
    public class MarioRunner
    {
        [Fact]
        public void Maze_xxx()
        {

        }
    }
}
