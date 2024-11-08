using Business.Dto.Product;

namespace Business.Services.Product
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetReviewsByProductIdAsync(Guid productId);
    }
}