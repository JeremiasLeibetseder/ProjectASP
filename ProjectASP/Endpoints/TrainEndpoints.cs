using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectASP.Business.Repositories;
using ProjectASP.Business.Repositories.IRepositories;
using ProjectASP.Data.Entities;
using ProjectASP.Models;
using System.Net;

namespace ProjectASP.WebAPI.Endpoints
{
    public static class TrainEndpoints
    {
        public static void ConfigureTrainEndpoints(this WebApplication app)
        {
            app.MapGet("/api/train", GetAllTrain)
                .WithName("GetTrains")
                .Produces<APIResponse>(200)
                .RequireAuthorization();


            app.MapGet("/api/train/{id:int}", GetTrain)
                .WithName("GetTrain")
                .Produces<APIResponse>(200);

            app.MapPost("/api/train", CreateTrain)
                .WithName("CreateTrain")
                .Accepts<TrainCreateDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400);

            app.MapPut("/api/train", UpdateTrain)
                .WithName("UpdateTrain")
                .Accepts<TrainUpdateDTO>("application/json")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapDelete("/api/train/{id:int}", DeleteTrain);
        }

        private async static Task<IResult> DeleteTrain(ITrainRepository trainRepository, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            TrainDTO trainFromStore = await trainRepository.GetAsync(id);
            if (trainFromStore != null)
            {
                await trainRepository.RemoveAsync(trainFromStore.Id);
                //await trainRepository.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> UpdateTrain(
            ITrainRepository trainRepository,
            IMapper mapper,
            [FromBody] TrainUpdateDTO train_U_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            await trainRepository.UpdateAsync(mapper.Map<TrainDTO>(train_U_DTO));
            //await trainRepository.SaveAsync();

            response.Result = mapper.Map<TrainDTO>(await trainRepository.GetAsync(train_U_DTO.Id)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreateTrain(
            ITrainRepository trainRepository,
            IMapper mapper,
            [FromBody] TrainCreateDTO train_C_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (trainRepository.GetAsync(train_C_DTO.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Coupon name already exists");
                return Results.BadRequest(response);
            }

            TrainDTO train = mapper.Map<TrainDTO>(train_C_DTO);

            await trainRepository.CreateAsync(train);
            //await TrainRepository.SaveAsync();

            TrainDTO trainDTO = mapper.Map<TrainDTO>(train);
            response.Result = trainDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);


        }

        [Authorize]
        private async static Task<IResult> GetAllTrain(
            ITrainRepository _trainRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all trains");
            response.Result = await _trainRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetTrain(
            ITrainRepository _trainRepo, ILogger<Program> _logger, int id)
        {
            APIResponse response = new();
            response.Result = await _trainRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

    }
}
