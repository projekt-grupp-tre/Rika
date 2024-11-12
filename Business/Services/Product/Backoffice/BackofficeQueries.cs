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

    public static readonly string GetProductByIdQuery = @"
            query GetProductById($productId: UUID!) {
                getProductById(productId: $productId) {
                    productId
                    name
                    description
                    images
                    category {
                        name
                    }
                    variants {
                        productVariantId
                        size
                        color
                        stock
                        price
                    }
                    reviews {
                        reviewId
                        clientName
                        rating
                        comment
                    }
                }
            }";
}
