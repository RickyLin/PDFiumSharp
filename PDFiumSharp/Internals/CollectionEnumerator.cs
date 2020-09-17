using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PDFiumSharp.Internals
{
	internal class CollectionEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		private int _index = -1;

		private IList<T> _collection;

		public T Current
		{
			get
			{
				if (_index < 0 || _index >= _collection.Count)
				{
					throw new InvalidOperationException();
				}
				return _collection[_index];
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public CollectionEnumerator(IList<T> collection)
		{
			_collection = collection;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			if (_index < _collection.Count - 1)
			{
				_index++;
				return true;
			}
			_index = _collection.Count;
			return false;
		}

		public void Reset()
		{
			_index = -1;
		}
	}
}
