using GerenciamentoEstoque.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoEstoque.Api.Repositories
{
    public class UserRepository
    {
        public static Login Get(string username, string password)
        {
            var users = new List<Login>();
            users.Add(new Login { Id = 1, UserName = "admin", Password = "admin", Role = "admin" });
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
