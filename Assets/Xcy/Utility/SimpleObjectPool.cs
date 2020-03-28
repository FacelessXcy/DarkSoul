using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Utility
{
	public interface IPool<T>
	{
		T Allocate();
		bool Recycle(T obj);
	}

	public interface IObjectFactory<T>
	{
		T Create();
	}

	public abstract class Pool<T> : IPool<T>
	{
		protected Stack<T> _cachedStack=new Stack<T>();
		protected IObjectFactory<T> _objectFactory;

		public int CurrentCount => _cachedStack.Count;
		
		public virtual T Allocate()
		{
			return _cachedStack.Count > 0 ? _cachedStack.Pop() : _objectFactory.Create();
		}

		public abstract bool Recycle(T obj);
	}

	public class CustomObjectFactory<T> : IObjectFactory<T>
	{
		private Func<T> _factoryMethod;
		public CustomObjectFactory(Func<T> factoryMethod)
		{
			_factoryMethod = factoryMethod;
		}

		public T Create()
		{
			return _factoryMethod();
		}
	}

	public class SimpleObjectPool<T> : Pool<T>
	{
		private Action<T> _resetMethod;

		public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
		{
			_objectFactory=new CustomObjectFactory<T>(factoryMethod);
			_resetMethod = resetMethod;
			for (int i = 0; i < initCount; i++)
			{
				_cachedStack.Push(_objectFactory.Create());
			}
		}

		public override bool Recycle(T obj)
		{
			if (_resetMethod!=null)
			{
				_resetMethod(obj);
			}
			_cachedStack.Push(obj);
			return true;
		}
	}
}