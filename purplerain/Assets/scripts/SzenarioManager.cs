using UnityEngine;

public class SzenarioManager : MonoBehaviour
{
    public GameObject seepage;
    public ISzenario[] szenarios = new ISzenario[4];

    public enum State
    {
        MovingNext,
        MovingPrevious,
        Stationary
    }

    Camera cam;
    private int slot = 0;
    public State state = State.Stationary;
    public float animationDuration;
    private Vector3 cameraHeight;
    private float timer;
    private Vector3 startingPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;
    private bool beginOfAnimation = true;

    public void Start()
    {
        szenarios[0] = seepage.GetComponent<ISzenario>();
        timer = 0f;
        cam = Camera.main;
        cameraHeight = new Vector3(0f, cam.transform.position.y, 0f);
        cam.transform.LookAt(Vector3.zero);
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
                startingPosition = new Vector3(cam.transform.position.x, 0f, cam.transform.position.z);
                endPosition = Quaternion.Euler(new Vector3(0f, -90f, 0f)) * startingPosition;
                beginOfAnimation = false;
            }
            if (timer > animationDuration)
            {
                cam.transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;
                ISzenario currentSzenario = szenarios[slot];
                ISzenario previousSzenario = szenarios[(slot -1) % 4];
                if (currentSzenario != null)
                {
                    currentSzenario.InSpotlight(true);
                }
                if (previousSzenario != null)
                {
                    previousSzenario.InSpotlight(false);
                }
                return;
            }
            cam.transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, timer/animationDuration));
            cam.transform.LookAt(Vector3.zero);
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
                cam.transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;
                ISzenario currentSzenario = szenarios[slot];
                ISzenario previousSzenario = szenarios[(slot +1) % 4];
                if (currentSzenario != null)
                {
                    currentSzenario.InSpotlight(true);
                }
                if (previousSzenario != null)
                {
                    previousSzenario.InSpotlight(false);
                }
                return;
            }
            cam.transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, timer/animationDuration));
            cam.transform.LookAt(Vector3.zero);
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
        slot = (slot + 1) % 4;
    }

    public void Previous()
    {
        state = State.MovingPrevious;
        slot = (slot - 1) % 4;
    }
}
