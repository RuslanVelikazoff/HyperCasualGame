using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 15;

    public int minSwipeRecognition = 500;

    private bool isTraveling;
    private Vector3 travelDirection;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Vector3 nextCollisionPosition;

    private Color solveColor;

    private void Start()
    {
        solveColor = Random.ColorHSV(.5f, 1);
        GetComponent<MeshRenderer>().material.color = solveColor;
    }

    private void FixedUpdate()
    {
        if (isTraveling) {
            rb.velocity = travelDirection * speed;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up/2), .05f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            GroundPiece ground = hitColliders[i].transform.GetComponent<GroundPiece>();

            if (ground && !ground.isColored)
            {
                ground.Colored(solveColor);
            }

            i++;
        }

        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }

        if (isTraveling)
            return;

        #region PC

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame != Vector2.zero)
            {

                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition) 
                    return;

                currentSwipe.Normalize();

                if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back); 
                }   

                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }


            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }

        #endregion

        #region Androind

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                if (swipePosLastFrame != Vector2.zero)
                {

                    currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                    if (currentSwipe.sqrMagnitude < minSwipeRecognition)
                        return;

                    currentSwipe.Normalize();

                    if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                    }

                    if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                    }
                }


                swipePosLastFrame = swipePosCurrentFrame;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                swipePosLastFrame = Vector2.zero;
                currentSwipe = Vector2.zero;
            }
        }

        #endregion
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPosition = hit.point;
        }

        isTraveling = true;
    }
}
