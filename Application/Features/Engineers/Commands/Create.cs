using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Persistence;

namespace Application.Features.Engineers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Engineer Engineer { get; set; }
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

                _context.Engineers.Add(request.Engineer);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == false)
                    return Result<Unit>.Failure("Failed To Create Engineer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}