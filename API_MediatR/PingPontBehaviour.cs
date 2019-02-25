using System;
using System.Threading;
using System.Threading.Tasks;
using API_MediatR.Command;
 using MediatR;
 
 namespace API_MediatR
 {
     public class PingPongBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
     {
         public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
         {
             var response = await next();
             Console.WriteLine($"response is {response}");
             return response;
         }
     }
 }