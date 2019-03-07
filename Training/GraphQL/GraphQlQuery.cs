using commercetools.Sdk.Domain;

namespace Training.GraphQL
{
    public class GraphQlQuery : IDraft<GraphQLResult>
    {
        public string Query { get; set; }

        public GraphQlQuery(string query)
        {
            this.Query = query;
        }
    }
}