using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
	public class JoinedEnumerable<T> : IEnumerable<T>
	{
		public readonly IEnumerable<T> Source;
		public bool IsOuter;

		public JoinedEnumerable(IEnumerable<T> source) { Source = source; }

		IEnumerator<T> IEnumerable<T>.GetEnumerator() { return Source.GetEnumerator(); }
		IEnumerator IEnumerable.GetEnumerator() { return Source.GetEnumerator(); }
	}

	public static class JoinedEnumerable
	{
		public static JoinedEnumerable<TElement> Inner<TElement>(this IEnumerable<TElement> source)
		{
			return Wrap(source, false);
		}

		public static JoinedEnumerable<TElement> Outer<TElement>(this IEnumerable<TElement> source)
		{
			return Wrap(source, true);
		}

		public static JoinedEnumerable<TElement> Wrap<TElement>(IEnumerable<TElement> source, bool isOuter)
		{
			var joinedSource = source as JoinedEnumerable<TElement> ?? new JoinedEnumerable<TElement>(source);
			joinedSource.IsOuter = isOuter;
			return joinedSource;
		}
	}

	public static class LinqExtensions
	{
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>
			(this JoinedEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer = null)
		{
			if (null == outer) throw new ArgumentNullException("outer");
			if (null == inner) throw new ArgumentNullException("inner");
			if (null == outerKeySelector) throw new ArgumentNullException("outerKeySelector");
			if (null == innerKeySelector) throw new ArgumentNullException("innerKeySelector");
			if (null == resultSelector) throw new ArgumentNullException("resultSelector");

			bool leftOuter = outer.IsOuter;
			bool rightOuter = (inner is JoinedEnumerable<TInner>) && ((JoinedEnumerable<TInner>)inner).IsOuter;

			if (leftOuter && rightOuter)
				return FullOuterJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

			if (leftOuter)
				return LeftOuterJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

			if (rightOuter)
				return RightOuterJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

			return Enumerable.Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>
			(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer = null)
		{
			ILookup<TKey, TInner> innerLookup = inner.ToLookup(innerKeySelector, comparer);

			foreach (TOuter outerItem in outer)
				foreach (TInner innerItem in innerLookup[outerKeySelector(outerItem)].DefaultIfEmpty())
					yield return resultSelector(outerItem, innerItem);
		}

		public static IEnumerable<TResult> RightOuterJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer = null)
		{
			ILookup<TKey, TOuter> outerLookup = outer.ToLookup(outerKeySelector, comparer);

			foreach (TInner innerItem in inner)
				foreach (TOuter outerItem in outerLookup[innerKeySelector(innerItem)].DefaultIfEmpty())
					yield return resultSelector(outerItem, innerItem);
		}

		public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>
			(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer = null)
		{
			ILookup<TKey, TOuter> outerLookup = outer.ToLookup(outerKeySelector, comparer);
			ILookup<TKey, TInner> innerLookup = inner.ToLookup(innerKeySelector, comparer);

			foreach (IGrouping<TKey, TInner> innerGrouping in innerLookup)
				if ( ! outerLookup.Contains(innerGrouping.Key))
					foreach (TInner innerItem in innerGrouping)
						yield return resultSelector(default(TOuter), innerItem);

			foreach (IGrouping<TKey, TOuter> outerGrouping in outerLookup)
				foreach (TInner innerItem in innerLookup[outerGrouping.Key].DefaultIfEmpty())
					foreach (TOuter outerItem in outerGrouping)
						yield return resultSelector(outerItem, innerItem);
		}
	}
}
