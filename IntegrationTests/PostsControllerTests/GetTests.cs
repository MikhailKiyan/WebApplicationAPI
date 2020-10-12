using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

using WebApplicationAPI.Contracts.V1;
using WebApplicationAPI.Contracts.V1.Requests;
using WebApplicationAPI.IntegrationTests.Extensions;
using WebApplicationAPI.Contracts.V1.Responses;
using WebApplicationAPI.Domain;

namespace WebApplicationAPI.IntegrationTests.PostsControllerTests {
    public class GetTests : BaseTest {
        public GetTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task Get_ReturnPost_WhenPostExists() {
            // Arrenge
            var postName = "TestPost";
            var client = ArrangeHttpClient();
            await client.AuthenticateAsync();
            var createdPost = await this.CreatePostAsync(client, new PostCreateRequest { Name = postName });

            // Act
            var (response, result) = await client.ExecuteRequest<Post>(HttpMethod.Get, ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Id.Should().Be(createdPost.Id);
            result.Name.Should().Be(postName);
        }
    }
}
