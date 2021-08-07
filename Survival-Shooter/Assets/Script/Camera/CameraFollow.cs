using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public Transform minPosition;
    public Transform maxPosition;
    
    
    private  float m_MinYPosition = 0;
    private  float m_MaxYPosition = 0;
    private  float m_MinXPosition = 0;
    private  float m_MaxXPosition = 0;

	
    float m_OffsetZ;
    Vector3 m_LastTargetPosition;
    Vector3 m_CurrentVelocity;
    Vector3 m_LookAheadPos;
    
    float m_NextTimeToSearch = 0;
	   
    // Use this for initialization
    void Start () {
        // lastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
        
        var vertExtent = Camera.main.GetComponent<Camera>().orthographicSize ;    
        var horzExtent = vertExtent * Screen.width / Screen.height;
 
        // Calculations assume map is position at the origin
        m_MinXPosition = horzExtent + minPosition.position.x;
        m_MaxXPosition = maxPosition.position.x - horzExtent;
        m_MinYPosition = vertExtent + minPosition.position.y;
        m_MaxYPosition = maxPosition.position.y - vertExtent;
    }
	   
    // Update is called once per frame
    void Update () {
    
        if (target == null) {
            FindPlayer ();
            return;
        }
    
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;
    
        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
    
        if (updateLookAheadTarget) {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
        }
		  
        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
        
        float posX = Mathf.Clamp(newPos.x, m_MinXPosition, m_MaxXPosition);
        float posY = Mathf.Clamp(newPos.y, m_MinYPosition, m_MaxYPosition);
        newPos = new Vector3 (posX, posY, newPos.z);

        transform.position = newPos;
		  
        m_LastTargetPosition = target.position;		
    }
    
    void FindPlayer () {
        if (m_NextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
            if (searchResult != null)
                target = searchResult.transform;
            m_NextTimeToSearch = Time.time + 0.5f;
        }
    }
}