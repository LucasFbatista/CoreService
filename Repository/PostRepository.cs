using CoreServices.Models;
using CoreServices.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    namespace CoreServices.Repository
    {
        public class PostRepository : IPostRepository
        {
            BlogDBContext _context;
            public PostRepository(BlogDBContext context)
            {
                _context = context;
            }


            //LISTA DE CATEGORIAS
            public async Task<List<Category>> GetCategories()
            {
                if (_context  != null)
                {
                    return await _context.Category.ToListAsync();
                }

                return null;
            }


            //LISTA DE POSTAGENS
            public async Task<List<PostViewModel>> GetPosts()
            {
                if (_context != null)
                {
                    return await (from p in _context.Post
                                  from c in _context.Category
                                  where p.CategoryId == c.Id
                                  select new PostViewModel
                                  {
                                      PostId = p.PostId,
                                      Title = p.Title,
                                      Description = p.Description,
                                      CategoryId = p.CategoryId,
                                      CategoryName = c.Name,
                                      CreatedDate = p.CreatedDate
                                  }).ToListAsync();
                }

                return null;
            }


            //GET UNICO POST POR ID
            public async Task<PostViewModel> GetPost(int? postId)
            {
                if (_context != null)
                {
                    return await (from p in _context.Post
                                  from c in _context.Category
                                  where p.PostId == postId
                                  select new PostViewModel
                                  {
                                      PostId = p.PostId,
                                      Title = p.Title,
                                      Description = p.Description,
                                      CategoryId = p.CategoryId,
                                      CategoryName = c.Name,
                                      CreatedDate = p.CreatedDate
                                  }).FirstOrDefaultAsync();
                }

                return null;
            }

            //ADICIONANDO NOVA POSTAGEM
            public async Task<int> AddPost(Post post)
            {
                if (_context != null)
                {
                    await _context.Post.AddAsync(post);
                    await _context.SaveChangesAsync();

                    return post.PostId;
                }

                return 0;
            }

            //DELETANDO POSTAGEM
            public async Task<int> DeletePost(int? postId)
            {
                int result = 0;

                if (_context != null)
                {
                    // Encontre a postagem para um ID de postagem específico
                    var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == postId);

                    if (post != null)
                    {
                        // Excluir essa postagem
                        _context.Post.Remove(post);

                        // Confirme a transação
                        result = await _context.SaveChangesAsync();
                    }
                    return result;
                }

                return result;
            }

            // ATUALIZANDO POSTAGEM
            public async Task UpdatePost(Post post)
            {
                if (_context != null)
                {
                    // Atualizar essa postagem
                    _context.Post.Update(post);

                    // Confirme a transação
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
