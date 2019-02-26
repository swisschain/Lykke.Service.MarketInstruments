using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Exceptions;
using Lykke.Service.MarketInstruments.Client.Api;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;
using Lykke.Service.MarketInstruments.Domain;
using Lykke.Service.MarketInstruments.Domain.Exceptions;
using Lykke.Service.MarketInstruments.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.MarketInstruments.Controllers
{
    [Route("/api/[controller]")]
    public class AssetPairsController : Controller, IAssetPairsApi
    {
        private readonly IInstrumentService _instrumentService;

        public AssetPairsController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        /// <inheritdoc/>
        /// <response code="200">A collection of asset pairs.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AssetPairModel>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<AssetPairModel>> GetAsync()
        {
            IReadOnlyList<AssetPair> assetPairs = await _instrumentService.GetAssetPairsAsync();

            return Mapper.Map<List<AssetPairModel>>(assetPairs);
        }

        /// <inheritdoc/>
        /// <response code="204">The asset pair successfully added.</response>
        /// <response code="400">An error occurred while adding asset pair.</response>
        /// <response code="409">The asset pair already exists.</response>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.Conflict)]
        public async Task AddAsync([FromBody] AssetPairModel model, string userId)
        {
            try
            {
                var assetPair = Mapper.Map<AssetPair>(model);

                await _instrumentService.AddAssetPairAsync(assetPair, userId);
            }
            catch (EntityAlreadyExistsException)
            {
                throw new ValidationApiException(HttpStatusCode.Conflict, "The asset pair already exists");
            }
            catch (FailedOperationException exception)
            {
                throw new ValidationApiException(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        /// <inheritdoc/>
        /// <response code="204">The asset pair successfully updated.</response>
        /// <response code="400">An error occurred while updating asset pair.</response>
        /// <response code="404">The asset pair does not exist.</response>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        public async Task UpdateAsync([FromBody] AssetPairModel model, string userId)
        {
            try
            {
                var assetPair = Mapper.Map<AssetPair>(model);

                await _instrumentService.UpdateAssetPairAsync(assetPair, userId);
            }
            catch (EntityNotFoundException)
            {
                throw new ValidationApiException(HttpStatusCode.NotFound, "The asset pair does not exist");
            }
            catch (FailedOperationException exception)
            {
                throw new ValidationApiException(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        /// <inheritdoc/>
        /// <response code="204">The asset pair successfully deleted.</response>
        /// <response code="400">An error occurred while deleting asset pair.</response>
        /// <response code="404">The asset pair does not exist.</response>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        public async Task DeleteAsync(string name, string exchange, string userId)
        {
            try
            {
                await _instrumentService.DeleteAssetPairAsync(name, exchange, userId);
            }
            catch (EntityNotFoundException)
            {
                throw new ValidationApiException(HttpStatusCode.NotFound, "The asset pair does not exist");
            }
            catch (FailedOperationException exception)
            {
                throw new ValidationApiException(HttpStatusCode.BadRequest, exception.Message);
            }
        }
    }
}
