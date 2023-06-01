using Microsoft.EntityFrameworkCore;
using UniversityAPI.Database;
using UniversityAPI.Extensions;
using UniversityAPI.Models.Comments;
using UniversityAPI.Models.Database;
using UniversityAPI.Models.Users;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class CommentsService : ICommentsService
{
    private readonly IUniversityContext _context;
    private readonly IConfiguration _configuration;

    public CommentsService(IUniversityContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public Task<IEnumerable<CommentResponse>> GetCommentsForProductAsync(int productId)
    {
        return Task.FromResult<IEnumerable<CommentResponse>>(
            _context.Comments
                .Where(p => p.ProductId == productId)
                .Include(c => c.User)
                .Select(MapComments)
                .ToList());
    }

    public async Task<CommentResponse> AddCommentAsync(AddCommentRequest request)
    {
        var comment = new Comments
        {
            Rating = request.Rating,
            Text = request.Text,
            ProductId = request.ProductId,
            UserId = request.UserId
        };

        _context.Comments.Add(comment);

        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(request.UserId);

        comment.User = user;

        return MapComments(comment);
    }

    private CommentResponse MapComments(Comments data) =>
        new()
        {
            ProductId = data.ProductId,
            User = new UserSnapshot
            {
                Id = data.User.Id,
                Email = data.User.Email,
                FirstName = data.User.FirstName,
                LastName = data.User.LastName,
                PhotoUrl = data.User.PhotoUrl.GetImageUrl(nameof(Users), _configuration)
            },
            Id = data.Id,
            Rating = data.Rating,
            Text = data.Text
        };
}