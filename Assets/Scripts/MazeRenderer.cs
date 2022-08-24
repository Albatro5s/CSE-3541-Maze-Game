using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanTristan.Lab3
{
    public class MazeRenderer : MonoBehaviour
    {

        [SerializeField] [Range(5, 50)] private int width = 5;

        [SerializeField] [Range(5, 50)] private int height = 5;

        [SerializeField] private Transform wallObject;

        [SerializeField] private Transform floorObject;

        [SerializeField] private Transform exitObject;

        [SerializeField] private Transform coinObject;

        [SerializeField] private Transform flagStartObject;

        [SerializeField] private Transform flagEndObject;

        [SerializeField] private Transform player;

        [SerializeField] private float scale = 5f;

        private float size = 5f; //same as scale

        // Camera
        // [SerializeField] private Camera selectedCamera;


        // Start is called before the first frame update
        void Start()
        {
            var maze = MazeGenerator.Generate(width, height);
            Draw(maze, player);
        }

        private void Draw(WallState[,] maze, Transform player)
        {
            // i>-------------------
            //  |                   |
            //  |                   |
            //  |                   |
            //  |                   |
            //  |                   |
            //  |                   |
            // j^-------------------

            for (int i = 0; i < width; i++) // create an array
            {
                for (int j = 0; j < width; j++)
                {
                    var cell = maze[i, j];
                    var position = new Vector3((-width / 2 + i) * scale, 0, (-height / 2 + j) * scale);

                    var floor = Instantiate(floorObject, transform) as Transform;
                    floor.position = position;

                    if (cell.HasFlag(WallState.UP))
                    {
                        var upWall = Instantiate(wallObject, transform) as Transform;
                        upWall.position = position + new Vector3(0, 0, size / 2); // each wall is offset from the center of a cell
                    }

                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallObject, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }

                    if (i == 0)
                    {
                        if (cell.HasFlag(WallState.LEFT))
                        {
                            var leftWall = Instantiate(wallObject, transform) as Transform;
                            leftWall.position = position + new Vector3(-size / 2, 0, 0);
                            leftWall.eulerAngles = new Vector3(0, 90, 0);
                        }
                    }

                    if (j == 0)
                    {
                        if (cell.HasFlag(WallState.DOWN))
                        {
                            var downWall = Instantiate(wallObject, transform) as Transform;
                            downWall.position = position + new Vector3(0, 0, -size / 2);
                        }
                    }

                    // Generate coins
                    if(CountWalls(cell) >= 3)
                    {
                        var coin = Instantiate(coinObject, transform) as Transform;
                        coin.position = position + new Vector3(0, 2, 0);
                    }
                    else
                    {
                        if (Random.Range(0f, 3f) % 3 == 0)
                        {
                            var coin = Instantiate(coinObject, transform) as Transform;
                            coin.position = position + new Vector3(0, 2, 0);
                        }
                    }

                }
            }

            // Character start
            player.position = new Vector3((-width / 2) * scale, 3, (height / 2) * scale);

            // Generate exit door
            var exit = Instantiate(exitObject, transform) as Transform;
            exit.position = new Vector3((width / 2) * scale + size / 2, 0, (-height / 2) * scale);
            exit.eulerAngles = new Vector3(0, 90, 0);

            // Goal flags
            var flagStart = Instantiate(flagStartObject, transform) as Transform;
            flagStart.position = new Vector3((-width / 2 - 1 / 2) * scale, 10, (height / 2 + 1 / 2) * scale);
            var flagEnd = Instantiate(flagEndObject, transform) as Transform;
            flagEnd.position = new Vector3((width / 2 + 1 / 2) * scale, 10, (-height / 2 - 1 / 2) * scale);
        }

        private int CountWalls(WallState cell)
        {
            int numberOfWalls = 0;

            if (cell.HasFlag(WallState.UP))
            {
                numberOfWalls++;
            }
            if (cell.HasFlag(WallState.DOWN))
            {
                numberOfWalls++;
            }
            if (cell.HasFlag(WallState.LEFT))
            {
                numberOfWalls++;
            }
            if (cell.HasFlag(WallState.RIGHT))
            {
                numberOfWalls++;
            }

            return numberOfWalls;
        }

    }
}

