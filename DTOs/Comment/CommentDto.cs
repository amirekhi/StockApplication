namespace WebApplication1.DTOs.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CraetedBy { get; set; } = string.Empty;
        public int StockId { get; set; }


    }
}
