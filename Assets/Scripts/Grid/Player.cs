using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019
{
    public class Player : MonoBehaviour
    {
        public Grid grid;

        private Vector2Int gridPosition;

        private bool    right   = false, 
                        left    = false,
                        up      = false,
                        down    = false;

        private Vector2 step;

        [HideInInspector]public bool playerMoved = false;

        [HideInInspector]public RectTransform rectTransform;

        //Time for window feeling
        float startTime = 0;
        float startTimeFlood = 0;
        public float floodRate = 1;
        public float timeBeforeFlood = 1;
        bool inFlood = false;

        // Start is called before the first frame update
        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            step = grid.GetStep();
            gridPosition = new Vector2Int(0,0);
        }

        // Update is called once per frame
        void Update()
        {
            playerMoved = false;

            //Use input.GetKeyDown() as it makes simpler code for behavior
            if (Input.GetButtonDown("Up"))
            {
                right = false;
                left = false;
                up = true;
                down = false;
                startTime = Time.time;
            }
            if (Input.GetButtonDown("Left"))
            {
                right = false;
                left = true;
                up = false;
                down = false;
                startTime = Time.time;
            }
            if (Input.GetButtonDown("Down"))
            {
                right = false;
                left = false;
                up = false;
                down = true;
                startTime = Time.time;
            }
            if (Input.GetButtonDown("Right"))
            {
                right = true;
                left = false;
                up = false;
                down = false;
                startTime = Time.time;
            }

            if (Input.GetButtonUp("Right"))
            {
                if (right)
                    inFlood = false;
                right = false;
            }
            if (Input.GetButtonUp("Up"))
            {
                if (up)
                    inFlood = false;
                up = false;
            }
            if (Input.GetButtonUp("Left"))
            {
                if (left)
                    inFlood = false;
                left = false;
            }
            if (Input.GetButtonUp("Down"))
            {
                if (down)
                    inFlood = false;
                down = false;
            }


            playerMoved = MovePlayer();
            Vector3 newPos = new Vector3((gridPosition.x + 1)* step.y, (gridPosition.y + 1) * step.x );
            rectTransform.position = newPos;
        }

        //Returns true if the player has moved
        private bool MovePlayer()
        {
            //Move player in direction acquired.
            if(right)
            {
                if((gridPosition.x + 1) < grid.width && (grid.grid[gridPosition.y][gridPosition.x + 1].isCrossable || grid.grid[gridPosition.y][gridPosition.x + 1] == null ))
                {
                    if (startTime == Time.time)
                    {
                        gridPosition.x++;
                        return true;
                    }
                    else if (Time.time - startTime > timeBeforeFlood)
                    {
                        startTimeFlood = Time.time;
                        inFlood = true;
                        gridPosition.x++;
                        return true;
                    }
                    else if (Time.time - startTimeFlood > floodRate && inFlood)
                    {
                        startTimeFlood = Time.time;
                        gridPosition.x++;
                        return true;
                    }
                }
            }
            else if(left)
            {
                if(gridPosition.x - 1 >= 0 && (grid.grid[gridPosition.y][gridPosition.x - 1].isCrossable || grid.grid[gridPosition.y][gridPosition.x + 1] == null))
                {
                    if (startTime == Time.time)
                    {
                        gridPosition.x--;
                        return true;
                    }
                    else if (Time.time - startTime > timeBeforeFlood)
                    {
                        startTimeFlood = Time.time;
                        inFlood = true;
                        gridPosition.x--;
                        return true;
                    }
                    else if (Time.time - startTimeFlood > floodRate && inFlood)
                    {
                        startTimeFlood = Time.time;
                        gridPosition.x--;
                        return true;
                    }
                }
            }
            else if(down)
            {
                if(gridPosition.y -1 >= 0 && (grid.grid[gridPosition.y - 1][gridPosition.x].isCrossable || grid.grid[gridPosition.y][gridPosition.x + 1] == null))
                {
                    if (startTime == Time.time)
                    {
                        gridPosition.y--;
                        return true;
                    }
                    else if (Time.time - startTime > timeBeforeFlood)
                    {
                        startTimeFlood = Time.time;
                        inFlood = true;
                        gridPosition.y--;
                        return true;
                    }
                    else if (Time.time - startTimeFlood > floodRate && inFlood)
                    {
                        startTimeFlood = Time.time;
                        gridPosition.y--;
                        return true;
                    }
                }
            }
            else if(up)
            {
                if(gridPosition.y + 1 < grid.height && (grid.grid[gridPosition.y + 1][gridPosition.x].isCrossable || grid.grid[gridPosition.y][gridPosition.x + 1] == null))
                {
                    if (startTime == Time.time)
                    {
                        gridPosition.y++;
                        return true;
                    }
                    else if (Time.time - startTime > timeBeforeFlood)
                    {
                        startTimeFlood = Time.time;
                        inFlood = true;
                        gridPosition.y++;
                        return true;
                    }
                    else if (Time.time - startTimeFlood > floodRate && inFlood)
                    {
                        startTimeFlood = Time.time;
                        gridPosition.y++;
                        return true;
                    }
                }
            }
            //gridPosition corresponds to coordinates in grid list

            return false;

        }

        //  /!\
        //Add secondary fixedupdate at windows tick rate for moving with a windows feel
    }

}
