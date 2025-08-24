using BookCatalog.Business.Interfaces;
using BookCatalog.DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookCatalog.Business.Managers;

public class JwtManager : IJwtManager
{
    public string GenerateToken(User user)
    {
        var secretKey = "yv78r65rvZ76t87#y$W&970kv#nj67365jBIb!bbkhLHIHDOQD:N*%z4rtv76hgfjxvy";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        SigningCredentials sc = new (
                                        key,
                                        SecurityAlgorithms.HmacSha256
                                    );

        var claims = new List<Claim>
        {
            new ("UserName", user.UserName),
            new (ClaimTypes.Role, user.Role.ToString())
        };

        JwtSecurityToken token = new(
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: sc
                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
