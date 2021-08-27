using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Services.Implementations
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly AuthService _authService;
        private readonly LikeRepository _likeRepository;

        public PostService(PostRepository postRepository, AuthService authService, LikeRepository likeRepository)
        {
            _postRepository = postRepository;
            _authService = authService;
            _likeRepository = likeRepository;
        }

        public async Task<List<Post>> ListPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Post> list = await _postRepository.ListPosts(currentUser.Id);

            return list;
        }
        public async Task<List<Post>> ListUserPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Post> list = await _postRepository.ListUserPosts(currentUser.Id);

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _postRepository.GetPost(postId);

            if (post == null)
                throw new ArgumentException("Post doesn't exists.");

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Post newPost = new Post();

            newPost.ApplicationUserId = currentUser.Id;
            newPost.Data = DateTime.Now;
            newPost.Titulo = post.Titulo;
            newPost.Conteudo = post.Conteudo;

            newPost = await _postRepository.CreatePost(newPost);

            return newPost;
        }

        public async Task<Like> NewLike(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Post findPost = await _postRepository.GetPost(postId);

            Like alreadyLike = await _likeRepository.GetLike(postId);
            if(alreadyLike != null)
                throw new ArgumentException("You already liked this post.");
            if (findPost == null)
                throw new ArgumentException("Post doesn't exists.");
            if (findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("You can't like your own post.");

  
            Like like = new Like();
            like.ApplicationUserId = currentUser.Id;
            like.Data = DateTime.Now;
            like.PostId = postId;
            like.Post = findPost;
            like = await _likeRepository.NewLike(like);
            findPost.QtdLikes ++;
            findPost.LikePost.Add(like);
            await _postRepository.UpdatePost(findPost);

            return like;
        }
        public async Task<bool> DeleteLike(int likeId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Like findLike = await _likeRepository.GetLike(likeId);
            Post findPost = await _postRepository.GetPost(findLike.PostId);

            if (findLike == null)
                throw new ArgumentException("Like doesn't exists.");

            if (findLike.ApplicationUserId != currentUser.Id)
                throw new ArgumentException("You don't have permission to this.");

            findPost.QtdLikes--;
            await _postRepository.UpdatePost(findPost);
            await _likeRepository.DeleteLike(likeId);

            return true;
        }

        public async Task<List<Like>> ListUserLikes()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Like> list = await _likeRepository.ListUserLikes(currentUser.Id);

            return list;
        }


        public async Task<Like> GetLike(int likeId)
        {
            Like like = await _likeRepository.GetLike(likeId);

            if (like == null)
                throw new ArgumentException("Post doesn't exists.");

            return like;
        }
        
        public async Task<Like> GetLikeByPostIdAndApplicationUser(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Like like = await _likeRepository.GetLikeByPostIdAndApplicationUser(postId, currentUser.Id);

            if (like == null)
                throw new ArgumentException("This like doesn't exists.");

            return like;
        }
        public async Task<int> UpdatePost(int postId, Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Post findPost = await _postRepository.GetPost(postId);

            if (findPost == null)
                throw new ArgumentException("This post doesn't exists.");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("You don't have permission to this.");

            findPost.Titulo = post.Titulo;
            findPost.Conteudo = post.Conteudo;

            return await _postRepository.UpdatePost(findPost);
        }

        public async Task<bool> DeletePost(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Post findPost = await _postRepository.GetPost(postId);

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("You don't have permission to this.");

            if (findPost == null)
                throw new ArgumentException("Post doesn't exists.");

            await _postRepository.DeletePost(postId);

            return true;
        }
    }
}