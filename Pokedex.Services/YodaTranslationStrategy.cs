using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Pokedex.Models;
using Pokedex.Services.Interface;

namespace Pokedex.Services
{
    public class YodaTranslationStrategy: ITranslationStrategy
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FunTranslationsService> _logger;
        //private const string baseUrl = "https://api.funtranslations.com/translate/";
        private readonly ConfigParameters _config; 
        public YodaTranslationStrategy(HttpClient client, ILogger<FunTranslationsService> logger, IOptions<ConfigParameters>  configParams)
        {
            _httpClient = client;
            _logger = logger;
            _config = configParams.Value;
        }

        public async Task<FunTranslation> Translate(string text)
        {
            string baseUrl = _config.FunTranslationUrl;
            try
            {
                text = Regex.Replace(text, @"\t|\n|\r|\f", " ");
                text = HttpUtility.UrlEncode(text);
                return await _httpClient.GetFromJsonAsync<FunTranslation>($"{baseUrl}yoda.json?text={text}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred on Yoda translation, Exception Details: ", ex.Message, ex.InnerException?.Message);
                return null;
            }
        }
        
    }
}