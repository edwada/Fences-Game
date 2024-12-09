using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FencesGame.Exceptions;
using System.Media;

namespace FencesGame.UI
{
    public partial class FencingGameControl : UserControl
    {
        private const int GameSize = 5;
        private Game _game = new Game(GameSize, false);
        public event EventHandler GameEnded;
        private SoundPlayer _moveSound = new SoundPlayer("fence-move.wav");
        private const int _dotRadius = 9;
        private const int _lineThickness = 6;

        public FencingGameControl()
        {
            InitializeComponent();

            _game.Ended += _game_Ended;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void Restart(bool vsAI)
        {
            _game.Ended -= _game_Ended;
            _game = new Game(GameSize, vsAI);
            _game.Ended += _game_Ended;
            this.Invalidate();
        }

        void _game_Ended(Turns winner)
        {
            string color = winner == Turns.Player1 ? "Blue" : "Red";

            MessageBox.Show(color + " player won!", "Winner", MessageBoxButtons.OK);
            GameEnded(this, new EventArgs());
        }

        private void FencingGameControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawBlueDots(e);
            DrawRedDots(e);
            DrawLines(e);
        }

        private void DrawLines(PaintEventArgs e)
        {
            foreach (var c in _game.Board.Connections)
            {
                DrawConnection(c, e);
            }
        }

        private void DrawConnection(Connection c, PaintEventArgs e)
        {
            Point start, end, middle;

            middle = BoardPositionToPoint(c.Row, c.Collumn);
            int tileSize = GetTileSize() - _dotRadius + 1;

            Rectangle position;
            Image texture;
            if (c.Direction == Orientation.Vertical)
            {
                position = new Rectangle(middle.X - _lineThickness / 2, middle.Y - tileSize, _lineThickness, 2 * tileSize);
                texture = c.Color == TileState.Player1 ? Image.FromFile("./Resources/blue vertical connection.gif") : Image.FromFile("./Resources/red vertical connection.gif");
            }
            else
            {
                position = new Rectangle(middle.X - tileSize, middle.Y - _lineThickness / 2, 2 * tileSize, _lineThickness);
                texture = c.Color == TileState.Player1 ? Image.FromFile("./Resources/blue horizontal connection.gif") : Image.FromFile("./Resources/red horizontal connection.gif");
            }

            
            //e.Graphics.DrawRectangle(new Pen(Brushes.Blue), position);
            e.Graphics.DrawImage(texture, position.X, position.Y, position.Width, position.Height);

            
        }

        
        private void DrawRedDots(PaintEventArgs e)
        {
            _game.Board.EachPlayer2Dot((i, j) =>
            {
                Point center = BoardPositionToPoint(i, j);

                Rectangle rect = new Rectangle(center.X - _dotRadius, center.Y - _dotRadius, 2 * _dotRadius, 2 * _dotRadius);

                e.Graphics.DrawImage(Image.FromFile("./Resources/Red ball.gif"), rect);
            });
        }

        private void DrawBlueDots(PaintEventArgs e)
        {
            _game.Board.EachPlayer1Dot((i, j) =>
            {
                Point center = BoardPositionToPoint(i, j);

                Rectangle rect = new Rectangle(center.X - _dotRadius, center.Y - _dotRadius, 2 * _dotRadius, 2 * _dotRadius);

                e.Graphics.DrawImage(Image.FromFile("./Resources/Blue ball.gif"), rect);
            });
        }

        private int GetTileSize()
        {
            Rectangle viewport = GetViewPort();
            return viewport.Height / _game.Board.Tiles.GetLength(0);
        }

        private Point BoardPositionToPoint(int i, int j)
        {
            Rectangle viewport = GetViewPort();
            int tile_size = GetTileSize();
            int x = j*tile_size + tile_size/2;
            int y = i*tile_size + tile_size/2;

            return new Point(viewport.X + x, viewport.Y + y);
        }

        private Rectangle GetViewPort()
        {
            if (this.Width < this.Height)
            {
                int size = this.Width;
                return new Rectangle(0, (Height - size) / 2, size, size);
            }
            else
            {
                int size = this.Height;
                return new Rectangle((Width - size) / 2, 0, size, size);
            }

        }

        private void FencingGameControl_Click(object sender, EventArgs e)
        {
            Position pos = PointToBoardPosition(((MouseEventArgs)e).Location);

            try
            {
                _game.Play(pos.Row, pos.Col);

                if (!_game.HasEnded)
                    _moveSound.Play();

                this.Invalidate();
            }
            catch (InvalidMoveException) { }
        }

        private Position PointToBoardPosition(Point point)
        {
            Position result = new Position();

            Point vpoint = ControlPointToViewPortPoint(point);
            int tileSize = GetTileSize();

            result.Row = vpoint.Y/tileSize;
            result.Col = vpoint.X/tileSize;

            return result;
        }

        private Point ControlPointToViewPortPoint(Point point)
        {
            var vp = GetViewPort();

            return new Point(point.X - vp.X, point.Y - vp.Y);
        }

        private void FencingGameControl_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
