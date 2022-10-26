using System;

using R5T.T0150;


namespace R5T.T0151
{
    [StrongTypeMarker]
    public abstract class TypedInteger : IEquatable<TypedInteger>, IComparable<TypedInteger>
    {
        #region Static

        public static bool operator ==(TypedInteger a, TypedInteger b)
        {
            if (a is null)
            {
                var output = b is null;
                return output;
            }
            else
            {
                var output = a.Equals(b);
                return output;
            }
        }

        public static bool operator !=(TypedInteger a, TypedInteger b)
        {
            var output = !(a == b);
            return output;
        }

        /// <summary>
        /// Allow comparison directly to a integer.
        /// </summary>
        public static bool operator ==(TypedInteger a, int b)
        {
            if (a is null)
            {
                // Cannot compare null (for reference types) to an integer (a value type).
                return false;
            }
            else
            {
                var output = a.Value == b;
                return output;
            }
        }

        /// <inheritdoc cref="operator ==(TypedInteger, int)"/>
        public static bool operator !=(TypedInteger a, int b)
        {
            var equals = a == b;

            var output = !equals;
            return output;
        }

        /// <summary>
        /// Allow implicit conversion to integer, allowing a <see cref="TypedInteger"/> to be just as good as an integer when required.
        /// </summary>
        public static implicit operator int(TypedInteger TypedInteger)
        {
            return TypedInteger.Value;
        }

        #endregion


        public int Value { get; }


        // Protected constructor, in case sub-types want to limit access.
        protected TypedInteger(int value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            // No type-check required since performing GetType() on (obj as TypedInteger) will still return the actual original type.

            var objAsTypedInteger = obj as TypedInteger;

            var isEqual = this.Equals(objAsTypedInteger);
            return isEqual;
        }

        protected virtual bool Equals_ByValue(TypedInteger other)
        {
            var isEqual = this.Value.Equals(other.Value);
            return isEqual;
        }

        public override int GetHashCode()
        {
            var hashCode = this.Value.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var representation = this.Value.ToString();
            return representation;
        }

        public bool Equals(TypedInteger other)
        {
            var otherIsNull = other is null;
            if(otherIsNull)
            {
                // This instance can't be null (since we are calling a method on it), so if the other is null, then the two instance are not equal.
                return false;
            }

            // Required type-check for derived classes using the base class TypedInteger.Equals(TypedInteger).
            var typesAreSame = other.GetType().Equals(this.GetType());
            if (!typesAreSame)
            {
                return false;
            }

            var isEqual = this.Equals_ByValue(other);
            return isEqual;
        }

        public int CompareTo(TypedInteger other)
        {
            var output = this.Value.CompareTo(other.Value);
            return output;
        }
    }
}
