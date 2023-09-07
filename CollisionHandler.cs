namespace Space_Invaders;

public class CollisionHandler
{
    private readonly Player _player;
    private readonly EnemyManager _enemyManager;
    private readonly ScoreManager _scoreManager;


    public CollisionHandler(Player player, EnemyManager enemyManager, ScoreManager scoreManager)
    {
        _player = player;
        _enemyManager = enemyManager;
        _scoreManager = scoreManager;
    }

    public void Update()
    {
        HandleEnemiesCollision();
    }

    private void HandleEnemiesCollision()
    {
        var enemies = _enemyManager.Enemies;

        for (var i = 0; i < _enemyManager.Enemies.Count; i++)
        {
            if (HasCollisionEnemyWithBullet(enemies[i], out Bullet bullet))
            {
                _player.DestroyBullet(bullet);
                _enemyManager.DestroyEnemy(enemies[i]);
                _scoreManager.IncreaseScore();
                continue;
            }

            if (HasCollisionEnemyWithPlayer(enemies[i]))
            {
                _player.Destroy();
            }
        }
    }

    private bool HasCollisionEnemyWithBullet(Enemy enemy, out Bullet bullet)
    {
        var bullets = _player.GetBullets();

        for (int i = 0; i < bullets.Count; i++)
        {
            bullet = bullets[i];

            if (enemy.GetGlobalBounds().Intersects(bullets[i].GetGlobalBounds()))
            {
                return true;
            }
        }

        bullet = null;
        return false;
    }

    private bool HasCollisionEnemyWithPlayer(Enemy enemy)
    {
        return _player.GetGlobalBounds().Intersects(enemy.GetGlobalBounds());
    }
}