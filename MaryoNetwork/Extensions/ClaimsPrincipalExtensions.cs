﻿using System.Security.Claims;

namespace MaryoNetwork.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                //throw new ArgumentNullException(nameof(principal));
                return null;
            }

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
