﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos
{
    public record UserSession(string? Id, string? Name, string? Email, string? Role);
}
