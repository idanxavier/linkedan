using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Services.Implementations
{
    public class MessageService
    {
        private readonly AuthService _authService;
        private readonly MessageRepository _messageRepository;

        public MessageService(AuthService authService, MessageRepository messageRepository)
        {
            _authService = authService;
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> ListUserSendMessages()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Message> list = await _messageRepository.ListUserSendMessages(currentUser.Id);

            return list;
        }

        public async Task<List<Message>> ListUserReceivedMessages()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Message> list = await _messageRepository.ListUserReceivedMessages(currentUser.Id);

            return list;
        }

        public async Task<Message> NewMessage(string userName, Message message)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            ApplicationUser receiver = await _authService.GetUserByUserName(userName);

            if(currentUser == receiver)
                throw new ArgumentException("You can't message yourself!");

            if (receiver == null || currentUser == null)
                throw new ArgumentException("This user doesn't exist");

            Message newMessage = new Message();
            newMessage.SenderId = currentUser.Id;
            newMessage.Data = DateTime.Now;
            newMessage.Receiver = receiver;
            newMessage.ReceiverId = receiver.Id;
            newMessage.Conteudo = message.Conteudo;

            newMessage = await _messageRepository.NewMessage(newMessage);

            return newMessage;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            Message message = await _messageRepository.GetMessage(messageId);

            if (message == null)
                throw new ArgumentException("Message doesn't exists.");

            return message;
        }

        public async Task<bool> DeleteMessage(int messageId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            Message findMsg = await _messageRepository.GetMessage(messageId);

            if (findMsg == null)
                throw new ArgumentException("Message doesn't exists.");

            await _messageRepository.DeleteMessage(messageId);

            return true;
        }
    }
}
