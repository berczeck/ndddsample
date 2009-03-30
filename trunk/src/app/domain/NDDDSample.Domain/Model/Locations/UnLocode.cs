﻿namespace NDDDSample.Domain.Model.Locations
{
    #region Usings

    using System.Text.RegularExpressions;
    using Shared;
    using TempHelper;

    #endregion

    /// <summary>
    /// United nations location code.
    /// http://www.unece.org/cefact/locode/
    /// http://www.unece.org/cefact/locode/DocColumnDescription.htm#LOCODE</summary>
    public class UnLocode : IValueObject<UnLocode>
    {
        private readonly string unlocode;

        // Country code is exactly two letters.
        // Location code is usually three letters, but may contain the numbers 2-9 as well
        private static readonly Regex VALID_PATTERN = new Regex("[a-zA-Z]{2}[a-zA-Z2-9]{3}", RegexOptions.Compiled);

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="countryAndLocation">Location string</param>
        public UnLocode(string countryAndLocation)
        {
            Validate.notNull(countryAndLocation, "Country and location may not be null");
            Validate.isTrue(VALID_PATTERN.Match(countryAndLocation).Success,
                            countryAndLocation + " is not a valid UN/LOCODE (does not match pattern)");

            unlocode = countryAndLocation.ToUpper();
        }

        /// <summary>
        /// Get code and location code concatenated, always upper case.
        /// </summary>
        /// <returns>code and location code concatenated, always upper case.</returns>
        public string IdString()
        {
            return unlocode;
        }


       

        /// <summary>
        /// Value objects compare by the values of their attributes, they don't have an identity.
        /// </summary>
        /// <param name="other">The other value object.</param>
        /// <returns>true if the given value object's and this value object's attributes are the same.</returns>
        public bool SameValueAs(UnLocode other)
        {
            return other != null && unlocode.Equals(other.unlocode);
        }



        #region Object's Override 

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (UnLocode)obj;

            return SameValueAs(other);
        }

        public override int GetHashCode()
        {
            return unlocode.GetHashCode();
        }
     
        public override string ToString()
        {
            return IdString();
        }

        #endregion


        protected UnLocode()
        {
            // Needed by Hibernate
        }
    }
}