import json
import sys

argv = sys.argv

# made to convert https://github.com/akaAgar/vocabulaire-francais/blob/master/Adjectifs%2C%20masculin%20singulier%20(complet).json
# into a usable format for the bot

if len(argv) < 2:
    print("not enough args")
    exit(1);

print("reading...")
with open(argv[1], "r") as f:
    data = json.load(f)
    print("writing...")
    with open(argv[2], 'a') as outp:
        for line in data:
            outp.write(line + "\n")    
print("completed")