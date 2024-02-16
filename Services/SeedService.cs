using Newtonsoft.Json;

namespace BlogApi.Services
{
    public class SeedService : ISeedService
    {
        private readonly ISeedRepository _seedRepo;
        
        public SeedService(ISeedRepository seedRepo)
        {
            _seedRepo = seedRepo;
        }
        public async Task SeedDatabase()
        {
            var users = await _seedRepo.GetAllUsers();

            if (users.Count == 0)
            {
                var userList = await DeserializeToUser();
                await _seedRepo.SaveUsers(userList);


                var posts = await _seedRepo.GetAllPosts();

                if (posts.Count == 0)
                {
                    var postsList = await DeserializeToPost();
                    await _seedRepo.SavePosts(postsList);
                }

            }

            
            
        }

        private async Task<List<User>> DeserializeToUser()
        {
            string? json = await GetJson("https://jsonplaceholder.typicode.com/users");
            if (json is not null)
            {
                var userCredentials = JsonConvert.DeserializeObject<List<UserCredentials>>(json);
                if (userCredentials is not null)
                {
                    var users = userCredentials.Select(u =>
                    {
                        string[] name = u.Name.Split(" ");
                        var user = new User();
                        user.FirstName = name[0];
                        user.LastName = name[1];
                        user.UserName = u.UserName;
                        user.CompanyName = u.Company!.Name;
                        user.Telephone = u.Phone;
                        user.Email = u.Email;
                        user.Addres = $"{u.Address!.City} {u.Address.Street}";

                        return user;
                    }).ToList();

                    return users;
                }
            }
            
            return new List<User>();
        }

        private async Task<List<Post>> DeserializeToPost()
        {
            string? json = await GetJson("https://jsonplaceholder.typicode.com/posts");
            if (json is not null)
            {
                var postCredentials = JsonConvert.DeserializeObject<List<PostCredentials>>(json);
                if (postCredentials is not null)
                {
                    var posts = postCredentials.Select(p =>
                    {
                        var existingUser = _seedRepo.ExistsUser(p.UserId);
                        if (existingUser is not null)
                        {
                            var post = new Post();
                            post.Title = p.Title;
                            post.Body = p.Body;

                            post.User = existingUser.Result;
                            return post;
                        }
                        return null;

                    }).ToList();

                    return posts!;
                }
            }

            return [];
        }

        private async Task<string?> GetJson(string uri)
        {
            string json = "";
            using (HttpClient client = new HttpClient())
            {
                string url = uri;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    return json;
                }

                return null;
            }
        }




    }
}
