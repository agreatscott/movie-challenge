using System;
using System.Collections.Generic;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge.Models
{
    class MovieDataItem
    {
        private Dictionary<MovieDataField, string> DataMap { get; } = new Dictionary<MovieDataField, string>();

        public MovieDataItem()
        {
            foreach (MovieDataField dataField in Enum.GetValues(typeof(MovieDataField)))
            {
                if (dataField != MovieDataField.UnknownOrInvalidField)
                {
                    this.DataMap[dataField] = "";
                }
            }
        }

        public string getValue(MovieDataField dataField) {
            if (dataField == MovieDataField.UnknownOrInvalidField) {
                throw new ArgumentException("invalid movie data field");
            }
            return this.DataMap[dataField];
        }

        public void setValue(MovieDataField dataField, string value) {
            if (dataField == MovieDataField.UnknownOrInvalidField) {
                throw new ArgumentException("invalid movie data field");
            }
            this.DataMap[dataField] = value;
        }
    }
}