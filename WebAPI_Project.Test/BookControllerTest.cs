using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI_Project.Controllers;
using WebAPI_Project.Models;
using WebAPI_Project.Services;
using Xunit;

namespace WebAPI_Project.Test
{
    public class BookControllerTest
    {

        private readonly BookController bookController;
        private readonly IBookService bookService;
        public BookControllerTest()
        {
            bookService = new BookService();
            bookController = new BookController(bookService);
        }

        [Fact]
        public void GetAllBooksTest()
        {
            var result = bookController.Get();
            Assert.IsType<OkObjectResult>(result.Result);

            var bookList = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(bookList.Value);

            var productCount = bookList.Value as List<Book>;
            Assert.Equal(5, productCount.Count);
        }
        [Theory]
        [InlineData("66ff5116-bcaa-4061-85b2-6f58fbb6db25", "66ff5116-bcaa-4061-85b2-6f58fbb6db29")]
        public void GetBookByIdTest(string Id1, string Id2)
        {
            //Arrange
            var validId = new Guid(Id1);
            var invalidId = new Guid(Id2);
            //Act
            var validResult = bookController.Get(validId);
            Assert.IsType<OkObjectResult>(validResult.Result);

            var invalidResult = bookController.Get(invalidId);
            Assert.IsType<NotFoundResult>(invalidResult.Result);

            //Assert
            var book = validResult.Result as OkObjectResult;
            Assert.IsType<Book>(book.Value);

            var bookItem = book.Value as Book;
            Assert.Equal(validId, bookItem.Id);

        }
        [Fact]
        public void AddBookTest()
        {
            //Arrange
            var completeBook = new Book() { Author = "Author1", Description = "Description1", Title = "Title1" };
           
            //Act
            var addedResponse = bookController.Post(completeBook);
            //Assert
            Assert.IsType<CreatedAtActionResult>(addedResponse);

            var item = addedResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author,bookItem.Author);
            Assert.Equal(completeBook.Description, bookItem.Description);
            Assert.Equal(completeBook.Title, bookItem.Title);

            //Arrange
            var inCompleteBook = new Book() { Author = "Author2", Description = "Description2" };

            //Act
            bookController.ModelState.AddModelError("Title","Title is Required");
            var badrequestResponse = bookController.Post(inCompleteBook);
            //Assert
            Assert.IsType<BadRequestObjectResult>(badrequestResponse);
        }
        [Theory]
        [InlineData("66ff5116-bcaa-4061-85b2-6f58fbb6db25", "66ff5116-bcaa-4061-85b2-6f58fbb6db29")]
        public void RemoveBookByIdTest(string Id1, string Id2)
        {
            //Arrange
            var validId = new Guid(Id1);
            var invalidId = new Guid(Id2);

            //Act
            var notFoundResult = bookController.Remove(invalidId);
            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, bookService.GetAll().Count());

            //Act
            var okResult = bookController.Remove(validId);
            //Assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, bookService.GetAll().Count());

           

        }
    }
}
