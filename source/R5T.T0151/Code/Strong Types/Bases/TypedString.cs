using System;

using R5T.T0150;


namespace R5T.T0151
{
    /// <summary>
    /// Allows wrapping a string with a specific type to allow extra context to be communicated.
    /// This is helpful in creating strongly-typed strings for stringly-typed data. Examples:
    ///     * It's not a string, it's a diretory path.
    ///     * It's not a string, it's an AWS S3 Bucket Name.
    /// Value is read-only.
    /// </summary>
    /// <remarks>
    /// Many objects are "stringly-typed". For example, project name, project directory name, and project directory path are all strings, but are really different types of string.
    /// Creating methods that operate on these different types of string is clumsy. Because overloading is not possible (all methods take in the same argument type, string), methods must have different names.
    /// These extra names require more effort by the writer, but by the user, since they have to sort through the confusion caused by having multiple names for what is really the same operation being applied to different input types.
    /// The resolution to this difficulty is creating types that merely contain a string.
    /// This base type provides the actual functionality, inheritors just provide a name for their type of string.
    /// </remarks>
    [StrongTypeMarker]
    public abstract class TypedString : IEquatable<TypedString>, IComparable<TypedString>
    {
        #region Static

        public static bool operator ==(TypedString a, TypedString b)
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

        public static bool operator !=(TypedString a, TypedString b)
        {
            var output = !(a == b);
            return output;
        }

        /// <summary>
        /// Allow comparison directly to a string.
        /// </summary>
        public static bool operator ==(TypedString a, string b)
        {
            if (a is null)
            {
                var output = b is null;
                return output;
            }
            else
            {
                var output = a.Value == b;
                return output;
            }
        }

        /// <inheritdoc cref="operator ==(TypedString, string)"/>
        public static bool operator !=(TypedString a, string b)
        {
            var equals = a == b;

            var output = !equals;
            return output;
        }

        /// <summary>
        /// Allow implicit conversion to string, allowing a <see cref="TypedString"/> to be just as good as a string when required.
        /// </summary>
        public static implicit operator string(TypedString typedString)
        {
            return typedString.Value;
        }

        #endregion


        public string Value { get; }


        // Protected constructor, in case sub-types want to limit access.
        protected TypedString(string value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            // No type-check required since performing GetType() on (obj as TypedString) will still return the actual original type.

            var objAsTypedString = obj as TypedString;

            var isEqual = this.Equals(objAsTypedString);
            return isEqual;
        }

        protected virtual bool Equals_ByValue(TypedString other)
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
            return this.Value;
        }

        public bool Equals(TypedString other)
        {
            var otherIsNull = other is null;
            if(otherIsNull)
            {
                // This instance can't be null (since we are calling a method on it), so if the other is null, then the two instance are not equal.
                return false;
            }

            // Required type-check for derived classes using the base class TypedString.Equals(TypedString).
            var typesAreSame = other.GetType().Equals(this.GetType());
            if (!typesAreSame)
            {
                return false;
            }

            var isEqual = this.Equals_ByValue(other);
            return isEqual;
        }

        public int CompareTo(TypedString other)
        {
            var output = this.Value.CompareTo(other.Value);
            return output;
        }
    }
}
