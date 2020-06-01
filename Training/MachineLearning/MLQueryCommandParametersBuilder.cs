using System;
using System.Collections.Generic;
using commercetools.Sdk.Domain;
using commercetools.Sdk.HttpApi.SearchParameters;

namespace Training.MachineLearningExtensions
{
    public class MLQueryCommandParametersBuilder : IQueryParametersBuilder
    {
        public bool CanBuild(Type type)
        {
            return type == typeof(QueryCommandParameters);
        }

        public List<KeyValuePair<string, string>> GetQueryParameters(IQueryParameters queryParameters)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            return parameters;
        }
    }
}