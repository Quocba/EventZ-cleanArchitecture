using Identity.Application.DTO;
using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserClaim user);
        string GenerateRefreshToken(int length = 64);
    }
}
