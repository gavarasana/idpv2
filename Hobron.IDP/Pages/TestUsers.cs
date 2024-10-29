// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;

namespace Hobron.IDP;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var david_address = new
            {
                street_address = "40 Bay Street",
                city = "Toronto",
                postal_code = "M1R 0E9",
                country = "Canada"
                
            };

            var emma_address = new
            {
                street_address = "234 Hacker Way",
                city = "Chicago",
                postal_code = "60645",
                country = "USA"
            };

            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "sdavid",
                    Password = "david_password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "David Smith"),
                        new Claim(JwtClaimTypes.GivenName, "David"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "dSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://davidsmith.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(david_address), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("role","Free"),
                        new Claim("country","CA")
                    }
                },
                new TestUser
                {
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "jemma",
                    Password = "emma_password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Emma Johnson"),
                        new Claim(JwtClaimTypes.GivenName, "Emma"),
                        new Claim(JwtClaimTypes.FamilyName, "Johnson"),
                        new Claim(JwtClaimTypes.Email, "eJohnson@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://emma.com"),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(emma_address), IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("role","Premium"),
                        new Claim("country","USA")
                    }
                }
            };
        }
    }
}