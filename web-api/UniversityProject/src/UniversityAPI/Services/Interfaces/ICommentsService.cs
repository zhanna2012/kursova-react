using UniversityAPI.Models.Comments;

namespace UniversityAPI.Services.Interfaces;

public interface ICommentsService
{
    Task<IEnumerable<CommentResponse>> GetCommentsForProductAsync(int productId);
    
    Task<CommentResponse> AddCommentAsync(AddCommentRequest request);
}