using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp
{
	public class ReadOnlyList<T> : IEnumerable<T>, IEnumerable
	{
		private readonly List<T> _baseList = new List<T>();

		public T this[int index]
		{
			get
			{
				return this._baseList[index];
			}
			internal set
			{
				this._baseList[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._baseList.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public int IndexOf(T item)
		{
			return this._baseList.IndexOf(item);
		}

		public bool Contains(T item)
		{
			return this._baseList.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this._baseList.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._baseList.GetEnumerator();
		}


		internal void Insert(int index, T item)
		{
			this._baseList.Insert(index, item);
		}

		internal void RemoveAt(int index)
		{
			this._baseList.RemoveAt(index);
		}

		internal void Add(T item)
		{
			this._baseList.Add(item);
		}

		internal void AddRange(IEnumerable<T> collection)
		{
			this._baseList.AddRange(collection);
		}

		internal void Clear()
		{
			this._baseList.Clear();
		}

		internal bool Remove(T item)
		{
			return this._baseList.Remove(item);
		}

		public T Find(Predicate<T> match)
		{
			return this._baseList.Find(match);
		}

        IEnumerator IEnumerable.GetEnumerator()
        {
			return this._baseList.GetEnumerator();
		}
    }
}
