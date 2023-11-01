

namespace WASM_JUDOPLAT.Components.Posts
{
	public partial class PostListComponent
	{

        #region DI


        [Inject] IConfiguration _config { get; set; }
        [Inject] IPostRepo _postRepo { get; set; }
        [Inject] ILocalStorageService _localStorage { get; set; }
    
        [Inject] AuthenticationStateProvider _authProvider { get; set; }

        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] NavigationManager _nav { get; set; }


        #endregion
        #region Properties
        private HubConnection? hubConnection;

        public List<PostDto> Posts ;
        public PostDto RecentPost = new();
        public PostDto ModifiedPost = new();
        public PostDto RemovePost = new();
        private IJSObjectReference jsModule { get; set; }
        public bool isVisible { get; set; }
        #endregion

        #region Parameters
        [Parameter] public bool ShouldAutoScorll { get; set; }
        #endregion



        #region Methods


        private async ValueTask<ItemsProviderResult<Post>> LoadPosts(ItemsProviderRequest request)
        {
            //var posts = await _postRepo.GetAll();
            //return new ItemsProviderResult<Post>(posts.Skip(request.StartIndex).Take(request.Count), posts.Count());
            throw new NotImplementedException();
        }

        protected override async Task OnInitializedAsync()
        {

            AuthenticationState state = await _authProvider.GetAuthenticationStateAsync();

            var user = state.User;

            var posts = await _postRepo.GetAll();
            //Posts = _mapper.Map<List<PostDto>>(posts);
            jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Posts/PostListComponent.razor.js");
         
            base.OnInitializedAsync();

        }

        #region start connection

        private async Task StartHubConnection()
        {
            //hubConnection = new HubConnectionBuilder()
            //    .WithUrl(_nav.ToAbsoluteUri("/notifhub"), options =>
            //        {
            //            options.AccessTokenProvider = async () =>
            //            {
            //                var token= await _localStorage.GetItemAsync<string>("authToken");
            //                Console.WriteLine(token);
            //                return token;
            //            };
            //        }
            //    )
            //    .Build();

            //hubConnection.On<FullPostNotification>("refreshPost", async (notif) =>
            //{

            //    await JsRuntime.InvokeVoidAsync("console.log", notif.table);
            //    if (notif.action == "INSERT")
            //    {

            //        RecentPost = notif.data;
            //        Posts.Add(RecentPost);
            //        Posts.OrderByDescending(p => p.Id);
            //        InvokeAsync(StateHasChanged);


            //    }
            //    else if (notif.action =="DELETE")
            //    {
            //        RemovePost = notif.data;
            //        Posts.Remove(Posts.FirstOrDefault(p => p.Id == RemovePost.Id));
            //        Posts.OrderByDescending(p => p.Id);
            //        InvokeAsync(StateHasChanged);

            //    }
            //    else if (notif.action == "UPDATE")
            //    {

            //        ModifiedPost = notif.data;
            //        Posts.Remove(Posts.FirstOrDefault(p => p.Id == ModifiedPost.Id));
            //        Posts.Add(ModifiedPost);
            //        Posts.OrderByDescending(p => p.Id);
             
            //        InvokeAsync(StateHasChanged);
            //        ShouldRender();

            //    }
            //    InvokeAsync(StateHasChanged);
            //});
            //await hubConnection.StartAsync();
            //Console.WriteLine(hubConnection.State);
        }
        #endregion

        protected override async Task OnParametersSetAsync()
        {
             await StartHubConnection();
            if (ShouldAutoScorll==true)
            await jsModule.InvokeVoidAsync("scrollToElement", "eleScroll");

            base.OnParametersSetAsync();
        }


        private void OpenDialogCreateComment(Post p)
        {
            var parameters = new DialogParameters();
            parameters.Add("Post", p);
            var options = new DialogOptions { CloseOnEscapeKey = true, };
            DialogService.Show<PostCommentCreationDialog>("Créer un commentaire", parameters, options);
        }
        //public bool IsConnected =>
        //hubConnection?.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null)
            {
                //await hubConnection.DisposeAsync();
            }
        }

        public void ToggleOverlay(bool value)
        {
            isVisible = value;
        }
        #endregion
    }
}

