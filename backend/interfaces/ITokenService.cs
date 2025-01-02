using System;
using backend.Models;

namespace backend.interfaces;

public interface ITokenService
{
    string CreateToken(User user);

}
