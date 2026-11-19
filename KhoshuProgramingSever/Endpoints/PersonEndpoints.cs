namespace KhoshuProgramingSever.Endpoints;

public static class PersonEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapPersonEndpoints()
        {
            var group = app.MapGroup("/api/Person").WithTags("Persons");

            group.MapGet("/" ,() => {});
        }
    }
}
