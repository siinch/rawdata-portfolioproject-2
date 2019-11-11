﻿//using AutoMapper;
using WebService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using rawdata_portfolioproject_2;

//namespace WebService.Controllers
namespace WebService
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        //private IMapper _mapper;

        public PostsController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
            // _mapper = mapper;
        }

        [HttpGet("rankedsearch")] 
        public ActionResult RankedSearch([FromBody] RankedSearchDto dto)
        {
            var posts = _dataService.RankedWeightSearch(/*dto.PagingAttributes,*/ dto.Keywords);
            if (posts == null)
            {
                return NotFound();
            }
            //var result = CreateResult(posts, dto.PagingAttributes);

            return Ok(posts);
        }

        //[HttpGet("{postId}", Name = nameof(GetPost))]
        //public ActionResult GetPost(int postId)
        //{
        //    var post = _dataService.GetPost(postId);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(CreatePostDto(post));
        //}

        ///////////////////
        //
        // Helpers
        //
        //////////////////////

        private PostDto CreatePostDto(Post post)
        {
            var dto = new PostDto();
            dto.Body = post.Body;
            dto.Link = Url.Link(
                    nameof(RankedSearch),
                    new { postId = post.Id });
            return dto;
        }

        private object CreateResult(IEnumerable<Ranked_Weight_VariadicResult> posts, PagingAttributes attr)
        {
            var totalItems = posts.Count();
            
            var numberOfPages = Math.Ceiling((double)totalItems / attr.PageSize);

            var prev = attr.Page > 0
                ? CreatePagingLink(attr.Page - 1, attr.PageSize)
                : null;
            var next = attr.Page < numberOfPages - 1
                ? CreatePagingLink(attr.Page + 1, attr.PageSize)
                : null;

            return new
            {
                totalItems,
                numberOfPages,
                prev,
                next,
                //items = posts.Select(CreatePostDto)
                items = posts
            };
        }

        private string CreatePagingLink(int page, int pageSize)
        {
            return Url.Link(nameof(RankedSearch), new { page, pageSize });
        }

    }
}