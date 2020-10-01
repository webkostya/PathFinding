## How to use

```C#
// create the tiles map
var tilemap = new float[width, height];
// set values here....
// every float in the array represent the cost of passing the tile at that position.
// use 0.0f for blocking tiles.

// create a grid
var grid = new Path.Grid(tilemap);

// create source and target points
var from = new Path.Point(1, 1);
var to = new Path.Point(10, 10);

// get path
// path will either be a list of Points (x, y), or an empty list if no path is found.
var path = Path.Behavior.Find(grid, from, to);

// for Long distance
var path = Path.Behavior.Find(grid, from, to, Path.Behavior.DistanceType.Long);

```

If you don't care about price of tiles (eg tiles can only be walkable or blocking), you can also pass a 2d array of *booleans* when creating the grid:
```C#
// create the tiles map
var tilemap = new bool[width, height];
// set values here....
// true = walkable, false = blocking

// create a grid
var grid = new Path.Grid(tilemap);

// rest is the same..
```

After creating the grid with a tilemap, you can update the grid using:
```C#
// create a grid
var grid = new Path.Grid(tilemap);

// change the tilemap here

// update later
grid.UpdateGrid(tilemap);
```