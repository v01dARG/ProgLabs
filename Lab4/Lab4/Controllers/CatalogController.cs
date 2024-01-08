using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab4.Controllers
{
    public class CatalogController : ApiController
    {

        // GET api/values
        // возвращаем список всех книг или найденных после запроса
        public IEnumerable<Book> Get()
        {
            if (WebApiApplication.selectall)
            {
                return WebApiApplication.Catalog.GetBookList();
            }
            else
            {
                return WebApiApplication.FoundBooks;
            }
        }
        // GET api/values/5
        // возвращаем книгу по индексу (ind)
        public string Get(int ind)
        {
            if (WebApiApplication.selectall)
            {
                return WebApiApplication.Catalog.GetBook(ind);
            }
            else
            {
                if (ind >= 1 && ind <= WebApiApplication.FoundBooks.Count)
                {
                    return WebApiApplication.FoundBooks[ind - 1].Title + ";" + WebApiApplication.FoundBooks[ind - 1].Author + ";" + WebApiApplication.FoundBooks[ind - 1].Description;
                }
                else
                {
                    return "";
                }
            }
        }
        // POST api/values
        // Получаем список параметров, первый - команда
        public IHttpActionResult Post([FromBody] List<String> value)
        {
            if (value.Count == 0)
            {
                return this.StatusCode(HttpStatusCode.NotAcceptable);
            }
            if (value[0] == "add")
            {
                if (value.Count != 5)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                if (WebApiApplication.Catalog.AddBook(value[1], value[2], value[3], value[4]))
                {
                    WebApiApplication.selectall = true;
                }
            }
            else
            if (value[0] == "select_all")//выбрать все без условий
            {
                if (value.Count != 1)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.selectall = true;
            }
            else
            if (value[0] == "select_title")//выбрать по названию
            {
                if (value.Count != 2)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.selectall = false;
                WebApiApplication.Catalog.SelectBooksByTitle(value[1], ref WebApiApplication.FoundBooks);
            }
            else
            if (value[0] == "select_parttitle")//выбрать по фрагменту названия
            {
                if (value.Count != 2)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.selectall = false;
                WebApiApplication.Catalog.SelectBooksByPartOfTitle(value[1], ref WebApiApplication.FoundBooks);
            }
            else
            if (value[0] == "select_author")//выбрать по автору
            {
                if (value.Count != 2)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.selectall = false;
                WebApiApplication.Catalog.SelectBooksByAuthor(value[1], ref WebApiApplication.FoundBooks);
            }
            else
            if (value[0] == "select_keywords")//выбрать по ключевым
            {
                if (value.Count != 2)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.selectall = false;
                WebApiApplication.Catalog.SelectBooksByKeyWords(value[1], ref WebApiApplication.FoundBooks);
            }
            else
            if (value[0] == "save_xml")
            {
                if (value.Count != 1)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.Catalog.SaveXML();
            }
            else
            if (value[0] == "load_xml")
            {
                if (value.Count != 1)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                if (WebApiApplication.Catalog.LoadXML())
                {
                    WebApiApplication.selectall = true;
                }
            }
            else
            if (value[0] == "save_json")
            {
                if (value.Count != 1)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                WebApiApplication.Catalog.SaveJSON();
            }
            else
            if (value[0] == "load_json")
            {
                if (value.Count != 1)
                {
                    return this.StatusCode(HttpStatusCode.NotAcceptable);
                }
                if (WebApiApplication.Catalog.LoadJSON())
                {
                    WebApiApplication.selectall = true;
                }
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NotAcceptable);
            }
            return this.StatusCode(HttpStatusCode.OK);
        }

        // PUT api/values/5
        // изменяет книгу с индексом ind, value - поля через точка с запятой
        public IHttpActionResult Put(int ind, [FromBody] string value)
        {
            if (value.Length>0)
            {
                if (WebApiApplication.Catalog.UpdateBook(ind, value))
                {
                    return this.StatusCode(HttpStatusCode.OK);
                }
                else
                {
                    return this.StatusCode(HttpStatusCode.NotFound);
                }
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE api/values/5
        // удаляет книгу с индексом ind
        public IHttpActionResult Delete(int ind)
        {
            if (WebApiApplication.Catalog.DeleteBook(ind))
            {
                return this.StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return this.StatusCode(HttpStatusCode.NotFound);
            }
        }
    }
}
