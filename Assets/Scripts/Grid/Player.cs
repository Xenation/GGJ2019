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

        [HideInInspector] public bool playerMoved = false;

        [HideInInspector]public RectTransform rectTransform;
		
        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
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
			Vector3 newPos = grid.GetCanvasPos(gridPosition);
            rectTransform.position = newPos;
            Debug.Log(gridPosition.x + " :   : " + gridPosition.y);
        }

        //Returns true if the player has moved
        private bool MovePlayer()
        {
			//Move player in direction acquired.
			GridIcon destIcon;
			if (right && (gridPosition.x + 1) < grid.width)
            {
				destIcon = grid.grid[gridPosition.x + 1, gridPosition.y];
				if (destIcon == null || destIcon.isCrossable)
                {
                    gridPosition.x++;
                    return true;
                }
            }
            else if(left && gridPosition.x - 1 >= 0)
            {
				destIcon = grid.grid[gridPosition.x - 1, gridPosition.y];
				if (destIcon == null || destIcon.isCrossable)
                {
                    gridPosition.x--;
                    return true;
                }
            }
            else if(down && gridPosition.y - 1 >= 0)
            {
				destIcon = grid.grid[gridPosition.x, gridPosition.y - 1];
				if (destIcon == null || destIcon.isCrossable)
                {
                    gridPosition.y--;
                    return true;
                }
            }
            else if(up && gridPosition.y + 1 < grid.height)
            {
				destIcon = grid.grid[gridPosition.x, gridPosition.y + 1];
                if(destIcon == null || destIcon.isCrossable)
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
