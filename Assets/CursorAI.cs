using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019
{
    public class CursorAI : MonoBehaviour
    {
        public GameObject edgy;
        public GameObject bin;
        public float speed;
        
        private float cursorPosZ;
        private Vector3 direction;

        private bool hasEdgy        = false;
        private bool prevHasEdgy    = false;

        //Temporary placeholder for the size of the icon (player)
        private float IconSize       = 0.2f;

        // Start is called before the first frame update
        void Start()
        {
            cursorPosZ = transform.position.z;
            PlayerMoved();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Calculate direction to the player if the player has moved
            if(prevHasEdgy != hasEdgy)
            {
                OnHasEdgy();
            }

            if (!hasEdgy)
            {
                if (!CursorOnDestination(edgy.transform.position))
                    transform.position += direction * Time.deltaTime;
                else
                    hasEdgy = true;
            }
            else
            {
                if (!CursorOnDestination(bin.transform.position))
                    transform.position += direction * Time.deltaTime;
            }
        }

        /******************************
         * Call everytime player moves
         * ---------------------------
         * Can be replaced by 
         * ChangeCursorDirection
         * ***************************/
        void PlayerMoved()
        {
            ChangeCursorDirection(edgy.transform.position);
        }

        void ChangeCursorDirection(Vector3 destination)
        {
            direction = new Vector3(destination.x - transform.position.x, destination.y - transform.position.y, transform.position.z);
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
                ChangeCursorDirection(bin.transform.position);
            }
            else
            {
                ChangeCursorDirection(edgy.transform.position);
            }
        }

        //Checks if the cursor is on edgy
        bool CursorOnDestination(Vector3 destination)
        {   
            return (transform.position.x < destination.x + IconSize 
                 && transform.position.x > destination.x - IconSize
                 && transform.position.y < destination.y + IconSize 
                 && transform.position.y > destination.y - IconSize);
        }


    }

}
