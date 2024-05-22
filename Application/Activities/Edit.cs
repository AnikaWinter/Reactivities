using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using SQLitePCL;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set;}
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper) 
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id); //go and get the activity from our databse
                
                //activity.Title = request.Activity.Title ?? activity.Title; //if title was not updated (null) we set it as the previous title
                _mapper.Map(request.Activity, activity); //updates properties one to the other
            
                await _context.SaveChangesAsync(); //database gets updated
            }
        }
    }
}