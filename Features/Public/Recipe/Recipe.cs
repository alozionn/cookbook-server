public class Recipe : Endpoint<RecipeRequest, RecipeResponse>
{
    public override void Configure()
    {
        Post("/api/user/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RecipeRequest req, CancellationToken ct)
    {
        await Send.OkAsync(
            new() { FullName = req.FirstName + " " + req.LastName, IsOver18 = req.Age > 18 }
        );
    }
}
