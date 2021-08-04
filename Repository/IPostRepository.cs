using CoreServices.Models;
using CoreServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{

    //ESSA AQUI É NOSSA INTERFACE DO REPOSITORIO BASEADO NELE SEGUIREMOS NOSSA REGRA
    //DE NEGOCIO DE IMPLEMENTAÇÃO DAS FUNÇÕES
    public interface IPostRepository
    {
        Task<List<Category>> GetCategories();

        Task<List<PostViewModel>> GetPosts();

        Task<PostViewModel> GetPost(int? postId);

        Task<int> AddPost(Post post);

        Task<int> DeletePost(int? postId);

        Task UpdatePost(Post post);
    }
}


//Agora vá para a implementação CRUD real com o código.Portanto, abra IPostRepositorye 
//adicione os métodos necessários para as operações CRUD. Assim, podemos ver com o seguinte 
//IPostRepository interface, definimos métodos diferentes para um propósito diferente.
//GetCategoriesobterá a lista da categoria disponível, GetPostsobterá a lista de postagens disponíveis, GetPostobterá a postagem individual para o ID da postagem específico, AddPostadicionará novos detalhes da postagem, DeletePostexcluirá a postagem individual com base no ID da postagem e, por último UpdatePost, atualizará a postagem existente.
//Como estamos retornando dados específicos da tarefa, isso significa que os dados retornarão de forma assíncrona.