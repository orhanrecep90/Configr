using Configr.Business.Abstract;
using Configr.Entities.Concrete;
using Configr.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Configr.WebApp.Test
{
    public class HomeControllerTest
    {
        private readonly Mock<IConfigurationDatasService> _mockService;
        private readonly HomeController _homeController;
        private List<ConfigurationDatas> _configurationDataList;
        public HomeControllerTest()
        {
            _mockService = new Mock<IConfigurationDatasService>();
            _homeController = new HomeController(_mockService.Object);
            _configurationDataList = new List<ConfigurationDatas>
            {
                new ConfigurationDatas{ID=1,Name="SiteName",Type="String",Value="allyouplay.com",IsActive=true,ApplicationName="SERVICE-A"},
                new ConfigurationDatas{ID=2,Name="IsBasketEnabled",Type="Boolean",Value="1",IsActive=true,ApplicationName="SERVICE-B"},
                new ConfigurationDatas{ID=3,Name="MaxItemCount",Type="Int",Value="50",IsActive=false,ApplicationName="SERVICE-A"},
            };
        }

        [Fact]
        public async void Index_SuccesfullyActionExecutes_ReturnView()
        {
            var result = await _homeController.Index();
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async void ListData_EmptyNameFilter_ReturnAllConfigurationDataList()
        {
            _mockService.Setup(service => service.GetAll()).ReturnsAsync(_configurationDataList);
            var result = (ViewResult)(await _homeController.ListData(""));
            var dataList = ((List<ConfigurationDatas>)result.Model);
            Assert.Equal(3, dataList.Count);
        }
        [Theory]
        [InlineData("SiteName")]
        [InlineData("MaxItemCount")]
        public async void ListData_NotEmptyNameFilter_ReturnConfigurationDataListByName(string value)
        {
            //Arrange
            var confDatas = _configurationDataList.Where(y => y.Name == value).ToList();
            _mockService.Setup(x => x.GetAllByName(value)).ReturnsAsync(confDatas);
            //Act
            var result = Assert.IsType<ViewResult>(await _homeController.ListData(value));
            var resultData = Assert.IsAssignableFrom<List<ConfigurationDatas>>(result.Model);
            //Assert
            Assert.Equal(confDatas.Count(), resultData.Count());
        }

        //Test cases were not written because there aren't any conditions in the other action methods.
    }
}