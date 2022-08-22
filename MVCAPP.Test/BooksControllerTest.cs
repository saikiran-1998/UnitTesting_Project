using Library.API.Data.Models;
using Library.API.Data.Services;
using LibraryApp.Controllers;
using LibraryApp.Data.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MVCAPP.Test
{
    public class BooksControllerTest
    {
        [Fact]
        public void IndexTest()
        {
            //Arrange
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(x => x.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(mockRepo.Object);
            //Act
            var result = controller.Index();
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(viewResult.ViewData.Model);

            Assert.Equal(5, viewResultBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void DetailsTest(string validId, string invalidId)
        {
            //for valid Guid
            var validGuid = new Guid(validId);
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(m => m.GetById(validGuid)).Returns(MockData.GetTestBookItems().FirstOrDefault(x => x.Id == validGuid));
            var controller = new BooksController(mockRepo.Object);
            var result = controller.Details(validGuid);
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultValue = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);

            //for invalid Guid
            var inValidGuid = new Guid(invalidId);
            mockRepo.Setup(m => m.GetById(inValidGuid)).Returns(MockData.GetTestBookItems().FirstOrDefault(x => x.Id == inValidGuid));
            var notFoundResult = controller.Details(inValidGuid);
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void CreateTest()
        {
            //For valid item
            var mockRepo = new Mock<IBookService>();
            var controller = new BooksController(mockRepo.Object);
            var newBook = new Book()
            {
                Title = "Title",
                Author = "Author",
                Description = "Description",
            };
            var result = controller.Create(newBook);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);

            //For invalid item
            var newBook2 = new Book()
            {
                Title = "Title2",
                Description = "Description2",
            };
            controller.ModelState.AddModelError("Author", "Author is required");

            var inValidResult = controller.Create(newBook2);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(inValidResult);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void DeleteTest(string validId)
        {
            var validGuid = new Guid(validId);
            var mockRepo = new Mock<IBookService>();
            mockRepo.Setup(x => x.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(mockRepo.Object);

            var result = controller.Delete(validGuid, null);
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index",actionResult.ActionName);
            Assert.Null(actionResult.ControllerName);
        }
    }
}
