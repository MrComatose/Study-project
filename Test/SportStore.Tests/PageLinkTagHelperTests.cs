
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using SportStore.Models.ViewModels;
using SportStore.Infrastructure;
using Xunit;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SportStore.Tests
{
  public   class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_PageLinks()
          {
            var UrlHelper = new Mock<IUrlHelper>();
            UrlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(UrlHelper.Object);
            PageLinkTagHelper tagHelper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10

                },
                PageAction = "Test"
            };
            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "");
            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput("div",
                new TagHelperAttributeList(),
                (cache,encoder)=>Task.FromResult(content.Object));


            tagHelper.Process(ctx,output);

            Assert.Equal(@"<a href=""Test/Page1"">1</a>"
                +@"<a href=""Test/Page2"">2</a>"
                + @"<a href=""Test/Page3"">3</a>",
                output.Content.GetContent()

);

        }
    }
}
