using System;

using R5T.T0150;


namespace R5T.T0151
{
    [StrongTypeMarker]
    public abstract class TypedGuid : IEquatable<TypedGuid>, IComparable<TypedGuid>
    {
        #region Static

        public static bool operator ==(TypedGuid a, TypedGuid b)
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

        public static bool operator !=(TypedGuid a, TypedGuid b)
        {
            var output = !(a == b);
            return output;
        }

        /// <summary>
        /// Allow comparison directly to a Guid.
        /// </summary>
        public static bool operator ==(TypedGuid a, Guid b)
        {
            if (a is null)
            {
                // Cannot compare null (for reference types) to an Guid (a value type).
                return false;
            }
            else
            {
                var output = a.Value == b;
                return output;
            }
        }

        /// <inheritdoc cref="operator ==(TypedGuid, Guid)"/>
        public static bool operator !=(TypedGuid a, Guid b)
        {
            var equals = a == b;

            var output = !equals;
            return output;
        }

        /// <summary>
        /// Allow implicit conversion to Guid, allowing a <see cref="TypedGuid"/> to be just as good as an Guid when required.
        /// </summary>
        public static implicit operator Guid(TypedGuid TypedGuid)
        {
            return TypedGuid.Value;
        }

        #endregion


        public Guid Value { get; }


        // Protected constructor, in case sub-types want to limit access.
        protected TypedGuid(Guid value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            // No type-check required since performing GetType() on (obj as TypedGuid) will still return the actual original type.

            var objAsTypedGuid = obj as TypedGuid;

            var isEqual = this.Equals(objAsTypedGuid);
            return isEqual;
        }

        protected virtual bool Equals_ByValue(TypedGuid other)
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

        public bool Equals(TypedGuid other)
        {
            var otherIsNull = other is null;
            if(otherIsNull)
            {
                // This instance can't be null (since we are calling a method on it), so if the other is null, then the two instance are not equal.
                return false;
            }

            // Required type-check for derived classes using the base class TypedGuid.Equals(TypedGuid).
            var typesAreSame = other.GetType().Equals(this.GetType());
            if (!typesAreSame)
            {
                return false;
            }

            var isEqual = this.Equals_ByValue(other);
            return isEqual;
        }

        public int CompareTo(TypedGuid other)
        {
            var output = this.Value.CompareTo(other.Value);
            return output;
        }
    }
}
