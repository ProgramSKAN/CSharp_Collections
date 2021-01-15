using System;
using System.Collections.Generic;
using System.Text;

namespace Collections.Stack
{
	public static class LinkedListExtension
	{
		public static LinkedListNode<T> GetNthNode<T>(this LinkedList<T> lst, int n)
		{
			LinkedListNode<T> currentNode = lst.First;
			for (int i = 0; i < n; i++)
				currentNode = currentNode.Next;
			return currentNode;
		}
	}
}
