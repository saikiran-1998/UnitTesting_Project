using System;
using System.Collections.Generic;
using WebAPI_Project.Models;

namespace WebAPI_Project.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book newBook);
        Book GetById(Guid id);
        void Remove(Guid id);
    }
}
