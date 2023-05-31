namespace UniversityAPI.Models.Comments;

public class AddCommentRequest
{
    public string Text { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public byte Rating { get; set; }
}