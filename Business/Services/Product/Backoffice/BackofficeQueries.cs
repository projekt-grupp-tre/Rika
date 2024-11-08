namespace Business.Services.Product.Backoffice;

internal record BackofficeQueries
{
    public static readonly string GetProductsQuery = @"
        query {
            getProducts {
                productId
                name
                createdAt

                category {
                    name
                }
            }
        }";
}
