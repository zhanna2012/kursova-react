using UniversityAPI.Models.Users;

namespace UniversityAPI.Models.Comments;

public class CommentResponse
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Text { get; set; }

    public byte Rating { get; set; }

    public UserSnapshot User { get; set; }
}