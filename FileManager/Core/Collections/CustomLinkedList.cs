using System.Collections;

namespace Core.Collections
{
    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private Node<T>? root;
        public int Count => GetLength(root);

        public void Add(T value)
        {
            if (root is null)
            {
                root = new Node<T>(value);
            }
            else
            {
                var node = FindLast(root);
                node.Next = new Node<T>(value);
            }
        }
        public T Pop()
        {
            if (root is null)
            {
                throw new InvalidOperationException("Can't pop from empty list");
            }
            else if (root.Next is null)
            {
                var v = root.Value;
                root = null;

                return v;
            }
            else
            {
                return RemoveLast(root);
            }

        }

        public void Remove(int index)
        {

            if (index == 0 && root != null)
            {
                root = root.Next;
            }
            else
            {
                var prev = GetNodeWithIndex(root, index - 1);
                var next = GetNodeWithIndex(root, index + 1);
                prev.Next = next;
            }
        }

        public T At(int index)
        {
            return GetNodeWithIndex(root, index).Value;
        }

        public void Set(int index, T value)
        {
            GetNodeWithIndex(root, index).Value = value;
        }

        public T this[int index]
        {
            get => At(index);
            set => Set(index, value);
        }

        private Node<T> GetNodeWithIndex(Node<T>? node, int index, int i = 0)
        {

            if (node is null || index < 0)
            {
                throw new IndexOutOfRangeException($"Index {i} is out of bounds");
            }
            else if (index == i)
            {
                return node;
            }
            else
            {
                return GetNodeWithIndex(node.Next, index, i + 1);
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
            return RemoveLast(node);


        }

        private Node<T> FindLast(Node<T> node)
        {
            if (node.Next is null)
            {
                return node;
            }
            return FindLast(node.Next);
        }

        private int GetLength(Node<T>? node, int l = 0)
        {
            if (node is null)
            {
                return l;
            }
            return GetLength(node.Next, l + 1);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var cur = root;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node<TInner>
        {
            public TInner Value { get; set; }

            public Node<TInner>? Next { get; set; }

            public Node(TInner value, Node<TInner>? next = null)
            {
                Value = value;
                Next = next;
            }

        }
    }
}
