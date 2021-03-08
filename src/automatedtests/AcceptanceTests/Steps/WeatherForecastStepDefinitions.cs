using System;
using System.Collections.Generic;
using System.Net.Http;
using AcceptanceTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace AcceptanceTests.Steps
{
    [Binding]
    public sealed class WeatherForecastStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private HttpClient _httpClient;

        public WeatherForecastStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"Weather forecast is running")]
        public void GivenWeatherForecastIsRunning()
        {
            _httpClient = new HttpClient();
        }

        [Given(@"I am logged in as '(.*)'")]
        public void GivenIAmLoggedInAs(string userName)
        {
        }

        [When(@"I called Get Weather forecast")]
        public void WhenICalledGetWeatherForecast()
        {
            var request = new HttpRequestMessage() {
                RequestUri = new Uri("http://localhost:8000/weatherforecast"),
                Method = HttpMethod.Get,
            };
            var response = _httpClient.SendAsync(request).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(responseContent);
            _scenarioContext.Set(data);
        }

        [Then(@"I should get list of forecast")]
        public void ThenIShouldGetListOfForecast()
        {
            var data = _scenarioContext.Get<IEnumerable<WeatherForecast>>();
            Assert.IsNotNull(data);
            data.Should().HaveCount(5);
            foreach (var item in data)
            {
                item.TemperatureF.Should().Be(32 + (int)(item.TemperatureC / 0.5556));
            }
        }

    }
}
