// Have Mario collect all the gold in the maze.

/**
 * A maze always starts on a single Crossroad
 * On a Crossroad, you can
 *    - Go Left leading to another Crossroad (or null if it is a dead end)
 *    - Go Right leading to another Crossroad (or null if it is a dead end)
 *    - Pick up gold, if any
 */
export class Crossroad {
  Left: Crossroad;
  Right: Crossroad;
  GoldCoins: number;

  constructor(left: Crossroad = null, right: Crossroad = null) {
    this.Left = left;
    this.Right = right;
  }

  /** Create a Crossroad with # goldCoins */
  static withTreasure(goldCoins: number): Crossroad {
    var road = new Crossroad();
    road.GoldCoins = goldCoins;
    return road;
  }
}


/**
 * The Maze:
 * Assume there is always only one way to reach a given road
 */
export class Maze {
  start: Crossroad;

  /** @param {Crossroad} start - The starting road */
  constructor(start: Crossroad) {
    this.start = start;
  }

  /** Returns the amount of the gold in the maze */
  GetAllGoldInMaze(): number {
    // TODO: Mario doesn't seem to crossing many roads
    // --> Your implementation here!!
    return 0;
  }
}
