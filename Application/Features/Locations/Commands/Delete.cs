using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.FindAsync(request.Id);

                _context.Remove(location);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false) return Result<Unit>.Failure("Failed To Delete Location");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}