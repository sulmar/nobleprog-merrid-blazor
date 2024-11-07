using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlazorWebAssemblyApp.Authorization;

public record MinimumAgeRequirement(byte Age) : IAuthorizationRequirement; // Mark interface

public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        if (!context.User.HasClaim(p => p.Type == ClaimTypes.DateOfBirth))
        {
            context.Fail();

            return Task.CompletedTask;
        }

        var dateOfBirth = DateTime.Parse(context.User.FindFirst(p=>p.Type == ClaimTypes.DateOfBirth).Value);

        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (age >= requirement.Age)
        {
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;

    }
}