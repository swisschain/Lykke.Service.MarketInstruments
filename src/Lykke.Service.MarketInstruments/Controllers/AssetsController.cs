using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.ApiLibrary.Exceptions;
using Lykke.Service.MarketInstruments.Client.Api;
using Lykke.Service.MarketInstruments.Client.Models.Assets;
using Lykke.Service.MarketInstruments.Domain;
using Lykke.Service.MarketInstruments.Domain.Exceptions;
using Lykke.Service.MarketInstruments.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.MarketInstruments.Controllers
{
    [Route("/api/[controller]")]
    public class AssetsController : Controller, IAssetsApi
    {
        private readonly IInstrumentService _instrumentService;

        public AssetsController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        /// <inheritdoc/>
        /// <response code="200">A collection of assets.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AssetModel>), (int) HttpStatusCode.OK)]
        public async Task<IReadOnlyList<AssetModel>> GetAsync()
        {
            IReadOnlyList<Asset> assets = await _instrumentService.GetAssetsAsync();

            return Mapper.Map<List<AssetModel>>(assets);
        }

        /// <inheritdoc/>
        /// <response code="204">The asset successfully added.</response>
        /// <response code="409">The asset already exists.</response>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.Conflict)]
        public async Task AddAsync([FromBody] AssetModel model, string userId)
        {
            try
            {
                var asset = Mapper.Map<Asset>(model);

                await _instrumentService.AddAssetAsync(asset, userId);
            }
            catch (EntityAlreadyExistsException)
            {
                throw new ValidationApiException(HttpStatusCode.Conflict, "The asset already exists");
            }
        }

        /// <inheritdoc/>
        /// <response code="204">The asset successfully updated.</response>
        /// <response code="404">The asset does not exist.</response>
        [HttpPut]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        public async Task UpdateAsync([FromBody] AssetModel model, string userId)
        {
            try
            {
                var asset = Mapper.Map<Asset>(model);

                await _instrumentService.UpdateAssetAsync(asset, userId);
            }
            catch (EntityNotFoundException)
            {
                throw new ValidationApiException(HttpStatusCode.NotFound, "The asset does not exist");
            }
        }

        /// <inheritdoc/>
        /// <response code="204">The asset successfully deleted.</response>
        /// <response code="400">An error occurred while deleting asset.</response>
        /// <response code="404">The asset does not exist.</response>
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.NotFound)]
        public async Task DeleteAsync(string name, string exchange, string userId)
        {
            try
            {
                await _instrumentService.DeleteAssetAsync(name, exchange, userId);
            }
            catch (EntityNotFoundException)
            {
                throw new ValidationApiException(HttpStatusCode.NotFound, "The asset does not exist");
            }
            catch (FailedOperationException exception)
            {
                throw new ValidationApiException(HttpStatusCode.BadRequest, exception.Message);
            }
        }
    }
}
