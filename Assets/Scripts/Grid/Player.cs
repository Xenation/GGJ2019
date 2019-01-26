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

        private Vector2 step;

        [HideInInspector] public bool playerMoved = false;

        [HideInInspector]public RectTransform rectTransform;

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

            if (Input.GetKeyUp(KeyCode.D))
            {
                right = false;
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
                if((gridPosition.x + 1) < grid.width && grid.grid[gridPosition.y][gridPosition.x + 1].isCrossable)
                {
                    gridPosition.x++;
                    return true;
                }
            }
            else if(left)
            {
                if(gridPosition.x - 1 >= 0 && grid.grid[gridPosition.y][gridPosition.x - 1].isCrossable)
                {
                    gridPosition.x--;
                    return true;
                }
            }
            else if(down)
            {
                if(gridPosition.y -1 >= 0 && grid.grid[gridPosition.y - 1][gridPosition.x].isCrossable)
                {
                    gridPosition.y--;
                    return true;
                }
            }
            else if(up)
            {
                if(gridPosition.y + 1 < grid.height && grid.grid[gridPosition.y + 1][gridPosition.x].isCrossable)
                {
                    gridPosition.y++;
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
