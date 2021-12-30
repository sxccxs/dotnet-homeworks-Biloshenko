using Core.Exceptions;
using System.Collections;

namespace Core.Collections
{

    public class CustomDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private KeyValuePair<TKey, TValue>[] _values;

        public int Count => _values.Length;

        public CustomDictionary()
        {
            _values = Array.Empty<KeyValuePair<TKey, TValue>>();
        }


        public void Add(TKey aKey, TValue aValue)
        {
            if (aKey is null) { throw new ArgumentNullException("Key can not be null"); }
            if (Contains(aKey)) { throw new DublicateKeyException($"Key {aKey} already exists"); }
            _values = _values.Append(new KeyValuePair<TKey, TValue>(aKey, aValue)).ToArray();
        }

        public TValue Get(TKey aKey)
        {
            if (!Contains(aKey))
            {
                throw new KeyNotFoundException($"Key {aKey} does not exist.");
            }
            return _values.Where(x => x.Key.Equals(aKey)).ToArray()[0].Value;

        }

        public bool Contains(TKey aKey)
        {
            return _values.Where(x => x.Key.Equals(aKey)).Any();
        }

        public void Remove(TKey aKey)
        {
            if (!Contains(aKey))
            {
                throw new KeyNotFoundException($"Key {aKey} does not exist");
            }
            _values = _values.Where(x => !x.Key.Equals(aKey)).ToArray();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }
        }



        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _values.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", _values.Select(x => $"{x.Key}: {x.Value}"));
        }

    }
}
