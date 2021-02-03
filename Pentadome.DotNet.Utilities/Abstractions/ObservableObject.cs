using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Pentadome.DotNet.Utilities
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private static bool _cacheEventArguments = true;
        private static ConcurrentDictionary<string, PropertyChangedEventArgs>? _cachedPropertyChangedEventArguments;
        private static ConcurrentDictionary<string, PropertyChangingEventArgs>? _cachedPropertyChangingEventArguments;

        private static ConcurrentDictionary<string, PropertyChangedEventArgs> CachedPropertyChangedEventArguments =>
            _cachedPropertyChangedEventArguments ??= new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        private static ConcurrentDictionary<string, PropertyChangingEventArgs> CachedPropertyChangingEventArguments =>
            _cachedPropertyChangingEventArguments ??= new ConcurrentDictionary<string, PropertyChangingEventArgs>();

        public static void EnableEventArgumentsCaching() => _cacheEventArguments = true;

        public static void DisableEventArgumentsCaching()
        {
            _cacheEventArguments = false;
            _cachedPropertyChangedEventArguments = null;
            _cachedPropertyChangingEventArguments = null;
        }

        /// <summary>
        /// <para>
        /// Changes value of <paramref name="backingStore"/> to <paramref name="value"/> if they are not equal.
        /// Before changing, fires a <see cref="INotifyPropertyChanging.PropertyChanging"/> <see langword="event"/>.
        /// After changing, executes <paramref name="onChanged"/> if defined and then fires a <see cref="INotifyPropertyChanged.PropertyChanged"/> <see langword="event"/>.
        /// Returns <see langword="true"/> if changed, otherwise  <see langword="false"/>.
        /// </para>
        /// <para>
        /// <paramref name="propertyName"/> is, using <see cref="CallerMemberNameAttribute"/>, automatically set, but can still be overridden.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingStore">The backing field of the property.</param>
        /// <param name="value">The value to set to the property.</param>
        /// <param name="onChanged">The action to execute if the property has changed.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual bool SetProperty<T>(
#if SUPPORTSNULLATTRIBUTES
            [AllowNull, MaybeNull, NotNullIfNotNull("value")]
#endif
        ref T backingStore,
            T value,
            Action? onChanged = null,
            [CallerMemberName] string propertyName = "")
        {
#if NETSTANDARD2_1
            // Not appropriately annotated in Dotnet2_1
    #pragma warning disable CS8604 // Possible null reference argument.
#endif

            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

#if NETSTANDARD2_1
    #pragma warning restore CS8604 // Possible null reference argument.
#endif

            Check.StringArgumentNotNullOrWhiteSpace(propertyName, nameof(propertyName));

            OnPropertyChanging(propertyName);
            // backingStore came in as a ref parameter. 
            // Therefore overriding the parameter here also overrides the value where this parameter value came from.
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <summary>
        /// Sends a <see cref="INotifyPropertyChanged.PropertyChanged"/> <see langword="event"/> to all listeners, if any, with <see langword="this"/> as sender and
        /// <paramref name="propertyName"/> as property that has changed.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged is null)
                return;

            Check.StringArgumentNotNullOrWhiteSpace(propertyName, nameof(propertyName));

            var propertyChangedEventArgs = _cacheEventArguments
                ? CachedPropertyChangedEventArguments.GetOrAdd(propertyName, key => new PropertyChangedEventArgs(key))
                : new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }

        /// <summary>
        /// Sends a <see cref="INotifyPropertyChanging.PropertyChanging"/> <see langword="event"/> to all listeners, if any, with <see langword="this"/> as sender and
        /// <paramref name="propertyName"/> as property that is going to change.
        /// </summary>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanging is null)
                return;

            Check.StringArgumentNotNullOrWhiteSpace(propertyName, nameof(propertyName));

            var propertyChangingEventArgs = _cacheEventArguments
              ? CachedPropertyChangingEventArguments.GetOrAdd(propertyName, key => new PropertyChangingEventArgs(key))
              : new PropertyChangingEventArgs(propertyName);

            PropertyChanging?.Invoke(this, propertyChangingEventArgs);
        }
    }
}
