class Captcha
    {
        public Image Draw(Size size)
        {
            string POOL = "abcdefgijkmnopqrstwxyzABCDEFGHJKLMNPQRSTWXYZ0192837465";
            string captcha = "";

            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            int seed = BitConverter.ToInt32(randomBytes, 0);
            Random random = new Random(seed);

            for (int i = 0; i < 7; i++)
            {
                captcha += POOL[random.Next(0, POOL.Count())];
            }

            //Bitmap objBitmap = new Bitmap(130, 80);
            Bitmap objBitmap = new Bitmap(size.Width, size.Height);
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);
            Random objRandom = new Random();
            objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
            objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
            objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
            Brush objBrush = default(Brush);

            //create background style
            HatchStyle[] aHatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };

            //create rectangular area
            RectangleF oRectangleF = new RectangleF(0, 0, size.Width, size.Height);
            objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
            objGraphics.FillRectangle(objBrush, oRectangleF);

            //Generate the image for captcha
            //add the captcha value in session
            int fontSize = 20;
            Font objFont = new Font("Courier New", fontSize, FontStyle.Bold);
            SizeF textSize = new SizeF();

            bool isSizeOk = false;
            while (!isSizeOk)
            {
                textSize = objGraphics.MeasureString(captcha, objFont);
                if (textSize.Width <= size.Width - 2 && textSize.Height <= size.Height - 2)
                {
                    isSizeOk = true;
                }
                else
                {
                    fontSize--;
                    objFont = new Font("Courier New", fontSize, FontStyle.Bold);
                }
            }

            //Draw the image for captcha
            objGraphics.DrawString(captcha, objFont, Brushes.Black, (size.Width - textSize.Width) / 2, (size.Height - textSize.Height) / 2);

            objGraphics.Save();

            return (Image)objBitmap;
        }
    }
