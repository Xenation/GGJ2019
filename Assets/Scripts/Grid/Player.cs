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

        [HideInInspector] public RectTransform rectTransform;

        [HideInInspector] public bool takenByCursor = false;

        //Time for window feeling
        float startTime = 0;
        float startTimeFlood = 0;
        public float floodRate = 1;
        public float timeBeforeFlood = 1;
        bool inFlood = false;

        private int Times_Gotten = 1;
        public int NumberToEscape = 5;
        private int NumberArrow = 0;
        //true = right, false = left
        private bool RightLeft;
        public bool escaped;
        public GameObject Arrow_Left;
        public GameObject Arrow_Right;

        // Start is called before the first frame update
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
            if (Input.GetButtonDown("Up"))
            {
                right = false;
                left = false;
                up = true;
                down = false;
                startTime = Time.time;
                inFlood = false;
            }
            if (Input.GetButtonDown("Left"))
            {
                right = false;
                left = true;
                up = false;
                down = false;
                startTime = Time.time;
                inFlood = false;
            }
            if (Input.GetButtonDown("Down"))
            {
                right = false;
                left = false;
                up = false;
                down = true;
                startTime = Time.time;
                inFlood = false;
            }
            if (Input.GetButtonDown("Right"))
            {
                right = true;
                left = false;
                up = false;
                down = false;
                startTime = Time.time;
                inFlood = false;
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
			Vector3 newPos = grid.GetCanvasPos(gridPosition);
            rectTransform.position = newPos;
        }

        //Returns true if the player has moved
        private bool MovePlayer()
        {
            //check if player is not taken by the cursor
            if (!takenByCursor)
            {
				GridIcon destIcon;
                //reset escape
                escaped = false;
				//Move player in direction acquired.
				if (right && (gridPosition.x + 1) < grid.width)
                {
					destIcon = grid.grid[gridPosition.x + 1, gridPosition.y];
					if (destIcon == null || destIcon.isCrossable)
                    {
                        if (startTime == Time.time)
                        {
                            gridPosition.x++;
                            return true;
                        }
                        else if (Time.time - startTime > timeBeforeFlood && !inFlood)
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
                else if (left && gridPosition.x - 1 >= 0)
                {
					destIcon = grid.grid[gridPosition.x - 1, gridPosition.y];
					if (destIcon == null || destIcon.isCrossable)
                    {
                        if (startTime == Time.time)
                        {
                            gridPosition.x--;
                            return true;
                        }
                        else if (Time.time - startTime > timeBeforeFlood && !inFlood)
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
                else if (down && gridPosition.y - 1 >= 0)
                {
					destIcon = grid.grid[gridPosition.x, gridPosition.y - 1];
					if (destIcon == null || destIcon.isCrossable)
                    {
                        if (startTime == Time.time)
                        {
                            gridPosition.y--;
                            return true;
                        }
                        else if (Time.time - startTime > timeBeforeFlood && !inFlood)
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
                else if (up && gridPosition.y + 1 < grid.height)
                {
					destIcon = grid.grid[gridPosition.x, gridPosition.y + 1];
					if (destIcon == null || destIcon.isCrossable)
                    {
                        if (startTime == Time.time)
                        {
                            gridPosition.y++;
                            return true;
                        }
                        else if (Time.time - startTime > timeBeforeFlood && !inFlood)
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
            }
            //Escape the cursor minigame
            else
            {
               if(NumberArrow == NumberToEscape * Times_Gotten)
                {
                    Times_Gotten++;
                    takenByCursor = false;
                    escaped = true;
                    NumberArrow = 0;
                    RightLeft = false;
                    Arrow_Left.SetActive(false);
                    Arrow_Right.SetActive(false);
                }
               else if(Input.GetButtonDown("Right"))
                {
                    if(RightLeft)
                    {
                        NumberArrow++;
                        RightLeft = !RightLeft;
                        Arrow_Left.SetActive(false);
                        Arrow_Right.SetActive(true);
                    }
                }
               else if(Input.GetButtonDown("Left"))
                {
                    if(!RightLeft)
                    {
                        NumberArrow++;
                        RightLeft = !RightLeft;
                        Arrow_Left.SetActive(true);
                        Arrow_Right.SetActive(false);
                    }
                }
               else if(NumberArrow == 0)
                {
                    Arrow_Right.SetActive(true);
                }
            }
            return false;
        }

        //  /!\
        //Add secondary fixedupdate at windows tick rate for moving with a windows feel
    }

}
