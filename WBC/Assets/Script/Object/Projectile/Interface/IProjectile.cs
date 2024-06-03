// 발사체 인터페이스
public interface IProjectile
{
    ProjectileStat SetStat(int damage); 
    void Action();
}