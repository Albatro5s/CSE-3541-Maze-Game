using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallState
{
    //Based on David Maug's research on representing wall states with bits
    //(i.e. 0000 = no walls, 1111 = all walls, 1010 = wall up and down, 0110 = wall right and down)

    UP = 8,     // 1000
    RIGHT = 4,  // 0100
    DOWN = 2,   // 0010
    LEFT = 1,   // 0001

    EXPLORED = 32,

    // Functions:

    // wallState = WallState.LEFT | WallState.RIGHT;      Generate walls
    // wallState |= WallState.UP;                         Add walls
    // wallState &= ~WallState.RIGHT;                     Remove walls
}

public struct Position  //node position
{
    public int X;
    public int Y;
}

public struct Neighbor  //neighboring nodes
{
    public Position Position;
    public WallState SameWall;
}

namespace RomanTristan.Lab3
{
    public static class MazeGenerator
    {
        // Maze generator algorithm #1 - Recursive Backtracking

        // Description: A random starting cell is chosen, and marked as explored. Then repeat the following steps:
        // 1. If there are unvisited neighbors, choose a random one and remove the wall between them. Mark this new cell as explored.
        // 2. If all neighbors have been explored, back up until finding a cell that has unexplored neighbors.
        private static WallState[,] RecursiveBacktracker(WallState[,] maze, int width, int height)
        {
            var randNum = new System.Random();
            var position = new Position { X = randNum.Next(0, width), Y = randNum.Next(0, height) };
            var positionStack = new Stack<Position>();

            maze[position.X, position.Y] |= WallState.EXPLORED;
            positionStack.Push(position);

            while (positionStack.Count > 0)
            {
                var currentCell = positionStack.Pop();
                var neighbors = UnexploredNeighbors(maze, currentCell, width, height);

                if (neighbors.Count > 0) //if node has unexplored neighbors, randomly choose one and remove wall between the nodes, then travel to that node
                {
                    positionStack.Push(currentCell);

                    var randIndex = randNum.Next(0, neighbors.Count);
                    var randomNeighbor = neighbors[randIndex];
                    var neighborPosition = randomNeighbor.Position;

                    maze[currentCell.X, currentCell.Y] &= ~randomNeighbor.SameWall;

                    maze[neighborPosition.X, neighborPosition.Y] &= ~OppositeWall(randomNeighbor.SameWall);

                    maze[neighborPosition.X, neighborPosition.Y] |= WallState.EXPLORED;

                    positionStack.Push(neighborPosition);
                }
            }

            //Create entrance and exit
            maze[width - 1, 0] &= ~WallState.RIGHT;
            //maze[0, height - 1] &= ~WallState.LEFT;

            return maze;
        }

        private static WallState OppositeWall(WallState wall)
        {
            switch (wall)
            {
                case WallState.UP:
                    return WallState.DOWN;
                case WallState.RIGHT:
                    return WallState.LEFT;
                case WallState.DOWN:
                    return WallState.UP;
                case WallState.LEFT:
                    return WallState.RIGHT;
                default: return WallState.UP;
            }
        }

        //Checks for all neighboring nodes that haven't been explored yet and returns a list of those nodes
        private static List<Neighbor> UnexploredNeighbors(WallState[,] maze, Position pos, int width, int height)
        {
            var list = new List<Neighbor>();

            if (pos.Y < height - 1) //up walls
            {
                if (!maze[pos.X, pos.Y + 1].HasFlag(WallState.EXPLORED))
                {
                    list.Add(new Neighbor { Position = new Position { X = pos.X, Y = pos.Y + 1 }, SameWall = WallState.UP });
                }
            }

            if (pos.X < width - 1) //right walls
            {
                if (!maze[pos.X + 1, pos.Y].HasFlag(WallState.EXPLORED))
                {
                    list.Add(new Neighbor { Position = new Position { X = pos.X + 1, Y = pos.Y }, SameWall = WallState.RIGHT });
                }
            }

            if (pos.Y > 0) //down walls
            {
                if (!maze[pos.X, pos.Y - 1].HasFlag(WallState.EXPLORED))
                {
                    list.Add(new Neighbor { Position = new Position { X = pos.X, Y = pos.Y - 1 }, SameWall = WallState.DOWN });
                }
            }

            if (pos.X > 0) //left walls
            {
                if (!maze[pos.X - 1, pos.Y].HasFlag(WallState.EXPLORED))
                {
                    list.Add(new Neighbor { Position = new Position { X = pos.X - 1, Y = pos.Y }, SameWall = WallState.LEFT });
                }
            }

            return list;
        }

        public static WallState[,] Generate(int width, int height)
        {
            WallState[,] maze = new WallState[width, height];
            WallState initial = WallState.UP | WallState.RIGHT | WallState.DOWN | WallState.LEFT;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    maze[i, j] = initial; // 1111

                    //maze[i, j].HasFlag(WallState.RIGHT);
                }
            }

            return RecursiveBacktracker(maze, width, height);
        }
    }
}
