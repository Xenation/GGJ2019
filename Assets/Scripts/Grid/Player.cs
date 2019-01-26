using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019
{
    public class Player : MonoBehaviour
    {
        public IconGrid grid;

        private Vector2Int gridPosition;

        private bool    right   = false, 
                        left    = false,
                        up      = false,
                        down    = false;

        private Vector2 Step;

        [HideInInspector] public bool playerMoved = false;

        private RectTransform rectTransform;

        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            Step = grid.GetStep();
        }

        // Update is called once per frame
        void Update()
        {
            playerMoved = false;

            //Use input.GetKeyDown() as it makes simpler code for behavior
            if (Input.GetKeyDown(KeyCode.Z))
            {
                right = false;
                left = false;
                up = true;
                down = false;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                right = false;
                left = true;
                up = false;
                down = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                right = false;
                left = false;
                up = false;
                down = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                right = true;
                left = false;
                up = false;
                down = false;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                up = false;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                left = false;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                down = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                right = false;
            }


            playerMoved = MovePlayer();

            rectTransform.position = (gridPosition + new Vector2(1, 1)) * grid.GetStep();
        }

        //Returns true if the player has moved
        private bool MovePlayer()
        {
            //Move player in direction acquired.
            if(right)
            {
                if(grid.grid[gridPosition.x][gridPosition.y+1].isCrossable && gridPosition.y + 1 <= grid.width)
                {
                    gridPosition.y++;
                }
            }
            else if(left)
            {
                if(grid.grid[gridPosition.x][gridPosition.y-1].isCrossable && gridPosition.y - 1 >= grid.width)
                {
                    gridPosition.y--;
                }
            }
            else if(up)
            {
                if(grid.grid[gridPosition.x-1][gridPosition.y].isCrossable && gridPosition.x -1 >= grid.height)
                {
                    gridPosition.x--;
                }
            }
            else if(down)
            {
                if(grid.grid[gridPosition.x+1][gridPosition.y].isCrossable && gridPosition.x + 1 <= grid.height)
                {
                    gridPosition.x++;
                }
            }
            //gridPosition corresponds to coordinates in grid list

            return false;

        }

        //  /!\
        //Add secondary fixedupdate at windows tick rate for moving with a windows feel
    }

}
