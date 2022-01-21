using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.GUI.Services.Interfaces
{
  public interface IChatHub
  {
    Task Connect(Guid channelId);
  }
}
