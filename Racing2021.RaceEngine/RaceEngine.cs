using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Racing2021.Models.Enums;
using Racing2021.Models.RaceEngine;
using Racing2021.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Racing2021.RaceEngine
{
    public class RaceEngine : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _trackHorizontal;
        private Texture2D _trackUp;
        private Texture2D _trackDown;
        private SpriteFont _spriteFont;


        private IList<TrackTile> _trackTiles;
        private IList<TrackTileGraphic> _trackTileGraphics;
        private IList<CyclistRaceEngine> _cyclists;

        private Queue<CyclistRaceEngine> _finishedCyclists;

        private float _screenPosition;
        private float _centerX;
        private float _scrollSpeed = 200f;
        private float _leaderDifferenceWithStandardY;

        public RaceEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _trackTiles = new List<TrackTile>();
            //_cyclists = new List<Cyclist>();
            _finishedCyclists = new Queue<CyclistRaceEngine>();
        }

        public void InitializeCyclists(IList<CyclistRaceEngine> cyclists)
        {
            _cyclists = cyclists;
        }

        public void InitializeTrack(IList<TrackTile> trackTiles)
        {
            _trackTiles = trackTiles;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.ApplyChanges();

            _trackTileGraphics = DrawTrack.Track(_trackTiles, GraphicsDevice.DisplayMode.Width / 2);
            _centerX = GraphicsDevice.DisplayMode.Width / 2;

            foreach (var cyclist in _cyclists)
            {
                cyclist.CyclistPositionX = _centerX;
                cyclist.StartTime = DateTime.Now;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _trackHorizontal = Content.Load<Texture2D>("TrackHorizontal");
            _trackUp = Content.Load<Texture2D>("TrackDownUp");
            _trackDown = Content.Load<Texture2D>("TrackUpDown");
            _spriteFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

            foreach (var cyclist in _cyclists)
            {
                cyclist.CyclistTexture = Content.Load<Texture2D>(cyclist.Team.JerseyName);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            var raceLeader = _cyclists[0];

            foreach (var cyclist in _cyclists)
            {
                var positionCentertrack = _trackTileGraphics.Where(x => (x.X - _screenPosition) <= cyclist.CyclistPositionX).Max(x => x.X);
                var centreTrack = _trackTileGraphics.Where(x => x.X == positionCentertrack).FirstOrDefault();
                var oldPosition = cyclist.CyclistPositionX;

                if (centreTrack.TrackTile == TrackTile.Horizontal)
                {
                    if (centreTrack.X + TextureParameters.Horizontal < cyclist.CyclistPositionX + _screenPosition)
                    {
                        if (!_finishedCyclists.Contains(cyclist))
                        {
                            _finishedCyclists.Enqueue(cyclist);
                            cyclist.FinishTime = DateTime.Now;
                        }
                        continue;
                    }
                    cyclist.CyclistPositionY = centreTrack.Y;
                    cyclist.CyclistPositionX += cyclist.CyclistSpeedHorizontal * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (centreTrack.TrackTile == TrackTile.Up)
                {
                    if (centreTrack.X + TextureParameters.UpDown < cyclist.CyclistPositionX + _screenPosition)
                    {
                        if (!_finishedCyclists.Contains(cyclist))
                        {
                            _finishedCyclists.Enqueue(cyclist);
                            cyclist.FinishTime = DateTime.Now;
                        }
                        continue;
                    }
                    var differenceX = cyclist.CyclistPositionX - (positionCentertrack - _screenPosition);
                    cyclist.CyclistPositionY = (centreTrack.Y + TextureParameters.UpDown / 2) - differenceX / 2;
                    cyclist.CyclistPositionX += cyclist.CyclistSpeedUp * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (centreTrack.TrackTile == TrackTile.Down)
                {
                    if (centreTrack.X + TextureParameters.UpDown < cyclist.CyclistPositionX + _screenPosition)
                    {
                        if (!_finishedCyclists.Contains(cyclist))
                        {
                            _finishedCyclists.Enqueue(cyclist);
                            cyclist.FinishTime = DateTime.Now;
                        }
                        continue;
                    }
                    var differenceX = cyclist.CyclistPositionX - (positionCentertrack - _screenPosition);
                    cyclist.CyclistPositionY = centreTrack.Y + differenceX / 2;
                    cyclist.CyclistPositionX += cyclist.CyclistSpeedDown * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (cyclist.CyclistPositionX > raceLeader.CyclistPositionX)
                {
                    raceLeader = cyclist;
                }

            }
            var _raceLeaderGain = raceLeader.CyclistPositionX - _centerX;
            _leaderDifferenceWithStandardY = GeneralParameters.CentralPositionY - raceLeader.CyclistPositionY;

            foreach (var cyclist in _cyclists)
            {
                cyclist.CyclistPositionX -= _raceLeaderGain;
            }
            _screenPosition += _raceLeaderGain;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var trackTileGraphic in _trackTileGraphics)
            {
                switch (trackTileGraphic.TrackTile)
                {
                    case TrackTile.Horizontal:
                        _spriteBatch.Draw(_trackHorizontal, new Vector2(trackTileGraphic.X - _screenPosition, trackTileGraphic.Y + _leaderDifferenceWithStandardY), Color.White);
                        break;
                    case TrackTile.Up:
                        _spriteBatch.Draw(_trackUp, new Vector2(trackTileGraphic.X - _screenPosition, trackTileGraphic.Y + _leaderDifferenceWithStandardY), Color.White);
                        break;
                    case TrackTile.Down:
                        _spriteBatch.Draw(_trackDown, new Vector2(trackTileGraphic.X - _screenPosition, trackTileGraphic.Y + _leaderDifferenceWithStandardY), Color.White);
                        break;
                    default:
                        break;
                }
            }

            foreach (var cyclist in _cyclists)
            {
                _spriteBatch.Draw(cyclist.CyclistTexture, new Vector2(cyclist.CyclistPositionX, cyclist.CyclistPositionY + _leaderDifferenceWithStandardY), Color.White);
                _spriteBatch.DrawString(_spriteFont, cyclist.Name, new Vector2(cyclist.CyclistPositionX, cyclist.CyclistPositionY - TextureParameters.FontSize + _leaderDifferenceWithStandardY), Color.White);
            }

            var counter = 0;
            foreach (var finishedCyclist in _finishedCyclists)
            {
                counter++;
                _spriteBatch.DrawString(_spriteFont, finishedCyclist.TotalTime.Minutes + ":" + finishedCyclist.TotalTime.Seconds + " - " + finishedCyclist.Name, new Vector2(0, TextureParameters.FontSize * counter), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public List<CyclistRaceEngine> GetFinishedCyclists()
        {
            return _finishedCyclists.ToList();
        }
    }
}
