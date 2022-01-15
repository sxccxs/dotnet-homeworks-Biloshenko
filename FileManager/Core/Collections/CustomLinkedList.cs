using System.Collections;

namespace Core.Collections
{
    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private Node<T> root;

        public int Count => this.GetLength(this.root);

        public T this[int index]
        {
            get => this.At(index);
            set => this.Set(index, value);
        }

        public void Add(T value)
        {
            if (this.root is null)
            {
                this.root = new Node<T>(value);
            }
            else
            {
                var node = this.FindLast(this.root);
                node.Next = new Node<T>(value);
            }
        }

        public T Pop()
        {
            if (this.root is null)
            {
                throw new InvalidOperationException("Can't pop from empty list");
            }
            else if (this.root.Next is null)
            {
                var v = this.root.Value;
                this.root = null;

                return v;
            }
            else
            {
                return this.RemoveLast(this.root);
            }
        }

        public void Remove(int index)
        {
            if (index == 0 && this.root != null)
            {
                this.root = this.root.Next;
            }
            else
            {
                var previous = this.GetNodeWithIndex(this.root, index - 1);
                var next = this.GetNodeWithIndex(this.root, index + 1);
                previous.Next = next;
            }
        }

        public T At(int index)
        {
            return this.GetNodeWithIndex(this.root, index).Value;
        }

        public void Set(int index, T value)
        {
            this.GetNodeWithIndex(this.root, index).Value = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cur = this.root;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Node<T> GetNodeWithIndex(Node<T> node, int index, int searchIndex = 0)
        {
            if (node is null || index < 0)
            {
                throw new IndexOutOfRangeException($"Index {searchIndex} is out of bounds");
            }
            else if (index == searchIndex)
            {
                return node;
            }
            else
            {
                return this.GetNodeWithIndex(node.Next, index, searchIndex + 1);
            }
        }

        private T RemoveLast(Node<T> node)
        {
            if (node is null || node.Next is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Next.Next is null)
            {
                var v = node.Next.Value;
                node.Next = null;

                return v;
            }

            return this.RemoveLast(node);
        }

        private Node<T> FindLast(Node<T> node)
        {
            if (node.Next is null)
            {
                return node;
            }

            return this.FindLast(node.Next);
        }

        private int GetLength(Node<T> node, int length = 0)
        {
            if (node is null)
            {
                return length;
            }

            return this.GetLength(node.Next, length + 1);
        }

        private class Node<TInner>
        {
            public Node(TInner value, Node<TInner> next = null)
            {
                this.Value = value;
                this.Next = next;
            }

            public TInner Value { get; set; }

            public Node<TInner> Next { get; set; }
        }
    }
}
