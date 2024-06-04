using System;

namespace TileBeat.scripts.GameUtils
{
    public abstract record Maybe<T>
    {
        public abstract Maybe<T> ForEach(Action<T> action);
        public abstract Maybe<S> Map<S>(Func<T, S> func);
        public abstract Maybe<S> FlatMap<S>(Func<T, Maybe<S>> func);
        public abstract T GetOrElse(T otherwise);
        public abstract bool IsPresent();
        public abstract S Fold<S>(Func<T, S> right, S left);
        public static Maybe<T> Of(T value)
        {
            return value != null ? new Some<T>(value) : new None<T>();
        }
    }

    public record Some<T>(T value) : Maybe<T>
    {
        public override Maybe<S> FlatMap<S>(Func<T, Maybe<S>> func)
        {
            return func(value);
        }

        public override S Fold<S>(Func<T, S> right, S left)
        {
            return right(value);
        }

        public override Maybe<T> ForEach(Action<T> action)
        {
            action(value);
            return this;
        }

        public override T GetOrElse(T otherwise)
        {
            return value;
        }

        public override bool IsPresent()
        {
            return true;
        }

        public override Maybe<S> Map<S>(Func<T, S> func)
        {
            return FlatMap(v => new Some<S>(func(v)));
        }
    }

    public record None<T>() : Maybe<T>
    {
        public override Maybe<S> FlatMap<S>(Func<T, Maybe<S>> func)
        {
            return new None<S>();
        }

        public override S Fold<S>(Func<T, S> right, S left)
        {
            return left;
        }

        public override Maybe<T> ForEach(Action<T> action)
        {
            return this;
        }

        public override T GetOrElse(T otherwise)
        {
            return otherwise;
        }

        public override bool IsPresent()
        {
            return false;
        }

        public override Maybe<S> Map<S>(Func<T, S> func)
        {
            return FlatMap(v => new None<S>());
        }
    }
}
