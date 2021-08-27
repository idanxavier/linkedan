using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            try
            {
                List<Post> list = await _postService.ListPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-user-posts")]
        public async Task<ActionResult> ListUserPosts()
        {
            try
            {
                List<Post> list = await _postService.ListUserPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            try
            {
                Post post = await _postService.GetPost(postId);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-post")]
        public async Task<ActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                post = await _postService.CreatePost(post);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("new-like")]
        public async Task<ActionResult> NewLike([FromBody] int postId)
        {
            try
            {
                return Ok(await _postService.NewLike(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-like")]
        public async Task<ActionResult> GetLike([FromQuery] int likeId)
        {
            try
            {
                Like like = await _postService.GetLike(likeId);

                return Ok(like);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-like-by-user-and-post")]
        public async Task<ActionResult> GetLikeByPostIdAndApplicationUser([FromQuery] int postId)
        {
            try
            {
                Like like = await _postService.GetLikeByPostIdAndApplicationUser(postId);
                return Ok(like);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-like")]
        public async Task<ActionResult> DeleteLike([FromQuery] int likeId)
        {
            try
            {
                return Ok(await _postService.DeleteLike(likeId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-user-likes")]
        public async Task<ActionResult> ListUserLikes()
        {
            try
            {
                List<Like> list = await _postService.ListUserLikes();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-post")]
        public async Task<ActionResult> UpdatePost([FromQuery] int postId, [FromBody] Post post)
        {
            try
            {
                return Ok(await _postService.UpdatePost(postId, post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-post")]
        public async Task<ActionResult> DeletePost([FromQuery] int postId)
        {
            try
            {
                return Ok(await _postService.DeletePost(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}