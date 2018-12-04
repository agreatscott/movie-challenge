using System;
using System.Collections.Generic;
using MoviesChallenge.Models.Enums;

namespace MoviesChallenge.Models
{
    class InputMetaData
    {
        private Dictionary<MovieDataField, MetaDataItem> MetaDataMap { get; } = new Dictionary<MovieDataField, MetaDataItem>();

        public InputMetaData()
        {
            foreach (MovieDataField dataField in Enum.GetValues(typeof(MovieDataField)))
            {
                //Enum.TryParse<MovieDataField>(value, out MovieDataField movieDataField);
                if (dataField != MovieDataField.UnknownOrInvalidField)
                {
                    this.MetaDataMap[dataField] = new MetaDataItem();
                }
            }
        }

        public MetaDataItem getMetaDataValue(MovieDataField movieDataField)
        {
            if (movieDataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            return this.MetaDataMap[movieDataField];

        }
        public void setMetaDataHeaderValue(MovieDataField movieDataField, string valueHeader)
        {
            if (movieDataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            this.MetaDataMap[movieDataField].ValueHeader = valueHeader;
        }
        public void setMetaDataIndexValue(MovieDataField movieDataField, int valueIndex)
        {
            if (movieDataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            this.MetaDataMap[movieDataField].ValueIndex = valueIndex;
        }


        public MovieDataField getMovieDataFieldFromHeaderValue(string headerValue) /* this assumes that header values are unique */
        {
            foreach (KeyValuePair<MovieDataField, MetaDataItem> entry in this.MetaDataMap)
            {
                if (entry.Value != null && entry.Value.ValueHeader == headerValue)
                {
                    return entry.Key;
                }
            }
            throw new ArgumentException("invalid movie data field");
        }
        public MovieDataField getMovieDataFieldFromIndexValue(int indexValue) /* this assumes that index values are unique */
        {
            foreach (KeyValuePair<MovieDataField, MetaDataItem> entry in this.MetaDataMap)
            {
                if (entry.Value != null && entry.Value.ValueIndex == indexValue)
                {
                    return entry.Key;
                }
            }
            throw new ArgumentException("invalid movie data field");
        }
    }
}