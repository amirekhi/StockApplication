using WebApplication1.DTOs.Comment;
using WebApplication1.Entity;

namespace WebApplication1.Mappers
{
    public static class CommnetMapprs
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                CreatedOn = commentModel.CreatedOn,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CraetedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId,
            };
        }


        public static Comment ToCommentFromCreate(this CreateCommentDto commentDTO, int stockId)
        {
            return new Comment
            {

                Title = commentDTO.Title,
                Content = commentDTO.Content,
                StockId = stockId,
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommentREquestDto UpdateDto)
        {
            return new Comment
            {

                Title = UpdateDto.Title,
                Content = UpdateDto.Content,

            };
        }
    }
}
