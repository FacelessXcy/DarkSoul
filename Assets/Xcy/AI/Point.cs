using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Point
{
	private GameObject _gameObject;
	private Point _parent;
	private float _f;
	private float _g;
	private float _h;
	private int _x;
	private int _y;
	private bool _isWall;

	public GameObject GameObject
	{
		get => _gameObject;
		set => _gameObject = value;
	}

	public Point Parent
	{
		get => _parent;
		set => _parent = value;
	}

	public float F
	{
		get => _f;
		set => _f = value;
	}

	public float G
	{
		get => _g;
		set => _g = value;
	}

	public float H
	{
		get => _h;
		set => _h = value;
	}

	public int X
	{
		get => _x;
		set => _x = value;
	}

	public int Y
	{
		get => _y;
		set => _y = value;
	}


	public bool IsWall
	{
		get => _isWall;
		set => _isWall = value;
	}

	public Point(int x, int y, Point parent=null)
	{
		_x = x;
		_y = y;
		_parent = parent;
		_isWall = false;
	}

	public void UpdateParent(Point parent,float g)
	{
		this._parent = parent;
		this._g = g;
		this._f = _g + _h;
	}


}
