using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Engineers
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
                var engineer = await _context.Engineers.FindAsync(request.Id);

                _context.Remove(engineer);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false) return Result<Unit>.Failure("Failed To Delete Engineer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}