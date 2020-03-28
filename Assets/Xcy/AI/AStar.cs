using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AStar : MonoBehaviour
{
	private int _mapWidth = 20;
	private int _mapHeight = 20;
	private Point[,] _map=new Point[20,20];
	private Point _start;
	private Point _end;


	private void Start()
	{
		InitMap();
		InitCube();
		_start = _map[2, 3];
		_start.GameObject.GetComponent<Renderer>().material.color=Color.green;
		//FindPath(start,end);
		//ShowPath(start,end);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit))
			{
				Debug.Log(hit.collider.gameObject.transform.position);
				_end = _map[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y];
				FindPath(_start,_end);
				ShowPath(_start,_end);
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			ClearCube();
		}
	}

	private void InitMap()
	{
		for (int x = 0; x < _mapWidth; x++)
		{
			for (int y = 0; y < _mapHeight; y++)
			{
				_map[x,y]=new Point(x,y);
			}
		}
		_map[4, 2].IsWall = true;
		_map[4, 3].IsWall = true;
		_map[4, 4].IsWall = true;
	}

	private void InitCube()
	{
		for (int x = 0; x < _mapWidth; x++)
		{
			for (int y = 0; y < _mapHeight; y++)
			{
				if (_map[x,y].IsWall)
				{
					_map[x,y].GameObject=CreateCube(x,y,Color.blue);
				}
				else
				{
					_map[x,y].GameObject=CreateCube(x,y,Color.grey);
				}

			}
		}
	}

	private void ClearCube()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}


	private void FindPath(Point start,Point end)
	{
		List<Point> openList=new List<Point>();
		List<Point> closeList=new List<Point>();
		openList.Add(start);
		while (openList.Count!=0)
		{
			Point point = FindMinFPoint(openList);
			openList.Remove(point);
			closeList.Add(point);
			List<Point> surroundPoints = GetSurroundPoints(point);
			PointsFilter(surroundPoints,closeList);
			foreach (Point s in surroundPoints)
			{
				if (openList.Contains(s))
				{
					float nowG = CalcG(s, point);
					if (s.G>nowG)
					{
						s.UpdateParent(point,nowG);
					}
				}
				else
				{
					s.Parent = point;
					CalcF(s,end);
					openList.Add(s);
				}
			}

			if (openList.Contains(end))
			{
				break;
			}
		}
		
		
	}

	private GameObject CreateCube(int x,int y,Color color)
	{
		GameObject go= GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.transform.position=new Vector3(x,y,0);
		go.GetComponent<Renderer>().material.color = color;
		return go;
	}

	private void ShowPath(Point start,Point end)
	{
		Point temp = end;
		while (true)
		{
			//Debug.Log(temp.X+","+temp.Y);
			Color defColor=Color.yellow;
			if (temp==end)
			{
				defColor=Color.red;
				_map[temp.X, temp.Y].GameObject.GetComponent<Renderer>().material.color = defColor;
			}else if (temp!=start)
			{
				defColor=Color.yellow;
				_map[temp.X, temp.Y].GameObject.GetComponent<Renderer>().material.color = defColor;
			}
			if (temp.Parent==null)
				break;
			temp = temp.Parent;
		}
		
	}

	private void PointsFilter(List<Point> src,List<Point> closeList)
	{
		foreach (Point point in closeList)
		{
			if (src.Contains(point))
			{
				src.Remove(point);
			}
		}
	}

	private List<Point> GetSurroundPoints(Point point)
	{
		List<Point> list=new List<Point>();
		Point up = null, down = null, left = null, right = null;
		Point lu= null, ru= null, ld= null, rd= null;
		if (point.Y<_mapHeight-1)
		{
			up = _map[point.X, point.Y + 1];
			if (!up.IsWall)
			{
				list.Add(up);
			}
		}
		if (point.Y>0)
		{
			down = _map[point.X, point.Y - 1];
			if (!down.IsWall)
			{
				list.Add(down);
			}
		}
		if (point.X>0)
		{
			left = _map[point.X - 1, point.Y];
			if (!left.IsWall)
			{
				list.Add(left);
			}
		}
		if (point.X<_mapWidth-1)
		{
			right = _map[point.X + 1, point.Y];
			if (!right.IsWall)
			{
				list.Add(right);
			}
		}
		
		if (up!=null&&left!=null)
		{
			lu = _map[point.X - 1, point.Y + 1];
			if ((!lu.IsWall)&&(!left.IsWall&&!up.IsWall))
			{
				list.Add(lu);
			}
		}
		if (up!=null&&right!=null)
		{
			ru = _map[point.X + 1, point.Y + 1];
			if ((!ru.IsWall)&&(!right.IsWall&&!up.IsWall))
			{
				list.Add(ru);
			}
		}
		if (down!=null&&left!=null)
		{
			ld = _map[point.X - 1, point.Y - 1];
			if ((!ld.IsWall)&&(!left.IsWall&&!down.IsWall))
			{
				list.Add(ld);
			}
		}
		if (down!=null&&right!=null)
		{
			rd = _map[point.X + 1, point.Y - 1];
			if ((!rd.IsWall)&&(!right.IsWall&&!down.IsWall))
			{
				list.Add(rd);
			}
		}

		return list;
	}

	private Point FindMinFPoint(List<Point> openList)
	{
		Point temp = null;
		float f = float.MaxValue;
		foreach (Point point in openList)
		{
			if (point.F<f)
			{
				temp = point;
				f = point.F;
			}
		}
		return temp;
	}

	private float CalcG(Point now, Point parent)
	{
		return Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(parent.X, parent.Y)) + parent.G;
	}

	private void CalcF(Point now, Point end)
	{
		//F=G+H
		float h = Mathf.Abs(end.X - now.X) + Mathf.Abs(end.Y - now.Y);
		float g = 0;
		if (now.Parent==null)
			g = 0;
		else
		{
			g = Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(now.Parent.X, now.Parent.Y)) + now.Parent.G;
		}
		float f = g + h;
		now.F = f;
		now.G = g;
		now.H = h;
	}



}
