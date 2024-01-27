﻿using GXPEngine;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gxpengine_template
{
    public class TextMesh : GameObject
    {
        #region Properties
        public int Width => _canvas.width;
        public int Height => _canvas.height;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Draw();
            }
        }
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Draw();
            }
        }
        public CenterMode HorizontalAlign
        {
            get => _horizontalAlign;
            set
            {
                _horizontalAlign = value;
                Draw();
            }
        }
        public CenterMode VerticalAlign
        {
            get => _verticalAlign;
            set
            {
                _verticalAlign = value;
                Draw();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Draw();
            }
        }
        public int TextSize
        {
            get => _textSize;
            set
            {
                _textSize = value;
                Draw();
            }
        }
        #endregion
        string _text;
        Color _backgroundColor = Color.Transparent;
        Color _textColor = Color.Black;
        CenterMode _horizontalAlign;
        CenterMode _verticalAlign;
        int _textSize;
        readonly EasyDraw _canvas;
    
        public TextMesh(string content, int width, int height,
            //optional params
            CenterMode horizontalAlign = CenterMode.Center, CenterMode verticalAlign = CenterMode.Center, int textSize = 10

        )
        {
            _canvas = new EasyDraw(width, height, false);
            _canvas.SetOrigin(width / 2, height / 2);
            AddChild(_canvas);

            _text = content;
            _horizontalAlign = horizontalAlign;
            _verticalAlign = verticalAlign;
            _textSize = textSize;
            Draw();
        }
        public TextMesh(string content, int width, int height, Color txtColr, Color bgColr,
            //optional params
            CenterMode horizontalAlign = CenterMode.Center, CenterMode verticalAlign = CenterMode.Center, int textSize = 10

        )
        {
            _canvas = new EasyDraw(width, height, false);
            _canvas.SetOrigin(width / 2, height / 2);
            AddChild(_canvas);

            _text = content;
            _textColor = txtColr;
            _backgroundColor = bgColr;
            _horizontalAlign = horizontalAlign;
            _verticalAlign = verticalAlign;
            _textSize = textSize;

            Draw();
        }
        

        public void Draw() 
        {
            _canvas.Clear(_backgroundColor);
            _canvas.TextAlign(_horizontalAlign, _verticalAlign);
            _canvas.Fill(_textColor);
            _canvas.TextSize(_textSize);
            _canvas.Text(_text);
        }
        public void SetOrigin(int x, int y)
        {
            _canvas.SetOrigin(x, y);
            //_canvas.SetXY(0, 0);
        }
    }
}
