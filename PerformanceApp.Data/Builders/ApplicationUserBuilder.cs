using Microsoft.Identity.Client;
using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class ApplicationUserBuilder : IBuilder<ApplicationUser>
{
    private string _id = ApplicationUserBuilderDefaults.UserId;
    private string _userName = ApplicationUserBuilderDefaults.UserName;

    public ApplicationUserBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public ApplicationUserBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public ApplicationUser Build()
    {
        return new ApplicationUser
        {
            Id = _id,
            UserName = _userName
        };
    }

    public ApplicationUser Clone()
    {
        return new ApplicationUserBuilder()
            .WithId(_id)
            .WithUserName(_userName)
            .Build();
    }

    public IEnumerable<ApplicationUser> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new ApplicationUserBuilder()
                .WithId($"User-{i}")
                .WithUserName($"UserName-{i}")
                .Build();
        }
    }
}