using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Logic
{
    public class Ball : INotifyPropertyChanged
    {
        private float _x;
        private float _y;
        private readonly int _radius;
        private bool _isMoving;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public Ball(float x, float y, int radius)
        {
            _x = x;
            _y = y;
            _radius = radius;
        }

        public float X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(); }
        }

        public float Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged();}
        }

        public int Radius
        {
            get => _radius;
        }

        public bool IsMoving
        {
            get => _isMoving;
            set { _isMoving = value; }
        }

        public async Task Move(int width, int height)
        {
            Random rand = new Random();
            double velocity = 3;
            _isMoving = true;

            float xStep = (float)(rand.NextDouble() * 2 - 1);
            float yStep = (float)(rand.NextDouble() * 2 - 1);

            double magnitude = Math.Sqrt(xStep * xStep + yStep * yStep);
            xStep = (float)(xStep / magnitude * velocity);
            yStep = (float)(yStep / magnitude * velocity);

            while (_isMoving)
            {
                _x += xStep;
                _y += yStep;

                if (_x - _radius <= -15 || _x + _radius >= width)
                {
                    xStep = -xStep;
                }

                if (_y - _radius <= -15 || _y + _radius >= height)
                {
                    yStep = -yStep;
                }

                OnPropertyChanged(nameof(X));
                OnPropertyChanged(nameof(Y));

                await Task.Delay(20);
            }
        }

    }
}
