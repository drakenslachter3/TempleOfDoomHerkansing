using TempleOfDoom.Core.Game;

namespace TempleOfDoom.Core.Doors
{
    public abstract class DoorDecorator : IDoor
    {
        protected readonly IDoor _door;
        private bool _isOpen;

        protected DoorDecorator(IDoor door)
        {
            _door = door;
            _isOpen = door.IsOpen; 
        }

        public virtual bool IsOpen
        {
            get => _isOpen && _door.IsOpen; 
            set
            {
                _isOpen = value;
                _door.IsOpen = value;
            }
        }

        public virtual string Description => _door.Description; //Beschrijft de door en kan worden afgelezen door andere methodes om te zien waar de decorator over gaat. Bijv: Color: Blue

        public virtual bool CanPass(GameState gameState)
        {
            return IsOpen && _door.CanPass(gameState); 
        }

        public virtual void OnPass(GameState gameState)
        {
            _door.OnPass(gameState);
        }
    }
}
