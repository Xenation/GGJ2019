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

        [HideInInspector] public bool playerMoved = false;

        private RectTransform rectTransform;

        // Start is called before the first frame update
        void Start()
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                right = false;
                left = false;
                up = true;
                down = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                right = false;
                left = true;
                up = false;
                down = false;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                right = false;
                left = false;
                up = false;
                down = true;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                right = true;
                left = false;
                up = false;
                down = false;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                right = false;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                up = false;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                left = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                down = false;
            }


            playerMoved = MovePlayer();
            Vector3 newPos = new Vector3((gridPosition.x + 1)* step.y, (gridPosition.y + 1) * step.x );
            rectTransform.position = newPos;
            Debug.Log(gridPosition.x + " :   : " + gridPosition.y);
        }

        //Returns true if the player has moved
        private bool MovePlayer()
        {
            //Move player in direction acquired.
            if(right)
            {
                if((gridPosition.y + 1) < grid.width && grid.grid[gridPosition.x][gridPosition.y + 1].isCrossable)
                {
                    gridPosition.y++;
                    return true;
                }
            }
            else if(left)
            {
                if(gridPosition.y - 1 >= 0 && grid.grid[gridPosition.x][gridPosition.y - 1].isCrossable)
                {
                    gridPosition.y--;
                    return true;
                }
            }
            else if(up)
            {
                if(gridPosition.x -1 >= 0 && grid.grid[gridPosition.x - 1][gridPosition.y].isCrossable)
                {
                    gridPosition.x--;
                    return true;
                }
            }
            else if(down)
            {
                if(gridPosition.x + 1 < grid.height && grid.grid[gridPosition.x + 1][gridPosition.y].isCrossable)
                {
                    gridPosition.x++;
                    return true;
                }
            }
            //gridPosition corresponds to coordinates in grid list

            return false;

        }

        //  /!\
        //Add secondary fixedupdate at windows tick rate for moving with a windows feel
    }

}
