using UnityEngine;

public class SzenarioManager : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] szenarios = new GameObject[4];

    public enum State
    {
        MovingNext,
        MovingPrevious,
        Stationary
    }

    Camera cam;
    public int slot = 0;
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
        cam = Camera.main;
        cam.transform.position = new Vector3(-gm.chunkSize * 2.5f, gm.chunkSize * 1.5f, -gm.chunkSize * 2.5f);
        cameraHeight = new Vector3(0f, cam.transform.position.y, 0f);
        cam.transform.LookAt(Vector3.zero);
    }
    public void Update()
    {
        //cam.transform.position = new Vector3(-gm.chunkSize * 2.5f, gm.chunkSize*2, -gm.chunkSize * 2.5f);
        if (Input.GetKeyDown(KeyCode.LeftArrow) && state == State.Stationary)
        {
            state = State.MovingPrevious;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && state == State.Stationary)
        {
            state = State.MovingNext;
        }
        if (state == State.MovingNext)
        {
            if (beginOfAnimation)
            {
                slot = Mod(slot + 1, 4);
                cameraHeight = new Vector3(0, cam.transform.position.y, 0);
                startingPosition = new Vector3(cam.transform.position.x, 0f, cam.transform.position.z);
                endPosition = Quaternion.Euler(new Vector3(0f, -90f, 0f)) * startingPosition;
                beginOfAnimation = false;
                if (szenarios[slot] != null)
                {
                    ISzenario currentSzenario = szenarios[slot].GetComponent(typeof(ISzenario)) as ISzenario;
                    currentSzenario.InSpotlight(true);
                }
                int previousSlot = Mod(slot - 1, 4);
                if (szenarios[previousSlot] != null)
                {
                    ISzenario previousSzenario = szenarios[previousSlot].GetComponent(typeof(ISzenario)) as ISzenario;
                    previousSzenario.InSpotlight(false);

                }
            }
            if (timer > animationDuration)
            {
                cam.transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;

                return;
            }
            cam.transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, timer / animationDuration));
            cam.transform.LookAt(Vector3.zero);
            timer += Time.deltaTime;
        }
        else if (state == State.MovingPrevious)
        {
            if (beginOfAnimation)
            {
                cameraHeight = new Vector3(0, cam.transform.position.y, 0);
                slot = Mod((slot - 1), 4);
                startingPosition = new Vector3(cam.transform.position.x, 0f, cam.transform.position.z);
                endPosition = Quaternion.Euler(new Vector3(0f, 90f, 0f)) * startingPosition;
                beginOfAnimation = false;
                if (szenarios[slot] != null)
                {
                    ISzenario currentSzenario = szenarios[slot].GetComponent(typeof(ISzenario)) as ISzenario;
                    currentSzenario.InSpotlight(true);
                }
                int previousSlot = Mod(slot + 1, 4);
                if (szenarios[previousSlot] != null)
                {
                    ISzenario previousSzenario = szenarios[previousSlot].GetComponent(typeof(ISzenario)) as ISzenario;
                    previousSzenario.InSpotlight(false);

                }
            }
            if (timer > animationDuration)
            {
                cam.transform.position = cameraHeight + endPosition;
                timer = 0;
                state = State.Stationary;
                beginOfAnimation = true;
                Debug.Log("slot after animation: " + slot);
                return;
            }
            cam.transform.position = cameraHeight + Vector3.Slerp(startingPosition, endPosition, Mathf.SmoothStep(0f, 1f, timer / animationDuration));
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
        slot = Mod((slot + 1), 4);
    }

    public void Previous()
    {
        state = State.MovingPrevious;
        slot = Mod((slot - 1), 4);
    }

    int Mod(int a, int n)
    {
        int result = a % n;
        if ((result < 0 && n > 0) || (result > 0 && n < 0))
        {
            result += n;
        }
        return result;
    }
}
