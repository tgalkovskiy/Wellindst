using ForestValley.Assistants;
using ForestValley.Behaviours.Movement;
using ForestValley.Managers;

namespace ForestValley.Environment.Platforms
{
    public class PlatformPoolManager: PoolManager<SimplePlatform>
    {

        public override void Init()
        {
            base.Init();

            var lm = GameManager.gm.FindController<LevelManager>();
            CharacterMover characterMover = lm.PlayerFacadeInstance.Mover;
            
            
            foreach (var poolItem in pool)
            {
                var platform = poolItem.GetComponent<Platform>();
                if (platform != null)
                {
                    characterMover.Landed += platform.OvercomeCheckProcess;
                }
            }
        }
        
        
        
    }
}