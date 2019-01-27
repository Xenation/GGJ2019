using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2019
{
    public class CursorAI : MonoBehaviour
    {
        public Player edgy;
        public GameObject bin;
        public float speed;
        
        private float cursorPosZ;
        private Vector3 direction;
        private Vector3 currentDestination;

        private bool hasEdgy        = false;
        private bool prevHasEdgy    = false;

        //Temporary placeholder for the size of the icon (player)
        // Add correct size
        private float IconSize       = 5f;

        private RectTransform rectTransform;

        public GameObject playerSelected;
        private GameObject playerSelectedInstance;

        private AudioSource clickSound;

        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            cursorPosZ = rectTransform.position.z;
            clickSound = GetComponent<AudioSource>();
            PlayerMoved();
        }
        

        // Update is called once per frame
        void FixedUpdate()
        {
			Window win = WindowManager.I.GetTopWindow();
			WindowManager winManager = WindowManager.I;
			if (win == null)
            {
                if (edgy.playerMoved)
                {
                    PlayerMoved();
                }
                //Calculate direction to the player if the player has moved
                if (prevHasEdgy != hasEdgy)
                {
                    OnHasEdgy();
                }

                if (!hasEdgy)
                {
                    //Cursor to player
                    if (!CursorOnDestination())
                    {
                        rectTransform.position += direction * Time.deltaTime;
                    }
                    else
                    {
                        //Has player
                        hasEdgy = true;
                        edgy.takenByCursor = true;
                        playerSelectedInstance = Instantiate(playerSelected, rectTransform.position - (Vector3.down * IconSize), Quaternion.identity, rectTransform);
                        playerSelectedInstance.transform.localPosition = Vector3.down * IconSize;
                        clickSound.Play();
                    }
                }
                else
                {
                    //Brings player to bin
                    if (!CursorOnDestination())
                    {
                        rectTransform.position += direction * Time.deltaTime;
                    }
                    else
                    {
                        //Add someway for player to escape
                        
                        //Is on bin
                        Destroy(playerSelectedInstance);
                        //Add end!
                    }
                }
            }
            //If there are existing windows
            // CHECK IF IT IS A FUCKIN FOLDER !!! :C
            //If it as a folder, set it with containsPlayer returning false;
            else
            {
                //Is NOT on player, and player is in top window
                if ((!CursorOnDestination() && winManager.isPlayerOnTopMostWindow()))
                {
                    if(currentDestination == edgy.transform.position)
                    {
                        rectTransform.position += direction * Time.deltaTime;
                    }
                    else
                    {
                        ChangeCursorDirection(edgy.transform.position);
                    }
                }
                //is NOT on closeDestinationd and player is NOT in top window
                else if (!CursorOnDestination() && !winManager.isPlayerOnTopMostWindow())
                {
                    if(currentDestination == (Vector3) winManager.GetTopWindowClosePosition())
                    {
                        rectTransform.position += direction * Time.deltaTime;
                    }
                    else
                    {
                        ChangeCursorDirection(winManager.GetTopWindowClosePosition());
                    }
                }
                //is on closeDestination and player is NOT in top window
                else if(CursorOnDestination() && !winManager.isPlayerOnTopMostWindow())
                {
                    if(currentDestination == (Vector3) winManager.GetTopWindowClosePosition())
                    {
                        //CLOSE WINDOW
						winManager.CloseTopWindow();
                        clickSound.Play();
						win = winManager.GetTopWindow();
                        
                        //CHECK NEW DEST FOR CURSOR
                        if(win != null)
                        {
                            if (winManager.isPlayerOnTopMostWindow())
                            {
                                ChangeCursorDirection(edgy.rectTransform.position);
                            }
                            else
                            {
                                ChangeCursorDirection(winManager.GetTopWindowClosePosition());
                            }
                        }
                        else
                        {
                            ChangeCursorDirection(edgy.rectTransform.position);
                        }

                    }
                    else
                    {
                        ChangeCursorDirection(winManager.GetTopWindowClosePosition());
                    }
                }
            }

            WindowBorders();
        }

        void WindowBorders()
        {
            if(rectTransform.position.x < 0)
            {
                rectTransform.position = new Vector3(0, rectTransform.position.y, rectTransform.position.z);
            }
            else if (rectTransform.position.x > Screen.width)
            {
                rectTransform.position = new Vector3(Screen.height, rectTransform.position.y, rectTransform.position.z);
            }

            if(rectTransform.position.y < 0)
            {
                rectTransform.position = new Vector3(rectTransform.position.x, 0, rectTransform.position.z);
            }
            else if (rectTransform.position.y > Screen.height)
            {
                rectTransform.position = new Vector3(rectTransform.position.x, Screen.height, rectTransform.position.z);
            }
            if (edgy.escaped)
            {
                hasEdgy = false;
                Destroy(playerSelectedInstance);
            }
        }

        /******************************
         * Call everytime player moves
         * ---------------------------
         * Can be replaced by 
         * ChangeCursorDirection
         * ***************************/
        public void PlayerMoved()
        {
            ChangeCursorDirection(edgy.rectTransform.position);
        }

        void ChangeCursorDirection(Vector3 destination)
        {
            currentDestination = destination;
            direction = new Vector3(destination.x - rectTransform.position.x, destination.y - rectTransform.position.y, rectTransform.position.z);
            direction.Normalize();

            direction *= speed;
            direction.z = cursorPosZ;
        }

        /*******************************
         * Called on the first frame 
         * after edgy is taken by cursor
         * ****************************/
        void OnHasEdgy()
        {
            prevHasEdgy = hasEdgy;

            if (hasEdgy)
            {
                ChangeCursorDirection(bin.GetComponent<RectTransform>().position);
            }
            else
            {
                ChangeCursorDirection(edgy.rectTransform.position);
            }

        }

        //Checks if the cursor is on edgy
        bool CursorOnDestination()
        {   
            return (rectTransform.position.x < currentDestination.x + IconSize 
                 && rectTransform.position.x > currentDestination.x - IconSize
                 && rectTransform.position.y < currentDestination.y + IconSize 
                 && rectTransform.position.y > currentDestination.y - IconSize);
        }

    }

}
