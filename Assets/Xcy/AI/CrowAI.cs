using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrowAI : MonoBehaviour
{
	private float _speed = 3;
	private Vector3 _startVelocity;
	public float mass = 1;
	//当前速度
	public Vector3 velocity = Vector3.forward;
	//总力
	[SerializeField]
	private Vector3 _sumForce = Vector3.zero;

	//分离力
	[SerializeField]
	private Vector3 _separationForce=Vector3.zero;
	private float _separationDistance = 0.5f;
	private List<GameObject> _separationNeighbors=new List<GameObject>();
	//队列力
	[SerializeField]
	private Vector3 _alignmentForce=Vector3.zero;
	private float _alignmentDistance = 4;
	private List<GameObject> _alignmentNeighbors=new List<GameObject>();
	private Vector3 avgDir=Vector3.zero;
	//聚合力
	[SerializeField]
	private Vector3 _cohesionForce=Vector3.zero;
	//力检查间隔时间
	public float CheckInterval=0.2f;
	public float separationWeight = 1.0f;
	public float alignmentWeight = 1.0f;
	public float cohesionWeight = 1.0f;
	private Animation _animation;
	private float _animRandomTime = 2;

	private Transform target;

	private void Start()
	{
		target = GameObject.Find("Target").transform;
		_startVelocity = velocity;
		InvokeRepeating("CalcForce",0,CheckInterval);
		_animation = GetComponentInChildren<Animation>();
		Invoke("PlayAnim", Random.Range(0, _animRandomTime));
		
	}

	private void Update()
	{
		velocity += (_sumForce / mass) * Time.deltaTime;
		transform.rotation =Quaternion.Slerp(
			transform.rotation, Quaternion.LookRotation(velocity),Time.deltaTime*3);
		transform.Translate(transform.forward*velocity.magnitude*Time.deltaTime,Space.Self);
	}


	private void CalcForce()
	{
		ResetForce();
		//计算分离力
		Collider[] colliders= Physics.OverlapSphere(transform.position, _separationDistance);
		_separationNeighbors.Clear();
		foreach (Collider col in colliders)
		{
			if (col!=null&&col.gameObject!=this.gameObject)
			{
				_separationNeighbors.Add(col.gameObject);
			}
		}
		foreach (GameObject neighbor in _separationNeighbors)
		{
			Vector3 dir = transform.position - neighbor.transform.position;
			_separationForce += dir.normalized/dir.magnitude;
		}
		if (_separationNeighbors.Count>0)
		{
			_separationForce *= separationWeight;
			_sumForce += _separationForce;
		}
		//计算队列的力
		colliders = Physics.OverlapSphere(transform.position, _alignmentDistance);
		_alignmentNeighbors.Clear();
		foreach (Collider col in colliders)
		{
			if (col!=null&&col.gameObject!=this.gameObject)
			{
				_alignmentNeighbors.Add(col.gameObject);
			}
		}
		avgDir=Vector3.zero;
		foreach (GameObject neighbor in _alignmentNeighbors)
		{
			avgDir += neighbor.transform.forward;
		}
		if (_alignmentNeighbors.Count>0)
		{
			avgDir/=_alignmentNeighbors.Count;
			_alignmentForce= avgDir - transform.forward;
			_alignmentForce *= alignmentWeight;
			_sumForce += _alignmentForce;
		}
		//聚集力
		if (_alignmentNeighbors.Count > 0)
		{
			Vector3 center = Vector3.zero;
			foreach (GameObject neighbor in _alignmentNeighbors)
			{
				center += neighbor.transform.position;
			}
			center /= _alignmentNeighbors.Count;
			_cohesionForce += center - transform.position;
			_cohesionForce *= cohesionWeight;
			_sumForce += _cohesionForce;
		}
		//保持恒定飞行速度
		_sumForce+=(velocity.normalized * _startVelocity.magnitude)*0.1f;

		Vector3 targetDir= target.position - transform.position;
		_sumForce+= (targetDir.normalized - transform.forward)*_speed;
	}

	private void ResetForce()
	{
		_sumForce = Vector3.zero;
		_separationForce=Vector3.zero;
		_alignmentForce=Vector3.zero;
		_cohesionForce=Vector3.zero;
	}
	void PlayAnim()
	{
		_animation.Play();
	}

}
