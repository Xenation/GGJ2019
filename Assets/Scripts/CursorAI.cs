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

        private bool hasEdgy        = false;
        private bool prevHasEdgy    = false;
        private bool openWindow     = false;

        //Temporary placeholder for the size of the icon (player)
        // Add correct size
        private float IconSize       = 5f;

        private RectTransform rectTransform;

        public GameObject playerSelected;
        private GameObject playerSelectedInstance;

        // Start is called before the first frame update
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            cursorPosZ = rectTransform.position.z;
            PlayerMoved();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (edgy.playerMoved)
            {
                PlayerMoved();
            }
            //Calculate direction to the player if the player has moved
            if(prevHasEdgy != hasEdgy)
            {
                OnHasEdgy();
            }

            if (!hasEdgy)
            {
                //Cursor to player
                if (!CursorOnDestination(edgy.rectTransform.position))
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
                }
            }
            else
            {
                //Brings player to bin
                if (!CursorOnDestination(bin.GetComponent<RectTransform>().position))
                {
                    rectTransform.position += direction * Time.deltaTime;
                }
                else
                {
                    //Is on bin
                    Destroy(playerSelectedInstance);
                }
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
        bool CursorOnDestination(Vector3 destination)
        {   
            return (rectTransform.position.x < destination.x + IconSize 
                 && rectTransform.position.x > destination.x - IconSize
                 && rectTransform.position.y < destination.y + IconSize 
                 && rectTransform.position.y > destination.y - IconSize);
        }


    }

}
