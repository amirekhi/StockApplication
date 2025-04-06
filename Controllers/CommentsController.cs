using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Extensions;
using MyWebApi.Models;
using WebApplication1.DTOs.Comment;
using WebApplication1.Entity;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers
{
    [Route("api/Comment")]
    [ApiController]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository<Comment> _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;
        public CommentsController(ICommentRepository<Comment> commentRepo, UserManager<AppUser> userManager, IStockRepository stockRepo, IFMPService fmpService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }
        

        [HttpGet]


        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();
            var commentDTos = comments.Select(x => x.ToCommentDto());
            return Ok(commentDTos);
        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.GetByIdAsync(id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }

        [Authorize]
        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto)
        {
           if (!ModelState.IsValid)
               return BadRequest(ModelState);

           var stock = await _stockRepo.GetBySymbolAsync(symbol);

           if (stock == null)
           {
               stock = await _fmpService.FindStockBySymbolAsync(symbol);
               if (stock == null)
               {
                   return BadRequest("Stock does not exists");
               }
               else
               {
                   await _stockRepo.CreateAsync(stock);
               }
           }

           var username = User.GetUsername();
           var appUser = await _userManager.FindByNameAsync(username);

           var commentModel = commentDto.ToCommentFromCreate(stock.Id);
           commentModel.AppUserId = appUser.Id;
           await _commentRepo.CreateAsync(commentModel);
           return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentREquestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound("comment not found");
            }

            return Ok(comment.ToCommentDto());

        }


        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound("comment not found");
            }

            return Ok(commentModel);
        }
    }
}
