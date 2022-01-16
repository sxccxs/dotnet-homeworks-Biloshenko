using System.Collections;
using Core.Exceptions;

namespace Core.Collections
{
    public class CustomDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private KeyValuePair<TKey, TValue>[] values;

        public CustomDictionary()
        {
            this.values = Array.Empty<KeyValuePair<TKey, TValue>>();
        }

        public int Count => this.values.Length;

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.Add(key, value);
            }
        }

        public void Add(TKey aKey, TValue aValue)
        {
            if (aKey is null)
            {
                throw new ArgumentNullException("Key can not be null");
            }

            if (this.Contains(aKey))
            {
                throw new DuplicateKeyException($"Key {aKey} already exists");
            }

            this.values = this.values.Append(new KeyValuePair<TKey, TValue>(aKey, aValue)).ToArray();
        }

        public TValue Get(TKey aKey)
        {
            if (!this.Contains(aKey))
            {
                throw new KeyNotFoundException($"Key {aKey} does not exist.");
            }

            return this.values.Where(x => x.Key.Equals(aKey)).ToArray()[0].Value;
        }

        public bool Contains(TKey aKey)
        {
            return this.values.Where(x => x.Key.Equals(aKey)).Any();
        }

        public void Remove(TKey aKey)
        {
            if (!this.Contains(aKey))
            {
                throw new KeyNotFoundException($"Key {aKey} does not exist");
            }

            this.values = this.values.Where(x => !x.Key.Equals(aKey)).ToArray();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.values.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", this.values.Select(x => $"{x.Key}: {x.Value}"));
        }
    }
}
