using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Curiosity : MonoBehaviour
{

    //Movement
    private float normal_speed;
    private float current_speed;

    private GameObject splineObject;
    private SplineContainer splinecontainer;
    private bool facingRight;
    private float distancePercentage = 0f;
    private bool isReversed = false;


    private float timeBeforeSlowDown = 2;
    
    //Capture
    private float maxCapturePoint;
    private float currentCapturePoint =0;
    private float captureSpeed;
    private float uncaptureSpeed;

    public bool isBeeingCaptured = false;

    [field:SerializeField]private UI_Capture _uiCapture;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Instantiate the spline 
        GameObject spline = Instantiate(this.splineObject,this.transform.parent);
        splinecontainer = spline.GetComponent<SplineContainer>();
        // Teleport to start
        Vector3 startPos = splinecontainer.EvaluatePosition(0);
        transform.position = startPos; 
    }

    public void Initialize(float normal_speed, float maxCapturePoint, float captureSpeed, float uncaptureSpeed, GameObject spline)
    {
        this.normal_speed = normal_speed;
        current_speed = this.normal_speed;
        this.maxCapturePoint = maxCapturePoint;
        this.captureSpeed = captureSpeed;
        this.uncaptureSpeed = uncaptureSpeed;
        this.splineObject = spline;
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveAlongSpline();
        CaptureUpdate();
    }
    
    private void CaptureUpdate()
    {
        if (isBeeingCaptured)
        {
            currentCapturePoint += Time.deltaTime * captureSpeed;
            if (currentCapturePoint >= maxCapturePoint)
            {
                //capture the curiosity
            }
            _uiCapture.UpdateCaptureBar(CapturePercent());
            //GameManager.Instance.CaptureUpdate(this);
        }
        else if (currentCapturePoint > 0)
        {
            currentCapturePoint -= Time.deltaTime * uncaptureSpeed;
            _uiCapture.UpdateCaptureBar(CapturePercent());
            //GameManager.Instance.CaptureUpdate(this);
        }
    }
    
    private void MoveAlongSpline()
    {
        Debug.Log("we are moving");
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

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
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

    public void OnSpeedUp(float dash_speed, float time_before_slow_down)
    {
        timeBeforeSlowDown = time_before_slow_down;
        current_speed = dash_speed;
        StartCoroutine(SlowDown());
    }
    
    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(timeBeforeSlowDown);
        current_speed = normal_speed;
    }

    public void Capture()
    {
        isBeeingCaptured = true;
    }
    
    public void UnCapture()
    {
        isBeeingCaptured = false;
    }

    public float CapturePercent()
    {
        return (currentCapturePoint/maxCapturePoint);
    }
}
