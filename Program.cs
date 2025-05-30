using cli_bot;
using adachi_reaction_bot;
using Path = cli_bot.Path;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using Quill.Pages;
using Quill;

internal class Program
{
    private static Language[] langs;

    private static void Main(string[] args)
    {
        langs =
        [
            new(Path.Assembly / "lang/en.txt", "en", 90.0), // english
            new(Path.Assembly / "lang/es.txt", "es", 5.0),  // spanish
            new(Path.Assembly / "lang/fr.txt", "fr", 5.0),  // french
        ];

        DriverCreation.options.headless = false;
        TwitterBot adachi = new(TimeSpan.FromMinutes(60)) { DisplayName = "Adachi Reaction" };
        adachi.runAction += Run;

        adachi.Start();
    }

    static float GetFontSize(string input, Font font, Language lang)
    {
        float fontSize = input.Any(chr => char.ConvertToUtf32(input, input.IndexOf(chr)) > 127) ? 21 : 150;

        float maxSize = 645;

        FontRectangle textSize = TextMeasurer.MeasureAdvance(input, new(font));
        FontFamily fontFamily = font.Family;

        while (textSize.Width > maxSize)
        {
            fontSize--;
            font = fontFamily.CreateFont(fontSize, FontStyle.Regular);

            //remeasures for check to see if it fits
            textSize = TextMeasurer.MeasureAdvance(input, new(font));
        }

        return fontSize;
    }


    private static void Run(ComposePage composer, string[] args)
    {
        try
        {
            Language lang = Language.RollChances(langs.ToList());

            ///--------               
            /// Handles the variables the bot uses to put onto the image               

            var word = lang.GetWord();
            //var word = DrawingHelper.CustomWord($"DEADBEAT");

            using Image<Rgba32> adachiPort = DrawingHelper.GetRandomImage();
            //using Image adachiPort = DrawingHelper.CustomImage(AdachiExpressions.ShadowMad);

            Color textColor = DrawingHelper.RandomColour(151);
            //Color textColor = Color.DarkRed;
            //Color textColor = DrawingHelper.CustomColour(255,255,255);

            ///--------

            var files = Directory.GetFiles(Path.Assembly / "adachi", "*.png", SearchOption.AllDirectories);

            using Image<Rgba32> bg = Image.Load<Rgba32>(Path.Assembly / "bg.png");
            using Image<Rgba32> resultImage = new Image<Rgba32>(bg.Width, bg.Height);

            adachiPort.Mutate(ctx => ctx.Crop(new Rectangle(107, 65, 285, 270)).Resize(700, 555, KnownResamplers.Welch));

            resultImage.Mutate(ctx => ctx.DrawImage(bg, new Point(0, 0), 1f));

            //draws adachi
            resultImage.Mutate(ctx => ctx.DrawImage(adachiPort, new Point(0, 0), 1f));

            FontCollection collection = new();
            collection.Add(Path.Assembly / "comic.ttf");
            FontFamily family = collection.Get("Comic Sans MS");
            Font font = family.CreateFont(150, FontStyle.Regular);
            font = family.CreateFont(GetFontSize(word.formatted, font, lang), FontStyle.Regular);

            RichTextOptions textOptions = new RichTextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Origin = new PointF(350, 630)
            };
            resultImage.Mutate(ctx => ctx.DrawText(textOptions, word.formatted, Brushes.Solid(textColor)));

            resultImage.Save(Path.Assembly / "output.png");

            Output.WriteLine($"Tweeting \"{word.raw}\" from lang \"{lang.code}\"");

            composer.Tweet(word.raw, Path.Assembly / "output.png");
            Output.WriteLine("finished tweeting");
        }
        catch { }
    }
}