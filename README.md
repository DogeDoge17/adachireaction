# Adachi Reaction
 This is where the latest code for [Adachi Reaction](https://x.com/adachireaction) will reside. You can find the old code with all parts that make the bot a it a bot [here](https://github.com/DogeDoge17/adachi-reaction-bot-open).\
 The bot uses [ImageSharp](https://sixlabors.com/products/imagesharp/) to generate each image to then tweet it out hourly with Quill. The bot has support for English (90%), Spanish (5%), and French (5%) words.  
# Setup
 Build [Quill](https://github.com/Doge-Productions/Quill.api/tree/experimental) and [cli-bot-lib](https://github.com/DogeDoge17/cli-bot-lib), and then make project references to both the libraries in the csproj. You may also recursive clone [cli-bot-lib](https://github.com/DogeDoge17/cli-bot-lib) and everything should work from there (granted you don't change the structure).\
 Make a login.txt file in the main directory file and fill it in this manner
```bash
username
password
```
 It's not the most secure method to do this but it'll have to work with me.
# Dependencies
 You can find the source of the cli-bot dll at [cli-bot-lib](https://github.com/DogeDoge17/cli-bot-lib)\
 You can find the Quill library [in this repo](https://github.com/Doge-Productions/Quill.api/tree/experimental).\
 You also need [Newtonsoft.Json](https://www.nuget.org/packages/newtonsoft.json/), [Selenium](https://www.nuget.org/packages/selenium.webdriver), and [WaitHelpers](https://www.nuget.org/packages/SeleniumExtras.WaitHelpers) as Quill depends on them.
# cli-bot-lib
 Again, this repo does not include most of the code that runs the management of the bot, but only what will be tweeted. All that code can be found in [cli-bot-lib](https://github.com/DogeDoge17/cli-bot-lib). Please refer to that if you want to see more of the innerworkings of the bot. 
# Quill
  Quill is a library that streamlines webscraping twitter with the sole purpose of making bots. It's limited in features as its only good for composing tweets but that should be enough to satisfy the needs of most bots. There is no nuget page setup as of right now so the only way to obtain the package is by compiling the [experimental branch](https://github.com/Doge-Productions/Quill.api/tree/experimental). If you recursively cloned the [cli-bot-lib repository](https://github.com/DogeDoge17/cli-bot-lib), the [Quill repo](https://github.com/Doge-Productions/Quill.api/tree/experimental) should be included.