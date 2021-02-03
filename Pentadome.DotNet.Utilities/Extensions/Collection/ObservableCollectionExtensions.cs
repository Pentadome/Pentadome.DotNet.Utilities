using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public static class ObservableCollectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return new ObservableCollection<T>(@this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollection<T> ToObservable<T>(this List<T> @this)
        {
            if (@this is null)
                throw new ArgumentNullException(nameof(@this));

            return new ObservableCollection<T>(@this);
        }
    }
}

