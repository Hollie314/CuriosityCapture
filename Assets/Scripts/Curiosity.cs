using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Curiosity : MonoBehaviour
{

    //Movement
    [SerializeField] private float normal_speed;
    [SerializeField] private float dash_speed;
    [SerializeField] private float current_speed;

    [SerializeField] private float timeBeforeReverse;
    [SerializeField] private float timeBeforeDash;
    [SerializeField] private float timeBeforeTrigger;
    [SerializeField] private float timeBeforeSlowDown = 2;
    
    [SerializeField] public SplineContainer splinecontainer;
    private bool facingRight;
    float distancePercentage = 0f;
    private bool isReversed = false;
    
    //Capture
    private int maxCapturePoint;
    private int currentCapturePoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_speed = normal_speed;
        
        //splineAnimate.MaxSpeed = normal_speed;
        if (timeBeforeDash > 0)
        {
            StartCoroutine(SpeedUp());
        }

        if (timeBeforeReverse > 0)
        {
            StartCoroutine(ChangeDirection());
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongSpline();
    }

    private void MoveAlongSpline()
    {
        
        // Calculate the target position on the spline
        Vector3 targetPosition = splinecontainer.EvaluatePosition(distancePercentage);

        // Move the character towards the target position on the spline
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, current_speed * Time.deltaTime);
        

        // Adjust the movement based on the length of the spline
        float splineLength = splinecontainer.CalculateLength();
        float movement = current_speed * Time.deltaTime / splineLength;

        // Update distancePercentage based on the movement direction
        if (!isReversed)
        {
            distancePercentage += movement;
        }
        else
        {
            distancePercentage -= movement;
        }

        // Clamp distancePercentage to stay within [0, 1]
        if (distancePercentage >= 1f) distancePercentage = 0f;
        if (distancePercentage < 0f) distancePercentage = 1f;


        // calculate orientation
        float orientation = transform.position.x - targetPosition.x;
        if (orientation < 0 && facingRight)
        {
            Flip();
        }
        if (orientation > 0 && !facingRight)
        {
            Flip();
        }
    }
    
    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(timeBeforeDash);
        current_speed = dash_speed;
        yield return StartCoroutine(SlowDown());
        StartCoroutine(SpeedUp());
    }
    
    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(timeBeforeSlowDown);
        current_speed = normal_speed;
    }
    
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(timeBeforeReverse);
        OnchangeDirection();
        StartCoroutine(ChangeDirection());
    }

    public void OnchangeDirection()
    {
        Debug.Log("we are changing direction");
        for (int i = 0; i < splinecontainer.Splines.Count; i++)
        {
            splinecontainer.ReverseFlow(i);
        }
         
        // Flip the movement direction
        isReversed = !isReversed;

        // Optionally, reset the position if you want to smooth out the transition
        // distancePercentage = isReversed ? 1f - distancePercentage : distancePercentage;
    }

    public void OnSpeedUp()
    {
        current_speed = dash_speed;
        StartCoroutine(SlowDown());
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
