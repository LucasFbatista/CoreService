using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostRepository postRepository;
        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }


        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await postRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await postRepository.GetPosts();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpGet]
        [Route("GetPost")]
        public async Task<IActionResult> GetPost(int? postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await postRepository.GetPost(postId);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await postRepository.AddPost(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpDelete]
        [Route("DeletePost")]
        public async Task<IActionResult> DeletePost(int? postId)
        {
            int result = 0;

            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await postRepository.DeletePost(postId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        //AQUI IREMOS TRANFERIR A RESPONSÁBILIDADE PARA O NOSSA CAMADA DE REPOSITORIO
        //AQUI IREMOS SÓ VALIDAR O RETORNO DO NOSSO REPOSITORIO.
        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await postRepository.UpdatePost(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }



    }
}
