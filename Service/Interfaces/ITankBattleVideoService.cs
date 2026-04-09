using Tank_Wiki.DTOs.TankVideo;
using Tank_Wiki.Models;

namespace Tank_Wiki.Service.Interfaces;

public interface ITankBattleVideoService
{
    Task<TankBattleVideoDto?> CreateBattleVideoAsync(CreateBattleVideoDto dto);
    Task<TankBattleVideoDto?> GetBattleVideoAsync(int t1, int t2);
}
