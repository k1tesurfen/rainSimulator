using UnityEngine;

public class SzenarioSelector : MonoBehaviour
{
    public enum State
    {
        MovingNext,
        MovingPrevious,
        Stationary
    }

    public State state = State.Stationary;
    public float animationDuration;
    private Vector3 cameraHeight;
    private float timer;
    private Vector3 startingPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private bool beginOfAnimation = true;

    public void Start()
    {
        timer = 0f;
        cameraHeight = new Vector3(0f, transform.position.y, 0f);
        transform.LookAt(Vector3.zero);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            state = State.MovingPrevious;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            state = State.MovingNext;
        }
        if (state == State.MovingNext)
        {
            if (beginOfAnimation)
            {
                startingPosition = new Vector3(transform.position.x, 0f, this.transform.position.z);
                endPosition = Quaternion.Euler(new Vector3(0f, -90f, 0f)) * startingPosition;
                beginOfAnimation = false;
            }
            if (timer > animationDuration)
            {
                transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;
                return;
            }
            float currentPos = Ramp(timer / animationDuration);
            transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, currentPos));
            transform.LookAt(Vector3.zero);
            timer += Time.deltaTime;
        }
        else if (state == State.MovingPrevious)
        {
            if (beginOfAnimation)
            {
                startingPosition = new Vector3(transform.position.x, 0f, transform.position.z);
                endPosition = Quaternion.Euler(new Vector3(0f, 90f, 0f)) * startingPosition;
                beginOfAnimation = false;
            }
            if (timer > animationDuration)
            {
                transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;
                return;
            }
            float currentPos = Ramp(timer / animationDuration);
            transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, currentPos));
            transform.LookAt(Vector3.zero);
            timer += Time.deltaTime;
        }
    }

    private float Ramp(float x)
    {
        float ret = (Mathf.Atan((3 * x) - 1.5f) + 1) / 2f;
        return ret;
    }

    public void Next()
    {
        state = State.MovingNext;
    }

    public void Previous()
    {
        state = State.MovingPrevious;
    }

}
