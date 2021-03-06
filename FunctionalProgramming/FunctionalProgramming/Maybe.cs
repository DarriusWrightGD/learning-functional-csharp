﻿using NullGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public struct Maybe<T> : IEquatable<Maybe<T>> where T : class
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (HasNoValue)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        public bool HasValue => _value != null;

        public bool HasNoValue => !HasValue;

        private Maybe([AllowNull]T value)
        {
            _value = value;
        }

        public static implicit operator Maybe<T>([AllowNull]T value)
        {
            return new Maybe<T>(value);
        }

        public static bool operator==(Maybe<T> maybe, T value)
        {
            if (maybe.HasNoValue)
                return false;

            return maybe.Value.Equals(value);
        }


        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Maybe<T>))
                return false;

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return (HasValue) ? _value.ToString() : "No Value";
        }

        [return:AllowNull]
        public T Unwarp([AllowNull]T defaultValue = default(T))
        {
            return (HasValue) ? Value : defaultValue;
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
                return true;

            if (HasNoValue || other.HasNoValue)
                return false;

            return _value.Equals(other.Value);
        }
    }
}
