namespace Workshop.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    using Workshop.Services.Data;
    using Workshop.Web.ViewModels.Chat;

    public class ChatHub : Hub
    {
        private IHashProvider hashProvider;

        public ChatHub(IHashProvider hashProvider)
        {
            this.hashProvider = hashProvider;
        }

        public async Task Send(string id, string messageInput)
        {
            var currUser = this.Context.UserIdentifier;
            var hashGroup = this.hashProvider.Hash(id, currUser);

            var message = new Message()
            {
                Username = this.Context.User.Identity.Name,
                MessageText = messageInput,
                CreatedOn = string.Concat("Today, ", DateTime.Now.ToString("HH:mm")),
            };

            await this.Clients.Group(hashGroup).SendAsync("NewMessage", message);
        }

        public async Task Add(string id)
        {
            var currUser = this.Context.UserIdentifier;
            var hashGroup = this.hashProvider.Hash(id, currUser);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, hashGroup);
        }
    }
}
