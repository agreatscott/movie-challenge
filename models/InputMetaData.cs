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
        private void setMetaDataHeaderValue(MovieDataField movieDataField, string valueHeader)
        {
            if (movieDataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            this.MetaDataMap[movieDataField].FieldHeader = valueHeader;
        }
        private void SetMetaDataIndexValue(MovieDataField movieDataField, int valueIndex)
        {
            if (movieDataField == MovieDataField.UnknownOrInvalidField)
            {
                throw new ArgumentException("invalid movie data field");
            }
            this.MetaDataMap[movieDataField].FieldIndex = valueIndex;
        }
        public MovieDataField GetMovieDataFieldFromHeaderValue(string headerValue) /* this assumes that header values are unique */
        {
            foreach (KeyValuePair<MovieDataField, MetaDataItem> entry in this.MetaDataMap)
            {
                if (entry.Value != null && entry.Value.FieldHeader == headerValue)
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
                if (entry.Value != null && entry.Value.FieldIndex == indexValue)
                {
                    return entry.Key;
                }
            }
            throw new ArgumentException("invalid movie data field");
        }


        public void MapConfigAndInputMetaData(IDictionary<string, string> configInputFileHeaders, string headerInputLine)
        {
            MapInputValueHeadersFromConfig(configInputFileHeaders);
            GetInputDataIndicies(headerInputLine);
        }

        private void MapInputValueHeadersFromConfig(IDictionary<string, string> configInputFileHeaders)
        {
            foreach (string key in configInputFileHeaders.Keys)
            {
                Enum.TryParse(key, out MovieDataField dataField);
                setMetaDataHeaderValue(dataField, configInputFileHeaders[key]);
            }
        }

        private void GetInputDataIndicies(string headerInputLine)
        {
            //tell me which index in a line of input is each value of MovieDataItem
            var lineArr = headerInputLine.Split(',');
            for (int index = 0; index < lineArr.Length; index++)
            {
                MovieDataField dataField = GetMovieDataFieldFromHeaderValue(lineArr[index].Trim());
                SetMetaDataIndexValue(dataField, index);
            }
        }
    }
}