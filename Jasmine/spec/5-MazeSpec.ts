import {Maze, Crossroad} from '../5-Maze';

describe('The Maze', () => {
  it('can solve a simple maze', () => {
    const simpleMaze = new Maze(new Crossroad(Crossroad.withTreasure(20), Crossroad.withTreasure(10)));
    const goldFound = simpleMaze.GetAllGoldInMaze();
    expect(goldFound).toBe(30);
  });


  it('can solve a complex maze', () => {
    const hardMaze = new Maze(
      new Crossroad(
        new Crossroad(
          new Crossroad(
            new Crossroad(),
            new Crossroad(
              new Crossroad(
                new Crossroad(
                  new Crossroad(),
                  new Crossroad(Crossroad.withTreasure(40))
                ),
                new Crossroad(null, Crossroad.withTreasure(10))
              )
            )
          ),
          new Crossroad(Crossroad.withTreasure(20))
        )
      )
    );

    const goldFound = hardMaze.GetAllGoldInMaze();
    expect(goldFound).toBe(70);
  });
});
